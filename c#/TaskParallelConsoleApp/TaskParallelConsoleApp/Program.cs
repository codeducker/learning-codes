using System;
using System.Threading.Tasks.Dataflow;

namespace TaskParallelConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var setupPipeline = SetupPipeline();
            setupPipeline.Post("D:\\github\\csharp-demo\\ReflectDymaincConsoleApp\\ReflectDynamicConsoleApp");

            // var task = Task.Run(()=>Producer());
            // var task1 = Task.Run(async ()=> await ConsumerAsync());
            // Task.WaitAll(task, task1);


            // ExecuteActionBlock();

            // CancelTask();

            // CancelParallelFor();

            // Task.WhenAll()//不阻塞
            // Task.WaitAll()//阻塞

            // ParentAndChild();

            // ContinuationTasks();

            // ExecuteTaskWithResult();

            // LongRunningTask();

            // TasksUsingThreadPool();

            // Parallel.Invoke(Bar,Car);

            // ParallelForeach();

            // ParallelForWithInit();

            // CallAsyncExecute();

            Console.ReadLine();
        }

        public static ITargetBlock<string> SetupPipeline()
        {
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(
                GetFileNames);
            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
                LoadLines);
            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
                GetWords);
            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            var display = new ActionBlock<IEnumerable<string>>(
                coll =>
                {
                    foreach (var s in coll)
                    {
                        Console.WriteLine(s);
                    }
                });
            words.LinkTo(display);
            return fileNamesForPath;
    }

        public static IEnumerable<string> GetFileNames(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                yield return fileName;
            }
        }

        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string? line = null;
                    while ((line = reader.ReadLine())!= null)
                    {
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',', '[',']');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }


        private static readonly BufferBlock<string> BufferBlockOnly = new BufferBlock<string>();

        public static async Task ConsumerAsync()
        {
            while (true)
            {
                string data = await BufferBlockOnly.ReceiveAsync();
                Console.WriteLine($"user input: {data}");
            }
        }

        public static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string? input = Console.ReadLine();
                if (String.Compare(input, "exit", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    exit = true;
                }
                else
                {
                    BufferBlockOnly!.Post(input);
                }
            }
        }

        public static void ExecuteActionBlock()
        {
            var processInput = new ActionBlock<string>(s =>
            {
                Console.WriteLine($"user input: {s}");
            });
            bool exit = false;
            while (!exit)
            {
                string? input = Console.ReadLine();
                if (String.CompareOrdinal(input, "exit") == 0)
                {
                    exit = true;
                }
                else
                {
                    processInput.Post(input??"");
                }
            }
        }

        public static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** task cancelled"));
            // send a cancel after 500 ms
            cts.CancelAfter(500);
            Task t1 = Task.Run(() =>
            {
                Console.WriteLine("in task");
                for (int i = 0; i < 20; i++)
                {
                    Task.Delay(100).Wait();
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("cancelling was requested, " +
                                          "cancelling from within the task");
                        token.ThrowIfCancellationRequested();
                        break;
                    }
                    Console.WriteLine("in loop");
                }
                Console.WriteLine("task finished without cancellation");
            }, cts.Token);
            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"inner exception: {ex.InnerException.GetType()}, " +
                                      $"{ex.InnerException.Message}");
                }
            }
        }

        public static void CancelParallelFor()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** token cancelled"));
            // send a cancel after 500 ms
            cts.CancelAfter(500);
            try
            {
                ParallelLoopResult result =
                    Parallel.For(0, 100, new ParallelOptions
                        {
                            CancellationToken = cts.Token,
                        },
                        x =>
                        {
                            Console.WriteLine($"loop {x} started");
                            int sum = 0;
                            for (int i = 0; i < 100; i++)
                            {
                                Task.Delay(2).Wait();
                                sum += i;
                            }
                            Console.WriteLine($"loop {x} finished");
                        });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();
            Task.Delay(100).Wait();
            Console.WriteLine(parent.Status);
            Task.Delay(300).Wait();
            Console.WriteLine(parent.Status);
        }
        private static void ParentTask()
        {
            Console.WriteLine($"task id {Task.CurrentId}");
            var child = new Task(ChildTask);
            child.Start();
            Task.Delay(100).Wait();
            Console.WriteLine("parent started child");
        }
        private static void ChildTask()
        {
            Console.WriteLine("child");
            Task.Delay(500).Wait();
            Console.WriteLine("child finished");
        }

        public static void ContinuationTasks()
        {
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            Task t3 = t1.ContinueWith(DoOnSecond);
            Task t4 = t2.ContinueWith(DoOnSecond);
            t1.Start();
        }

        public static void DoOnFirst()
        {
            Console.WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }
        public static void DoOnSecond(Task t)
        {
            Console.WriteLine($"task {t.Id} finished");
            Console.WriteLine($"this task id {Task.CurrentId}");
            Console.WriteLine("do some cleanup");
            Task.Delay(3000).Wait();
        }

        public static void ExecuteTaskWithResult()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(8, 3));
            t1.Start();
            Console.WriteLine(t1.Result);
            t1.Wait();
            Console.WriteLine($"result from task: {t1.Result.Item1} {t1.Result.Item2}");
        }

        public static Tuple<int, int> TaskWithResult(object? division)
        {
            if (division == null)
            {
                return Tuple.Create(0, 0);
            }

            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            Console.WriteLine("task creates a result...");
            return Tuple.Create(result, reminder);
        }

        private static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running",
                TaskCreationOptions.LongRunning);
            t1.Start();
            Console.ReadLine();
        }

        public static void TasksUsingThreadPool()
        {
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
            var t3 = new Task(TaskMethod, "using a task constructor and Start");
            t3.Start();
            Task t4 = Task.Run(() => TaskMethod("using the Run method"));
            var t5 = new Task(TaskMethod, "Synchronously");
            t5.RunSynchronously();
        }


        public static void TaskMethod(object? o)
        {
            ShareLog(o?.ToString());
        }

        private static readonly object Lock = new object();

        public static void ShareLog(string? title)
        {
            lock (Lock)
            {
                Console.WriteLine(title);
                Console.WriteLine($"Task id: {Task.CurrentId?.ToString() ?? "no task"}, " +
                    $"thread: {Thread.CurrentThread.ManagedThreadId}");
#if (!DNXCORE)
                Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
#endif
                Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
                Console.WriteLine();
            }
        }


        public static void Bar()
        {
            Console.WriteLine("Bar....");
        }

        public static void Car()
        {
            Console.WriteLine("Car...");
        }


        public static void ParallelForeach()
        {
            List<string> data = new List<string>()
            {
                "org","com","cc","cn","com.cn"
            };
            var parallelLoopResult = Parallel.ForEach(data, i =>
            {
                Console.WriteLine(i);
            });
            Console.WriteLine($"Task is completed!,{parallelLoopResult.IsCompleted}");
        }

        public static void ParallelForWithInit()
        {
            Parallel.For<string>(0, 10, () =>
                {
                    // invoked once for each thread
                    Log($"init thread");
                    return $"t{Thread.CurrentThread.ManagedThreadId}";
                },
                (i, pls, str) =>
                {
                    // invoked for each member
                    Log($"body i {i} str1 {str}");
                    Task.Delay(10).Wait();
                    return $"i {i}";
                },
                (str) =>
                {
                    // final action on each thread
                    Log($"finally {str}");
                });
        }

        public static void ParallelBreak()
        {
            var parallelLoopResult = Parallel.For(0, 20, (int i, ParallelLoopState pls) =>
            {
                Log($"S {i}");
                if (i > 15)
                {
                    pls.Break();
                    Log($"Break is now ! {i}");
                }

                Task.Delay(10).Wait();
                Log($"E {i}");
            });
            Console.WriteLine($"Task is Completed! {parallelLoopResult.IsCompleted}");
            Console.WriteLine($"Task is Break! {parallelLoopResult.LowestBreakIteration}");
        }

        public static void CallAsyncExecute()
        {

            var parallelLoopResult = Parallel.For(0, 10, AsyncExecute);
            Console.WriteLine($"Task is Completed! {parallelLoopResult.IsCompleted}");
        }

        private static async void AsyncExecute(int i)
        {
            Log($"S {i}");
            await Task.Delay(10);
            Log($"E {i}");
        }

        public static void ParallelBase()
        {

            var parallelLoopResult = Parallel.For(0, 10, i =>
            {
                Log($"S {i}");
                Task.Delay(10).Wait();
                Log($"E {i}");
            });
            Console.WriteLine($"Task is Completed! {parallelLoopResult.IsCompleted}");
        }

        public static void Log(string prefix)
        {
            Console.WriteLine($"{prefix}, task: {Task.CurrentId}, " +
                      $"thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}