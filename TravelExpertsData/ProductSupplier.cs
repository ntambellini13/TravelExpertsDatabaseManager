using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class ProductSupplier
    {
        public int ProductSupplierId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        public ProductSupplier(int productSupplierId, int productId, int supplierId)
        {
            ProductSupplierId = productSupplierId;
            ProductId = productId;
            SupplierId = supplierId;
        }
    }
}
