using System.Drawing;

namespace ObjectConsoleApp
{
    //结构体 不允许继承 结构的继承链 System.ValueType 可以通过 ref 来指定 引用传递
    struct ObjectStruct //结构通常存储在栈上 类通常存储堆 创建实例都是通过new关键字 类通过引用传入 / 结构值传入
    {
        public int Value { get; set; }
    }

    public static class UserPreferences
    {
        public static Color BackColor { get; }
        static UserPreferences()
        {
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Saturday
                || now.DayOfWeek == DayOfWeek.Sunday)
            {
                BackColor = Color.Green;
            }
            else
            {
                BackColor = Color.Red;
            }
        }
    }

    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public int? IntNumber { get; set; } = null; //可空类型 是用?标志
    }

    class Car
    {
        public readonly string _name;
        private string _description;
        private uint _nWheels;


        public string Id { get; } = Guid.NewGuid().ToString();//属性初始化器初始化

        public Car(string description, uint nWheels)
        {
            _description = description;
            _nWheels = nWheels;
            _name = "static maker";
        }

        //静态构造函数 ，只执行一遍，用于对静态属性之类的进行初始化 ，静态构造函数不能包含参数
        static Car()
        {
        
        }

        public Car(string description) : this(description, 4)//构造函数初始化器
        {
            _name = "static maker";
        }
    }

    // etc
    /// <summary>
    /// 单例模式
    /// </summary>
    public class Singleton
{
    private static Singleton? _instance;
    private int _state;
    private Singleton(int state)
    {
        _state = state;
    }
    public static Singleton Instance
    {
        get { return _instance ??= new Singleton(42); }
    }

    public void Check(int state)
    {
        Console.WriteLine(state == 1);
    }
}

    //public class Math
    //{
    //    public Math()//构造方法
    //    {
    //    }

    //    public int Value { get; set; }
    //    public int GetSquare() => Value * Value;
    //    public static int GetSquareOf(int x) => x * x;

    //    public static int GetSquareOf(string x) => int.Parse(x) * int.Parse(x); //方法重载
    //    public static double GetPi() => 3.14159;

    //    public static int CalMath(int x, int y, int z) => x + y + z;

    //    public static string Kill(string x, string y = "ni hao") => x + y;

    //    public static int Cal(params int[] numbers) => numbers.Sum();//不定参数 

    //    public static string Cal(string va, params int[] val2) => va + val2.Sum();//params标记不定参数且必须为方法最后一个参数,且只有一个
    //}

    internal class PhoneCustomer
    {

        public int Age { get; set; } = 42;//自动实现属性初始化

        public string? LastName
        {
            /* protected .*/
            get;
            private set;
            //gets/set设置不同的修饰符 ,且必须有一个方法可对外访问，否则会报编译错误
        }

        public string? FirstName { get; set; }

        public string? Say()
        {
            return FirstName;
        }

        public void Change(ref ObjectStruct objectStruct)
        {
            objectStruct.Value = 2;
        }

        public void ChangeClass(ref Person person)
        {
            person.Name = "By Way";
            person = new Person("Two Way!");
        }

        public bool ParseInt(string? value, out int result)
        {
            try
            {
                result = int.Parse(value??="");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = 0;
                return false;
            }
        }
    }

    public struct Dimensions
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public Dimensions(double length, double width)
        {
            Length = length;
            Width = width;
        }
        public double Diagonal => Math.Sqrt(Length * Length + Width * Width);
    }

    //[Flags] //编译器创建值的另一个字符串表示的信息
    public enum DaysOfWeek
    {
        Monday = 0x1,
        Tuesday = 0x2,
        Wednesday = 0x4,
        Thursday = 0x8,
        Friday = 0x10,
        Saturday = 0x20,
        Sunday = 0x40
    }

    //partial 部分类 将内容分拆到不同代码块内 可以用于修饰 class struct interface等部分
    partial class SampleClass
    {
        public void MethodOne() {
            Console.WriteLine("One");
            ApiMethodLoad();
        }

        public partial void ApiMethodLoad(); //此时其他part部分必须存在同样的实现方法
    }
    partial class SampleClass
    {
        public void MethodTwo() { Console.WriteLine("Two");}

        public partial void ApiMethodLoad()
        {
            Console.WriteLine("Api Method Impl");
        }
    }

    public static class StringExtension
    {
        public static int GetWordCount(this string s) => //扩展方法同命名空间下
            s.Split().Length;

        public static int WorldLength(this string s, out int valCount) => //扩展方法
            valCount = s.Length;
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("FOX".GetWordCount());
            Console.WriteLine("FOX".WorldLength(out int valCount));
            Console.WriteLine(valCount);
            //var sampleClass = new SampleClass();
            //sampleClass.MethodOne();
            //sampleClass.MethodTwo();


            //Console.WriteLine(DaysOfWeek.Monday | DaysOfWeek.Friday);//么有flag标致 按照整数进行运算 值为17 否则为 Monday,Friday
            //ObjectStruct objectStruct;
            //objectStruct.Value = 3;
            //DaysOfWeek monDay;
            //Enum.TryParse<DaysOfWeek>("Monday", out monDay);
            //Console.WriteLine(monDay);

            //foreach (var day in Enum.GetNames(typeof(DaysOfWeek)))
            //{
            //    Console.WriteLine(day);
            //}

            //foreach (var val in Enum.GetValues(typeof(DaysOfWeek)))
            //{
            //    Console.WriteLine(val);
            //}


            //Dimensions point;
            //point.Length = 3;
            //point.Width = 6;

            ////匿名类型
            //var check = new
            //{
            //    Name = "Hello",
            //    LastName = "li",
            //    FirstName = "andy"
            //};
            //Console.WriteLine(check.LastName);

            //var docker = new
            //{
            //    Name = "Hello",
            //    LastName = "li",
            //    FirstName = "andy"
            //};
            //Console.Write(docker.GetType() == check.GetType());


            //var phoneCustomer = new PhoneCustomer
            //{
            //    FirstName = "andy"
            //};
            //var oj = new ObjectStruct
            //{
            //    Value = 10
            //};
            //phoneCustomer.Change(ref oj);
            //Console.WriteLine(oj.Value);

            //Person p = new Person("Newly");
            //phoneCustomer.ChangeClass(ref p);
            //Console.WriteLine(p.Name);
            //Console.WriteLine(phoneCustomer.ParseInt(Console.ReadLine(), out var result)
            //    ? $"当前输入数字为:{result}"
            //    : "输入非数字!");// out关键字使用 作为方法执行结果返回
            //Console.WriteLine("+++"+(p.IntNumber.HasValue ? p.IntNumber.Value : 0) +"+++");//使用可空类型的HasValue/Value判断
            //Console.WriteLine("+++"+(p.IntNumber ?? 0) +"+++");

            //Console.WriteLine(phoneCustomer.Say());
            //Console.WriteLine($"{Math.GetPi()}");
            //Math.CalMath(1, 2, 3);
            //Math.CalMath(y: 2, x: 1, z: 3); //两种调用方式
            //Math.Kill("many");//可选参数，其他非输入参数必须提供默认值
            //Math.Cal(1);
            //Math.Cal(1, 2);

            //var singleton = Singleton.Instance;
            //singleton.Check(2);

            //Console.WriteLine(UserPreferences.BackColor);
            //var car = new Car("GUB");
            //Console.WriteLine(car._name);
        }
}
}

