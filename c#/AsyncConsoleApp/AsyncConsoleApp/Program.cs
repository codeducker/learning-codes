namespace AsyncConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(Greeting("andy"));
            //
            // ConsoleAsync();
            //
            // ConsoleAsync2();
            //
            // MultipleCallMethod();
            //
            // MultipleAllCallMethod();
            //
            // MultipleAnyCallMethod();

            // FactoryAsync();
            
            DoNotHandle();

            HandleOneError();

            HandleTwoError();

            HandleAllError();

            Console.ReadLine();
        }

        //此时虽然只会显示第一个异常，但是两个方法都会被执行,但是此时会将所有异常包装到AggregatedException
        private static async void HandleAllError()
        {
            Task? allTask = null;
            try
            {
                var task1 = ThrowAfter(2, "这是个异常 HandleAllError");
                var task2 = ThrowAfter(1, "这是个异常 2 HandleAllError");
                await ( allTask = Task.WhenAll(task1, task2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (allTask != null && allTask.Exception != null)
                {
                    foreach (var exceptionInnerException in allTask.Exception.InnerExceptions)
                    {
                        Console.Write(exceptionInnerException.Message + "\t");
                    }
                }
            }
        }

        //此时只会处理一个异常
        private static async void HandleTwoError()
        {
            try
            {
                await ThrowAfter(2, "这是个异常 HandleTwoError");
                await ThrowAfter(1, "这是个异常 2 HandleTwoError");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //此时会打印异常
        private static async void HandleOneError()
        {
            try
            {
                await ThrowAfter(2, "这是个异常 HandleOneError");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /**
         * 此时异常不会打印
         */
        private static void DoNotHandle()
        {
            try
            { 
                ThrowAfter(2, "这是个异常 DoNotHandle");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<string> ThrowAfter(int seconds, string message)
        {
            Console.WriteLine($"seconds:{seconds},message:{message}\n");
            await Task.Delay(seconds*1000);
            throw new Exception(message);
        }

        private static async void FactoryAsync()
        {
            var s = await Task<string>.Factory.FromAsync<string>(
                BeginGreeting, EndGreeting, "Angela", null);
            Console.WriteLine(s);
        }

        private static readonly Func<string,string> FuncAction = Greeting;

        private static IAsyncResult BeginGreeting(string name, AsyncCallback asyncCallback,object? state)
        {
            return FuncAction.BeginInvoke(name, asyncCallback, state);
        }

        private static string EndGreeting(IAsyncResult asyncResult)
        {
            return FuncAction.EndInvoke(asyncResult);
        }

        static async void MultipleAnyCallMethod()
        {
            var greetingAsync1 = GreetingAsync("lok.any...");
            var greetingAsync2 = GreetingAsync("cok..any..");
            var whenAll = Task.WhenAny(greetingAsync1, greetingAsync2);//任一执行返回结果
            await whenAll;
            Console.WriteLine($"result1:{greetingAsync1.Result},result2:{greetingAsync2.Result}");
        }

        static async void MultipleAllCallMethod()
        {
            var greetingAsync1 = GreetingAsync("lok....");
            var greetingAsync2 = GreetingAsync("cok....");
            var whenAll = Task.WhenAll(greetingAsync1, greetingAsync2);
            await whenAll;
            Console.WriteLine($"result1:{greetingAsync1.Result},result2:{greetingAsync2.Result}");
            //两种方式都是可以获取
            // var strings = await whenAll;
            // Console.WriteLine($"result1:{strings[0]},result2:{strings[1]}");
        }

        static async void MultipleCallMethod()
        {
            string result1 = await GreetingAsync("a...");
            string result2 = await GreetingAsync("b....");
            Console.WriteLine($"result1:{result1},result2:{result2}");
        }

        static void ConsoleAsync2()
        {
            Task<string> greetingAsync = GreetingAsync("andy");
            greetingAsync.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);
            });
            // Console.WriteLine(await GreetingAsync("andy") );
        }

        static async void ConsoleAsync()
        {
            string result = await GreetingAsync("andy");
            Console.WriteLine(result);//无任何打印
        }

        static Task<string> GreetingAsync(string name)
        {
            return Task.Run(() => Greeting(name));
        }

        static string Greeting(string name)
        {
            Task.Delay(3000).Wait();
            return $"Hello, {name}";
        }
    }
}