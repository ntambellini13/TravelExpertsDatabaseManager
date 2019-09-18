using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating Supplier Objects
 * Author: Product
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public class Supplier
    {
        public int SupplierId { get; set; }//Public class property SupplierId

        private string supplierName;//Private class variable supplierName

        /// <summary>
        /// Public class property SupplierName
        /// </summary>
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

        public List<Product> Products;//Public class list variable Products

        /// <summary>
        /// Class constructor Product
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierName"></param>
        public Supplier(int supplierId, string supplierName)
        {
            SupplierId = supplierId;//assign input parameter to class property

            //null coalescing operator statement checks to see if input parameter is null; throws exception if value null
            this.supplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
            this.Products = new List<Product>();//assing new list to class variable
        }

        /// <summary>
        /// Overloaded class constructor Product
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierName"></param>
        /// <param name="products"></param>
        public Supplier(int supplierId, string supplierName, List<Product> products)
        {
            SupplierId = supplierId;//assign input parameter to class property

            //null coalescing operator statement checks to see if input parameter is null; throws exception if value null
            this.supplierName = supplierName ?? throw new ArgumentNullException(nameof(supplierName));
            Products = products;//assign input parameter list to class variable
        }

        /// <summary>
        /// Public Class method
        /// </summary>
        /// <param name="other">Supplier class method parameter other</param>
        /// <returns>returns bool value that indicates if a match was found or not</returns>
        public bool Equals(Supplier other)
        {
            //used to have a valid object reference against instances of this class by checking
            //if input parameter's properties match the properties of this class
            return (other.SupplierId == this.SupplierId && other.SupplierName == this.SupplierName) ;
        }

        /// <summary>
        /// Public class override method; overrides ToString method
        /// </summary>
        /// <returns>interpolated string containg class property values</returns>
        public override string ToString()
        {
            return $" {this.SupplierId} | {this.SupplierName}";
        }
    }
}
