﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lazeez.Resource.Shared {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lazeez.Resource.Shared.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure to delete this record ?.
        /// </summary>
        public static string Msg_ConfirmDelete {
            get {
                return ResourceManager.GetString("Msg_ConfirmDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure to update this record ?.
        /// </summary>
        public static string Msg_ConfirmUpdate {
            get {
                return ResourceManager.GetString("Msg_ConfirmUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing your request..
        /// </summary>
        public static string Msg_Error {
            get {
                return ResourceManager.GetString("Msg_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The number of restaurants allowed for this user is {0} restaurant(s).
        /// </summary>
        public static string Msg_MaxNumberOfRestaurant {
            get {
                return ResourceManager.GetString("Msg_MaxNumberOfRestaurant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry you do not have permission to operate this operation.
        /// </summary>
        public static string Msg_NotAuthorized {
            get {
                return ResourceManager.GetString("Msg_NotAuthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data was saved successfully.
        /// </summary>
        public static string Msg_SaveDone {
            get {
                return ResourceManager.GetString("Msg_SaveDone", resourceCulture);
            }
        }
    }
}
