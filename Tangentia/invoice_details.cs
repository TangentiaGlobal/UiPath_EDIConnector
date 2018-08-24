using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangentia
{
    public partial class invoice_details
    {
        public Nullable<int> SeqId { get; set; }
        public Nullable<int> DocumentNumber { get; set; }
        public string LineNumber { get; set; } = "";
        public Nullable<decimal> InvoiceQuantity { get; set; }
        public string UOM { get; set; } = "";
        public Nullable<decimal> UnitPrice { get; set; }
        public string VendorPartNumber { get; set; } = "";
        public string BuyerPartNumber { get; set; } = "";
        public string ProductDescription { get; set; } = "";
        public string PONumber { get; set; } = "";
        public Nullable<System.DateTime> ForecastDueDate { get; set; }
    }
}
