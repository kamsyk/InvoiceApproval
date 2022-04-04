﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BulkTest.WcfOrderDebug {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Order", Namespace="http://schemas.datacontract.org/2004/07/OTISCZ.InvoiceApproval.Models")]
    [System.SerializableAttribute()]
    public partial class Order : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ApplicationNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BuyerMailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CurrencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderNrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusTextField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ApplicationName {
            get {
                return this.ApplicationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ApplicationNameField, value) != true)) {
                    this.ApplicationNameField = value;
                    this.RaisePropertyChanged("ApplicationName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BuyerMail {
            get {
                return this.BuyerMailField;
            }
            set {
                if ((object.ReferenceEquals(this.BuyerMailField, value) != true)) {
                    this.BuyerMailField = value;
                    this.RaisePropertyChanged("BuyerMail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Currency {
            get {
                return this.CurrencyField;
            }
            set {
                if ((object.ReferenceEquals(this.CurrencyField, value) != true)) {
                    this.CurrencyField = value;
                    this.RaisePropertyChanged("Currency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderNr {
            get {
                return this.OrderNrField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderNrField, value) != true)) {
                    this.OrderNrField = value;
                    this.RaisePropertyChanged("OrderNr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StatusText {
            get {
                return this.StatusTextField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusTextField, value) != true)) {
                    this.StatusTextField = value;
                    this.RaisePropertyChanged("StatusText");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfOrderDebug.IOrderWcf")]
    public interface IOrderWcf {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderWcf/GetOrderStatus", ReplyAction="http://tempuri.org/IOrderWcf/GetOrderStatusResponse")]
        BulkTest.WcfOrderDebug.Order GetOrderStatus(string orderNr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderWcf/GetOrderStatus", ReplyAction="http://tempuri.org/IOrderWcf/GetOrderStatusResponse")]
        System.Threading.Tasks.Task<BulkTest.WcfOrderDebug.Order> GetOrderStatusAsync(string orderNr);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderWcfChannel : BulkTest.WcfOrderDebug.IOrderWcf, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderWcfClient : System.ServiceModel.ClientBase<BulkTest.WcfOrderDebug.IOrderWcf>, BulkTest.WcfOrderDebug.IOrderWcf {
        
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
        
        public BulkTest.WcfOrderDebug.Order GetOrderStatus(string orderNr) {
            return base.Channel.GetOrderStatus(orderNr);
        }
        
        public System.Threading.Tasks.Task<BulkTest.WcfOrderDebug.Order> GetOrderStatusAsync(string orderNr) {
            return base.Channel.GetOrderStatusAsync(orderNr);
        }
    }
}