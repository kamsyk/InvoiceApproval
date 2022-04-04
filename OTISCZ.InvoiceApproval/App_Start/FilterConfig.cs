using System.Web;
using System.Web.Mvc;

namespace OTISCZ.InvoiceApproval {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
