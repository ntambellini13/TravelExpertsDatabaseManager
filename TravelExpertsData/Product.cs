using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class Product
    {
        public int ProductId { get; set; }

        private string productName;

        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    productName = value;
                }
            }
        }

        public List<Supplier> Suppliers;

        public Product(int productId, string productName)
        {
            ProductId = productId;
            this.productName = productName ?? throw new ArgumentNullException(nameof(productName));
            this.Suppliers = new List<Supplier>();
        }

        public Product(int productId, string productName, List<Supplier> suppliers)
        {
            ProductId = productId;
            this.productName = productName ?? throw new ArgumentNullException(nameof(productName));
            this.Suppliers = suppliers;
        }

        public bool Equals(Product other)
        {
            return (other.ProductId == this.ProductId && other.ProductName == this.ProductName);
        }

        public override string ToString()
        {
            return $" {ProductId} | {ProductName}";
        }
    }
}
