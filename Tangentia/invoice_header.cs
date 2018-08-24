using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangentia
{
    public partial class invoice_header
    {
        public Nullable<int> DocumentNumber { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string PONumber { get; set; } = "";
        public string BillLadingNumber { get; set; } = "";
        public string TermsType { get; set; } = "";
        public string TermsDiscountPercent { get; set; } = "";
        public Nullable<decimal> TermsDiscountAmount { get; set; }
        public Nullable<System.DateTime> TermsNetDueDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public string ControlStatus { get; set; } = "";
        public Nullable<System.DateTime> ControlDatetime { get; set; }
        public string Company { get; set; } = "";
        public string Unit { get; set; } = "";
        public string TradingPartner { get; set; } = "";
        public string EdiUser { get; set; } = "";
        public string InterchangeControlNumber { get; set; } = "";
        public string GroupControlNumber { get; set; } = "";
        public string TransactionControlNumber { get; set; } = "";
        public string InvoiceStatus { get; set; } = "";
        public string OriginCountry { get; set; } = "";
        public string DestinationCountry { get; set; } = "";
        public string Terms { get; set; } = "";
        public Nullable<System.DateTime> BOLDate { get; set; }
        public string ContainerNo { get; set; } = "";
        public string ShipToCode { get; set; } = "";
        public string ShipToName { get; set; } = "";
        public string ShipToAddress { get; set; } = "";
        public string ShipToCity { get; set; } = "";
        public string ShipToState { get; set; } = "";
        public string ShipToZip { get; set; } = "";
        public string ShipToCountry { get; set; } = "";
        public string Carton { get; set; } = "";
        public string ShipmentId { get; set; } = "";
        public string PackingList { get; set; } = "";
        public List<invoice_details> invoice_details_list { get; set; }
    }
}
