using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating ProductSupplier Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public class ProductSupplier
    {
        //Class public properties
        public int ProductSupplierId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        /// <summary>
        /// Public class constructor
        /// </summary>
        /// <param name="productSupplierId">int productSupplierId</param>
        /// <param name="productId">int productId</param>
        /// <param name="supplierId">int supplierId</param>
        public ProductSupplier(int productSupplierId, int productId, int supplierId)
        {
            //assign input parameters to class properties
            ProductSupplierId = productSupplierId;
            ProductId = productId;
            SupplierId = supplierId;
        }
    }
}
