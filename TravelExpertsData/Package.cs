using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class Package
    {
        public int PackageId { get; set; }
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
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    imagePath = value;
                }
            }
        }
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
        public bool AirfairInclusion { get; set; }
        public DateTime PackageStartDate { get; set; }
        public DateTime PackageEndDate { get; set; }
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
        public decimal PackageBasePrice { get; set; }
        public decimal PackageAgencyCommission { get; set; }

        private string packageName;
        private string imagePath;
        private string partnerURL;
        private string packageDescription;

        public Package(int packageId, string packageName, string imagePath, string partnerURL, bool airfairInclusion, DateTime packageStartDate, DateTime packageEndDate, string packageDescription, decimal packageBasePrice, decimal packageAgencyCommission)
        {
            PackageId = packageId;
            this.packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
            ImagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
            PartnerURL = partnerURL ?? throw new ArgumentNullException(nameof(partnerURL));
            AirfairInclusion = airfairInclusion;
            PackageStartDate = packageStartDate;
            PackageEndDate = packageEndDate;
            PackageDescription = packageDescription ?? throw new ArgumentNullException(nameof(packageDescription));
            PackageBasePrice = packageBasePrice;
            PackageAgencyCommission = packageAgencyCommission;
        }
    }
}
