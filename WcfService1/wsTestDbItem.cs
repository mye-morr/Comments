using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService1
{
    [DataContract]
    public class wsTestDbItem
    {
        [DataMember]
        public int numRow { get; set; }

        [DataMember]
        public string sCat { get; set; }

        [DataMember]
        public string sSubcat { get; set; }

        [DataMember]
        public string sItem { get; set; }

        [DataMember]
        public string sDialog { get; set; }
    }
}