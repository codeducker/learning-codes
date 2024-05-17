using System;
using System.Collections;
using Microsoft.VisualBasic.CompilerServices;


namespace ElementOperateConsoleApp
{

    internal class Program
    {
        internal static void Main(string[] args)
        {

            var vector = new Vector(1.0,2.3,4.0);
            Console.WriteLine(vector);
            float balance = vector;
            Console.WriteLine($"convert balance : {balance}");
            Vector otherVec = (Vector)balance;
            Console.WriteLine($"Convert Vector :{otherVec} ");

            long lBanace = (long)vector;//此时默认先将vector转成float然后float再转换成long
            Console.WriteLine(lBanace);




            //var vector = new Vector(1.0,2.3,4.0);
            //var f = (float)vector;
            //Console.WriteLine(f);

            var check = 2;
            short bak = (short)check;
            Console.WriteLine(bak);


            var p1 = new Person(12, "Senna", new DateTime(1960, 3, 21));
            var p2 = new Person(43, "Peterson", new DateTime(1944, 2, 14));
            var p3 = new Person(25, "RinZ", new DateTime(1942, 4, 18));
            var p4 = new Person(78, "CeoVt", new DateTime(1944, 2, 25));
            var coll = new PersonCollection(p1, p2, p3, p4);

            Console.WriteLine(coll[2]);//自定义索引运算符

            foreach (var r in coll[new DateTime(1960, 3, 21)])
            {
                Console.WriteLine(r);
            }

            Vector v1, v2, v3;
            v1 = new Vector(3.0, 3.0, 1.0);
            v2 = new Vector(2.0, -4.0, -4.0);
            v3 = v1 + v2;
            Console.WriteLine($"V1 = {v1}");
            Console.WriteLine($"V2 = {v2}");
            Console.WriteLine($"V3 = {v3}");

            //System.ValueType 重写 Equal方法 用于值类型相等比较
            Person co = new Person(1, "name");
            Person cd = co;
            Console.WriteLine(ReferenceEquals(co,cd));
            Console.WriteLine(Equals(co, cd));
            int? a = null;
            int  b = a ?? 10; //值为空，默认值
            Console.WriteLine(b);


            //unsafe unsafe只能是在unsafe上下文中使用
            //{
            //    Console.WriteLine(sizeof(int));
            //}

            long val = 3000000000;
            int ios = unchecked((int)val);
            Console.WriteLine(ios);

            Console.WriteLine(nameof(PrintValues));//可以传入符号，属性，方法

            byte vb = Byte.MaxValue;
            unchecked
            {
                vb++;
            }
            Console.WriteLine(vb);

            //IStructuralEquatable
            var musicTitles = new MusicTitles(
                new string[] { "Tubular Bells", "Hero Ridge", "War Jia", "Platinum" });
            //foreach(var musicTitle in  musicTitles){
            //    Console.WriteLine(musicTitle);
            //}

            var enumerable = musicTitles.Reverse();

            //foreach (var musicTitle in enumerable)
            //{
            //    Console.WriteLine(musicTitle);
            //}

            var subset = musicTitles.Subset(0,1);

            foreach (var musicTitle in subset)
            {
                Console.WriteLine(musicTitle);
            }

            foreach (int i in ProduceEvenNumbers(9))
            {
                Console.Write(i);
                Console.Write(" ");
            }
            // Output: 0 2 4 6 8

            IEnumerable<int> ProduceEvenNumbers(int upto)
            {
                for (int i = 0; i <= upto; i += 2)
                {
                    yield return i;
                }
            }

            Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 2, 3, 4, 5, -1, 3, 4 })));
            // Output: 2 3 4 5

            Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 9, 8, 7 })));
            // Output: 9 8 7

            IEnumerable<int> TakeWhilePositive(IEnumerable<int> numbers)
            {
                foreach (int n in numbers)
                {
                    if (n > 0)
                    {
                        yield return n;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }


            int[] ar1 = { 1, 4, 5, 11, 13, 18 };
            int[] ar2 = { 3, 4, 5, 18, 21, 27, 33 };
            var segments = new ArraySegment<int>[2]
            {
                new ArraySegment<int>(ar1, 0, 3),
                new ArraySegment<int>(ar2, 3, 3)
            };
            var sum = SumOfSegments(segments);

            Person[] persons = new Person[4];
            persons[0] = new Person(age: 4, name: "andy1");
            persons[1] = new Person(age: 5, name: "andy2");
            persons[2] = new Person(age: 2, name: "andy3");
            persons[3] = new Person(age: 7, name: "andy4");

            var enumerator = persons.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var enumeratorCurrent = enumerator.Current;
                Console.WriteLine(enumeratorCurrent.ToString());
            }

            //数组作为参数可以支持协变，但是只能是引用类型，值类型数组发生报错
            CheckGet(persons);

            Array.Sort(persons, new PersonComparer(PersonCompareType.Age));
            foreach (var person in persons)
            {
                if (person != null)
                {
                    Console.WriteLine($"age: {person.Age} , name: {person.Name}");
                }
            }

            Stu[] students = new Stu[4]
            {
                new Stu("andy", 44),
                new Stu("lucy", 23),
                new Stu("candy", 65),
                new Stu("mark", 12)
            };

            Array.Sort(students);
            foreach (var student in students)
            {
                Console.Write(Convert.ToString(student.Name)+" ");
            }
            Console.WriteLine("");

            int[] unsortedArr = new int[] { 82, 341, 42, 132, 432, 45 };

            //自定义类 需实现IComparable接口且实现compareTo方法
            Array.Sort(unsortedArr);
            for (int i = 0 ; i < unsortedArr.Length;i++)
            {
                Console.Write(" " + Convert.ToString(unsortedArr[i]));
            }
            Console.WriteLine("");

            int[] intArray1 = { 1, 2 };
            int[] intArray2 = (int[])intArray1.Clone();
            Console.WriteLine(intArray2[0]);
            Console.WriteLine(intArray1[0]);
            intArray1[0] = 4;
            Console.WriteLine(intArray2[0]);
            Console.WriteLine(intArray1[0]);

            var personArr = Array.CreateInstance(typeof(Person), new int[] { 2, 3 }, new int[] { 3, 4 });
            for (int i = personArr.GetLowerBound(0); i <= personArr.GetUpperBound(0); i++)
            {
                for (int j = personArr.GetLowerBound(1); j <= personArr.GetUpperBound(1); j++)
                {
                    personArr.SetValue(new Person(i, "name:"+Convert.ToString( j)), new int[] { i, j });   
                }
            }

            PrintValues(personArr);

            Person[,] standardPersonArr = (Person[,])personArr;
            Console.WriteLine(standardPersonArr[3,4]);//此时这里从下表3 开始 长度为2  从4 开始长度为3


            int[] singleWei = new int[] { 1, 2 };
            int[] twoWei = new int[] { 2, 3 };
            var myArray = Array.CreateInstance(typeof(string), singleWei, twoWei);
            for (int i = myArray.GetLowerBound(0); i <= myArray.GetUpperBound(0); i++)
            {
                for (int j = myArray.GetLowerBound(1); j <= myArray.GetUpperBound(1); j++)
                {
                    int[] myIndicesArray = new int[2] { i, j };
                    myArray.SetValue(Convert.ToString(i) + j, myIndicesArray);
                }
            }
            PrintValues(myArray);


            var twoGenInstance = Array.CreateInstance(typeof(string),2,3);
            for (int i = twoGenInstance.GetLowerBound(0); i <= twoGenInstance.GetUpperBound(0); i++)
            {
                for (int j = twoGenInstance.GetLowerBound(1); j <= twoGenInstance.GetUpperBound(1); j++)
                {
                    twoGenInstance.SetValue(i+5 + "---" + j+5, new int[] { i, j });
                }
            }
            //PrintValues(twoGenInstance);

            int[] singleLen = new int[] { 2, 3 };
            var twoGenArr = Array.CreateInstance(typeof(string),singleLen);
            for (int i = twoGenArr.GetLowerBound(0); i <= twoGenArr.GetUpperBound(0); i++)
            {
                for (int j = twoGenArr.GetLowerBound(1); j <= twoGenArr.GetUpperBound(1); j++)
                {
                    twoGenArr.SetValue(i+"---"+j,new int[]{i,j});
                }
            }

            //PrintValues(twoGenArr);

            //创建数组
            Int32[] instance = (Int32[])Array.CreateInstance(typeof(Int32),6);
            for (int i = instance.GetLowerBound(0) ; i <= instance.GetUpperBound(0); i++)
            {
                instance.SetValue((int)Math.Pow(i,3),i);
            }
            for (int i = 0; i < instance.Length; i++)
            {
                Console.WriteLine(instance.GetValue(i));
            }

            //PrintValues(instance);

            //多维数组
            int[,] mutiArray = new int[3,4];
            Console.WriteLine(mutiArray[2,3]);

            //锯齿数组
            int[][] gArray = new int[3][];
            gArray[0] = new int[2] { 1, 2 };
            gArray[1] = new int[3];//需要先初始化
            gArray[1][0] = 2;
            for (var i = 0; i < gArray.Length; i++)
            {
                if (gArray[i] != null)
                {
                    for (int j = 0; j < gArray[i].Length; j++)
                    {
                        Console.WriteLine(gArray[i][j]);
                    }
                }
            }

            int[] integers = new int[3]{1,23,4};
            Console.WriteLine(integers);

            string[] targets = { "her", "is", "body" };
            for (int i = targets.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(targets[i]);
            }

            //数组引用类型
            object[] array = new object[10];
            array[0] = 1;
            array[1] = "hello";
            foreach (var o in array)
            {
                Console.WriteLine(o);
            }
        }

        static int SumOfSegments(ArraySegment<int>[]? segments)
        {
            int sum = 0;
            foreach (var segment in segments)
            {
                for (int i = segment.Offset; i < segment.Offset + segment.Count; i++)
                {
                    sum += segment.Array[i];
                }
            }
            return sum;
        }

        public static void PrintValues(Array myArr)
        {
            System.Collections.IEnumerator myEnumerator = myArr.GetEnumerator();
            int i = 0;
            int cols = myArr.GetLength(myArr.Rank - 1);
            while (myEnumerator.MoveNext())
            {
                if (i < cols)
                {
                    i++;
                }
                else
                {
                    Console.WriteLine();
                    i = 1;
                }
                Console.Write("\t{0}", myEnumerator.Current);
            }
            Console.WriteLine();
        }

        public static void CheckGet(object[] array)
        {
            PrintValues(array);
        }
    }

    public enum PersonCompareType
    {
        Name,
        Age
    }

    public class MusicTitles
    {
        string[] Names;

        public MusicTitles(string[] names)
        {
            Names = names;
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return Names[i];
            }
        }
        public IEnumerable<string> Reverse()
        {
            for (int i = 3; i >= 0; i --)
            {
                yield return Names[i];
            }
        }
        public IEnumerable<string> Subset(int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                yield return Names[i];
            }
        }
    }

    class Stu : IComparable<Stu>
    {
        public string Name;

        public int Age;

        public int CompareTo(Stu? other)
        {
            return this.Age.CompareTo(other?.Age);
        }

        public Stu(string
            name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    //实现自定义索引运算符
    class PersonCollection
    {
        private Person[] _persons;

        public PersonCollection(params Person[] pers)
        {
            _persons = pers.ToArray();
        }

        public Person this[int index] //这里不仅可以为int 也可以是其他类型
        {
            get => _persons[index];
            set => _persons[index] = value;
        }
        
        public IEnumerable<Person> this[DateTime birthday]
        {
            get => _persons.Where(p => p.Birthday == birthday);
        }
    }


    class Person : IEquatable<Person>
    {
        public int Id { get; private set; }

        public int Age { get; }

        public string Name { get; }

        public DateTime Birthday { get; set; }

        public Person(int age, string name)
        {
            this.Age = age;
            this.Name = name;
        }

        public Person(int age, string name, DateTime birthday)
        {
            this.Age = age;
            this.Name = name;
            this.Birthday = birthday;
        }

        public override string ToString() => $"{Id}, {Name} {Age}";

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return base.Equals(obj);
            }
            return Equals(obj as Person);
        }

        public override int GetHashCode() => Id.GetHashCode();


        public bool Equals(Person? other)
        {
            if (other == null)
                return base.Equals(other);
            return Id == other.Id && Name == other.Name &&
                   Age == other.Age;
        }
    }

    class PersonComparer : IComparer<Person>
    {
        private readonly PersonCompareType _compareType;
        public PersonComparer(PersonCompareType compareType)
        {
            _compareType = compareType;
        }
        public int Compare(Person? x, Person? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            switch (_compareType)
            {
                case PersonCompareType.Name:
                    return String.CompareOrdinal(x.Name, y.Name);
                case PersonCompareType.Age:
                    return x.Age - y.Age;
                default:
                    throw new ArgumentException("unexpected compare type");
            }
        }
    }

    struct Vector
    {
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public override string ToString() => $"( {X}, {Y}, {Z} )";
        
        //操作符重载 
        public static Vector operator +(Vector left, Vector right) =>
            new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector operator *(double left, Vector right) =>
            new Vector(left * right.X, left * right.Y, left * right.Z);

        //比较运算符 == 必须 配合 != 一同申明
        public static bool operator ==(Vector left, Vector right)
        {
            if (object.ReferenceEquals(left, right)) return true;
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(Vector left, Vector right) => !(left == right);

        public override bool Equals(object other)
        {
            return (other as Vector?) == this;
        }
        //自定义类型强制转换
        public static implicit operator float(Vector v)
        {
            return (float)(v.X+v.Y);
        }
        
        public static explicit operator Vector(float value)
        {
            uint dollars = (uint)value;
            ushort cents = (ushort)((value - dollars) * 100);
            return new Vector(dollars, cents,0.0);
        }
    }
}