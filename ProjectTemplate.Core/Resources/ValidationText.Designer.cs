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
    public class ValidationText {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationText() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("ProjectTemplate.Core.Resources.ValidationText", typeof(ValidationText).Assembly);
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
        
        public static string ArabicOnly {
            get {
                return ResourceManager.GetString("ArabicOnly", resourceCulture);
            }
        }
        
        public static string Compare {
            get {
                return ResourceManager.GetString("Compare", resourceCulture);
            }
        }
        
        public static string DateMustBeGreaterThan {
            get {
                return ResourceManager.GetString("DateMustBeGreaterThan", resourceCulture);
            }
        }
        
        public static string DateMustBeGreaterThanOrEqual {
            get {
                return ResourceManager.GetString("DateMustBeGreaterThanOrEqual", resourceCulture);
            }
        }
        
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        public static string EnglishOnly {
            get {
                return ResourceManager.GetString("EnglishOnly", resourceCulture);
            }
        }
        
        public static string GeneralValidation {
            get {
                return ResourceManager.GetString("GeneralValidation", resourceCulture);
            }
        }
        
        public static string MinLength {
            get {
                return ResourceManager.GetString("MinLength", resourceCulture);
            }
        }
        
        public static string MinLength1 {
            get {
                return ResourceManager.GetString("MinLength1", resourceCulture);
            }
        }
        
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
    }
}
