using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Eec
{
    public class SystemOverview : DataObject
    {
        private const string _objName = @"api/SystemOverview";
        private const string _rootXmlName = @"DialDataDetail";
        private const ServiceType serviceType = ServiceType.EEC_POLLING;
        private MtuValObject _mtuValObj = new MtuValObject();

        protected override string objectName
        {
            get { return _objName; }
        }

        protected override string rootXmlName
        {
            get { return _rootXmlName; }
        }

        public override ServiceType Type
        {
            get { return serviceType; }
        }

        public class MtuValObject
        {
            private List<Mtu> _mtus;

            public List<Mtu> Mtus
            {
                get { return _mtus; }
                set { _mtus = value; }
            }

            public MtuValObject()
            {
                _mtus = new List<Mtu>();
            }

            public class Mtu
            {
                public struct PhaseCurrentObject
                {
                    public int A { get; set; }
                    public int B { get; set; }
                    public int C { get; set; }

                }

                public struct PhaseVoltageObject
                {
                    public int A { get; set; }
                    public int B { get; set; }
                    public int C { get; set; }
                }

                public int MtuNumber { get; set; }
                public int Value { get; set; }
                public int Kva { get; set; }
                public int Pf { get; set; }
                public int Voltage { get; set; }
                public int Phase { get; set; }
                public int Conn { get; set; }

                public PhaseCurrentObject PhaseCurrent { get; set; }
                public PhaseVoltageObject PhaseVoltage { get; set; }

            }
        }

        public MtuValObject MtuVal {
            get { return _mtuValObj; }
        } 

        protected override bool ParseRawXML()
        {
            MtuValObject mtuVal = new MtuValObject();

            try
            {
                bool mtuFound = true;
                int currMtu = 1;
                while (mtuFound)
                {
                    MtuValObject.Mtu mtu = new MtuValObject.Mtu();
                    mtu.MtuNumber = currMtu;

                    string tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Value", out tempValue))
                        mtu.Value = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/KVA", out tempValue))
                        mtu.Kva = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PF", out tempValue))
                        mtu.Pf = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Voltage", out tempValue))
                    {
                        int outVal = 0;
                        int.TryParse(tempValue, out outVal);
                        mtu.Voltage = outVal;
                    }

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Phase", out tempValue))
                        mtu.Phase = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Conn", out tempValue))
                        mtu.Conn = int.Parse(tempValue);

                    int pcA = 0, pcB = 0, pcC = 0;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseCurrent/A", out tempValue))
                        pcA = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseCurrent/B", out tempValue))
                        pcB = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseCurrent/C", out tempValue))
                        pcC = int.Parse(tempValue);

                    mtu.PhaseCurrent = new MtuValObject.Mtu.PhaseCurrentObject() {
                        A = pcA,
                        B = pcB,
                        C = pcC
                    };

                    int pvA = 0, pvB = 0, pvC = 0;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseVoltage/A", out tempValue))
                        pvA = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseVoltage/B", out tempValue))
                        pvB = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/PhaseVoltage/C", out tempValue))
                        pvC = int.Parse(tempValue);

                    mtu.PhaseVoltage = new MtuValObject.Mtu.PhaseVoltageObject() {
                        A = pvA,
                        B = pvB,
                        C = pvC
                    };

                    mtuVal.Mtus.Add(mtu);
                    currMtu++;

                    if (!base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Value", out tempValue))
                        mtuFound = false;

                }

                _mtuValObj = mtuVal;
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
