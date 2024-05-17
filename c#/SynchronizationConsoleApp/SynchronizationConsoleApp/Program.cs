using System.Diagnostics;
using System.Net;
using System.Text;

namespace SynchronizationConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var state = new StateObject();
            //
            // for (int i = 2; i >= 0; i--)
            // {
            //     Task.Run(() => new SampleTask().RaceCondition(state));
            // }
            
            
            // int length = 1000;
            // Task[] tasks = new Task[length];
            // for (int i = length - 1; i >= 0; i--)
            // {
            //     tasks[i] = Task.Run(MonitorClass.AddMonitor);
            // }
            //
            // Task.WaitAll(tasks);
            
            // var spinLockClass = new SpinLockClass();
            // spinLockClass.Get(1);

            // EventsClass.CallCalculations();
            
            // WaitHandleClass.InvokeWaitHandle();
            
            BarrierClass.CallBarrier();
            
            Console.ReadLine();
        }
    }
    
    public delegate int TaskAsWhileDelegate(int x, int ms);


    public class BarrierClass
    {
        
        public static void CallBarrier()
        {
            const int numberTasks = 2;
            const int partitionSize = 1000000;
            const int loops = 5;
            var taskResults = new Dictionary<int, int[][]>();
            var data = new List<string>[loops];
            for (int i = 0; i < loops; i++)
            {
                data[i] = new List<string>(FillData(partitionSize * numberTasks));
            }
            var barrier = new Barrier(numberTasks + 1);
            LogBarrierInformation("initial participants in barrier", barrier);
            for (int i = 0; i < numberTasks; i++)
            {
                barrier.AddParticipant();
                int jobNumber = i;
                taskResults.Add(i, new int[loops][]);
                // for (int loop = 0; loop < loops; loop++)
                // {
                //     var taskResult = new int[i,loop];
                //     taskResult[i, loop] = new int[4,26];
                // }
                Console.WriteLine($"Main - starting task job {jobNumber}");
                Task.Run(() => CalculationInTask(jobNumber, partitionSize,
                    barrier, data, loops, taskResults[jobNumber]));
            }
            for (int loop = 0; loop < 5; loop++)
            {
                LogBarrierInformation("main task, start signaling and wait", barrier);
                barrier.SignalAndWait();
                LogBarrierInformation("main task waiting completed", barrier);
                int[][] resultCollection1 = taskResults[0];
                int[][] resultCollection2 = taskResults[1];
                var resultCollection = resultCollection1[loop].Zip(
                    resultCollection2[loop], (c1, c2) => c1 + c2);
                char ch = 'a';
                int sum = 0;
                foreach (var x in resultCollection)
                {
                    Console.WriteLine($"{ch++}, count: {x}");
                    sum += x;
                }
                LogBarrierInformation($"main task finished loop {loop}, sum: {sum}",
                    barrier);
            }
            Console.WriteLine("finished all iterations");
        }
                
        private static void CalculationInTask(int jobNumber, int partitionSize,
            Barrier barrier, IList<string>[] coll, int loops, int[][] results)
        {
            LogBarrierInformation("CalculationInTask started", barrier);
            for (int i = 0; i < loops; i++)
            {
                var data = new List<string>(coll[i]);
                int start = jobNumber * partitionSize;
                int end = start + partitionSize;
                Console.WriteLine($"Task {Task.CurrentId} in loop {i}: partition " +
                          $"from {start} to {end}");
                for (int j = start; j < end; j++)
                {
                    char c = data[j][0];
                    results[i][c -97]++;
                }
                Console.WriteLine($"Calculation completed from task {Task.CurrentId} " +
                          $"in loop {i}. {results[i][0]} times a, {results[i][25]} times z");
                LogBarrierInformation("sending signal and wait for all", barrier);
                barrier.SignalAndWait();
                LogBarrierInformation("waiting completed", barrier);
            }
            barrier.RemoveParticipant();
            LogBarrierInformation("finished task, removed participant", barrier);
        }
        
        private static IEnumerable<string> FillData(int size)
        {
            var r = new Random();
            return Enumerable.Range(0, size).Select(x => GetString(r));
        }

        private static string GetString(Random r)
        {
            var sb = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                sb.Append((char)(r.Next(26) + 97));
            }
            return sb.ToString();
        }
        private static void LogBarrierInformation(string info, Barrier barrier)
        {
            Console.WriteLine($"Task {Task.CurrentId}: {info}. " +
                      $"{barrier.ParticipantCount} current and " +
                      $"{barrier.ParticipantsRemaining} remaining participants, " +
                      $"phase {barrier.CurrentPhaseNumber}");
        }

    }

    public class EventsClass
    {
        private ManualResetEventSlim _mEvent;
        public int Result { get; private set; }
        public EventsClass(ManualResetEventSlim ev)
        {
            _mEvent = ev;
        }
        public void Calculation(int x, int y)
        {
            Console.WriteLine($"Task {Task.CurrentId} starts calculation");
            Task.Delay(new Random().Next(3000)).Wait();
            Result = x + y;
            // signal the event-completed!
            Console.WriteLine($"Task {Task.CurrentId} is ready");
            _mEvent.Set();
        }
        
        public static void CallCalculations()
        {
            const int taskCount = 4;
            var mEvents = new ManualResetEventSlim[taskCount];
            var waitHandles = new WaitHandle[taskCount];
            var calcs = new EventsClass[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                int i1 = i;
                mEvents[i] = new ManualResetEventSlim(false);
                waitHandles[i] = mEvents[i].WaitHandle;
                calcs[i] = new EventsClass(mEvents[i]);
                Task.Run(() => calcs[i1].Calculation(i1 + 1, i1 + 3));
            }
            for (int i = 0; i < taskCount; i++)
            {
                int index = WaitHandle.WaitAny(waitHandles);
                if (index == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("Timeout! ! ");
                }
                else
                {
                    mEvents[index].Reset();
                    Console.WriteLine($"finished task for {index}, result: {calcs[index].Result}");
                }
            }
        }
    }

    public class SemaphoreClass
    {
        public static void CallSema()
        {
            int taskCount = 6;
            int semaphoreCount = 3;
            var semaphore = new SemaphoreSlim(semaphoreCount, semaphoreCount);
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() => TaskMain(semaphore));
            }
            Task.WaitAll(tasks);
            Console.WriteLine("All tasks finished");
        }

        // etc
        public static void TaskMain(SemaphoreSlim semaphore)
        {
            bool isCompleted = false;
            while (! isCompleted)
            {
                if (semaphore.Wait(600))
                {
                    try
                    {
                        Console.WriteLine($"Task {Task.CurrentId} locks the semaphore");
                        Task.Delay(2000).Wait();
                    }
                    finally
                    {
                        Console.WriteLine($"Task {Task.CurrentId} releases the semaphore");
                        semaphore.Release();
                        isCompleted = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Timeout for task {Task.CurrentId}; wait again");
                }
            }
        }
    }

    public class MutexClass
    {
        public static void CallMutex()
        {
            var mutex = new Mutex(false,"ProCsharpMutex",out var createdNew);
            // mutex = Mutex.OpenExisting("ProCsharpMutex");
            if (mutex.WaitOne())
            {
                try
                {
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                
            }
        }
    }

    public class WaitHandleClass
    {
        public static void InvokeWaitHandle()
        {
            var asWhileDelegate = new TaskAsWhileDelegate(TaskAsWhile);
            var task = Task.Run(() => asWhileDelegate.Invoke(1, 3000));
            while (true)
            {
                Console.Write(".");
                if (((IAsyncResult)task).AsyncWaitHandle.WaitOne(50))
                {
                    Console.WriteLine("\nCan Get The Result Now!");
                    break;
                }
                Console.WriteLine($"\nResult: {task.Result}");
            }

            //.NET4.5之后不再支持 BeginInvoke / EndInvoke
            // var taskAsWhileDelegate = TaskAsWhile;
            // var beginInvoke = taskAsWhileDelegate.BeginInvoke(1, 3000, null, null);
            // while (true)
            // {
            //     Console.WriteLine(".");
            //     if (beginInvoke.AsyncWaitHandle.WaitOne(50))
            //     {
            //         Console.WriteLine("Can Get The Result Now!");
            //         break;
            //     }
            //
            //     var endInvoke = taskAsWhileDelegate.EndInvoke(beginInvoke);
            //     Console.WriteLine($"Result: {endInvoke}");
            // }
        }

        public static int TaskAsWhile(int x, int ms)
        {
            Task.Delay(ms).Wait();
            return 42;
        }
    }


    //自旋锁 单个线程在获取锁对象，此时锁已经被其他线程获取，则该线程会循环等待，不断去获取锁，直到获取到锁为止，适用于原子操作时间非常短的场景
    //优点：
    //  避免线程上下文切换，性能较高
    //缺点:
    //  长时间等待消耗大量CPU资源
    //  多个等待线程并不是等待时间越长就先获取锁，会导致某一线程一致获取不到锁

    public class SpinLockClass
    {
        private static int _SpinLock = 0;
        private static int incrValue = 0;

        private void Run()
        {
            while (Interlocked.Exchange(ref _SpinLock, 1) != 0)
            {
                Thread.Sleep(1);
            }

            incrValue++;
            Console.WriteLine($"Current Value: {incrValue}");
            Interlocked.Exchange(ref _SpinLock, 0);
        }

        public void Get(int type)
        {
            var parallelLoopResult = Parallel.For(0, 1000, (i) =>
            {
                switch (type)
                {
                    case 0:
                    {
                        Run();
                        break;
                    }
                    case 1:
                    {
                        Run2();
                        break;
                    }
                }
            });
            Console.WriteLine($"incrValue:{incrValue}");
            Console.WriteLine($"isCompleted :{parallelLoopResult.IsCompleted}");
        }

        private static SpinLock _spinLock = new SpinLock();

        private void Run2()
        {
            bool locked = false;
            _spinLock.TryEnter(100,ref locked);//获取锁
            incrValue++;
            if (locked)
            {
                _spinLock.Exit();
            }
            Console.WriteLine($"Current SpinLock Value: {incrValue}");
        }
    }



    public class MonitorClass
    {
        public static object _obj = new object();
        public static void AddMonitor()
        {
            bool _lockTaken = false;
            Monitor.TryEnter(_obj,500,ref _lockTaken);
            if (_lockTaken)
            {
                //Monitor.Enter(_obj);
                try
                {
                    Console.WriteLine("Monitor Execute...");
                }
                finally
                {
                    Monitor.Exit(_obj);
                }
            }
        }
    }


    public class SharedState
    {
        private int _state = 0;
        private object _syncRoot = new object();
        public int State // there's still a race condition,
            // don't do this!
        {
            get { lock (_syncRoot) { return _state; } }
            set { lock (_syncRoot) { _state = value; } }
        }
    }

    public class Demo
    {
        public virtual bool IsSynchronized => false;
        public static Demo Synchronized(Demo d)
        {
            if (!d.IsSynchronized)
            {
                return new SynchronizedDemo(d);
            }
            return d;
        }
        public virtual void DoThis()
        {
        }
        public virtual void DoThat()
        {
        }

        private class SynchronizedDemo : Demo
        {
            private object _syncRoot = new object();
            private Demo _d;
            public SynchronizedDemo(Demo d)
            {
                _d = d;
            }
            public override bool IsSynchronized => true;
            public override void DoThis()
            {
                lock (_syncRoot)
                {
                    _d.DoThis();
                }
            }
            public override void DoThat()
            {
                lock (_syncRoot)
                {
                    _d.DoThat();
                }
            }
        }

    }

    public class SampleTask
    {

        public void RaceCondition(object o)
        {
            Trace.Assert(o is StateObject, "o must be of type StateObject");
            StateObject? state = o as StateObject;
            int i = 0;
            while (true)
            {
                Console.WriteLine($"State:{state.State} Add ....");
                state?.ChangeState(i++);
            }
        }
    }

    public class StateObject
    {
        private int _state = 5;

        private object sync = new object();
        public void ChangeState(int loop)
        {
            lock (sync)
            {
                if (_state == 5)
                {
                    _state++;
                    Trace.Assert(_state == 6,
                        $"Race condition occurred after {loop} loops");
                }
            }

            _state = 5;
        }

        public int State { get { return _state;} }
    }
}