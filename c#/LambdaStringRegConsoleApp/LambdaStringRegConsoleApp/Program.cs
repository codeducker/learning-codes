using System.Globalization;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace LambdaStringRegConsoleApp
{

    public delegate string StringRegDelegate(string input);


    public delegate double DoubleOperatorDelegate(double a, double b);

    internal class Program
    {
        internal static void Main(string[] args)
        {

            Regex reg = new Regex("[0-9]*");//这是搜索匹配0-9的数字
            Console.WriteLine(reg.Match("12asda"));//最后提取出了12成功，嘻嘻


            var line = "Hey, I've just found this amazing URI at " +
                          "http:// what was it -oh yes https://www.wrox.com or " +
                          "http://www.wrox.com:80";
            var pattern = @"\bhttp[s]{0,}://\w+.\w+.\w+([:]{0,}\d+)?";
            var r = new Regex(pattern);
            MatchCollection mc = r.Matches(line);
            foreach (Match m in mc)
            {
                Console.WriteLine($"Match: {m}");
                foreach (Group g in m.Groups)
                {
                    if (g.Success)
                    {
                        Console.WriteLine($"group index: {g.Index}, value: {g.Value}");
                    }
                }
                Console.WriteLine();
            }


            //string pattern = @"\b(? <protocol>https? )(? :://)" +
            //                 @"(? <address>[.\w]+)([\s:](? <port>[\d]{2,4})? )\b";
            //Regex r = new Regex(pattern, RegexOptions.ExplicitCapture);
            //MatchCollection mc = r.Matches(line);
            //foreach (Match m in mc)
            //{
            //    Console.WriteLine($"match: {m} at {m.Index}");
            //    foreach (var groupName in r.GetGroupNames())
            //    {
            //        Console.WriteLine($"match for {groupName}: {m.Groups[groupName].Value}");
            //    }
            //}

            const string input =
                @"This book is perfect for both experienced C# programmers looking to " +
                "sharpen their skills and professional developers who are using C# for " +
                "the first time. The authors deliver unparalleled coverage of " +
                "Visual Studio 2013 and .NET Framework 4.5.1 additions, as well as " +
                "new test-driven development and concurrent programming features. " +
                "Source code for all the examples are available for download, so you " +
                "can start writing Windows desktop, Windows Store apps, and ASP.NET " +
                "web applications immediately.";
            FindStr(input, "ion");


            double d = 3.1415;
            Console.WriteLine($"{d:###.###}");
            Console.WriteLine($"{d:000.000}");

            int si = 2477;
            Console.WriteLine($"{si:n} {si:e} {si:x} {si:c}");

            //模板字符串 转义{}
            string sr = "Hello";
            Console.WriteLine($"{{sr}} displays the value of s: {sr}");

            string formatString = $"{sr}, {{0}}";
            string s2 = "World";
            Console.WriteLine(formatString, s2);


            var day = new DateTime(2025, 2, 14);
            Console.WriteLine($"{day:d}");
            Console.WriteLine(Invariant($"{day:d}"));

            int x1 = 3, y1 = 4;
            FormattableString s1 = $"The result of {x1} + {y1} is {x1 + y1}";
            Console.WriteLine($"format: {s1.Format}");
            for (int i = 0; i < s1.ArgumentCount; i++)
            {
                Console.WriteLine($"argument {i}: {s1.GetArgument(i)}");
            }

            Console.WriteLine("{0} is Coming",666);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("check");
            Console.WriteLine(stringBuilder.ToString());

            //string message = Console.ReadLine();
            //Console.WriteLine($"Length:{message.Length},first Latter: {message.Substring(0,1)}, is Contains 'World':{message.IndexOf("world") > 0}," +
            //    $"Pad Left : {message.PadLeft(2)} ");

            int someVal = 5;
            Func<int, int> f = x => x + someVal;

            //lambda 表达式
            Func<double, double, string> func = (double va, double vb) => "" + (va + vb);
            var s = func(1.2,2.4);
            Console.WriteLine(s);

            //匿名委托
            Func<string,int> action = delegate(string param)
            {
                Console.WriteLine("Start Parse Int");
                return Convert.ToInt32(param);
            };
            var actionResult = action("1234");
            Console.WriteLine($"Action Result : {actionResult}");

            //多播委托，此时若一发生异常，则无法继续执行
            double fixNum = 4.5;
            Action<double> actions = MathOperations.CheckRatio;
            actions+= MathOperations.GetRatio;//此时使用+= , + 添加委托 ， -= , - 减少委托
            try
            {
                actions(fixNum);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catch e ,{e.GetBaseException()}!");
            }
            //若单委托发生异常处理流程

            Delegate[] invocationList = actions.GetInvocationList();
            foreach (var invocation in invocationList)
            {
                var @delegate = (Action<double>)invocation;
                try
                {
                    @delegate(fixNum);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Caught An e : {e.GetBaseException()}");
                }
            }

            //Action<double> d1 = One;
            //d1 += Two;
            //Delegate[] delegates = d1.GetInvocationList();
            //foreach (Action<double> d in delegates)
            //{
            //    try
            //    {
            //        d(fixNum);
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Exception caught");
            //    }
            //}

            Employee[] employees =
            {
                new Employee("Bugs Bunny", 20000),
                new Employee("Elmer Fud", 10000),
                new Employee("Daffy Duck", 25000),
                new Employee("Wile Coyote", 1000000.38m),
                new Employee("Foghorn Leghorn", 23000),
                new Employee("RoadRunner", 50000)
            };
            BubbleSorter.Sort(employees, Employee.CompareSalary);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
            //Action<T1....> 不带返回
            DoubleOperatorDelegate[] doubleOperatorDelegates =
                new DoubleOperatorDelegate[]
                {
                    MathOperations.Add,
                    MathOperations.Subtract,
                };
            double a = 1.2, b = 3.4;
            for (int i = doubleOperatorDelegates.Length - 1; i >= 0; i--)
            {

                Console.WriteLine($"Method Name: {doubleOperatorDelegates[i].Method.ToString()} , Result : {doubleOperatorDelegates[i](a,b)}");   
            }
            //Func<in T1....,out TResult> 带返回
            Func<double,double,double>[] doubleOperatorDelegatesFunc = {
                    MathOperations.Add,
                    MathOperations.Subtract
            };
            double a1 = 3.2, b2 = 4.9;
            for (int i = doubleOperatorDelegatesFunc.Length - 1; i >= 0; i--)
            {

                Console.WriteLine($"Method Name: {doubleOperatorDelegatesFunc[i].Method.ToString()} , Result : {ProcessAction(doubleOperatorDelegatesFunc[i], a1, b2)}");
            }


            int x = 0;
            var stringRegDelegate = new StringRegDelegate(x.ToString);
            Console.WriteLine(stringRegDelegate(Convert.ToString(x)));
            StringRegDelegate stringDelegate = x.ToString;
            Console.WriteLine(stringDelegate("hello"));
        }


        //static void One(double a)
        //{
        //    Console.WriteLine($"One {a}");
        //    throw new Exception("Error in one");
        //}
        //static void Two(double a)
        //{
        //    Console.WriteLine($"Two {a}");
        //}

        public static double ProcessAction(Func<double, double,double> action, double value1,double value2)
        {
            var result = action(value1,value2);
            Console.WriteLine(result);
            return result;
        }

        private static string Invariant(FormattableString s) =>
            s.ToString(CultureInfo.InvariantCulture);

        public static void FindStr(string text,string pattern)
        {
            MatchCollection matches = Regex.Matches(text, pattern,
                RegexOptions.IgnoreCase |
                RegexOptions.ExplicitCapture);
            foreach (Match nextMatch in matches)
            {
                Console.WriteLine(nextMatch.Index);
            }
        }

    }

    class BubbleSorter
    {
        static public void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 0; i < sortArray.Count - 1; i++)
                {
                    if (comparison(sortArray[i + 1], sortArray[i]))
                    {
                        (sortArray[i], sortArray[i + 1]) = (sortArray[i + 1], sortArray[i]);
                        swapped = true;
                    }
                }
            } while (swapped);
        }
    }

    class Employee
    {
        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }
        public string Name { get; }
        public decimal Salary { get; private set; }
        public override string ToString() => $"{Name}, {Salary:C}";
        public static bool CompareSalary(Employee e1, Employee e2) =>
            e1.Salary < e2.Salary;
    }

    class MathOperations
    {
        public static double Add(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b; 

        public static void CheckRatio(double a)
        {
            Console.WriteLine($"Check a double : {a}");
            throw new Exception("Illegal Operation!");
        }

        public static void GetRatio(double a) => Console.WriteLine($"Get a double: {a}");
    }
}
