﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SepCommon.SepActivation {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="jActivation", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class jActivation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LicenseKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SoftwareEditionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExpireDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PurchaseDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModuleListField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string LicenseKey {
            get {
                return this.LicenseKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.LicenseKeyField, value) != true)) {
                    this.LicenseKeyField = value;
                    this.RaisePropertyChanged("LicenseKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string SoftwareEdition {
            get {
                return this.SoftwareEditionField;
            }
            set {
                if ((object.ReferenceEquals(this.SoftwareEditionField, value) != true)) {
                    this.SoftwareEditionField = value;
                    this.RaisePropertyChanged("SoftwareEdition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ExpireDate {
            get {
                return this.ExpireDateField;
            }
            set {
                if ((object.ReferenceEquals(this.ExpireDateField, value) != true)) {
                    this.ExpireDateField = value;
                    this.RaisePropertyChanged("ExpireDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string PurchaseDate {
            get {
                return this.PurchaseDateField;
            }
            set {
                if ((object.ReferenceEquals(this.PurchaseDateField, value) != true)) {
                    this.PurchaseDateField = value;
                    this.RaisePropertyChanged("PurchaseDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string ModuleList {
            get {
                return this.ModuleListField;
            }
            set {
                if ((object.ReferenceEquals(this.ModuleListField, value) != true)) {
                    this.ModuleListField = value;
                    this.RaisePropertyChanged("ModuleList");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SepActivation.activationSoap")]
    public interface activationSoap {
        
        // CODEGEN: Generating message contract since element name Username from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_License_Details", ReplyAction="*")]
        SepCommon.SepActivation.Get_License_DetailsResponse Get_License_Details(SepCommon.SepActivation.Get_License_DetailsRequest request);
        
        // CODEGEN: Generating message contract since element name APIKey from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/New_License", ReplyAction="*")]
        SepCommon.SepActivation.New_LicenseResponse New_License(SepCommon.SepActivation.New_LicenseRequest request);
        
        // CODEGEN: Generating message contract since element name APIKey from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AESEncryption", ReplyAction="*")]
        SepCommon.SepActivation.AESEncryptionResponse AESEncryption(SepCommon.SepActivation.AESEncryptionRequest request);
        
        // CODEGEN: Generating message contract since element name Username from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Check_License", ReplyAction="*")]
        SepCommon.SepActivation.Check_LicenseResponse Check_License(SepCommon.SepActivation.Check_LicenseRequest request);
        
        // CODEGEN: Generating message contract since element name Get_VersionResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get_Version", ReplyAction="*")]
        SepCommon.SepActivation.Get_VersionResponse Get_Version(SepCommon.SepActivation.Get_VersionRequest request);
        
        // CODEGEN: Generating message contract since element name version from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Load_Tip", ReplyAction="*")]
        SepCommon.SepActivation.Load_TipResponse Load_Tip(SepCommon.SepActivation.Load_TipRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Get_License_DetailsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Get_License_Details", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Get_License_DetailsRequestBody Body;
        
        public Get_License_DetailsRequest() {
        }
        
        public Get_License_DetailsRequest(SepCommon.SepActivation.Get_License_DetailsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Get_License_DetailsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string LicenseKey;
        
        public Get_License_DetailsRequestBody() {
        }
        
        public Get_License_DetailsRequestBody(string Username, string Password, string LicenseKey) {
            this.Username = Username;
            this.Password = Password;
            this.LicenseKey = LicenseKey;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Get_License_DetailsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Get_License_DetailsResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Get_License_DetailsResponseBody Body;
        
        public Get_License_DetailsResponse() {
        }
        
        public Get_License_DetailsResponse(SepCommon.SepActivation.Get_License_DetailsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Get_License_DetailsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SepCommon.SepActivation.jActivation Get_License_DetailsResult;
        
        public Get_License_DetailsResponseBody() {
        }
        
        public Get_License_DetailsResponseBody(SepCommon.SepActivation.jActivation Get_License_DetailsResult) {
            this.Get_License_DetailsResult = Get_License_DetailsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class New_LicenseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="New_License", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.New_LicenseRequestBody Body;
        
        public New_LicenseRequest() {
        }
        
        public New_LicenseRequest(SepCommon.SepActivation.New_LicenseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class New_LicenseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string APIKey;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EmailAddress;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string FirstName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string LastName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string StreetAddress;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string City;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string State;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string PostalCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string Country;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string PhoneNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string SecretQuestion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string SecretAnswer;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string ReferrerSite;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=15)]
        public bool FromPlesk;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=16)]
        public string Reseller_Id;
        
        public New_LicenseRequestBody() {
        }
        
        public New_LicenseRequestBody(
                    string APIKey, 
                    string Username, 
                    string Password, 
                    string EmailAddress, 
                    string FirstName, 
                    string LastName, 
                    string StreetAddress, 
                    string City, 
                    string State, 
                    string PostalCode, 
                    string Country, 
                    string PhoneNumber, 
                    string SecretQuestion, 
                    string SecretAnswer, 
                    string ReferrerSite, 
                    bool FromPlesk, 
                    string Reseller_Id) {
            this.APIKey = APIKey;
            this.Username = Username;
            this.Password = Password;
            this.EmailAddress = EmailAddress;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.StreetAddress = StreetAddress;
            this.City = City;
            this.State = State;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.PhoneNumber = PhoneNumber;
            this.SecretQuestion = SecretQuestion;
            this.SecretAnswer = SecretAnswer;
            this.ReferrerSite = ReferrerSite;
            this.FromPlesk = FromPlesk;
            this.Reseller_Id = Reseller_Id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class New_LicenseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="New_LicenseResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.New_LicenseResponseBody Body;
        
        public New_LicenseResponse() {
        }
        
        public New_LicenseResponse(SepCommon.SepActivation.New_LicenseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class New_LicenseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string New_LicenseResult;
        
        public New_LicenseResponseBody() {
        }
        
        public New_LicenseResponseBody(string New_LicenseResult) {
            this.New_LicenseResult = New_LicenseResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AESEncryptionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AESEncryption", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.AESEncryptionRequestBody Body;
        
        public AESEncryptionRequest() {
        }
        
        public AESEncryptionRequest(SepCommon.SepActivation.AESEncryptionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AESEncryptionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string APIKey;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string EncryptString;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string EncryptKey;
        
        public AESEncryptionRequestBody() {
        }
        
        public AESEncryptionRequestBody(string APIKey, string EncryptString, string EncryptKey) {
            this.APIKey = APIKey;
            this.EncryptString = EncryptString;
            this.EncryptKey = EncryptKey;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AESEncryptionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AESEncryptionResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.AESEncryptionResponseBody Body;
        
        public AESEncryptionResponse() {
        }
        
        public AESEncryptionResponse(SepCommon.SepActivation.AESEncryptionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AESEncryptionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string AESEncryptionResult;
        
        public AESEncryptionResponseBody() {
        }
        
        public AESEncryptionResponseBody(string AESEncryptionResult) {
            this.AESEncryptionResult = AESEncryptionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Check_LicenseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Check_License", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Check_LicenseRequestBody Body;
        
        public Check_LicenseRequest() {
        }
        
        public Check_LicenseRequest(SepCommon.SepActivation.Check_LicenseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Check_LicenseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string LicenseKey;
        
        public Check_LicenseRequestBody() {
        }
        
        public Check_LicenseRequestBody(string Username, string Password, string LicenseKey) {
            this.Username = Username;
            this.Password = Password;
            this.LicenseKey = LicenseKey;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Check_LicenseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Check_LicenseResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Check_LicenseResponseBody Body;
        
        public Check_LicenseResponse() {
        }
        
        public Check_LicenseResponse(SepCommon.SepActivation.Check_LicenseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Check_LicenseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Check_LicenseResult;
        
        public Check_LicenseResponseBody() {
        }
        
        public Check_LicenseResponseBody(string Check_LicenseResult) {
            this.Check_LicenseResult = Check_LicenseResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Get_VersionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Get_Version", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Get_VersionRequestBody Body;
        
        public Get_VersionRequest() {
        }
        
        public Get_VersionRequest(SepCommon.SepActivation.Get_VersionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class Get_VersionRequestBody {
        
        public Get_VersionRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Get_VersionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Get_VersionResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Get_VersionResponseBody Body;
        
        public Get_VersionResponse() {
        }
        
        public Get_VersionResponse(SepCommon.SepActivation.Get_VersionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Get_VersionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Get_VersionResult;
        
        public Get_VersionResponseBody() {
        }
        
        public Get_VersionResponseBody(string Get_VersionResult) {
            this.Get_VersionResult = Get_VersionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Load_TipRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Load_Tip", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Load_TipRequestBody Body;
        
        public Load_TipRequest() {
        }
        
        public Load_TipRequest(SepCommon.SepActivation.Load_TipRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Load_TipRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string version;
        
        public Load_TipRequestBody() {
        }
        
        public Load_TipRequestBody(string version) {
            this.version = version;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Load_TipResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Load_TipResponse", Namespace="http://tempuri.org/", Order=0)]
        public SepCommon.SepActivation.Load_TipResponseBody Body;
        
        public Load_TipResponse() {
        }
        
        public Load_TipResponse(SepCommon.SepActivation.Load_TipResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Load_TipResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Load_TipResult;
        
        public Load_TipResponseBody() {
        }
        
        public Load_TipResponseBody(string Load_TipResult) {
            this.Load_TipResult = Load_TipResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface activationSoapChannel : SepCommon.SepActivation.activationSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class activationSoapClient : System.ServiceModel.ClientBase<SepCommon.SepActivation.activationSoap>, SepCommon.SepActivation.activationSoap {
        
        public activationSoapClient() {
        }
        
        public activationSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public activationSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public activationSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public activationSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.Get_License_DetailsResponse SepCommon.SepActivation.activationSoap.Get_License_Details(SepCommon.SepActivation.Get_License_DetailsRequest request) {
            return base.Channel.Get_License_Details(request);
        }
        
        public SepCommon.SepActivation.jActivation Get_License_Details(string Username, string Password, string LicenseKey) {
            SepCommon.SepActivation.Get_License_DetailsRequest inValue = new SepCommon.SepActivation.Get_License_DetailsRequest();
            inValue.Body = new SepCommon.SepActivation.Get_License_DetailsRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.LicenseKey = LicenseKey;
            SepCommon.SepActivation.Get_License_DetailsResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).Get_License_Details(inValue);
            return retVal.Body.Get_License_DetailsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.New_LicenseResponse SepCommon.SepActivation.activationSoap.New_License(SepCommon.SepActivation.New_LicenseRequest request) {
            return base.Channel.New_License(request);
        }
        
        public string New_License(
                    string APIKey, 
                    string Username, 
                    string Password, 
                    string EmailAddress, 
                    string FirstName, 
                    string LastName, 
                    string StreetAddress, 
                    string City, 
                    string State, 
                    string PostalCode, 
                    string Country, 
                    string PhoneNumber, 
                    string SecretQuestion, 
                    string SecretAnswer, 
                    string ReferrerSite, 
                    bool FromPlesk, 
                    string Reseller_Id) {
            SepCommon.SepActivation.New_LicenseRequest inValue = new SepCommon.SepActivation.New_LicenseRequest();
            inValue.Body = new SepCommon.SepActivation.New_LicenseRequestBody();
            inValue.Body.APIKey = APIKey;
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.EmailAddress = EmailAddress;
            inValue.Body.FirstName = FirstName;
            inValue.Body.LastName = LastName;
            inValue.Body.StreetAddress = StreetAddress;
            inValue.Body.City = City;
            inValue.Body.State = State;
            inValue.Body.PostalCode = PostalCode;
            inValue.Body.Country = Country;
            inValue.Body.PhoneNumber = PhoneNumber;
            inValue.Body.SecretQuestion = SecretQuestion;
            inValue.Body.SecretAnswer = SecretAnswer;
            inValue.Body.ReferrerSite = ReferrerSite;
            inValue.Body.FromPlesk = FromPlesk;
            inValue.Body.Reseller_Id = Reseller_Id;
            SepCommon.SepActivation.New_LicenseResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).New_License(inValue);
            return retVal.Body.New_LicenseResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.AESEncryptionResponse SepCommon.SepActivation.activationSoap.AESEncryption(SepCommon.SepActivation.AESEncryptionRequest request) {
            return base.Channel.AESEncryption(request);
        }
        
        public string AESEncryption(string APIKey, string EncryptString, string EncryptKey) {
            SepCommon.SepActivation.AESEncryptionRequest inValue = new SepCommon.SepActivation.AESEncryptionRequest();
            inValue.Body = new SepCommon.SepActivation.AESEncryptionRequestBody();
            inValue.Body.APIKey = APIKey;
            inValue.Body.EncryptString = EncryptString;
            inValue.Body.EncryptKey = EncryptKey;
            SepCommon.SepActivation.AESEncryptionResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).AESEncryption(inValue);
            return retVal.Body.AESEncryptionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.Check_LicenseResponse SepCommon.SepActivation.activationSoap.Check_License(SepCommon.SepActivation.Check_LicenseRequest request) {
            return base.Channel.Check_License(request);
        }
        
        public string Check_License(string Username, string Password, string LicenseKey) {
            SepCommon.SepActivation.Check_LicenseRequest inValue = new SepCommon.SepActivation.Check_LicenseRequest();
            inValue.Body = new SepCommon.SepActivation.Check_LicenseRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.LicenseKey = LicenseKey;
            SepCommon.SepActivation.Check_LicenseResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).Check_License(inValue);
            return retVal.Body.Check_LicenseResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.Get_VersionResponse SepCommon.SepActivation.activationSoap.Get_Version(SepCommon.SepActivation.Get_VersionRequest request) {
            return base.Channel.Get_Version(request);
        }
        
        public string Get_Version() {
            SepCommon.SepActivation.Get_VersionRequest inValue = new SepCommon.SepActivation.Get_VersionRequest();
            inValue.Body = new SepCommon.SepActivation.Get_VersionRequestBody();
            SepCommon.SepActivation.Get_VersionResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).Get_Version(inValue);
            return retVal.Body.Get_VersionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SepCommon.SepActivation.Load_TipResponse SepCommon.SepActivation.activationSoap.Load_Tip(SepCommon.SepActivation.Load_TipRequest request) {
            return base.Channel.Load_Tip(request);
        }
        
        public string Load_Tip(string version) {
            SepCommon.SepActivation.Load_TipRequest inValue = new SepCommon.SepActivation.Load_TipRequest();
            inValue.Body = new SepCommon.SepActivation.Load_TipRequestBody();
            inValue.Body.version = version;
            SepCommon.SepActivation.Load_TipResponse retVal = ((SepCommon.SepActivation.activationSoap)(this)).Load_Tip(inValue);
            return retVal.Body.Load_TipResult;
        }
    }
}
