﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectTemplate.Core.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DomainText {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DomainText() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("ProjectTemplate.Core.Resources.DomainText", typeof(DomainText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string User_CreatedDate {
            get {
                return ResourceManager.GetString("User_CreatedDate", resourceCulture);
            }
        }
        
        public static string User_Email {
            get {
                return ResourceManager.GetString("User_Email", resourceCulture);
            }
        }
        
        public static string User_FullName {
            get {
                return ResourceManager.GetString("User_FullName", resourceCulture);
            }
        }
        
        public static string User_Username {
            get {
                return ResourceManager.GetString("User_Username", resourceCulture);
            }
        }
        
        public static string AuditableEntity_CreatedBy {
            get {
                return ResourceManager.GetString("AuditableEntity_CreatedBy", resourceCulture);
            }
        }
        
        public static string AuditableEntity_CreatedDate {
            get {
                return ResourceManager.GetString("AuditableEntity_CreatedDate", resourceCulture);
            }
        }
        
        public static string AuditableEntity_ModifiedDate {
            get {
                return ResourceManager.GetString("AuditableEntity_ModifiedDate", resourceCulture);
            }
        }
    }
}
