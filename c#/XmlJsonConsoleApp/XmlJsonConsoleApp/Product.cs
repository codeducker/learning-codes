using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlJsonConsoleApp
{
    [XmlRoot]
    public class Product
    {
        [XmlAttribute(attributeName:"discount")]
        public int Discount { get; set; }

        [XmlElement]
        public long ProductId { get; set; }

        [XmlElement]
        public string ProductName { get; set; }

        [XmlElement]
        public int SupplyId { get; set; }

        [XmlElement]
        public double Price { get; set; }

        public override string ToString()
        {
            return $"dis: {Discount}, proId:{ProductId}, proName:{ProductName},supId:{SupplyId}, price: {Price}";
        }
    }

    public class BookProduct : Product
    {
        [XmlElement] 
        public bool IsBN { get; set; }
    }

    public class Inventory
    {
        [XmlArrayItem( typeof(Product))]
        public List<Product> Products { get; set; }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < Products.Count; i++)
            {
                stringBuilder.Append(Products[i].ProductName);
            }
            return stringBuilder.ToString();
        }
    }
}
