﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FishingBotFoffosEdition.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableImageLog {
            get {
                return ((bool)(this["EnableImageLog"]));
            }
            set {
                this["EnableImageLog"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DefaultDeviceIndex {
            get {
                return ((int)(this["DefaultDeviceIndex"]));
            }
            set {
                this["DefaultDeviceIndex"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.10")]
        public decimal DefaultVolumeTreshold {
            get {
                return ((decimal)(this["DefaultVolumeTreshold"]));
            }
            set {
                this["DefaultVolumeTreshold"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public int DefaultExecuteTaskNumber {
            get {
                return ((int)(this["DefaultExecuteTaskNumber"]));
            }
            set {
                this["DefaultExecuteTaskNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ForceLureFinding {
            get {
                return ((bool)(this["ForceLureFinding"]));
            }
            set {
                this["ForceLureFinding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public decimal MinimalPrecisionRequired {
            get {
                return ((decimal)(this["MinimalPrecisionRequired"]));
            }
            set {
                this["MinimalPrecisionRequired"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int DebugRuns {
            get {
                return ((int)(this["DebugRuns"]));
            }
            set {
                this["DebugRuns"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int YOffset {
            get {
                return ((int)(this["YOffset"]));
            }
            set {
                this["YOffset"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int XOffset {
            get {
                return ((int)(this["XOffset"]));
            }
            set {
                this["XOffset"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("default")]
        public string DefaultTemplate {
            get {
                return ((string)(this["DefaultTemplate"]));
            }
            set {
                this["DefaultTemplate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2500")]
        public int DefaultMsBeforeSearch {
            get {
                return ((int)(this["DefaultMsBeforeSearch"]));
            }
            set {
                this["DefaultMsBeforeSearch"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AppData/Resources/BuffImage.png")]
        public string DefaultBuffImagePath {
            get {
                return ((string)(this["DefaultBuffImagePath"]));
            }
            set {
                this["DefaultBuffImagePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultRefreshBuff {
            get {
                return ((bool)(this["DefaultRefreshBuff"]));
            }
            set {
                this["DefaultRefreshBuff"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DefaultMouseLockOnWow {
            get {
                return ((bool)(this["DefaultMouseLockOnWow"]));
            }
            set {
                this["DefaultMouseLockOnWow"] = value;
            }
        }
    }
}
