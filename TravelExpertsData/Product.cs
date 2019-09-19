using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating Product Objects
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public class Product
    {
        public int ProductId { get; set; }//Public class property ProductId

        private string productName;//Private class variable productName

        /// <summary>
        /// Public class property ProductName 
        /// </summary>
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

        public List<Supplier> Suppliers;//Public class list variable Suppliers

        /// <summary>
        /// Class constructor Product
        /// </summary>
        /// <param name="productId">int constructor parameter productId</param>
        /// <param name="productName">string constructor parameter productName</param>
        public Product(int productId, string productName)
        {
            ProductId = productId;//assign input parameter to class property
            
            //null coalescing operator statement checks to see if input parameter is null; throws exception if value null
            this.productName = productName ?? throw new ArgumentNullException(nameof(productName));
            this.Suppliers = new List<Supplier>();//assing new list to class variable
        }

        /// <summary>
        /// Overloaded class constructor Product
        /// </summary>
        /// <param name="productId">int constructor parameter productId</param>
        /// <param name="productName">string constructor parameter productName</param>
        /// <param name="suppliers">Supplier list constructor parameter productName</param>
        public Product(int productId, string productName, List<Supplier> suppliers)
        {
            ProductId = productId;//assign input parameter to class property

            //null coalescing operator statement checks to see if input parameter is null; throws exception if value null
            this.productName = productName ?? throw new ArgumentNullException(nameof(productName));
            this.Suppliers = suppliers;//assign input parameter list to class variable
        }

        /// <summary>
        /// Public Class method
        /// </summary>
        /// <param name="other">Product class method parameter other</param>
        /// <returns>returns bool value that indicates if a match was found or not</returns>
        public bool Equals(Product other)
        {
            //used to have a valid object reference against instances of this class by checking
            //if input parameter's properties match the properties of this class
            return (other.ProductId == this.ProductId && other.ProductName == this.ProductName);
        }

        /// <summary>
        /// Public class override method; overrides ToString method
        /// </summary>
        /// <returns>interpolated string containg class property values</returns>
        public override string ToString()
        {
            return $" {ProductId} | {ProductName}";
        }
    }
}
