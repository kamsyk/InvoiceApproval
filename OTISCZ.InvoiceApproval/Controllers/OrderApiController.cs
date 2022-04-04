using OTISCZ.ClcPur.Model;
using OTISCZ.ClcPur.Model.Repositories;
using OTISCZ.InvoiceApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static OTISCZ.InvoiceApproval.Controllers.OrderBaseController;

namespace OTISCZ.InvoiceApproval.Controllers
{
    public class OrderApiController : ApiController {
        
        public Order GetOrderStatus(string orderNr) {
            return new OrderBaseController().GetOrderStatus(orderNr); 
        }

        public void SetSupplierLastStampDate() {

        }
    }
}