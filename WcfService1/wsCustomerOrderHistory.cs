using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService1
{
    [DataContract]
    [Serializable]
    public class wsCustomerOrderHistory
    {
        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Total { get; set; }
    }
}