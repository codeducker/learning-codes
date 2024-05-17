using System;


namespace ExtendConsoleApp
{
    interface IInterface
    {
        
    }
    interface IPrivateInterface
    {
        
    }

    class BasePrivateClass
    {
        public virtual int Size { get; } = 20;

        //private required int Top { get; set; }

        //public BasePrivateClass(int size, int top)
        //{
        //    Size = size;
        //    Top = top;
        //}

        public void CallBase()
        {
            Console.WriteLine("CallBase");
        }
        public void AddComponent()
        {
        }
    }

    class SecPrivateClass : BasePrivateClass
    {
        //virtual标识方法为 虚方法 可在继承类中进行重写
        public virtual void CallSec()
        {
            Console.WriteLine("CallSec");
        }

        public void AddComponent()
        {
        }
    }

    //抽象类
    abstract class AbstractPrivateClass
    {
        public abstract int GetSize();
    }

    //类 单继承 但支持多接口  结构体只支持多接口
    //System.Object 为基类
    class PrivateClass : SecPrivateClass, IPrivateInterface,IInterface
    {
        private int Age { get; set; }

        public void ConsoleFork()
        {
            this.Age = 1;
        }

        public void CallBase()
        {
            Console.WriteLine("CallBasePrivate");
        }

        public void CallSec()
        {
            base.CallSec();//调用基类方法
            Console.WriteLine("CallSecPrivate");
        }

        public override int Size { //set;
                                   get; } //重载基类虚属性
    }

    //sealed比较该类无法被继承

    sealed class SealedPrivateClass
    {
        public void CheckIs(object o)
        {
            //判断是否为 基类的派生类 
            if (o is IMyClassInterface)
            {
                var myClassInterface = o as IMyClassInterface;//类型转换关键字
                myClassInterface?.FinalMethod();
            }
        }
    }

    //class ExtendSealedPrivateClass : SealedPrivateClass//此时会抛出异常
    //{
    //}

    interface IMyClassInterface
    {
        public void FinalMethod();

        public virtual void SatGet()
        {
            //todo something
        }
    }


    abstract class AbstractMyClass : IMyClassInterface
    {
        public abstract void FinalMethod();
    }

    class MyClass : AbstractMyClass
    {

        private int size { set; get; }

        public MyClass()
        {
            this.size = 0;
        }

        public MyClass(int outSize)
        {
            this.size = outSize;
        }

        public override void FinalMethod()
        {
            //throw new NotImplementedException();
        }
    }

    /**
     * 访问修饰符
     * public 全部可见
     * protected 派生类型可见
     * internal 包含程序集可见
     * private   所属类型可见
     * protected internal 包含和派生类型可见
     */

    class DerivedClass : MyClass
    {

        protected static int Locked { get; set; }

        public override void FinalMethod()  // wrong. Will give compilation error
        {
        }

        public DerivedClass() : base()
        {
        }

        //继承基类构造函数
        public DerivedClass(int outSize) : base(outSize:outSize)
        {
        }

        protected internal class InnerDerivedClass
        {
            static bool IsLocked()
            {
                return Locked == 1 ;
            }
        }
    }

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var derivedClass = new DerivedClass();
            var innerDerivedClass = new DerivedClass.InnerDerivedClass();
            //SecPrivateClass privateClass = new PrivateClass();
            //privateClass.CallBase();
            //privateClass.CallSec();//子类重载时调用方法为子类 ，修饰符为new / 不加入修饰符 此时调用父类方法 ，当修饰符为 override 时 此时调用子类方法
            //var privateClass2 = new PrivateClass();
            //privateClass2.CallBase();
            //privateClass2.CallSec();//此时调用为子类 重写方法

            //A a;         // 定义一个a这个A类的对象.这个A就是a的申明类
            //A b;         // 定义一个b这个A类的对象.这个A就是b的申明类
            //A c;         // 定义一个c这个A类的对象.这个A就是b的申明类
            //A d;         // 定义一个d这个A类的对象.这个A就是b的申明类
            //a = new A(); // 实例化a对象,A是a的实例类
            //b = new B(); // 实例化b对象,B是b的实例类
            //c = new C(); // 实例化b对象,C是b的实例类
            //d = new D(); // 实例化b对象,D是b的实例类
            //a.Func();    // 执行a.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类A，就为本身 4.执行实例类A中的方法 5.输出结果 Func In A
            //b.Func();    // 执行b.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类B，有重载的 4.执行实例类B中的方法 5.输出结果 Func In B
            //c.Func();    // 执行c.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类C，无重载的 4.转去检查类C的父类B，有重载的 5.执行父类B中的Func方法 5.输出结果 Func In B
            //d.Func();    // 执行d.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类D，无重载的（这个地方要注意了，虽然D里有实现Func()，但没有使用override关键字，所以不会被认为是重载） 4.转 
            ////去检查类D的父类A，就为本身 5.执行父类A中的Func方法 5.输出结果 Func In A
            //D d1 = new D();
            //d1.Func(); // 执行D类里的Func()，输出结果 Func In D
            //Console.ReadLine();
        }
    }
    class A
    {
        public virtual void Func() // 注意virtual,表明这是一个虚拟函数
        {
            Console.WriteLine("Func In A");
        }
    }
    class B : A // 注意B是从A类继承,所以A是父类,B是子类
    {
        public override void Func() // 注意override ,表明重新实现了虚函数
        {
            Console.WriteLine("Func In B");
        }
    }
    class C : B // 注意C是从A类继承,所以B是父类,C是子类
    {
    }
    class D : A // 注意B是从A类继承,所以A是父类,D是子类
    {
        public new void Func() // 注意new ，表明覆盖父类里的同名类，而不是重新实现
        {
            Console.WriteLine("Func In D");
        }
    }
}