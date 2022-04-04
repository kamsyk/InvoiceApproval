using Kamsyk.Reget.Model;
using Kamsyk.Reget.Model.ExtendedModel;
using Kamsyk.Reget.Model.Repositories;
using OTISCZ.ClcPur.Model;
using OTISCZ.ClcPur.Model.Repositories;
using OTISCZ.InvoiceApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTISCZ.InvoiceApproval.Controllers {
    public class OrderBaseController {
        #region Constants
        private const string STATUS_TEXT_APPROVED = "Approved";
        private const string STATUS_TEXT_REJECTED = "Rejected";
        private const string STATUS_TEXT_WAITING = "Waiting for Approval";
        private const string STATUS_TEXT_CANCELED = "Cancelled";
        private const string STATUS_TEXT_UNKNOWN = "Unknown";

        private const int STATUS_APPROVED = 20;
        private const int STATUS_REJECTED = 30;
        private const int STATUS_WAITING = 10;
        private const int STATUS_CANCELED = 40;
        private const int STATUS_UNKNOWN = 50;

        private const int CLCPUR_STATUS_UNKNOWN = -1;
        private const int CLCPUR_STATUS_PRODIS_RELEASED = 50;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL1A = 130;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL1B = 140;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL2 = 200;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL3 = 250;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL4 = 275;
        private const int CLCPUR_STATUS_WAIT_FOR_APPROVALL5 = 280;
        private const int CLCPUR_STATUS_WAIT_FOR_OBP_APPROVALL1 = 285;
        private const int CLCPUR_STATUS_WAIT_FOR_OBP_APPROVALL2 = 290;
        private const int CLCPUR_STATUS_APPROVED = 300;
        private const int CLCPUR_STATUS_REJECTED = 400;
        private const int CLCPUR_STATUS_WAIT_FOR_SENDING = 450;
        private const int CLCPUR_STATUS_SENT = 500;
        private const int CLCPUR_STATUS_FINISHED_WO_SENT = 600;
        private const int CLCPUR_STATUS_PRODIS_CONFIRMED = 700;
        private const int CLCPUR_STATUS_PRODIS_CANCELED = 10;
        private const int CLCPUR_STATUS_MAIL_ERROR = -10;

        private const int REGET_STATUS_TMP_WAIT_FOR_APPROVAL = -100;
        private const int REGET_STATUS_TMP_REJECTED = -200;
        private const int REGET_STATUS_UNKNOWN = -1;
        private const int REGET_STATUS_DRAFT = 100;
        private const int REGET_STATUS_WAIT_FOR_L1 = 200;
        private const int REGET_STATUS_WAIT_FOR_L2 = 300;
        private const int REGET_STATUS_WAIT_FOR_L3 = 400;
        private const int REGET_STATUS_WAIT_FOR_L4 = 500;
        private const int REGET_STATUS_WAIT_FOR_L5 = 600;
        private const int REGET_STATUS_WAIT_FOR_L6 = 610;
        private const int REGET_STATUS_APPROVED_L1 = 700;
        private const int REGET_STATUS_APPROVED_L2 = 800;
        private const int REGET_STATUS_APPROVED_L3 = 900;
        private const int REGET_STATUS_APPROVED_L4 = 1000;
        private const int REGET_STATUS_APPROVED_L5 = 1100;
        private const int REGET_STATUS_APPROVED_L6 = 1110;
        private const int REGET_STATUS_REFUSED_L1 = 1200;
        private const int REGET_STATUS_REFUSED_L2 = 1300;
        private const int REGET_STATUS_REFUSED_L3 = 1400;
        private const int REGET_STATUS_REFUSED_L4 = 1500;
        private const int REGET_STATUS_REFUSED_L5 = 1600;
        private const int REGET_STATUS_REFUSED_L6 = 1610;
        private const int REGET_STATUS_APPROVED = 1700;
        private const int REGET_STATUS_WAIT_FOR_COMMENT = 1750;
        private const int REGET_STATUS_ORDERED = 1800;
        private const int REGET_STATUS_SUPPLIED = 1900;
        private const int REGET_STATUS_CLOSED = 2000;
        private const int REGET_STATUS_CANCELED_REQUESTOR = 10;
        private const int REGET_STATUS_CANCELED_ORDERER = 12;
        private const int REGET_STATUS_CANCELED_SYSTEM = 14;
        #endregion

        #region Enums
        public enum SourceApplication {
            Reget = 1,
            ClcPur = 2
        }
        #endregion

        #region Methods
        private SourceApplication GetOrderSourceApp(string orderNr) {
            if (orderNr.Trim().Length == 8) {
                int result;
                bool isNumber = Int32.TryParse(orderNr, out result);
                if (isNumber) {
                    return SourceApplication.ClcPur;
                }
            }

            return SourceApplication.Reget;
        }

        public Order GetOrderStatus(string orderNr) {
            if (String.IsNullOrEmpty(orderNr)) {
                throw new Exception("Order Number is missing");
            }

            try {
                if (GetOrderSourceApp(orderNr) == SourceApplication.ClcPur) {
                    return GetClcPurOrder(orderNr);
                } else {
                    return GetRegetOrder(orderNr);
                }
                                
            } catch (Exception ex) {
                throw new Exception("An Error Occured: " + ex.ToString());
            }
        }

        private Order GetClcPurOrder(string orderNr) {
            ClcPurPurchOrderRepository clcPurPurchOrderRepository = new ClcPurPurchOrderRepository();
            int iPurchNr = Convert.ToInt32(orderNr);
            Purch_Order_Header purchOrderHeader = clcPurPurchOrderRepository.GetPurchOrderByNr(iPurchNr);

            if (purchOrderHeader == null) {
                return null;
            }

            Order order = GetBlankOrder();
            order.ApplicationName = SourceApplication.ClcPur.ToString();
            order.OrderNr = purchOrderHeader.purch_contract_nr.ToString();
            order.Price = purchOrderHeader.price;
            order.Currency = purchOrderHeader.currency;
            order.BuyerMail = purchOrderHeader.buyer_mail;

            switch (purchOrderHeader.order_status) {
                case CLCPUR_STATUS_APPROVED:
                case CLCPUR_STATUS_FINISHED_WO_SENT:
                case CLCPUR_STATUS_MAIL_ERROR:
                case CLCPUR_STATUS_PRODIS_CONFIRMED:
                case CLCPUR_STATUS_SENT:
                    order.Status = STATUS_APPROVED;
                    order.StatusText = STATUS_TEXT_APPROVED;
                    break;
                case CLCPUR_STATUS_PRODIS_RELEASED:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL1A:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL1B:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL2:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL3:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL4:
                case CLCPUR_STATUS_WAIT_FOR_APPROVALL5:
                case CLCPUR_STATUS_WAIT_FOR_OBP_APPROVALL1:
                case CLCPUR_STATUS_WAIT_FOR_OBP_APPROVALL2:
                case CLCPUR_STATUS_WAIT_FOR_SENDING:
                    order.Status = STATUS_WAITING;
                    order.StatusText = STATUS_TEXT_WAITING;
                    break;
                case CLCPUR_STATUS_REJECTED:
                    order.Status = STATUS_REJECTED;
                    order.StatusText = STATUS_TEXT_REJECTED;
                    break;
                case CLCPUR_STATUS_PRODIS_CANCELED:
                    order.Status = STATUS_CANCELED;
                    order.StatusText = STATUS_TEXT_CANCELED;
                    break;
                default:
                    order.Status = STATUS_UNKNOWN;
                    order.StatusText = STATUS_TEXT_UNKNOWN;
                    break;
            }

            return order;
        }

        private Order GetRegetOrder(string orderNr) {
            RequestRepository regetRequestRepository = new RequestRepository();
            Request_Event requestEvent = regetRequestRepository.GetRequestEventByNr(orderNr);
                        
            if (requestEvent == null) {
                return null;
            }

            RequestMailRepository requestMailRepository = new RequestMailRepository();
            RequestMailLight requestMailLight = requestMailRepository.GetLastMail(requestEvent.id);
            

            Order order = GetBlankOrder();
            order.ApplicationName = SourceApplication.ClcPur.ToString();
            order.OrderNr = requestEvent.request_nr;
            order.Price = requestEvent.estimated_price;
            if (requestEvent.Currency != null) {
                order.Currency = requestEvent.Currency.currency_code;
            }
            if (requestMailLight != null) {
                order.BuyerMail = requestMailLight.sender;
            } else if(requestEvent.Orderer != null) {
                order.BuyerMail = requestEvent.Orderer.email;
            }

            switch (requestEvent.request_status) {
                case REGET_STATUS_APPROVED:
                case REGET_STATUS_CLOSED:
                case REGET_STATUS_ORDERED:
                case REGET_STATUS_SUPPLIED:
                    order.Status = STATUS_APPROVED;
                    order.StatusText = STATUS_TEXT_APPROVED;
                    break;
                case REGET_STATUS_APPROVED_L1:
                case REGET_STATUS_APPROVED_L2:
                case REGET_STATUS_APPROVED_L3:
                case REGET_STATUS_APPROVED_L4:
                case REGET_STATUS_APPROVED_L5:
                case REGET_STATUS_APPROVED_L6:
                case REGET_STATUS_DRAFT:
                case REGET_STATUS_TMP_WAIT_FOR_APPROVAL:
                case REGET_STATUS_WAIT_FOR_COMMENT:
                case REGET_STATUS_WAIT_FOR_L1:
                case REGET_STATUS_WAIT_FOR_L2:
                case REGET_STATUS_WAIT_FOR_L3:
                case REGET_STATUS_WAIT_FOR_L4:
                case REGET_STATUS_WAIT_FOR_L5:
                case REGET_STATUS_WAIT_FOR_L6:
                    order.Status = STATUS_WAITING;
                    order.StatusText = STATUS_TEXT_WAITING;
                    break;
                case REGET_STATUS_CANCELED_ORDERER:
                case REGET_STATUS_CANCELED_REQUESTOR:
                case REGET_STATUS_CANCELED_SYSTEM:
                    order.Status = STATUS_CANCELED;
                    order.StatusText = STATUS_TEXT_CANCELED;
                    break;
                case REGET_STATUS_REFUSED_L1:
                case REGET_STATUS_REFUSED_L2:
                case REGET_STATUS_REFUSED_L3:
                case REGET_STATUS_REFUSED_L4:
                case REGET_STATUS_REFUSED_L5:
                case REGET_STATUS_REFUSED_L6:
                case REGET_STATUS_TMP_REJECTED:
                    order.Status = STATUS_REJECTED;
                    order.StatusText = STATUS_TEXT_REJECTED;
                    break;
                default:
                    order.Status = STATUS_UNKNOWN;
                    order.StatusText = STATUS_TEXT_UNKNOWN;
                    break;
            }

            return order;
        }

        private Order GetBlankOrder() {
            Order order = new Order();
            order.OrderNr = null;
            order.ApplicationName = null;
            order.Status = STATUS_UNKNOWN;
            order.StatusText = STATUS_TEXT_UNKNOWN;
            order.Price = -1;
            order.Currency = null;
            order.BuyerMail = null;

            return order;
        }
        #endregion
    }
}