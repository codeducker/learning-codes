using System;
using System.Security.Cryptography.X509Certificates;

namespace PlayGroundConsoleApp
{
    //unsafe关键字 标记不安全 可修饰成员变量 、 类  、方法 、 代码块
    unsafe class DisposeClass : IDisposable
    {
        private readonly unsafe int* _dPoint;


        public DisposeClass()
        {
            int x = 188;
            _dPoint = &x ;
        }

        public void Dispose()
        {
            
        }

        unsafe void GetWord()
        {
            Console.WriteLine((uint)_dPoint);
        }
    }

    class DataObject
    {
        //析构函数 C#中默认会生成Finalize , 析构函数 第一次执行方法，不删除 第二次才删除
        ~DataObject()
        {
            
        }

        //protected override void Finalize()
        //{
        //    try
        //    {
        //    }
        //    finally
        //    {
        //        base.Finalize();
        //    }
        //}

        public bool Disposed { get; set; }
    }


    public class ResourceHolder : IDisposable
    {
        private bool _isDisposed = false;
        ~ResourceHolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their
                    // Dispose() methods.
                }
                // Cleanup unmanaged objects
            }
            _isDisposed = true;
        }
 
        public void SomeMethod()
        {
            // Ensure object not already disposed before execution of any method
            if (_isDisposed)
            {
                throw new ObjectDisposedException("ResourceHolder");
            }
            // method implementation…
        }
    }

    class DataClass
    {
        public int X;
        public int Y;
    }

    //结构体指针 不能包含引用类型
    internal struct PointerStruct
    {
        public int X { get; set; }
    }

    unsafe class GetPointer
    {
        public static void WrotePointer()
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 1.5;
            int* pX = &x;
            short* pY = &y;
            double* pZ = &z;
            Console.WriteLine($"Address of x is 0x{(ulong)&x:X}, " +
                              $"size is {sizeof(int)}, value is {x}");
            Console.WriteLine($"Address of y is 0x{(ulong)&y2:X}, " +
                              $"size is {sizeof(short)}, value is {y}");
            Console.WriteLine($"Address of y2 is 0x{(ulong)&y2:X}, " +
                              $"size is {sizeof(byte)}, value is {y2}");
            Console.WriteLine($"Address of z is 0x{(ulong)&z:X}, " +
                              $"size is {sizeof(double)}, value is {z}");
            Console.WriteLine($"Address of pX=&x is 0x{(ulong)&pX:X}, " +
                              $"size is {sizeof(int*)}, value is 0x{(ulong)pX:X}");
            Console.WriteLine($"Address of pY=&y is 0x{(ulong)&pY:X}, " +
                              $"size is {sizeof(short*)}, value is 0x{(ulong)pY:X}");
            Console.WriteLine($"Address of pZ=&z is 0x{(ulong)&pZ:X}, " +
                              $"size is {sizeof(double*)}, value is 0x{(ulong)pZ:X}");
            *pX = 20;
            Console.WriteLine($"After setting *pX, x = {x}");
            Console.WriteLine($"*pX = {*pX}");
            pZ = (double*)pX;
            Console.WriteLine($"x treated as a double = {*pZ}");
        }
    }


    internal struct CurrencyStruct
    {
        public long Dollars;
        public byte Cents;
        public override string ToString() => $"$ {Dollars}.{Cents}";
    }
    internal class CurrencyClass
    {
        public long Dollars = 0;
        public byte Cents = 0;
        public override string ToString() => $"$ {Dollars}.{Cents}";
    }
    //dumpbin /exports C:\Windows\System32\1045\VsGraphicsResources.dll | more 仅在vs Ide中打开命令行可用

    internal class Program
    {
        internal static void Main(string[] args)
        {
            unsafe
            {
                //分配数组栈内存
                int* span = stackalloc int[10];
                *span = 63;
                *(span + 1) = 65;
                span[4] = 67;
                Console.WriteLine($"{*(span+1)}");
                span[12] = 64;
                Console.WriteLine($"{span[12]}");


                //double[] maker = new double[10];
                //maker[12] = 6.3;//此时会显示数组溢出
            }
            //unsafe
            //{
            //    Console.WriteLine($"{sizeof(PointerStruct)}");

            //    Console.WriteLine($"Size of CurrencyStruct struct is {sizeof(CurrencyStruct)}");
            //    CurrencyStruct amount1, amount2;
            //    CurrencyStruct* pAmount = &amount1;
            //    long* pDollars = &(pAmount->Dollars);
            //    byte* pCents = &(pAmount->Cents);
            //    Console.WriteLine(&pAmount == &amount1);
            //    Console.WriteLine($"Address of amount1 is 0x{(ulong)&amount1:X}");
            //    Console.WriteLine($"Address of amount2 is 0x{(ulong)&amount2:X}");
            //    Console.WriteLine($"Address of pAmount is 0x{(ulong)&pAmount:X}");
            //    --pAmount;  // this should get it to point to amount2
            //    Console.WriteLine($"amount2 has address 0x{(ulong)pAmount:X} " +
            //                     $"and contains {*pAmount}");
            //    Console.WriteLine($"Address of pDollars is 0x{(ulong)&pDollars:X}");
            //    Console.WriteLine($"Address of pCents is 0x{(ulong)&pCents:X}");
            //    pAmount->Dollars = 20;
            //    *pCents = 50;
            //    Console.WriteLine($"amount1 contains {amount1}");


            //    Console.WriteLine("\nNow with classes");
            //    // now try it out with classes
            //    var amount3 = new CurrencyClass();
            //    fixed (long* pDollars2 = &(amount3.Dollars))
            //    fixed (byte* pCents2 = &(amount3.Cents))
            //    {
            //        Console.WriteLine($"amount3.Dollars has address 0x{(ulong)pDollars2:X}");
            //        Console.WriteLine($"amount3.Cents has address 0x{(ulong)pCents2:X}");
            //        *pDollars2 = -100;
            //        Console.WriteLine($"amount3 contains {amount3}");
            //    }
            //}

            //GetPointer.WrotePointer();
            ////传递弱引用
            //var weakReference = new WeakReference(new DataObject());
            //if (weakReference.IsAlive)
            //{
            //    if (weakReference.Target is DataObject weakReferenceTarget)
            //    {
            //        var referenceTarget = weakReferenceTarget as DataObject;
            //        Console.WriteLine(referenceTarget.Disposed);
            //    }
            //}

            //DisposeClass? disposeClass = null;
            //try
            //{
            //    disposeClass = new DisposeClass();
            //    //todo 执行业务逻辑
            //}
            //finally
            //{
            //    disposeClass?.Dispose();
            //}

            ////同上述手动调用一致
            //using (var instance = new DisposeClass())
            //{
            //    //todo 执行业务逻辑
            //}

            //unsafe
            //{
            //    int x = 10;
            //    int* px, py;
            //    px = &x;
            //    py = px;
            //    *py = 20;
            //    ulong y = (ulong)px;
            //    int* pd = (int*)y;
            //    //Console.WriteLine($"{y} , {*pd}");
            //    //Console.WriteLine($"{++y} , {*pd}");
            //    Console.WriteLine(*px);
            //    ++px;
            //    Console.WriteLine(*px);
            //}

            //Console.WriteLine(sizeof(double));
            //unsafe
            //{
            //    var pointerStruct = new PointerStruct();
            //    PointerStruct* pointer = &pointerStruct;
            //    (*pointer).X = 6;
            //    pointer->X = 20;
            //    Console.WriteLine(pointer->X);
            //}

            //unsafe
            //{
            //    var dataClass = new DataClass();
            //    int x = dataClass.X;
            //    uint* u = (uint*) & x;
            //    Console.WriteLine(*u);
            //    //多个fixed 仅在fixed执行域中起作用
            //    fixed (int* pObject = (&dataClass.X))
            //    fixed (int* qObject = (&dataClass.Y))
            //    {
            //        //todo 执行逻辑
            //        Console.WriteLine(*pObject);
            //    }
            //}
        }
    }
}

