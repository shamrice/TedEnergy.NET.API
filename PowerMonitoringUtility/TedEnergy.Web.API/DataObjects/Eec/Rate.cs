using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Eec
{
    public class Rate : DataObject
    {
        private const string _objName = @"api/Rate";
        private const string _rootXmlName = @"Rate";
        private const ServiceType serviceType = ServiceType.EEC_POLLING;

        protected override string objectName
        {
            get { return _objName; }
        }

        protected override string rootXmlName
        {
            get { return _rootXmlName ; }
        }

        public class DemandChargeDataObject
        {
            public bool InUse { get; private set; }
            public bool DemandBase { get; private set; }

            public int Power { get; private set; }

            public int Cost { get; private set; }

            public DemandChargeDataObject(bool inUse, bool demandBase, int power, int cost)
            {
                this.InUse = inUse;
                this.DemandBase = demandBase;
                this.Power = power;
                this.Cost = cost;
            }
        }

        public int Time { get; private set; }
        public int Value { get; private set; }
        public int Tier { get; private set; }
        public int Tou { get; private set; }
        public int MeterReadDate { get; private set; }
        public int DaysLeft { get; private set; }
        public int PlanType { get; private set; }
        public String TouDescription { get; private set; }

        public DemandChargeDataObject DemandCharge { get; private set; }

        public override ServiceType Type
        {
            get { return serviceType; }
        }


        /// <summary>
        /// HACK - I don't like how any of this works.
        /// </summary>
        /// <returns></returns>
        protected override bool ParseRawXML()
        {
            try
            {
                string tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Time", out tempValue))
                    this.Time = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Value", out tempValue))
                    this.Value = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Tier", out tempValue))
                    this.Tier = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("TOU", out tempValue))
                    this.Tou = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("MeterReadDate", out tempValue))
                    this.MeterReadDate = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("DaysLeft", out tempValue))
                    this.DaysLeft = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("PlanType", out tempValue))
                    this.PlanType = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("TOUDesc", out tempValue))
                    this.TouDescription = tempValue;

                //demand charge
                bool inUse = false, demandBase = false;
                int power = -1, cost = -1;

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("DemandCharge/InUse", out tempValue))
                {
                    if (tempValue.ToLower() == bool.TrueString.ToLower())
                        inUse = true;                   
                }

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("DemandCharge/DemandBase", out tempValue))
                {
                    if (tempValue.ToLower() == bool.TrueString.ToLower())
                        demandBase = true;
                }

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("DemandCharge/Power", out tempValue))
                    power = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("DemandCharge/Cost", out tempValue))
                    cost = int.Parse(tempValue);

                this.DemandCharge = new DemandChargeDataObject(inUse, demandBase, power, cost);

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
