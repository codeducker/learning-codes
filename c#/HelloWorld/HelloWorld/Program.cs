/*
 * 预处理指令
 * #define 标记符号
 * #undef 删除符号
 * define / undef 预处理指令必须在代码开头
 */
#define DEBUG
//#undef DEBUG
#define EnterFlag

using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Reflection.Metadata;//引入命名空间
using makerName = HelloWorld.Maker;//设置别名

//region endregion 折叠代码快
#region taf 
const string hel = "Hel";
#endregion

//C#预处理指令 https://learn.microsoft.com/zh-CN/dotnet/csharp/language-reference/preprocessor-directives
#line 25 "Pragram.cs" //定义文件行号
#line default //还原行号
namespace Fox
{
    internal interface IFly
    {
        public void Fly();
    }

    public class Bird : IFly
    {
        public Bird(string name)
        {
            Name = name;
            //name = name;
        }

        public string Name { get; set; }

        //public string name { get; set; }

        public void Fly()
        {

#if DEBUG //#if #endif #else #elif 用于判断指令
            //#warning "这一行要特数处理一下"
            //#error "抛出异常" //编译器抛出一场
            Console.WriteLine("Bird Fly....");

#endif

        }
    }
}



namespace HelloWorld
{



    //单行注释
    /*
     * 多行注释
     */
    /// <summary>
    /// xml文档
    /// </summary>
    internal class Program
    {

        //public enum TimeOfDay
        //{
        //    Morning = 0,
        //    Afternoon = 1,
        //    Evening = 2
        //}
        //private const int like = 100;
        //private static int j = 1020;
        new
        public static void Main(string[] args)//主函数入口 这里的修饰符 可以不写 也可以设为 private ,程序照样运行
        {

            foreach (var variable in args)
            {
                Console.WriteLine(/*这里也可以放注释*/variable);
            }

            var name = new makerName::Name();//允许引用别名
            Console.WriteLine(name.GetHashCode());

            var bird = new Fox.Bird("Polly");
            Console.WriteLine(bird.Name);
            //Console.WriteLine(bird.name);
            bird.Fly();

            //TimeOfDay time2 = (TimeOfDay)Enum.Parse(typeof(TimeOfDay), "afternoon", true);
            //Console.WriteLine((int)time2);

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("current value : " + i);
            //}

            ////for (int i = 0; i < 10; i++)
            ////{
            ////    Console.WriteLine("current value : " + i);
            ////}
            //var u = 0;
            //while (u < 10)
            //{

            //}

            //foreach (var m in new int[] { 1, 2, 3, 4 })
            //{

            //}
            //var num = Console.ReadLine();
            //if (num == null) return;
            //const string make = "last";//定义常量
            //const string up = "up";
            //switch (num)
            //{
            //    case "solr":
            //    case up:
            //    case make:
            //    {
            //        Console.WriteLine("Last");
            //        goto case "next";//在使用goto语句的情况下可以忽略break，否则编译器会检测是否缺少break
            //    }
            //    case "next":
            //    {
            //        Console.WriteLine("Next");
            //        break;
            //    }
            //    case "left":
            //    {
            //        Console.WriteLine("Left");
            //        break;
            //    }
            //    case "right":
            //    {
            //        Console.WriteLine("Right");
            //        break;
            //    }
            //}


            //var country = Console.ReadLine();
            //// assume country is of type string
            //const string england = "uk";
            //const string britain = "uk";
            //switch (country)
            //{
            //    case england:
            //    case britain:   // This will cause a compilation error.
            //        language = "English";
            //        break;
            //}

            //string value = "hello";
            //string val = value;
            //string val2 = value;
            //value = "hello world";
            //Console.WriteLine(val +"  " +val2);
            //string atValue = @"c:\users\loe";
            //Console.WriteLine(atValue +"  " +val2);

            //string vame = $"hello new {atValue}";//字符串插入
            //Console.WriteLine(vame);

            //if (vame == "hello")
            //    Console.WriteLine(value);

            //object obj = new object();
            //var a = 0;
            //Console.WriteLine(a);
            //Console.WriteLine("Hello World");
            //byte by = 1;
            //sbyte sby = 2;

            //decimal deci = 3.0M;//特殊后缀

            //float ratio = 4.0f;
            //bool result = false;
            //Console.WriteLine(by + "," + sby + "," + deci + "," + ratio + "," + result);

            //var name = "Bugs Bunny";
            //var age = 25;
            //var isRabbit = true;
            //Type nameType = name.GetType();
            //Type ageType = age.GetType();
            //Type isRabbitType = isRabbit.GetType();
            //Console.WriteLine($"name is type {nameType}");
            //Console.WriteLine($"age is type {ageType}");
            //Console.WriteLine($"isRabbit is type {isRabbitType}");

            //var a = 0;
            //Console.WriteLine(a);
            //Console.WriteLine("Hello World");
            //byte by = 1;
            //sbyte sby = 2;

            //decimal deci = 3.0M;//特殊后缀

            //float ratio = 4.0f;
            //bool result = false;
            //Console.WriteLine(by + "," + sby + "," + deci + "," + ratio + "," + result);

            //var name = "Bugs Bunny";
            //var age = 25;
            //var isRabbit = true;
            //Type nameType = name.GetType();
            //Type ageType = age.GetType();
            //Type isRabbitType = isRabbit.GetType();
            //Console.WriteLine($"name is type {nameType}");
            //Console.WriteLine($"age is type {ageType}");
            //Console.WriteLine($"isRabbit is type {isRabbitType}");

            //int j = 20;
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(Program.j.ToString());
            //}
        }
    }
}