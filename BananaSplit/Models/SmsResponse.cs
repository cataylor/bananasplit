using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BananaSplit.Helpers
{
    public class SmsResponse
    {
        [XmlRoot("Response")]
        public class SmsResponse
        {
            public String Sms { get; set; }
        }
    }
}