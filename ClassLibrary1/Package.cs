using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;


namespace TravelExpertsData
{
    public class Package
    {
        /// <summary>
        /// Package ID
        /// </summary>
        public int PackageId { get; set; }
        /// <summary>
        /// Package name. Can't be null/empty.
        /// </summary>
        public string PackageName
        {
            get
            {
                return packageName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    packageName = value;
                }
            }
        }
        /// <summary>
        /// Image stored as array of bytes. Can't be null.
        /// </summary>
        public byte[] Image {
            get
            {
                return image;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    image = value;
                }
            }
        }
        /// <summary>
        /// URL of partner site. Can't be null/empty.
        /// </summary>
        public string PartnerURL
        {
            get
            {
                return partnerURL;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    partnerURL = value;
                }
            }
        }
        /// <summary>
        /// Is airfair included in price?
        /// </summary>
        public bool AirfairInclusion { get; set; }
        /// <summary>
        /// The start date of the package
        /// </summary>
        public DateTime PackageStartDate { get { return packageStartDate; } }
        /// <summary>
        /// The end date of the package
        /// </summary>
        public DateTime PackageEndDate { get { return packageEndDate; } }
        /// <summary>
        /// Package description. Can't be null/empty.
        /// </summary>
        public string PackageDescription
        {
            get
            {
                return packageDescription;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    packageDescription = value;
                }
            }
        }
        /// <summary>
        /// The base price of the package
        /// </summary>
        public decimal PackageBasePrice { get; set; }
        /// <summary>
        /// The commission the agency receives for the package.
        /// </summary>
        public decimal PackageAgencyCommission
        {
            get
            {
                return packageAgencyCommission;
            }
            set
            {
                if (value >= PackageBasePrice)
                {
                    throw new ArgumentOutOfRangeException("Commission must be less than base price!");
                }
                else
                {
                    packageAgencyCommission = value;
                }
            }
        }

        /// <summary>
        /// Displays the image data in a format that the picture box can use
        /// </summary>
        public Image ImageForPictureBox
        {
            get
            {
                return Image==null?  null: ByteToImage(Image);
            }
        }

        private string packageName;
        private string partnerURL;
        private string packageDescription;
        private byte[] image;
        private DateTime packageStartDate;
        private DateTime packageEndDate;
        private decimal packageAgencyCommission;

    
        /// <summary>
        /// Creates package
        /// </summary>
        /// <param name="packageId">id</param>
        /// <param name="packageName">name</param>
        /// <param name="image">byte array of image data</param>
        /// <param name="partnerURL">partner url</param>
        /// <param name="airfairInclusion">is airfair included?</param>
        /// <param name="packageStartDate">pkg start date. should be before end date.</param>
        /// <param name="packageEndDate">package end date. should be after start date.</param>
        /// <param name="packageDescription">package description</param>
        /// <param name="packageBasePrice">base price</param>
        /// <param name="packageAgencyCommission">agency commission. should be less than base price.</param>
        public Package(int packageId, string packageName, byte[] image, string partnerURL, bool airfairInclusion, DateTime packageStartDate, DateTime packageEndDate, string packageDescription, decimal packageBasePrice, decimal packageAgencyCommission)
        {
            PackageId = packageId;
            this.packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
            Image = image ?? throw new ArgumentNullException(nameof(image));
            PartnerURL = partnerURL ?? throw new ArgumentNullException(nameof(partnerURL));
            AirfairInclusion = airfairInclusion;
            setPackageDates(packageStartDate, packageEndDate);
            PackageDescription = packageDescription ?? throw new ArgumentNullException(nameof(packageDescription));
            PackageBasePrice = packageBasePrice;
            PackageAgencyCommission = packageAgencyCommission;
        }

        /// <summary>
        /// Method to convert image byte array to Bitmap image
        /// </summary>
        /// <param name="blob">image byte[] array/param>
        /// <returns>Image as Bitmap</returns>
        public static Bitmap ByteToImage(byte[] blob)
        {
            if (blob == null) { throw new ArgumentNullException("Must supply blob array"); }
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;            
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        /// <summary>
        /// Sets the package dates together ensuring that the start date is before the end date.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public void setPackageDates(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentOutOfRangeException("Your start date must be before your end date.");
            }
            else
            {
                packageStartDate = startDate;
                packageEndDate = endDate;
            }
        }


    }
}
