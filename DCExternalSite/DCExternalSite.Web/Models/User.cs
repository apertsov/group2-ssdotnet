﻿namespace DCExternalSite.Web
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using DCExternalSite.Web;


    /// <summary>
    /// Class containing information about the authenticated user.
    /// </summary>
    public partial class User : UserBase
    {
        //// NOTE: Profile properties can be added for use in Silverlight application.
        //// To enable profiles, edit the appropriate section of web.config file.
        ////
        //// public string MyProfileProperty { get; set; }

        /// <summary>
        /// Gets and sets the friendly name of the user.
        /// </summary>
        public string FriendlyName { get; set; }
        public int ID { get; set; }
        //public int ID_Patient { get; set; }
        //public string Email { get; set; }
    }
}
