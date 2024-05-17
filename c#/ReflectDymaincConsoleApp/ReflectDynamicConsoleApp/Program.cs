using System.Dynamic;
using ReflectDynamicConsoleApp;
using System.Reflection;
using System.Reflection.Emit;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using static IronPython.Modules._ast;
using System.IO;

[assembly: AssemblyAttr("lucky body")]

namespace ReflectDynamicConsoleApp
{

    //下述两类型可以在任何地方
    // [assembly:SomeAssemblyAttribute(Parameters)]
    // [module:SomeAssemblyAttribute(Parameters)]

    [AttributeUsage(validOn:AttributeTargets.Interface,AllowMultiple = false,Inherited = true)]
    public class InterAttr : System.Attribute
    {
        private string _comment;

        public InterAttr(string comment)
        {
            _comment = comment;
        }

        public string Comment
        {
            get => _comment;
            set => _comment = value;
        }
    }

    [InterAttr(comment: "就是个自定义特性")]
    public interface ISomeInterface
    {
    }

    public class TypeInfoPackage
    {

    }


    public class DisplayInfo
    {
        public static void DisplayTypeInfo(Type type)
        {
            // make sure we only pick out classes
            if (!type.GetTypeInfo().IsClass)
            {
                return;
            }
            Console.WriteLine($"\nclass {type.Name}");
            IEnumerable<AssemblyAttr> attributes = type.GetTypeInfo()
                .GetCustomAttributes().OfType<AssemblyAttr>();
            if (attributes.Count() == 0)
            {
                Console.WriteLine("No changes to this class\n");
            }
            else
            {
                foreach (AssemblyAttr attribute in attributes)
                {
                    Console.WriteLine(attribute);
                }
            }
            Console.WriteLine("changes to methods of this class:");
            foreach (MethodInfo method in
                     type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
            {
                IEnumerable<AssemblyAttr> attributesToMethods =
                    method.GetCustomAttributes().OfType<AssemblyAttr>();
                if (attributesToMethods.Count() > 0)
                {
                    Console.WriteLine($"{method.ReturnType} {method.Name}()");
                    foreach (System.Attribute attribute in attributesToMethods)
                    {
                        Console.WriteLine(attribute);
                    }
                }
            }
        }
    }


#if DOTNETCORE
          private static object GetCalculator()
          {
              Assembly assembly =
          Assembly.LoadContext.Default.LoadFromAssemblyPath(CalculatorLib Path);
              Type type = assembly.GetType(CalculatorTypeName);
              return Activator.CreateInstance(type);
            }
#endif
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}";
    }


    public class DefaultDynamicObject : DynamicObject
    {
        private readonly Dictionary<string, object> _dynamicData = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            bool success;
            if (_dynamicData.TryGetValue(binder.Name, out var value))
            {
                result = value;
                success = true;
            }
            else
            {
                result = "Property Not Found! ";
                success = false;
            }
            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            _dynamicData[binder.Name] = value??new object();
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder,
            object[] args, out object result)
        {
            dynamic method = _dynamicData[binder.Name];
            result = method((DateTime)args[0]);
            return result != null;
        }
    }

    class Locker
    {
        public static void DoExpando()
        {
            dynamic expObj = new ExpandoObject();
            expObj.FirstName = "Daffy";
            expObj.LastName = "Duck";
            Console.WriteLine($"{expObj.FirstName} {expObj.LastName}");
            Func<DateTime, string> localFunc = today => today.AddDays(1).ToShortDateString();
            expObj.GetTomorrowDate = localFunc;
            Console.WriteLine($"Tomorrow is {expObj.GetTomorrowDate(DateTime.Now)}");
            expObj.Friends = new List<Person>();
            expObj.Friends.Add(new Person() { FirstName = "Bob", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Robert", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Bobby", LastName = "Jones" });
            foreach (Person friend in expObj.Friends)
            {
                Console.WriteLine($"{friend.FirstName} {friend.LastName}");
            }
        }

        public static StreamReader? GetStreamReader(string csvFile)
        {
            if (File.Exists(csvFile))
            {
                return new StreamReader(csvFile);
            }

            return null;
        }

        public static IEnumerable<dynamic> ParseFile(string fileName)
        {
            string[] headLine = new string[] { "ID", "姓名", "地址" };
            var retList = new List<dynamic>();
            var streamReader = GetStreamReader(fileName);
            if (streamReader == null)
            {
                return retList;
            }

            while (streamReader.Peek() > 0)
            {
                var readLine = streamReader.ReadLine();
                if (readLine == null)
                {
                    continue;
                }

                string[] dataLine = readLine.Trim().Split(',').ToArray();
                dynamic dynamicEntity = new ExpandoObject();
                for (int i = 0; i < headLine.Length; i++)
                {
                    ((IDictionary<string, object>)dynamicEntity).Add(headLine[i], dataLine[i]);
                }
                retList.Add(dynamicEntity);
            }
            return retList;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Locker.DoExpando();
            dynamic dymDefaultDynamicObject = new DefaultDynamicObject();
            dymDefaultDynamicObject.FirstName = "Bugs";
            dymDefaultDynamicObject.LastName = "Bunny";
            Console.WriteLine(dymDefaultDynamicObject.GetType());
            Console.WriteLine($"{dymDefaultDynamicObject.FirstName} {dymDefaultDynamicObject.LastName}");
            Func<DateTime,string> AddOneDay =  today => today.AddDays(1).ToShortTimeString() ;
            dymDefaultDynamicObject.AddOneDay = AddOneDay;
            Console.WriteLine($"execute extend method : {dymDefaultDynamicObject.AddOneDay(new DateTime())}");


            var engine = Python.CreateEngine();
            var scriptScope = engine.CreateScope();
            var scriptSource = engine.CreateScriptSourceFromString("""print("hello")""");
            var execute = scriptSource.Execute(scriptScope);
            Console.WriteLine($"python result :{execute}");

            // var fromConfiguration = ScriptRuntime.CreateFromConfiguration();
            // var scriptEngine = fromConfiguration.GetEngine("Python");
            // var scriptSourceFromString = scriptEngine.CreateScriptSourceFromString("""
            //     print("hello")
            //     """);
            // scriptSourceFromString.Execute(scriptEngine.CreateScope());

            dynamic dyn;
            dyn = 100;
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);
            dyn = "This is a string";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);
            dyn = new Person() { FirstName = "Bugs", LastName = "Bunny" };
            Console.WriteLine(dyn.GetType());
            Console.WriteLine($"{dyn.FirstName} {dyn.LastName}");

            var staticPerson = new Person();
            dynamic dynamicPerson = new Person();
            // staticPerson.GetFullName("John", "Smith");//此时直接会显示异常
            try
            {
                dynamicPerson.GetFullName("John", "Smith");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);//运行时抛出异常
            }

#if NET7_0_OR_GREATER
            var assemblyCal = Assembly.LoadFile(@"c:/addins/ConsoleAppLibrary.dll");
            var instance = assemblyCal.CreateInstance("ConsoleAppLibrary.Calculator");
            if (instance != null)
            {
                var instanceSelf = Activator.CreateInstance(instance.GetType());
                var method = instanceSelf?.GetType().GetMethod("Add", new Type[] { typeof(double), typeof(double) });
                Console.WriteLine($"NEW CHANNEL: {method}");
                var invoke = method?.Invoke(instanceSelf, new object?[]{2.3,4.5});
                Console.WriteLine($"method invoke : {invoke}");

                //使用动态类型调用
                dynamic? calc = Activator.CreateInstance(instance.GetType());
                Console.WriteLine($"dynamic result:{calc?.Add(20.3, 21.3)}");
            }


#endif



            // DisplayInfo.DisplayTypeInfo(typeof(TypeInfoPackage));
            // var assembly = Assembly.Load(new AssemblyName("ReflectDynamicConsoleApp"));
            // // var assembly = Assembly.LoadFrom(@"C:\Windows\assembly\GAC\ADODB");
            // Console.WriteLine(assembly);
            // var type = typeof(AssemblyAttr);
            // Attribute supportsAttributes = assembly.GetCustomAttribute(type);
            // Console.WriteLine(assembly.FullName);

            // double a = 7.0;
            // Console.WriteLine(a);
            // Console.WriteLine(a.GetType());
            // var value = Type.GetType("System.Double");
            // if (value != null)
            // {
            //     Console.WriteLine(value.BaseType);
            //     foreach (var methodInfo in value.GetMethods())
            //     {
            //         Console.WriteLine($"{methodInfo}");
            //     }
            // }
        }
    }

}