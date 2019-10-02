using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating Product-Supplier Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public class ProductSupplier
    {
        /// <summary>
        /// Public property for ProductSupplierId
        /// </summary>
        public int ProductSupplierId { get; set; }

        /// <summary>
        /// Public property for ProductId
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Public property for SupplierId
        /// </summary>
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
