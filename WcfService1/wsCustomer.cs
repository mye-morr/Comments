using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService1
{
    [DataContract]
    public class wsCustomer
    {
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}