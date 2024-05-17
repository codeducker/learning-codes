using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GenericConsoleApp
{

    public interface IDocument
    {
        public virtual string ToString()
        {
            return "";
        }
    }

    class Document : IDocument
    {
        public string Title { set; get; }

        public string Content { set; get; }

        public Document()
        {

        }

        public Document(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        public override string ToString()
        {
            return $"{Title} ===== {Content}";
        }
    }

    //这里可以是多维度类型 class /struct /IInterface / class基类 / new() 含有默认构造函数 / 泛型
    public class DocumentManager<T> where T : IDocument,new() //参照Java中 class<T extends B> / class<T implement D> 做限定约束
    {
        private readonly Queue<T> documentQueue = new Queue<T>();

        public void AddDocument(T obj)
        {
            lock (this)
            {
                documentQueue.Enqueue(obj);
            }
        }

        public bool IsDocumentAvailable => documentQueue.Count > 0;

        public T GetDocument()
        {
            T doc = default(T);
            lock (this)
            {
                doc = documentQueue.Dequeue();
            }
            return doc;
        }

        public void Display()
        {
            foreach (var document in documentQueue)
            {
                Console.WriteLine(document);
                Console.WriteLine(((IDocument)document).ToString());
            }
        }
    }

    public class Base<T>
    {
    }
    public class Derived<T> : Base<string>
    {
    }

    public abstract class Calc<T>
    {
        public abstract T Add(T x, T y);
        public abstract T Sub(T x, T y);
    }
    public class IntCalc : Calc<int>
    {
        public override int Add(int x, int y) => x + y;
        public override int Sub(int x, int y) => x - y;
    }


    public class StaticDemo<T>
    {
        public static int X;
    }

    public interface IIndex<out T>
    {
        T this[int index] { get; }
        int Count { get; }
    }


    public interface ICustomCovariant<out T>
    {
        T Get();
    }
    public class CustomCovariant<T> : ICustomCovariant<T>
    {
        public T Get()
        {
            return default(T);
        }
    }


    public interface IContravariant<in T>
    {
        void Get(T t);
    }
    public class CustomContravariant<T> : IContravariant<T>
    {
        public void Get(T t)
        {

        }
    }

    public class MethodOverloads
    {
        public void Foo<T>(T obj)
        {
            Console.WriteLine($"Foo<T>(T obj), obj type: {obj.GetType().Name}");
        }
        public void Foo(int x)
        {
            Console.WriteLine("Foo(int x)");
        }
        public void Foo<T1, T2>(T1 obj1, T2 obj2)
        {
            Console.WriteLine($"Foo<T1, T2>(T1 obj1, T2 obj2); {obj1.GetType().Name} " +
                              $"{obj2.GetType().Name}");
        }
        public void Foo<T>(int obj1, T obj2)
        {
            Console.WriteLine($"Foo<T>(int obj1, T obj2); {obj2.GetType().Name}");
        }
        public void Bar<T>(T obj)
        {
            Foo(obj);
        }
    }

    internal class Program
    {
        internal static void Main(string[] args)
        {

            var test = new MethodOverloads();
            test.Foo(33);
            test.Foo("abc");
            test.Foo("abc", 42);
            test.Foo(33, "abc");

            StaticDemo<string>.X = 4;
            StaticDemo<int>.X = 5;
            Console.WriteLine(StaticDemo<int>.X);   // writes 4


            //协变 / 逆变 
            object o = "str";
            //List<object> oList = new List<string>();
            IEnumerable<object> strs = new List<string>();
            Action<string> action = new Action<object>((o) => { });



            ICustomCovariant<object> oc = new CustomCovariant<string>();

            IContravariant<string> ob = new CustomContravariant<object>();

            //var documentManager = new DocumentManager<Document>();
            //documentManager.AddDocument(new Document("C++学习","这样也行"));
            ////documentManager.AddDocument(default);
            //var document = documentManager.GetDocument();
            //Console.WriteLine(document);

            //documentManager.Display();


            //var dstring = default(string);
            //var dint = default(int);
            //var dintNull = default(int?);
            //var d = default(dynamic);
            //var dt = default(DateTime);
            //var dt1 = default(DateTime?);
            //Console.WriteLine(dstring);//,dint,dintNull, d,dt,dt1);

            //var arrayList = new List<int>();
            //arrayList.Add(44);
            //Console.WriteLine(arrayList);

            //var list = new LinkedList<object>();
            //list.AddLast(new LinkedListNode<object>(12));
            //Console.WriteLine(list);
        }
    }
}
