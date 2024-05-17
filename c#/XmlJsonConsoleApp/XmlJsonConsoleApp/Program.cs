using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Xml.XPath;
using System.Text.Json;

namespace XmlJsonConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ReadStringContent();

            // ReadAttributes();

            // WriterSample();

            // ReadXMlByDom();

            // CreateXml();

            // ReadXPathXml();

            // Insert();

            // Serialization();

            // Deserialization();

            // SerializationArray();

            // DeserializeInventory();

            // LinqToXml();

            // CreateXmlLinq();

            // WithNamespace();

            // QueryHamlet();

            // CreateJson();

            ConvertObject();
        }

        public static void ConvertObject()
        {
            Inventory inventory = GetInventoryObject();
            string json = JsonSerializer.Serialize(inventory);
            Console.WriteLine(json);
            Console.WriteLine();
            Inventory? newInventory = JsonSerializer.Deserialize<Inventory>(json);
            if (newInventory?.Products != null){
                foreach (var product in newInventory?.Products)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
        }

        public static Inventory GetInventoryObject() =>
            new Inventory
            {
                Products = new List<Product>()
                {
                    new Product
                    {
                        ProductId = 100,
                        ProductName = "Product Thing",
                        SupplyId = 10
                    },
                    new BookProduct
                    {
                        ProductId = 101,
                        ProductName = "How To Use Your New Product Thing",
                        SupplyId = 10,
                        IsBN = false
                    }
                }
            };

        public static void CreateJson()
        {
            var book1 = new JsonObject();
            book1["title"] = "Professional C# 6 and .NET 5 Core";
            book1["publisher"] = "Wrox Press";
            var book2 = new JsonObject();
            book2["title"] = "Professional C# 5 and .NET 4.5.1";
            book2["publisher"] = "Wrox Press";
            var books = new JsonArray();
            books.Add(book1);
            books.Add(book2);
            var json = new JsonObject();
            json["books"] = books;
            Console.WriteLine(json);
        }

        public static void QueryHamlet()
        {
            XDocument doc = XDocument.Load("products.xml");
            IEnumerable<string> persons = (from people in doc.Descendants("Products")
                select people.Value).ToList();
            Console.WriteLine($"{persons.Count()} Players Found");
            Console.WriteLine();
            foreach (var item in persons)
            {
               Console.WriteLine(item);
            }
        }

        private static void WithNamespace()
        {
            var doc = new XDocument();
            XNamespace ns = "http://www.cninnovation.com/samples/2015";
            XComment xComment = new XComment("这是注释");
            doc.Add(xComment);
            var company =
                new XElement(ns + "Company",
                    new XElement("CompanyName", "Microsoft Corporation"),
                    new XElement("CompanyAddress",new XAttribute("taxID","972123342947"),
                        new XElement("Address", "One Microsoft Way"),
                        new XElement("City", "Redmond"),
                        new XElement("Zip", "WA 98052-6399"),
                        new XElement("State", "WA"),
                        new XElement("Country", "USA")));
            doc.Add(company);
            Console.WriteLine(doc);
        }

        private static void CreateXmlLinq()
        {
            var company =
                new XElement("Company",
                    new XElement("CompanyName", "Microsoft Corporation"),
                    new XElement("CompanyAddress",
                        new XElement("Address", "One Microsoft Way"),
                        new XElement("City", "Redmond"),
                        new XElement("Zip", "WA 98052-6399"),
                        new XElement("State", "WA"),
                        new XElement("Country", "USA")));
            Console.WriteLine(company);
        }

        private static void LinqToXml()
        {
            var xDocument = XDocument.Load("products.xml");
            Console.WriteLine($"{xDocument.Root.Name}");
            Console.WriteLine($"{xDocument.Root.Attribute("title")}");
        }

        private static void DeserializeInventory()
        {
            using (FileStream stream = File.OpenRead("products.xml"))
            {
                var serializer = new XmlSerializer(typeof(Inventory));
                Inventory newInventory = serializer.Deserialize(stream) as Inventory;
                foreach (Product prod in newInventory.Products)
                {
                    Console.WriteLine(prod.ProductName);
                }
            }
        }

        private static void SerializationArray()
        {
            var product = new Product()
            {
                Discount = 100,
                Price = 20.0D,
                ProductName = "Nike Pair Shoot",
                SupplyId = 1,
            };
            List<Product> products = new List<Product>();
            products.Add(product);
            Inventory inventory = new Inventory();
            inventory.Products  = products;

            var fileStream = File.OpenWrite("products.xml");
            using (StreamWriter stream = new StreamWriter(fileStream))
            {
                var xmlSerializer = new XmlSerializer(typeof(Inventory));
                xmlSerializer.Serialize(stream, inventory);
            }
        }


        private static void Deserialization()
        {
            Product? product; 
            using (var fileStream = File.Open("product.xml",FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(typeof(Product));
                product = xmlSerializer.Deserialize(fileStream) as Product;
            }
            Console.WriteLine(product);
        }


        private static void Serialization()
        {
            var product = new Product()
            {
                Discount = 100,
                Price = 20.0D,
                ProductName = "Nike Pair Shoot",
                SupplyId =  1,
            };

            var fileStream = File.OpenWrite("product.xml");
            using (StreamWriter stream = new StreamWriter(fileStream))
            {
                var xmlSerializer = new XmlSerializer(typeof(Product));
                xmlSerializer.Serialize(stream,product);
            }
        }


        private static void Insert()
        {
#if DNX46
          var doc = new XmlDocument();
          doc.Load(BooksFileName);
#else
            var doc = new XPathDocument("books.xml");
#endif
            XPathNavigator navigator = doc.CreateNavigator();
            if (navigator.CanEdit)
            {
                XPathNodeIterator iter = navigator.Select("/bookstore/book[potion()<4]/price");
                while (iter.MoveNext())
                {
                    iter.Current?.InsertAfter("<disc>5</disc>");
                }
            }
            using (var stream = File.CreateText("newBooksFileXpath.xml"))
            {
                var outDoc = new XmlDocument();
                outDoc.LoadXml(navigator.OuterXml);
                outDoc.Save(stream);
            }
        }

        private static void ReadXPathXml()
        {
            var xPathDocument = new XPathDocument("books.xml");
            var xPathNavigator = xPathDocument.CreateNavigator();
            // // 从根节点查询 @ 选取属性  .. 当前节点的父节点  / 从根节点查询 具体XPATH查询语法 https://www.runoob.com/xpath/xpath-syntax.html
            // var xPathNodeIterator = xPathNavigator.Select("/bookstore/book[1]");
            // var xPathNodeIterator = xPathNavigator.Select("/bookstore/book[last()]");
            // var xPathNodeIterator = xPathNavigator.Select("/bookstore/book[position()<3]");
            // var xPathNodeIterator = xPathNavigator.Select("/bookstore/book[@genre='novel'][position()<1]");
            var xPathNodeIterator = xPathNavigator.Select("/bookstore/book[position()<4]/title | /bookstore/book[position()<4]/author");
            while (xPathNodeIterator.MoveNext())
            {
                var pathNodeIterator = xPathNodeIterator.Current.SelectDescendants(XPathNodeType.Element, matchSelf: false);
                while (pathNodeIterator.MoveNext())
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine($"name:{pathNodeIterator.Current?.Name} , value: {pathNodeIterator.Current?.Value}");
                    Console.WriteLine("---------------------");
                }

                Console.WriteLine("***************");
                Console.WriteLine($"name:{xPathNodeIterator.Current?.Name} , value: {xPathNodeIterator.Current?.Value}");
                Console.WriteLine("***************");
                Console.WriteLine("");
            }
            Console.WriteLine($"Total Cost: {xPathNavigator.Evaluate("sum(/bookstore/book[0]/title)")}");
        }


        private static void CreateXml()
        {
            var doc = new XmlDocument();
            using (FileStream stream = File.OpenRead("books.xml"))
            {
                doc.Load(stream);
            }
            //create a new 'book' element
            XmlElement newBook = doc.CreateElement("book");
            //set some attributes
            newBook.SetAttribute("genre", "Mystery");
            newBook.SetAttribute("publicationdate", "2001");
            newBook.SetAttribute("ISBN", "123456789");
            //create a new 'title' element
            XmlElement newTitle = doc.CreateElement("title");
            newTitle.InnerText = "Case of the Missing Cookie";
            newBook.AppendChild(newTitle);
            //create new author element
            XmlElement newAuthor = doc.CreateElement("author");
            newBook.AppendChild(newAuthor);
            //create new name element
            XmlElement newName = doc.CreateElement("name");
            newName.InnerText = "Cookie Monster";
            newAuthor.AppendChild(newName);
            //create new price element
            XmlElement newPrice = doc.CreateElement("price");
            newPrice.InnerText = "9.95";
            newBook.AppendChild(newPrice);
            //add to the current document
            doc.DocumentElement.AppendChild(newBook);
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = Environment.NewLine
            };
            //write out the doc to disk
            using (StreamWriter streamWriter = File.CreateText("newDomBooks.xml"))
            using (XmlWriter writer = XmlWriter.Create(streamWriter, settings))
            {
                doc.WriteContentTo(writer);
            }
            XmlNodeList nodeLst = doc.GetElementsByTagName("title");
            foreach (XmlNode node in nodeLst)
            {
                Console.WriteLine(node.OuterXml);
            }
        }

        private static void ReadXMlByDom()
        {
            try
            {
                using (FileStream fs = File.OpenRead("newBooks.xml"))
                {
                    XmlDocument dom = new XmlDocument();
                    dom.Load(fs);
                    XmlNodeList xmlNodes = dom.GetElementsByTagName("title");
                   foreach (XmlNode xmlNode in xmlNodes)
                   {
                       Console.WriteLine(xmlNode.InnerText);
                   }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void WriterSample()
        {
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = true,
                Encoding = Encoding.UTF8,
                WriteEndDocumentOnClose = true
            };
            StreamWriter stream = File.CreateText(@"newBooks.xml");
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartDocument();
                //Start creating elements and attributes
                writer.WriteStartElement("book");
                writer.WriteAttributeString("genre", "Mystery");
                writer.WriteAttributeString("publicationdate", "2001");
                writer.WriteAttributeString("ISBN", "123456789");
                writer.WriteElementString("title", "Case of the Missing Cookie");
                writer.WriteStartElement("author");
                writer.WriteElementString("name", "Cookie Monster");
                writer.WriteEndElement();
                writer.WriteElementString("price", "9.99");
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private static void ReadStringContent()
        {
            var xmlReader = XmlReader.Create("books.xml");
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(xmlReader.Value);
                }
            }
        }

        public static void ReadAttributes()
        {
            using (XmlReader reader = XmlReader.Create("books.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            Console.WriteLine(reader.GetAttribute(i));
                        }
                    }
                }
            }
        }
    }
}