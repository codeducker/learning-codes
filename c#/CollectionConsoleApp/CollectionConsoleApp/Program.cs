using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace CollectionConsoleApp
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            PipelineStages.StartPipelineAsync();

            var concurrentQueue = new ConcurrentQueue<string>();

            ImmutableArray<string> a1 = ImmutableArray.Create<string>();
            var immutableArray = a1.Add("a");
            Console.WriteLine(immutableArray == a1);


            var bitVector32 = new BitVector32();
            var mask = BitVector32.CreateMask();
            var mask1 = BitVector32.CreateMask(mask);
            var mask2 = BitVector32.CreateMask(mask1);
            var mask3 = BitVector32.CreateMask(mask2);
            bitVector32[mask] = true;
            bitVector32[mask1] = true;
            bitVector32[mask2] = false;
            bitVector32[mask3] = true;
            Console.WriteLine(bitVector32.ToString());


            var bits1 = new BitArray(8);
            bits1.SetAll(true);
            bits1.Set(1, false);
            bits1[5] = false;
            bits1[7] = false;
            Console.Write("initialized: ");
            DisplayBits(bits1);
            Console.WriteLine();

            var companyTeams = new HashSet<string>()
                { "Ferrari", "McLaren", "Mercedes" };
            var privateTeams = new HashSet<string>()
                { "Red Bull", "Toro Rosso", "Force India", "Sauber" };
            if (privateTeams.Add("Williams"))
            {
                Console.WriteLine("Williams added");
            }
            if (!companyTeams.Add("McLaren"))
            {
                Console.WriteLine("McLaren was already in this set");
            }

            var traditionalTeams = new HashSet<string>() { "Ferrari", "McLaren" };
            Console.WriteLine(traditionalTeams.IsSubsetOf(companyTeams));



            //SortedList / SortedDictionary 区别 
            var sortedDictionary = new SortedDictionary<string, string>();
            Console.WriteLine(sortedDictionary);


            var idTony = new EmployeeId("C3755");
            var tony = new Employee(idTony, "Tony Stewart", 379025.00m);
            var idCarl = new EmployeeId("F3547");
            var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);
            var idKevin = new EmployeeId("C3386");
            var kevin = new Employee(idKevin, "Kevin Harwick", 415261.00m);
            var idMatt = new EmployeeId("F3323");
            var matt = new Employee(idMatt, "Matt Kenseth", 1589390.00m);
            var idBrad = new EmployeeId("D3234");
            var brad = new Employee(idBrad, "Brad Keselowski", 322295.00m);
            var employees = new Dictionary<EmployeeId, Employee>(31)
            {
                [idTony] = tony,
                [idCarl] = carl,
                [idKevin] = kevin,
                [idMatt] = matt,
                [idBrad] = brad
            };
            foreach (var employee in employees.Values)
            {
                Console.WriteLine(employee);
            }

            var lookup = employees.ToLookup((employee)=> employee.Value.Salary > 1000);
            Console.WriteLine(lookup);

            //散列表(
            //键值类GetHashCode用于计算索引位置(该方法要求
            //  不同实例可以返回相同值 ，
            //  相同实例返回相同值 ，
            //  不能抛出异常 至少使用一个实例字段，
            //  散列代码在生成周期不发生改变
            // 计算快，内存开销不大
            // 散列值平均分配在int整数范围上)
            //此外还需要重载 键值类的Equals方法
            //)
            var dictionary = new Dictionary<string, string>()
            {
                ["andy"] = "Name throwing!"
            };

            var dictionarySelf = new Dictionary<Document,string> { { new Document("Less" , "More Less"), "Just Locked!" } };


            //有序列表
            var books = new SortedList<string, string>
            {
                { "Professional WPF Programming", "978-0-470-04180-2" },
                { "Professional ASP.NET MVC 5", "978-1-118-79475-3" }
            };
            books["Beginning Visual C# 2012"] = "978-1-118-31441-8";
            books["Professional C# 5 and .NET 4.5.1"] = "978-1-118-83303-2";

            foreach (var book in books)
            {
                Console.WriteLine(book.Key,"   ", book.Value);
            }

            var linkedList = new LinkedList<Document>();
            var linkedListNode = new LinkedListNode<Document>(new Document("Title1", "Content1"));
            linkedList.AddFirst(linkedListNode);
            for (int i = 2; i <=10; i++)
            {
                var newLinkedListNode = new LinkedListNode<Document>(new Document("Title" + i, "Content" + i));
                linkedList.AddAfter(linkedListNode, newLinkedListNode);
                linkedListNode = newLinkedListNode;
            }

            foreach (var document in linkedList)
            {
                Console.WriteLine(document.ToString());
            }

    
            var chars = new Stack<char>();
            chars.Push('A');
            chars.Push('B');
            chars.Push('C');
            //foreach (var c in chars)
            //{
            //    Console.WriteLine(c);
            //}

            while (chars.Count > 0)
            {
                var pop = chars.Pop();
                Console.WriteLine(pop);
            }

            //队列先进先出
            var dm = new DocumentManager();
            ProcessDocuments.Start(dm);
            // Create documents and add them to the DocumentManager
            for (int i = 0; i < 20; i++)
            {
                var doc = new Document($"Doc {i.ToString()}", "content");
                dm.AddDocument(doc);
                Console.WriteLine($"Added document {doc.Title}");
                Thread.Sleep(new Random().Next(20));
            }

            var racers = new List<Racer>(2)
            {
                new Racer(1,"firstName1","lastName1","中国",0)
            };
            racers.Capacity = 20;
            Console.WriteLine(racers.Count);

            racers.Add(new Racer(2,"firstName2","lastName2","西班牙",1));
            racers.Add(new Racer(3,"firstName3","lastName3","法国",1));

            //这里参数为IEnumerable 可以传入数组或者IEnumerable的基类
            racers.AddRange(new Racer[]
            {
                new Racer(4,"firstName4","lastName4","西班牙",5),
                new Racer(5, "firstName5", "lastName5", "法国", 4)
            });

            //Lookup<TKey,TElement> 此时element可以为Collections
            var lookupRacers = racers.ToLookup(r => r.Country);
            foreach (Racer r in lookupRacers["Australia"])
            {
                Console.WriteLine(r);
            }

            //按照指定位置插入元素
            racers.Insert(0,new Racer(0,"firstName0","lastName0","未知",0));
            Console.WriteLine(racers.Sum(racer=>racer.Wins));

            //访问元素 索引 或者 使用foreach迭代
            for (int i = 0; i < racers.Count; i++)
            {
                Console.WriteLine(racers[i].ToString());  
            }

            //移除指定位置元素
            //racers.RemoveAt(0);

            //获取元素位置，默认根据IEquatable查询，若不存在则使用基类equals方法判断
            Console.WriteLine(racers.IndexOf(new Racer(0,"firstName0","lastName0","未知",1)));

            Console.WriteLine(racers.FindIndex(racer=>racer.FirstName == "firstName1"));

            //默认按照IComparable方法进行排序
            racers.Sort();
            racers.ForEach((racer) => Console.Write(racer.ToString()+"\t"));
            Console.WriteLine("\n");

            //自定义排序规则
            racers.Sort((racer1,racer2)=>racer1.Wins - racer2.Wins);

            racers.ForEach((racer) => Console.WriteLine(racer.ToString()));

            var ints = new List<int>();
            ints.Add(1);
            Console.WriteLine(ints);
        }

        public static void DisplayBits(BitArray bits)
        {
            foreach (bool bit in bits)
            {
                Console.Write(bit ? 1 : 0);
            }
        }

   
    }

    public static class PipelineStages
    {
        public static Task ReadFileNamesAsync(string path,
            BlockingCollection<string> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (string filename in Directory.EnumerateFiles(path, "*.cs",
                             SearchOption.AllDirectories))
                {
                    output.Add(filename);
                    ColoredConsole.WriteLine($"stage 1: added {filename}");
                }

                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static async Task StartPipelineAsync()
        {
            var fileNames = new BlockingCollection<string>();
            var lines = new BlockingCollection<string>();
            var words = new ConcurrentDictionary<string, int>();
            var items = new BlockingCollection<Info>();
            var coloredItems = new BlockingCollection<Info>();
            Task t1 = PipelineStages.ReadFileNamesAsync(@"../../..", fileNames);
            ColoredConsole.WriteLine("started stage 1");
            Task t2 = PipelineStages.LoadContentAsync(fileNames, lines);
            ColoredConsole.WriteLine("started stage 2");
            Task t3 = PipelineStages.ProcessContentAsync(lines, words);
            await Task.WhenAll(t1, t2, t3);
            ColoredConsole.WriteLine("stages 1, 2, 3 completed");
            Task t4 = PipelineStages.TransferContentAsync(words, items);
            Task t5 = PipelineStages.AddColorAsync(items, coloredItems);
            Task t6 = PipelineStages.ShowContentAsync(coloredItems);
            ColoredConsole.WriteLine("stages 4, 5, 6 started");
            await Task.WhenAll(t4, t5, t6);
            ColoredConsole.WriteLine("all stages finished");
        }

        public static Task ShowContentAsync(BlockingCollection<Info> input)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    ColoredConsole.WriteLine($"stage 6: {item}", item.Color);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public static Task AddColorAsync(BlockingCollection<Info> input,
            BlockingCollection<Info> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    if (item.Count > 40)
                    {
                        item.Color = "Red";
                    }
                    else if (item.Count > 20)
                    {
                        item.Color = "Yellow";
                    }
                    else
                    {
                        item.Color = "Green";
                    }
                    output.Add(item);
                    ColoredConsole.WriteLine($"stage 5: added color {item.Color} to {item}");
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static Task TransferContentAsync(
            ConcurrentDictionary<string, int> input,
            BlockingCollection<Info> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var word in input.Keys)
                {
                    int value;
                    if (input.TryGetValue(word, out value))
                    {
                        var info = new Info { Word = word, Count = value };
                        output.Add(info);
                        ColoredConsole.WriteLine($"stage 4: added {info}");
                    }
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static async Task LoadContentAsync(BlockingCollection<string> input,
            BlockingCollection<string> output)
        {
            foreach (var filename in input.GetConsumingEnumerable())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    var reader = new StreamReader(stream);
                    string? line;
                    while (( line = await reader.ReadLineAsync())!= null)
                    {
                        output.Add(line);
                        ColoredConsole.WriteLine($"stage 2: added {line}");
                    }
                }
            }
            output.CompleteAdding();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input,
            ConcurrentDictionary<string, int> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(' ', ';', '\t', '{', '}', '(', ')', ':',
                        ',', '"');
                    foreach (var word in words.Where(w => !string.IsNullOrEmpty(w)))
                    {
                        output.AddOrUpdate(key: word, addValue: 1,
                            updateValueFactory: (s, i) => ++i);
                        ColoredConsole.WriteLine($"stage 3: added {word}");
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }

    public static class ColoredConsole
    {
        private static object syncOutput = new object();

        public static void WriteLine(string message)
        {
            lock (syncOutput)
            {
                Console.WriteLine(message);
            }
        }
        public static void WriteLine(string message, string color)
        {
            lock (syncOutput)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(
                    typeof(ConsoleColor), color);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }

    public class Info
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public override string ToString() => $"{Count} times: {Word}";
    }


    //定义异常类
    public class EmployeeIdException : Exception
    {
        public EmployeeIdException(string message) : base(message) { }
    }

    public class Employee
    {
        private string _name;
        private decimal _salary;
        private readonly EmployeeId _id;
        public Employee(EmployeeId id, string name, decimal salary)
        {
            _id = id;
            _name = name;
            _salary = salary;
        }
        public override string ToString() => $"{_id.ToString()}: {_name,-20} {_salary:C}";

        public decimal Salary => _salary;
    }

    public readonly struct EmployeeId : IEquatable<EmployeeId>
    {
        private readonly char _prefix;
        private readonly int _number;
        public EmployeeId(string id)
        {
            //Contract.Requires<ArgumentNullException>(id!!= null);
            _prefix = (id.ToUpper())[0];
            int numLength = id.Length - 1;
            try
            {
                _number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
            }
            catch (FormatException)
            {
                throw new EmployeeIdException("Invalid EmployeeId format");
            }
        }
        public override string ToString() => _prefix.ToString() + $"{_number,6:000000}";

        public override int GetHashCode() => (_number ^ _number << 16) * 0x15051505;


        public bool Equals(EmployeeId other) => _prefix == other._prefix && _number == other._number;

        public override bool Equals(object? obj) => Equals((EmployeeId)(obj ?? new EmployeeId()));

        public static bool operator ==(EmployeeId left, EmployeeId right) =>
            left.Equals(right);

        public static bool operator !=(EmployeeId left, EmployeeId right) =>!(left == right);
    }


    public class PriorityDocumentManager
    {
        private readonly LinkedList<Document> _documentList;

        // priorities 0.9
        private readonly List<LinkedListNode<Document>> _priorityNodes;

        public PriorityDocumentManager()
        {
            _documentList = new LinkedList<Document>();
            _priorityNodes = new List<LinkedListNode<Document>>(10);
            //占用链表节点
            for (int i = 0; i < 10; i++)
            {
                _priorityNodes.Add(new LinkedListNode<Document>(null));
            }
        }

        public void AddDocument(Document? d)
        {
            if (d == null) throw new ArgumentException("Input Null Document");
            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document doc, int priority)
        {
            if (priority is > 9 or < 0)
            {
                throw new ArgumentException("Priority must be between 0 and 9");
            }

            if (_priorityNodes[priority] == null)
            {
                throw new ArgumentException("Linked Nodes not initial");
            }

            var document = _priorityNodes[priority].Value;
            if ( document == null)
            {
                --priority;
                if (priority <= 0)
                {
                    AddDocumentToPriorityNode(doc, priority);
                }
                else 
                {
                    _documentList.AddLast(doc);
                    _priorityNodes[doc.Priority] = _documentList.Last;
                }
                return;
            }
            else 
            {
                LinkedListNode<Document> priorityNode = _priorityNodes[priority];
                if (priority == doc.Priority)
                {
                    _documentList.AddAfter(priorityNode, doc);
                    _priorityNodes[doc.Priority] = priorityNode.Next;
                }
                else 
                {
                    LinkedListNode<Document> firstPriorityNode = priorityNode;
                    while (priorityNode != null &&
                           firstPriorityNode?.Previous != null &&
                           firstPriorityNode.Previous.Value.Priority == priorityNode.Value.Priority)
                    {
                        if (priorityNode.Previous != null) firstPriorityNode = priorityNode.Previous;
                        priorityNode = firstPriorityNode;
                    }

                    if (firstPriorityNode != null)
                    {
                        _documentList.AddBefore(firstPriorityNode, doc);
                        _priorityNodes[doc.Priority] = firstPriorityNode.Previous;
                    }
                }
            }
        }
    }

    public class ProcessDocuments
    {
        private readonly DocumentManager _documentManager;

        public static void Start(DocumentManager dm)
        {
            Task.Run(new ProcessDocuments(dm).Run);
        }
        protected ProcessDocuments(DocumentManager dm)
        {
            _documentManager = dm ?? throw new ArgumentNullException(nameof(dm));
        }
    
        protected async Task Run()
        {
            while (true)
            {
                if (_documentManager.IsDocumentAvailable)
                {
                    Document doc = _documentManager.GetDocument();
                    Console.WriteLine("Processing document {0}", doc.Title);
                }
                await Task.Delay(new Random().Next(20));
            }
        }
    }

    public class DocumentManager
    {
        private readonly Queue<Document> _documentQueue = new Queue<Document>();

        public void AddDocument(Document doc)
        {
            lock (this)
            {
                _documentQueue.Enqueue(doc);
            }
        }
        public Document GetDocument()
        {
            Document doc;
            lock (this)
            {
                doc = _documentQueue.Dequeue();
            }
            return doc;
        }
        public bool IsDocumentAvailable => _documentQueue.Count > 0;
    }

    public class Document
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        public int Priority { get; private set; }

        //构造方法重载
        public Document(string title, string content) :this(title, content, 0)
        {
           
        }

        public Document(string title, string content, int priority)
        {
            Title = title;
            Content = content;
            Priority = priority;
        }
        public override string ToString() => $"{Title} {Content} --- {Priority}";

    }

    public class Racer : IComparable<Racer>, IFormattable,IEquatable<Racer>
    {
        public int Id { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public int Wins { get; set; }

        public Racer(int id, string firstName, string lastName, string country)
          : this(id, firstName, lastName, country, wins: 0)
        { }

        public Racer(int id, string firstName, string lastName, string country,
                      int wins)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Wins = wins;
        }

        public bool Equals(Racer? other)
        {
            return CompareTo(other) == 0;
        }

        public override string ToString() => $"{FirstName} {LastName}";

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            format ??= "N";
            switch (format.ToUpper())
            {
                case "N": // name
                    return ToString();
                case "F": // first name
                    return FirstName;
                case "L": // last name
                    return LastName;
                case "W": // Wins
                    return $"{ToString()}, Wins: {Wins}";
                case "C": // Country
                    return $"{ToString()}, Country: {Country}";
                case "A": // All
                    return $"{ToString()}, Country: {Country} Wins: {Wins}";
                default:
                    throw new FormatException(String.Format(formatProvider,
                                    $"Format {format} is not supported"));
            }
        }
        public string ToString(string format) => ToString(format, null);

        public int CompareTo(Racer? other)
        {
            int compare = LastName?.CompareTo(other?.LastName) ?? -1;
            if (compare == 0)
            {
                return FirstName?.CompareTo(other?.FirstName) ?? -1;
            }
            return compare;
        }

    }
}