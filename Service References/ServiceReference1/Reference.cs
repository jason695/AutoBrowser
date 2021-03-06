﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.34209
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoBrowser.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.WSSoap")]
    public interface WSSoap {
        
        // CODEGEN: 命名空間 http://tempuri.org/ 的元素名稱  token 未標示為 nillable，正在產生訊息合約
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send", ReplyAction="*")]
        AutoBrowser.ServiceReference1.SendResponse Send(AutoBrowser.ServiceReference1.SendRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send", ReplyAction="*")]
        System.Threading.Tasks.Task<AutoBrowser.ServiceReference1.SendResponse> SendAsync(AutoBrowser.ServiceReference1.SendRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Send", Namespace="http://tempuri.org/", Order=0)]
        public AutoBrowser.ServiceReference1.SendRequestBody Body;
        
        public SendRequest() {
        }
        
        public SendRequest(AutoBrowser.ServiceReference1.SendRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string token;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string msg;
        
        public SendRequestBody() {
        }
        
        public SendRequestBody(string token, string msg) {
            this.token = token;
            this.msg = msg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendResponse", Namespace="http://tempuri.org/", Order=0)]
        public AutoBrowser.ServiceReference1.SendResponseBody Body;
        
        public SendResponse() {
        }
        
        public SendResponse(AutoBrowser.ServiceReference1.SendResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SendResult;
        
        public SendResponseBody() {
        }
        
        public SendResponseBody(string SendResult) {
            this.SendResult = SendResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSSoapChannel : AutoBrowser.ServiceReference1.WSSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSSoapClient : System.ServiceModel.ClientBase<AutoBrowser.ServiceReference1.WSSoap>, AutoBrowser.ServiceReference1.WSSoap {
        
        public WSSoapClient() {
        }
        
        public WSSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AutoBrowser.ServiceReference1.SendResponse AutoBrowser.ServiceReference1.WSSoap.Send(AutoBrowser.ServiceReference1.SendRequest request) {
            return base.Channel.Send(request);
        }
        
        public string Send(string token, string msg) {
            AutoBrowser.ServiceReference1.SendRequest inValue = new AutoBrowser.ServiceReference1.SendRequest();
            inValue.Body = new AutoBrowser.ServiceReference1.SendRequestBody();
            inValue.Body.token = token;
            inValue.Body.msg = msg;
            AutoBrowser.ServiceReference1.SendResponse retVal = ((AutoBrowser.ServiceReference1.WSSoap)(this)).Send(inValue);
            return retVal.Body.SendResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AutoBrowser.ServiceReference1.SendResponse> AutoBrowser.ServiceReference1.WSSoap.SendAsync(AutoBrowser.ServiceReference1.SendRequest request) {
            return base.Channel.SendAsync(request);
        }
        
        public System.Threading.Tasks.Task<AutoBrowser.ServiceReference1.SendResponse> SendAsync(string token, string msg) {
            AutoBrowser.ServiceReference1.SendRequest inValue = new AutoBrowser.ServiceReference1.SendRequest();
            inValue.Body = new AutoBrowser.ServiceReference1.SendRequestBody();
            inValue.Body.token = token;
            inValue.Body.msg = msg;
            return ((AutoBrowser.ServiceReference1.WSSoap)(this)).SendAsync(inValue);
        }
    }
}
