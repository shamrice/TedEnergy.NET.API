using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Mtu
{
    public class Stats : DataObject
    {
        private const string _objName = @"stats";
        private const string _rootXmlName = @"MTU3KStats";
        private const ServiceType serviceType = ServiceType.MTU3K_POLLING;
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

        public struct ReadingsObject
        {
            public int PowerPhaseA { get; set; }
            public int PowerPhaseB { get; set; }
            public int PowerPhaseC { get; set; }
            public int VoltagePhaseA { get; set; }
            public int VoltagePhaseB { get; set; }
            public int VoltagePhaseC { get; set; }
            public int KvaPhaseA { get; set; }
            public int KvaPhaseB { get; set; }
            public int KvaPhaseC { get; set; }
            public int CurrentPhaseA { get; set; }
            public int CurrentPhaseB { get; set; }
            public int CurrentPhaseC { get; set; }
        }

        public struct MeterRunningTotalObject
        {
            public long PhaseA { get; set; }
            public long PhaseB { get; set; }
            public long PhaseC { get; set; }

        }

        public struct SystemObject
        {
            public int PhSign { get; set; }
            public int Status0 { get; set; }
            public int Status1 { get; set; }
            public int AdeRun { get; set; }
            public int AdeReset { get; set; }
        }

        public struct CalibrationObject
        {
            public double VoltageConstA { get; set; }
            public double VoltageConstB { get; set; }
            public double VoltageConstC { get; set; }
            public int VoltageOffsetA { get; set; }
            public int VoltageOffsetB { get; set; }
            public int VoltageOffsetC { get; set; }

            public double CurrentConstA { get; set; }
            public double CurrentConstB { get; set; }
            public double CurrentConstC { get; set; }
            public int CurrentOffsetA { get; set; }
            public int CurrentOffsetB { get; set; }
            public int CurrentOffsetC { get; set; }

            public double PowerConstA { get; set; }
            public double PowerConstB { get; set; }
            public double PowerConstC { get; set; }
            public int PowerOffsetA { get; set; }
            public int PowerOffsetB { get; set; }
            public int PowerOffsetC { get; set; }

            public double KvaConstA { get; set; }
            public double KvaConstB { get; set; }
            public double KvaConstC { get; set; }
            public int KvaOffsetA { get; set; }
            public int KvaOffsetB { get; set; }
            public int KvaOffsetC { get; set; }

        }

        public struct DeltaCalibrationObject
        {
            public double DVoltageConstA { get; set; }
            public double DVoltageConstB { get; set; }
            public double DVoltageConstC { get; set; }
            public int DVoltageOffsetA { get; set; }
            public int DVoltageOffsetB { get; set; }
            public int DVoltageOffsetC { get; set; }

            public double DCurrentConstA { get; set; }
            public double DCurrentConstB { get; set; }
            public double DCurrentConstC { get; set; }
            public int DCurrentOffsetA { get; set; }
            public int DCurrentOffsetB { get; set; }
            public int DCurrentOffsetC { get; set; }

            public double DPowerConstA { get; set; }
            public double DPowerConstB { get; set; }
            public double DPowerConstC { get; set; }
            public int DPowerOffsetA { get; set; }
            public int DPowerOffsetB { get; set; }
            public int DPowerOffsetC { get; set; }

            public double DKvaConstA { get; set; }
            public double DKvaConstB { get; set; }
            public double DKvaConstC { get; set; }
            public int DKvaOffsetA { get; set; }
            public int DKvaOffsetB { get; set; }
            public int DKvaOffsetC { get; set; }
        }

        public struct HGDeltaCalibrationObject
        {
            public double HDVoltageConstA { get; set; }
            public double HDVoltageConstB { get; set; }
            public double HDVoltageConstC { get; set; }
            public int HDVoltageOffsetA { get; set; }
            public int HDVoltageOffsetB { get; set; }
            public int HDVoltageOffsetC { get; set; }

            public double HDCurrentConstA { get; set; }
            public double HDCurrentConstB { get; set; }
            public double HDCurrentConstC { get; set; }
            public int HDCurrentOffsetA { get; set; }
            public int HDCurrentOffsetB { get; set; }
            public int HDCurrentOffsetC { get; set; }

            public double HDPowerConstA { get; set; }
            public double HDPowerConstB { get; set; }
            public double HDPowerConstC { get; set; }
            public int HDPowerOffsetA { get; set; }
            public int HDPowerOffsetB { get; set; }
            public int HDPowerOffsetC { get; set; }

            public double HDKvaConstA { get; set; }
            public double HDKvaConstB { get; set; }
            public double HDKvaConstC { get; set; }
            public int HDKvaOffsetA { get; set; }
            public int HDKvaOffsetB { get; set; }
            public int HDKvaOffsetC { get; set; }
        }

        public class ThirdPartyPostingObject
        {
            public List<ThirdPartyDataObjects> ThirdParty { get; set; }
            public struct ThirdPartyDataObjects
            {
                public int ThirdPartyNumber { get;set; }
                public int Activated { get; set; }
                public int ActStatus { get; set; }
                public int Attempts { get; set; }
                public int Success { get; set; }
                public int Results { get; set; }
                public int TimeStamp { get; set; }
                public string Host { get; set; }
                public int Port { get; set; }
                public string Uri { get; set; }
            }
        }

        public ReadingsObject Readings { get; private set; }
        public MeterRunningTotalObject MeterRunningTotal { get; private set; }
        public SystemObject System { get; private set; }
        public CalibrationObject Calibration { get; private set; }
        public DeltaCalibrationObject DeltaCalibration { get; private set; }
        public HGDeltaCalibrationObject HGDeltaCalibration { get; set; }
        public ThirdPartyPostingObject ThirdPartyPosting { get; private set; }

        public string TimeServer { get; private set; }
        public int EpochTime { get; private set; }
        public int NtpEnabled { get; private set; }
        public int SerialId { get; private set; }
        public string FwVersion { get; private set; }
        public string UiVersion { get; private set; }


        public Stats()
        {
            this.Readings = new ReadingsObject();
            this.MeterRunningTotal = new MeterRunningTotalObject();
            this.System = new SystemObject();
            this.Calibration = new CalibrationObject();
            this.DeltaCalibration = new DeltaCalibrationObject();
            this.HGDeltaCalibration = new HGDeltaCalibrationObject();

            base.webClient = new XmlWebClient(objectName);
            base.rawXml = webClient.GetXmlData();

            if (!ParseRawXML())
                Console.WriteLine("Failure attempting to parse data from web services.");
        }

        private bool ParseRawXML()
        {

            try
            {
                //readings
                ReadingsObject tempReadingObject = new ReadingsObject();
                string tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/PowerPhaseA", out tempValue))
                    tempReadingObject.PowerPhaseA = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/PowerPhaseB", out tempValue))
                    tempReadingObject.PowerPhaseB = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/PowerPhaseC", out tempValue))
                    tempReadingObject.PowerPhaseC = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/VoltagePhaseA", out tempValue))
                    tempReadingObject.VoltagePhaseA = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/VoltagePhaseB", out tempValue))
                    tempReadingObject.VoltagePhaseB = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/VoltagePhaseC", out tempValue))
                    tempReadingObject.VoltagePhaseC = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/KVAPhaseC", out tempValue))
                    tempReadingObject.KvaPhaseA = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/KVAPhaseC", out tempValue))
                    tempReadingObject.KvaPhaseB = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/KVAPhaseC", out tempValue))
                    tempReadingObject.KvaPhaseC = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/CurrentPhaseC", out tempValue))
                    tempReadingObject.CurrentPhaseA = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/CurrentPhaseC", out tempValue))
                    tempReadingObject.CurrentPhaseB = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Readings/CurrentPhaseC", out tempValue))
                    tempReadingObject.CurrentPhaseC = int.Parse(tempValue);
                tempValue = string.Empty;

                this.Readings = tempReadingObject;

                //meter running total
                MeterRunningTotalObject tempMeterRunningTotalObject = new MeterRunningTotalObject();
                if (base.TryGetValueFromRawXml("MeterRunningTotal/PhaseA", out tempValue))
                    tempMeterRunningTotalObject.PhaseA = long.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("MeterRunningTotal/PhaseB", out tempValue))
                    tempMeterRunningTotalObject.PhaseB = long.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("MeterRunningTotal/PhaseC", out tempValue))
                    tempMeterRunningTotalObject.PhaseC = long.Parse(tempValue);
                tempValue = string.Empty;

                this.MeterRunningTotal = tempMeterRunningTotalObject;

                //system
                SystemObject tempSystemObject = new SystemObject();
                if (base.TryGetValueFromRawXml("System/phsign", out tempValue))
                    tempSystemObject.PhSign = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("System/status0", out tempValue))
                    tempSystemObject.Status0 = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("System/status1", out tempValue))
                    tempSystemObject.Status1 = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("System/aderun", out tempValue))
                    tempSystemObject.AdeRun = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("System/adereset", out tempValue))
                    tempSystemObject.AdeReset = int.Parse(tempValue);
                tempValue = string.Empty;

                this.System = tempSystemObject;

                //calibration
                CalibrationObject tempCalibrationObject = new CalibrationObject();

                if (base.TryGetValueFromRawXml("Calibration/VoltageCONSTA", out tempValue))
                    tempCalibrationObject.VoltageConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/VoltageCONSTB", out tempValue))
                    tempCalibrationObject.VoltageConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/VoltageCONSTC", out tempValue))
                    tempCalibrationObject.VoltageConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/VoltageOFFSETA", out tempValue))
                    tempCalibrationObject.VoltageOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/VoltageOFFSETB", out tempValue))
                    tempCalibrationObject.VoltageOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/VoltageOFFSETC", out tempValue))
                    tempCalibrationObject.VoltageOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentCONSTA", out tempValue))
                    tempCalibrationObject.CurrentConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentCONSTB", out tempValue))
                    tempCalibrationObject.CurrentConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentCONSTC", out tempValue))
                    tempCalibrationObject.CurrentConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentOFFSETA", out tempValue))
                    tempCalibrationObject.CurrentOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentOFFSETB", out tempValue))
                    tempCalibrationObject.CurrentOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/CurrentOFFSETC", out tempValue))
                    tempCalibrationObject.CurrentOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("Calibration/PowerCONSTA", out tempValue))
                    tempCalibrationObject.PowerConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/PowerCONSTB", out tempValue))
                    tempCalibrationObject.PowerConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/PowerCONSTC", out tempValue))
                    tempCalibrationObject.PowerConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/PowerOFFSETA", out tempValue))
                    tempCalibrationObject.PowerOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/PowerOFFSETB", out tempValue))
                    tempCalibrationObject.PowerOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/PowerOFFSETC", out tempValue))
                    tempCalibrationObject.PowerOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("Calibration/KVACONSTA", out tempValue))
                    tempCalibrationObject.KvaConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/KVACONSTB", out tempValue))
                    tempCalibrationObject.KvaConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/KVACONSTC", out tempValue))
                    tempCalibrationObject.KvaConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/KVAOFFSETA", out tempValue))
                    tempCalibrationObject.KvaOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/KVAOFFSETB", out tempValue))
                    tempCalibrationObject.KvaOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("Calibration/KVAOFFSETC", out tempValue))
                    tempCalibrationObject.KvaOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                this.Calibration = tempCalibrationObject;


                //Delta calibration
                DeltaCalibrationObject tempDeltaCalibrationObject = new DeltaCalibrationObject();

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageCONSTA", out tempValue))
                    tempDeltaCalibrationObject.DVoltageConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageCONSTB", out tempValue))
                    tempDeltaCalibrationObject.DVoltageConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageCONSTC", out tempValue))
                    tempDeltaCalibrationObject.DVoltageConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageOFFSETA", out tempValue))
                    tempDeltaCalibrationObject.DVoltageOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageOFFSETB", out tempValue))
                    tempDeltaCalibrationObject.DVoltageOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DVoltageOFFSETC", out tempValue))
                    tempDeltaCalibrationObject.DVoltageOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentCONSTA", out tempValue))
                    tempDeltaCalibrationObject.DCurrentConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentCONSTB", out tempValue))
                    tempDeltaCalibrationObject.DCurrentConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentCONSTC", out tempValue))
                    tempDeltaCalibrationObject.DCurrentConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentOFFSETA", out tempValue))
                    tempDeltaCalibrationObject.DCurrentOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentOFFSETB", out tempValue))
                    tempDeltaCalibrationObject.DCurrentOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DCurrentOFFSETC", out tempValue))
                    tempDeltaCalibrationObject.DCurrentOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerCONSTA", out tempValue))
                    tempDeltaCalibrationObject.DPowerConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerCONSTB", out tempValue))
                    tempDeltaCalibrationObject.DPowerConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerCONSTC", out tempValue))
                    tempDeltaCalibrationObject.DPowerConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerOFFSETA", out tempValue))
                    tempDeltaCalibrationObject.DPowerOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerOFFSETB", out tempValue))
                    tempDeltaCalibrationObject.DPowerOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DPowerOFFSETC", out tempValue))
                    tempDeltaCalibrationObject.DPowerOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVACONSTA", out tempValue))
                    tempDeltaCalibrationObject.DKvaConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVACONSTB", out tempValue))
                    tempDeltaCalibrationObject.DKvaConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVACONSTC", out tempValue))
                    tempDeltaCalibrationObject.DKvaConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVAOFFSETA", out tempValue))
                    tempDeltaCalibrationObject.DKvaOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVAOFFSETB", out tempValue))
                    tempDeltaCalibrationObject.DKvaOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("DeltaCalibration/DKVAOFFSETC", out tempValue))
                    tempDeltaCalibrationObject.DKvaOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                this.DeltaCalibration = tempDeltaCalibrationObject;



                //HGDeltaCalibration
                HGDeltaCalibrationObject tempHGDeltaCalibrationObject = new HGDeltaCalibrationObject();

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageCONSTA", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageCONSTB", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageCONSTC", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageOFFSETA", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageOFFSETB", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDVoltageOFFSETC", out tempValue))
                    tempHGDeltaCalibrationObject.HDVoltageOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentCONSTA", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentCONSTB", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentCONSTC", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentOFFSETA", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentOFFSETB", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDCurrentOFFSETC", out tempValue))
                    tempHGDeltaCalibrationObject.HDCurrentOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerCONSTA", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerCONSTB", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerCONSTC", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerOFFSETA", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerOFFSETB", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDPowerOFFSETC", out tempValue))
                    tempHGDeltaCalibrationObject.HDPowerOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;


                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVACONSTA", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaConstA = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVACONSTB", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaConstB = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVACONSTC", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaConstC = double.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVAOFFSETA", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaOffsetA = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVAOFFSETB", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaOffsetB = int.Parse(tempValue);
                tempValue = string.Empty;

                if (base.TryGetValueFromRawXml("HGDeltaCalibration/HDKVAOFFSETC", out tempValue))
                    tempHGDeltaCalibrationObject.HDKvaOffsetC = int.Parse(tempValue);
                tempValue = string.Empty;

                this.HGDeltaCalibration = tempHGDeltaCalibrationObject;


                //Third Party.

                this.ThirdPartyPosting = new ThirdPartyPostingObject();
                this.ThirdPartyPosting.ThirdParty = new List<ThirdPartyPostingObject.ThirdPartyDataObjects>();
                /*
                bool thirdPartyFound = true;
                int currThirdParty = 1;
                while (thirdPartyFound)
                {
                    ThirdPartyPostingObject.ThirdPartyDataObjects thirdParty = new ThirdPartyPostingObject.ThirdPartyDataObjects();
                    thirdParty.ThirdPartyNumber = currThirdParty;

                    tempValue = string.Empty;
                    if (base.TryGetValueFromRawXml("MTUVal/MTU" + currMtu + "/Value", out tempValue))
                        mtu.Value = int.Parse(tempValue);




                    this.ThirdPartyPosting.ThirdParty.Add(thirdParty);

                    currThirdParty++;

                    if (!base.TryGetValueFromRawXml("ThirdPartyPosting/ThirdParty" + currMtu + "/Value", out tempValue))
                        mtuFound = false;

                }

                _mtuValObj = mtuVal;
                */

                //base level variables
                if (base.TryGetValueFromRawXml("TimeServer", out tempValue))
                    this.TimeServer = tempValue;
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("EpochTime", out tempValue))
                    this.EpochTime = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("NTPEnabled", out tempValue))
                    this.NtpEnabled = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("SerialID", out tempValue))
                    this.SerialId = int.Parse(tempValue);
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("FWVersion", out tempValue))
                    this.FwVersion = tempValue;
                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("UIVersion", out tempValue))
                    this.UiVersion = tempValue;
                tempValue = string.Empty;


                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
