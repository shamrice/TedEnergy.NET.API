using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects
{
    public abstract class DataObject
    {
        protected abstract string objectName { get; }
        protected abstract string rootXmlName { get; }

        protected XmlDocument rawXml;
        protected WebClient webClient;

        protected abstract bool ParseRawXML();

        /// <summary>
        /// Raw XML data retreived from the Ted Energy web services.
        /// </summary>
        public XmlDocument RawXml { get { return rawXml; } }

        /// <summary>
        /// The type of web service the data object is associated with.
        /// </summary>
        public abstract ServiceType Type { get; }

        protected bool TryGetValueFromRawXml(string nodeName, out string value)
        {
            try
            {
                string nodeLocation = "/" + rootXmlName + "/" + nodeName;
                XmlNode node = RawXml.DocumentElement.SelectSingleNode(nodeLocation);

                if (null != node)
                {
                    value = node.InnerText;
                    return true;
                }
            }
            catch { }

            value = string.Empty;
            return false;
        }

        public DataObject()
        {
            webClient = new XmlWebClient(objectName);
            rawXml = webClient.GetXmlData();

            if (!ParseRawXML())
                throw new Exception("Failed to parse data object from TED web services.");
        }

    }
}
