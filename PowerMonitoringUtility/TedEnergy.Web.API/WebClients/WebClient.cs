using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TedEnergy.Web.API.WebClients
{
    public abstract class WebClient
    {
        public string Url { get; protected set; }
        public abstract XmlDocument GetXmlData();
    }
}
