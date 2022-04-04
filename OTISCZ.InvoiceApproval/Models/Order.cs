using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTISCZ.InvoiceApproval.Models {
    public class Order {
        public string OrderNr = null;
        public int Status = 0;
        public string StatusText = null;
        public string ApplicationName = null;
        public decimal? Price = null;
        public string Currency = null;
        public string BuyerMail = null;
    }
}