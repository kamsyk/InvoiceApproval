using OTISCZ.InvoiceApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OTISCZ.InvoiceApproval.Wcf {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderWcf" in both code and config file together.
    [ServiceContract]
    public interface IOrderWcf {
        [OperationContract]
        Order GetOrderStatus(string orderNr);
    }
}
