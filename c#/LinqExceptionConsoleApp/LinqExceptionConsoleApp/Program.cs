using System.Collections;
using System.Collections.Concurrent;
using static System.String;

namespace LinqExceptionConsoleApp
{

    public class ColdOpenClass : IDisposable
    {
        private static bool _isDisposed;

        private static FileStream _fs;

        private static StreamReader _sr;

        private static uint _nPeopleToRing;

        private static bool _isOpen;


        public static void Open(string fileName)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("peopleToRing");
            }

            _fs = new FileStream(fileName, FileMode.Open);
            _sr = new StreamReader(_fs);

            try
            {
                string? firstLine = _sr.ReadLine();
                _nPeopleToRing = uint.Parse(firstLine);
                _isOpen = true;
            }
            catch (FormatException ex)
            {
                throw new CustomException(
                    $"First line isn\'t an integer {ex}");
            }
            catch (Exception e)
            {
                throw new CustomException(
                    $"Second line isn\'t an integer {e}");

            }
        }
    }

    internal class Program
    {

        public static void ThrowException(int code)
        {
            throw new CustomException(code);
        }


        static void Main(string[] args)
        {

            Open("d://text.txt");
            var rand = new Random();
            var dataCollect = Enumerable.Range(0, 10000).Select(x => rand.Next(140)).ToList();
            var average = ( from x in dataCollect .AsParallel()
                where Math.Log(x) > 4
                select x ).Average();

            Console.WriteLine($"average:{average}");
            try
            {
                ThrowException(405);
            }
            catch (CustomException ex) when (ex.StatusCode == 405)//异常过滤器
            {
                Console.WriteLine($"Exception caught with filter {ex.Message} and {ex.StatusCode}");
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"Exception caught {ex.Message} and {ex.StatusCode}");
                //不改变异常的类型
                // throw;
            }

            var cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                try
                {
                    var res = (from x in Partitioner.Create(dataCollect,true).AsParallel().WithCancellation(cts.Token)
                        where Math.Log(x) < 4
                        select x).Average();
                    Console.WriteLine($"query finished, sum: {res}");
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }, cts.Token);
            Console.WriteLine("query started");
            Console.Write("cancel? ");
            string? input = Console.ReadLine();
            if (input != null && input.ToLower().Equals("y"))
            {
                // cancel!
                cts.Cancel();
            }


            var values = Enumerable.Range(1, 20);
            foreach (var item in values)
            {
                Console.Write($"{item} ", item);
            }
            Console.WriteLine();

            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query1 = data.OfType<string>();
            foreach (var s in query1)
            {
                Console.WriteLine(s);
            }

            var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };
            var namesWithJ = from n in names
                where n.StartsWith("J")
                orderby n
                select n;
            Console.WriteLine("First iteration");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            names.Add("John");
            names.Add("Jim");
            names.Add("Jack");
            names.Add("Denny");
            Console.WriteLine("Second iteration");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }

            var query = from r in FormulaBan.GetChampions()
                where r.Country == "UK"
                        orderby r.Wins descending
                select r;
            foreach (Racer r in query)
            {
                Console.WriteLine($"{r:A}");
            }

            var racers = FormulaBan.GetChampions().
                Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }

            var ferrariDrivers = from r in FormulaBan.GetChampions()
                from c in r.Cars
                where c == "Ferrari"
                orderby r.LastName
                select r.FirstName + " " + r.LastName;
            foreach (var ferrariDriver in ferrariDrivers)
            {
                Console.WriteLine(ferrariDriver);
            }

            "hello world".Foo();

            var ferrariDriversBak = FormulaBan.GetChampions()
                .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
                .Where(r => r.Car == "Ferrari")
                .OrderBy(r => r.Racer.LastName)
                .Select(r => r.Racer.FirstName + " " + r.Racer.LastName).Take(10);
            foreach (var varc in ferrariDriversBak)
            {
                Console.WriteLine(varc);
            }


            var countries = from r in FormulaBan.GetChampions()
                group r by r.Country into g
                orderby g.Count() descending, g.Key
                where g.Count() >= 2
                select new
                {
                    Country = g.Key,
                    Count = g.Count()
                };
            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }


            //可以转换成下述代码
            var countriesLet = from r in FormulaBan.GetChampions()
                group r by r.Country into g
                let count = g.Count()
                orderby count descending, g.Key
                where count >= 2
                select new
                {
                    Country = g.Key,
                    Count = count
                };
            foreach (var item in countriesLet)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

            var countriesSelect = FormulaBan.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new { Group = g, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Group.Key)
                .Where(g => g.Count >= 2)
                .Select(g => new
                {
                    Country = g.Group.Key,
                    Count = g.Count
                });


            var countriesGroup = from r in FormulaBan.GetChampions()
                group r by r.Country into g
                let count = g.Count()
                orderby count descending, g.Key
                where count >= 2
                select new
                {
                    Country = g.Key,
                    Count = count,
                    Racers = from r1 in g
                        orderby r1.LastName
                        select r1.FirstName + " " + r1.LastName
                };
            foreach (var item in countriesGroup)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write($"{name}; ");
                }
                Console.WriteLine();
            }

            var racersAndTeams = (from r in from r in FormulaBan.GetChampions()
                    from y in r.Years
                    select new
                    {
                        Year = y,
                        Name = r.FirstName + " " + r.LastName
                    }
                                  join t in (from t in FormulaBan.GetTeamChampions()
                                      from y in t.Years
                                      select new
                                      {
                                          Year = y,
                                          Name = t.Name
                                      } ) on r.Year equals t.Year into rt
                                  from t in rt.DefaultIfEmpty()
                select new
                {
                    r.Year,
                    Champion = r.Name,
                    Constructor = t== null ? "No Team !" : t.Name
                }).Take(10);
            Console.WriteLine("Year  World Champion\t  Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }

            var racersCoEnumerable = FormulaBan.GetChampionships()
                .SelectMany(cs => new List<RacerInfo>()
                {
                    new RacerInfo {
                        Year = cs.Year,
                        Position = 1,
                        FirstName = cs.First.FirstName(),
                        LastName = cs.First.LastName()
                    },
                    new RacerInfo {
                        Year = cs.Year,
                        Position = 2,
                        FirstName = cs.Second.FirstName(),
                        LastName = cs.Second.LastName()
                    },
                    new RacerInfo {
                        Year = cs.Year,
                        Position = 3,
                        FirstName = cs.Third.FirstName(),
                        LastName = cs.Third.LastName()
                    }
                });

            var racerNames = from r in FormulaBan.GetChampions()
                where r.Country == "Italy"
                orderby r.Wins descending
                select new
                {
                    Name = r.FirstName + " " + r.LastName
                };
            
            var racerNamesAndStarts = from r in FormulaBan.GetChampions()
                where r.Country == "Italy"
                orderby r.Wins descending
                select new
                {
                    LastName = r.LastName,
                    Starts = r.Starts
                };


            var racersZip = racerNames.Zip(racerNamesAndStarts,
                (first, second) => first.Name + ", starts: " + second.Starts);
            foreach (var r in racersZip)
            {
                Console.WriteLine(r);
            }

            int pageSize = 5;
            int numberPages = (int)Math.Ceiling(FormulaBan.GetChampions().Count() /
                                                (double)pageSize);
            for (int page = 0; page < numberPages; page++)
            {
                Console.WriteLine($"Page {page}");
                var racersOrders = (from r in FormulaBan.GetChampions()
                        orderby r.LastName, r.FirstName
                        select r.FirstName + " " + r.LastName).
                    Skip(page * pageSize).Take(pageSize);
                foreach (var name in racersOrders)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }

            var countrySum = (from c in
                    from r in FormulaBan.GetChampions()
                    group r by r.Country into c
                    select new
                    {
                        Country = c.Key,
                        Wins = (from r1 in c
                            select r1.Wins).Sum()
                    }
                orderby c.Wins descending, c.Country
                select c).Take(5);
            foreach (var country in countrySum)
            {
                Console.WriteLine($"{country.Country} {country.Wins}");
            }

            var racersLookuup = (from r in FormulaBan.GetChampions()
                from c in r.Cars
                select new
                {
                    Car = c,
                    Racer = r
                }).ToLookup(cr => cr.Car, cr => cr.Racer);
            if (racersLookuup.Contains("Williams"))
            {
                foreach (var williamsRacer in racersLookuup["Williams"])
                {
                    Console.WriteLine(williamsRacer);
                }
            }

            var list = new System.Collections.ArrayList(FormulaBan.GetChampions()
                as System.Collections.ICollection ?? new ArrayList());
            var queryCast = from r in list.Cast<Racer>()
                where r.Country == "USA"
                orderby r.Wins descending
                select r;
            foreach (var racer in queryCast)
            {
                Console.WriteLine($"{racer:A}");
            }
        }
    }
    public class CustomException : Exception
    {
        public int StatusCode { get; private set; }


        public CustomException(int statusCode) : base()
        {
            StatusCode = statusCode;
     
        }

        public CustomException(string message) : base(message) {
        }

        public override string Message => $"statusCode : {StatusCode}";
    }

    public class Championship
    {
        public int Year { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
    }

    public class RacerInfo
    {
        public int Year { get; set; }
        public int Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static partial class StringEx
    {
        public static void Foo(this string val)
        {
            Console.WriteLine($"Foo ... {val}");
        }
    }


    public  static partial class StringEx
    {
        public static string FirstName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(0, ix);
        }
        public static string LastName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(ix + 1);
        }
    }

    public static class FormulaBan
    {
        private static List<Racer>? _racers;

        public static IList<Racer> GetChampions()
        {
            if (_racers == null)
            {
                _racers = new List<Racer>(40)
                {
                    new Racer("Nino", "Farina", "Italy", 33, 5,
                        new int[] { 1950 }, new string[] { "Alfa Romeo" }),
                    new Racer("Alberto", "Ascari", "Italy", 32, 10,
                        new int[] { 1952, 1953 }, new string[] { "Ferrari" }),
                    new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24,
                        new int[] { 1951, 1954, 1955, 1956, 1957 },
                        new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }),
                    new Racer("Mike", "Hawthorn", "UK", 45, 3,
                        new int[] { 1958 }, new string[] { "Ferrari" }),
                    new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 },
                        new string[] { "Ferrari" }),
                    new Racer("John", "Surtees", "UK", 111, 6,
                        new int[] { 1964 }, new string[] { "Ferrari" }),
                    new Racer("Jim", "Clark", "UK", 72, 25,
                        new int[] { 1963, 1965 }, new string[] { "Lotus" }),
                    new Racer("Jack", "Brabham", "Australia", 125, 14,
                        new int[] { 1959, 1960, 1966 },
                        new string[] { "Cooper", "Brabham" }),
                    new Racer("Denny", "Hulme", "New Zealand", 112, 8,
                        new int[] { 1967 }, new string[] { "Brabham" }),
                    new Racer("Graham", "Hill", "UK", 176, 14,
                        new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }),
                    new Racer("Jochen", "Rindt", "Austria", 60, 6,
                        new int[] { 1970 }, new string[] { "Lotus" }),
                    new Racer("Jackie", "Stewart", "UK", 99, 27,
                        new int[] { 1969, 1971, 1973 },
                        new string[] { "Matra", "Tyrrell" })
                };
            }
            return _racers;
        }

        private static List<Team>? _teams;
        public static IList<Team> GetTeamChampions()
        {
            if (_teams == null)
            {
                _teams = new List<Team>()
                {
                    new Team("Vanwall", 1958),
                    new Team("Cooper", 1959, 1960),
                    new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982,
                        1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007, 2008),
                    new Team("BRM", 1962),
                    new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                    new Team("Brabham", 1966, 1967),
                    new Team("Matra", 1969),
                    new Team("Tyrrell", 1971),
                    new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                    new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996,
                        1997),
                    new Team("Benetton", 1995),
                    new Team("Renault", 2005, 2006),
                    new Team("Brawn GP", 2009),
                    new Team("Red Bull Racing", 2010, 2011, 2012, 1013),
                    new Team("Mercedes", 2014, 2015)
                };
            }
            return _teams;
        }

        private static List<Championship>? championShips;

        public static IEnumerable<Championship> GetChampionships()
        {
            if (championShips == null)
            {
                championShips = new List<Championship>();
                championShips.Add(new Championship
                {
                    Year = 1950,
                    First = "Nino Farina",
                    Second = "Juan Manuel Fangio",
                    Third = "Luigi Fagioli"
                });
                championShips.Add(new Championship
                {
                    Year = 1951,
                    First = "Juan Manuel Fangio",
                    Second = "Alberto Ascari",
                    Third = "Froilan Gonzalez"
                });
                //…
            }

            return championShips;
        }
    }

    public class Team
    {
        public Team(string name, params int[]? years)
        {
            Name = name;
            Years = years!= null ? new List<int>(years) : new List<int>();
        }
        public string Name { get; }
        public IEnumerable<int> Years { get; }
    }

    public class Racer : IComparable<Racer>, IFormattable
    {
        public Racer(string firstName, string lastName, string country,
          int starts, int wins)
          : this(firstName, lastName, country, starts, wins, null, null)
        {
        }
        public Racer(string firstName, string lastName, string country,
          int starts, int wins, IEnumerable<int>? years, IEnumerable<string>? cars)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Starts = starts;
            Wins = wins;
            Years = years!= null ? new List<int>(years) : new List<int>();
            Cars = cars!= null ? new List<string>(cars) : new List<string>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Wins { get; set; }
        public string Country { get; set; }
        public int Starts { get; set; }
        public IEnumerable<string> Cars { get; }
        public IEnumerable<int> Years { get; }


        public override string ToString() => $"{FirstName} {LastName}";


        public int CompareTo(Racer? other) => Compare(LastName, other?.LastName, StringComparison.Ordinal);


        public string ToString(string format) => ToString(format, null);


        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return $"{FirstName} {LastName}, {Country}; starts: {Starts}, wins: {Wins}";
                default:
                    throw new FormatException($"Format {format} not supported");
            }
        }
    }
}