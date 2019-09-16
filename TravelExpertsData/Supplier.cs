using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        private string supplierName;

        public string SupplierName
        {
            get
            {
                return supplierName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    supplierName = value;
                }
            }
        }

        public List<Product> Products;

        public Supplier(int supplierId, string supplierName)
        {
            SupplierId = supplierId;
            this.supplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
        }

        public Supplier(int supplierId, string supplierName, List<Product> products)
        {
            SupplierId = supplierId;
            this.supplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
            Products = products;
        }
    }
}
