﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.User {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OnTheRoad.MVC.Resources.User.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Възникна грешка при добавяне в следвани!.
        /// </summary>
        public static string FollowError {
            get {
                return ResourceManager.GetString("FollowError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Успешно добавихте в следвани..
        /// </summary>
        public static string FollowSucces {
            get {
                return ResourceManager.GetString("FollowSucces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Все още нямате съобщения..
        /// </summary>
        public static string NoConversationsYet {
            get {
                return ResourceManager.GetString("NoConversationsYet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Възникна грешка при премахване от следвани!.
        /// </summary>
        public static string UnfollowError {
            get {
                return ResourceManager.GetString("UnfollowError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Успешно премахнахте от следвани..
        /// </summary>
        public static string UnfollowSucces {
            get {
                return ResourceManager.GetString("UnfollowSucces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Възникна грешка при промяна на данните за потребителя!.
        /// </summary>
        public static string UpdateUserError {
            get {
                return ResourceManager.GetString("UpdateUserError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Успешно променихте данните за потребителя..
        /// </summary>
        public static string UpdateUserSuccess {
            get {
                return ResourceManager.GetString("UpdateUserSuccess", resourceCulture);
            }
        }
    }
}
