using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace TedEnergy.Web.API.WebClients
{
    public class XmlWebClient : WebClient
    {
        public XmlWebClient(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentException("Invalid object name passed to webclient.");
            
            if (!objectName.EndsWith(".xml"))
                objectName += ".xml";

            base.Url = ConfigurationManager.AppSettings["TedEnergyBaseApiUrl"];
            if (string.IsNullOrWhiteSpace(base.Url))
                throw new ConfigurationErrorsException("TedEnergyBaseApiUrl missing or invalid in configuration.");

            if (!base.Url.EndsWith("/"))
                base.Url += "/";

            base.Url += objectName;
        }

        public override XmlDocument GetXmlData()
        {
            XmlDocument xmlData = null;
            
            try
            {
                WebRequest req = WebRequest.Create(base.Url);
                WebResponse resp = req.GetResponse();

                xmlData = new XmlDocument();                
                string rawXml = string.Empty;

                using (StreamReader sw = new StreamReader(resp.GetResponseStream()))
                {
                    rawXml = sw.ReadToEnd();
                }
                
                xmlData.LoadXml(rawXml);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return xmlData;
        }
    }
}
