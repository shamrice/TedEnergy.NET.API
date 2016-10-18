using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.DataObjects;

namespace TedEnergy.Web.API
{
    public abstract class TedEnergyWebApi
    {
        protected List<DataObject> dataObjectCache = new List<DataObject>();
        protected object lockObj = new object();

        public abstract ServiceType Type { get; }

        public abstract void RefreshDataObjectCache();

        public List<DataObject> GetDataObjectCache()
        {
            return dataObjectCache;
        } 
        
    }
}
