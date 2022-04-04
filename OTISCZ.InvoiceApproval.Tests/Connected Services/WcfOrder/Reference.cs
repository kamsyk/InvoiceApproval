﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OTISCZ.InvoiceApproval.Tests.WcfOrder {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfOrder.IOrderWcf")]
    public interface IOrderWcf {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderWcf/GetOrderStatus", ReplyAction="http://tempuri.org/IOrderWcf/GetOrderStatusResponse")]
        OTISCZ.InvoiceApproval.Models.Order GetOrderStatus(string orderNr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderWcf/GetOrderStatus", ReplyAction="http://tempuri.org/IOrderWcf/GetOrderStatusResponse")]
        System.Threading.Tasks.Task<OTISCZ.InvoiceApproval.Models.Order> GetOrderStatusAsync(string orderNr);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderWcfChannel : OTISCZ.InvoiceApproval.Tests.WcfOrder.IOrderWcf, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderWcfClient : System.ServiceModel.ClientBase<OTISCZ.InvoiceApproval.Tests.WcfOrder.IOrderWcf>, OTISCZ.InvoiceApproval.Tests.WcfOrder.IOrderWcf {
        
        public OrderWcfClient() {
        }
        
        public OrderWcfClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderWcfClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderWcfClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderWcfClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OTISCZ.InvoiceApproval.Models.Order GetOrderStatus(string orderNr) {
            return base.Channel.GetOrderStatus(orderNr);
        }
        
        public System.Threading.Tasks.Task<OTISCZ.InvoiceApproval.Models.Order> GetOrderStatusAsync(string orderNr) {
            return base.Channel.GetOrderStatusAsync(orderNr);
        }
    }
}
