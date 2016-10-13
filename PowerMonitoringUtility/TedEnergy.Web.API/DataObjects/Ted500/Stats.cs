﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.Web.API.DataObjects.Ted500
{
    public class Stats : DataObject
    {
        private string objName = "stats";
        private string xmlRoot = "TED500STATS";
        private ServiceType serviceType = ServiceType.TED500_POLLING;
        private MtuValObject _mtuValObj;

        protected override string objectName
        {
            get { return objName; }
        }

        protected override string rootXmlName
        {
            get { return xmlRoot; }
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
                public int PowerNow { get; set; }
                public int VoltageNow { get; set; }
                public int KvaNow { get; set; }
                public int MtuRec { get; set; }
                public string MtuSkp { get; set; }
                public int MinCount { get; set; }
                public int ManCalPower { get; set; }
                public int ManCalVoltage { get; set; }
                public string MtuId { get; set; }
                public int MtuVer { get; set; }
                public int Timestamp { get; set; }
                public int MtuCumulative { get; set; }
                public int PfAvg { get; set; }
                public int Uptime { get; set; }
                public int PhaseType { get; set; }
                public string Modulation { get; set; }
                public string SQ { get; set; }
                public int UseTcp { get; set; }                

                public PhaseCurrentObject PhaseCurrent { get; set; }
                public PhaseVoltageObject PhaseVoltage { get; set; }

            }
        }

        public struct BootloaderObject
        {
            public int Uploaded { get; set; }
            public string Sent { get; set; }
        }

        public struct UsbObject
        {
            public int Mode { get; set; }
            public int Status { get; set; }
            public int LastError { get; set;}
            public int ErrorCount { get; set; }
            public int RxCount { get; set; }
            public int TxCount { get; set; }
        }

        public MtuValObject MtuVal
        {
            get { return _mtuValObj; }
        }

        public BootloaderObject Bootloader
        {
            get;
            private set;
        }

        public UsbObject Usb
        {
            get;
            private set;
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
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PowerNow", out tempValue))
                    {
                        int tempInt = 0;
                        int.TryParse(tempValue, out tempInt);
                        mtu.PowerNow = tempInt;
                    }

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/VoltageNow", out tempValue))
                    {
                        int tempInt = 0;
                        int.TryParse(tempValue, out tempInt);
                        mtu.VoltageNow = tempInt;
                    }

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/kVANow", out tempValue))
                        mtu.KvaNow = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MTURec", out tempValue))
                        mtu.MtuRec = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MTUSkp", out tempValue))
                        mtu.MtuSkp = tempValue;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MinCount", out tempValue))
                        mtu.MinCount = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/ManCalPower", out tempValue))
                        mtu.ManCalPower = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/ManCalVoltage", out tempValue))
                        mtu.ManCalVoltage = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MTUID", out tempValue))
                        mtu.MtuId = tempValue;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MTUVER", out tempValue))
                        mtu.MtuVer = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/Timestamp", out tempValue))
                        mtu.Timestamp = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/MTUCumulative", out tempValue))
                        mtu.MtuCumulative = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PFAvg", out tempValue))
                        mtu.PfAvg = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/Uptime", out tempValue))
                        mtu.Uptime = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseType", out tempValue))
                        mtu.PhaseType = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/Modulation", out tempValue))
                        mtu.Modulation = tempValue;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/SQ", out tempValue))
                        mtu.SQ = tempValue;


                    int pcA = 0, pcB = 0, pcC = 0;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseCurrent/A", out tempValue))
                        pcA = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseCurrent/B", out tempValue))
                        pcB = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseCurrent/C", out tempValue))
                        pcC = int.Parse(tempValue);

                    mtu.PhaseCurrent = new MtuValObject.Mtu.PhaseCurrentObject() {
                        A = pcA,
                        B = pcB,
                        C = pcC
                    };

                    int pvA = 0, pvB = 0, pvC = 0;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseVoltage/A", out tempValue))
                        pvA = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseVoltage/B", out tempValue))
                        pvB = int.Parse(tempValue);

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTU" + currMtu + "/PhaseVoltage/C", out tempValue))
                        pvC = int.Parse(tempValue);

                    mtu.PhaseVoltage = new MtuValObject.Mtu.PhaseVoltageObject() {
                        A = pvA,
                        B = pvB,
                        C = pvC
                    };

                    mtuVal.Mtus.Add(mtu);
                    currMtu++;

                    if (!base.TryGetValueFromRawXml("MTU" + currMtu + "/PowerNow", out tempValue))
                        mtuFound = false;

                }

                _mtuValObj = mtuVal;

                //bootloader

                int uploaded = 0;
                string sent = string.Empty;
                string tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("Bootloader/Uploaded", out tempVal))
                {
                    int tempInt = -1;
                    int.TryParse(tempVal, out tempInt);
                    uploaded = tempInt;
                }

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("Bootloader/Sent", out tempVal))
                    sent = tempVal;

                this.Bootloader = new BootloaderObject() {
                    Uploaded = uploaded,
                    Sent = sent
                };


                //usb
                int mode = 0, status = 0, lastError = 0, errorCount = 0, rxCount = 0, txCount = 0;

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/Mode", out tempVal))
                    mode = int.Parse(tempVal);

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/Status", out tempVal))
                    status = int.Parse(tempVal);

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/LastError", out tempVal))
                    lastError = int.Parse(tempVal);

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/ErrorCount", out tempVal))
                    errorCount = int.Parse(tempVal);

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/RxCount", out tempVal))
                    rxCount = int.Parse(tempVal);

                tempVal = string.Empty;
                if (base.TryGetValueFromRawXml("USB/TxCount", out tempVal))
                    txCount = int.Parse(tempVal);

                this.Usb = new UsbObject() {
                    Mode = mode,
                    Status = status,
                    LastError = lastError,
                    ErrorCount = errorCount,
                    RxCount = rxCount,
                    TxCount = txCount
                };

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
