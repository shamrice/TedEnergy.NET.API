using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.DataObjects;
using TedEnergy.Web.API.DataObjects.Eec;
using TedEnergy.Web.API.DataObjects.Mtu3k;
using TedEnergy.Web.API.WebClients;

namespace DataExporter
{
    public class DataExporterServices
    {
        private readonly IList<TedEnergyWebApi> webApis;
        private readonly ServicesConfiguration config;

        public DataExporterServices(ServicesConfiguration configuration)
        {
            this.config = configuration;
            this.webApis = new List<TedEnergyWebApi>();

            foreach (ServiceType serviceType in this.config.ConfiguredTypesOfServices)            
                this.webApis.Add(TedEnergyWebApiBuilder.Build(serviceType));

            foreach (TedEnergyWebApi webApi in this.webApis)
            {
                if (null != webApi)
                    webApi.RefreshDataObjectCache();
                else
                    throw new NullReferenceException("TedEnergyWebApiBuilder returned a null value attempting to build TedEnergyApi using the " +
                        "configuration suppled.");
            }
        }

        public string DebugTests()
        {
            string result = string.Empty;            
            
            var eecApi = webApis.OfType<EccPollingApi>().SingleOrDefault();
            DashData dashData = eecApi.GetDataObjectCache().OfType<DashData>().SingleOrDefault();
            Rate rate = eecApi.GetDataObjectCache().OfType<Rate>().SingleOrDefault();
            SystemOverview sysOverview = eecApi.GetDataObjectCache().OfType<SystemOverview>().SingleOrDefault();

            var tedApi = webApis.OfType<Ted500PollingApi>().SingleOrDefault();
            Stats stats = tedApi.GetDataObjectCache().OfType<Stats>().SingleOrDefault();
                    
            if (null != dashData)
            {
                result += "DashData:" + Environment.NewLine;
                result += "Now: " + dashData.Now + Environment.NewLine;
                result += "Tdy: " + dashData.Tdy + Environment.NewLine;
                result += "Mtd: " + dashData.Mtd + Environment.NewLine;
                result += "Avg: " + dashData.Avg + Environment.NewLine;
                result += "Proj: " + dashData.Proj + Environment.NewLine;
                result += "Voltage: " + dashData.Voltage + Environment.NewLine;
                result += "Phase: " + dashData.Phase + Environment.NewLine + Environment.NewLine;
            }
      
            if (null != rate)
            {
                result += "Rate: " + Environment.NewLine;
                result += "Time: " + rate.Time + Environment.NewLine;
                result += "Value: " + rate.Value + Environment.NewLine;
                result += "Tier: " + rate.Tier + Environment.NewLine;
                result += "TOU: " + rate.Tou + Environment.NewLine;
                result += "MeterReadDate: " + rate.MeterReadDate + Environment.NewLine;
                result += "DaysLeft: " + rate.DaysLeft + Environment.NewLine;
                result += "PlanType: " + rate.PlanType + Environment.NewLine;
                result += "TouDescription: " + rate.TouDescription + Environment.NewLine;
                result += "DemandCharge/InUse: " + rate.DemandCharge.InUse + Environment.NewLine;
                result += "DemandCharge/DemandBase: " + rate.DemandCharge.DemandBase + Environment.NewLine;
                result += "DemandCharge/Power: " + rate.DemandCharge.Power + Environment.NewLine;
                result += "DemandCharge/Cost: " + rate.DemandCharge.Cost + Environment.NewLine + Environment.NewLine; ;
            }
 
            if (null != sysOverview)
            {
                result += "SystemOverview: " + Environment.NewLine;
                foreach (TedEnergy.Web.API.DataObjects.Eec.SystemOverview.MtuValObject.Mtu mtu in sysOverview.MtuVal.Mtus)
                {
                    result += "MTU" + mtu.MtuNumber + Environment.NewLine;
                    result += "Value: " + mtu.Value + Environment.NewLine;
                    result += "KVA: " + mtu.Kva + Environment.NewLine;
                    result += "PF: " + mtu.Pf + Environment.NewLine;
                    result += "Voltage: " + mtu.Voltage + Environment.NewLine;
                    result += "Phase: " + mtu.Phase + Environment.NewLine;
                    result += "Conn: " + mtu.Conn + Environment.NewLine;
                    result += "PhaseCurrent-A: " + mtu.PhaseCurrent.A + Environment.NewLine;
                    result += "PhaseCurrent-B: " + mtu.PhaseCurrent.B + Environment.NewLine;
                    result += "PhaseCurrent-C: " + mtu.PhaseCurrent.C + Environment.NewLine;
                    result += "PhaseVoltage-A: " + mtu.PhaseVoltage.A + Environment.NewLine;
                    result += "PhaseVoltage-B: " + mtu.PhaseVoltage.B + Environment.NewLine;
                    result += "PhaseVoltage-C: " + mtu.PhaseVoltage.C + Environment.NewLine + Environment.NewLine; ;
                }
            }

            if (null != stats)
            {
                result += "Stats:\n";
                result += "Readings/PowerPhaseA: " + stats.Readings.PowerPhaseA + Environment.NewLine;
                result += "Readings/PowerPhaseB: " + stats.Readings.PowerPhaseB + Environment.NewLine;
                result += "Readings/PowerPhaseC: " + stats.Readings.PowerPhaseC + Environment.NewLine + Environment.NewLine;

                result += "Readings/VoltagePhaseA: " + stats.Readings.VoltagePhaseA + Environment.NewLine;
                result += "Readings/VoltagePhaseB: " + stats.Readings.VoltagePhaseB + Environment.NewLine;
                result += "Readings/VoltagePhaseC: " + stats.Readings.VoltagePhaseC + Environment.NewLine + Environment.NewLine;

                result += "Readings/KVAPhaseA: " + stats.Readings.KvaPhaseA + Environment.NewLine;
                result += "Readings/KVAPhaseB: " + stats.Readings.KvaPhaseB + Environment.NewLine;
                result += "Readings/KVAPhaseC: " + stats.Readings.KvaPhaseC + Environment.NewLine + Environment.NewLine;

                result += "Readings/CurrentPhaseA: " + stats.Readings.CurrentPhaseA + Environment.NewLine;
                result += "Readings/CurrentPhaseB: " + stats.Readings.CurrentPhaseB + Environment.NewLine;
                result += "Readings/CurrentPhaseC: " + stats.Readings.CurrentPhaseC + Environment.NewLine + Environment.NewLine;

                result += "MeterRunningTotal/PhaseA: " + stats.MeterRunningTotal.PhaseA + Environment.NewLine;
                result += "MeterRunningTotal/PhaseB: " + stats.MeterRunningTotal.PhaseB + Environment.NewLine;
                result += "MeterRunningTotal/PhaseC: " + stats.MeterRunningTotal.PhaseC + Environment.NewLine + Environment.NewLine;

                result += "System/phsign: " + stats.System.PhSign + Environment.NewLine;
                result += "System/status0: " + stats.System.Status0 + Environment.NewLine;
                result += "System/status1: " + stats.System.Status1 + Environment.NewLine;
                result += "System/aderun: " + stats.System.AdeRun + Environment.NewLine;
                result += "System/adereset: " + stats.System.AdeReset + Environment.NewLine + Environment.NewLine;

                result += "Calibration/VoltageCONSTA: " + stats.Calibration.VoltageConstA + Environment.NewLine;
                result += "Calibration/VoltageCONSTB: " + stats.Calibration.VoltageConstB + Environment.NewLine;
                result += "Calibration/VoltageCONSTC: " + stats.Calibration.VoltageConstC + Environment.NewLine;
                result += "Calibration/VoltageOFFSETA: " + stats.Calibration.VoltageOffsetA + Environment.NewLine;
                result += "Calibration/VoltageOFFSETB: " + stats.Calibration.VoltageOffsetB + Environment.NewLine;
                result += "Calibration/VoltageOFFSETC: " + stats.Calibration.VoltageOffsetC + Environment.NewLine;

                result += "Calibration/CurrentCONSTA: " + stats.Calibration.CurrentConstA + Environment.NewLine;
                result += "Calibration/CurrentCONSTB: " + stats.Calibration.CurrentConstB + Environment.NewLine;
                result += "Calibration/CurrentCONSTC: " + stats.Calibration.CurrentConstC + Environment.NewLine;
                result += "Calibration/CurrentOFFSETA: " + stats.Calibration.CurrentOffsetA + Environment.NewLine;
                result += "Calibration/CurrentOFFSETB: " + stats.Calibration.CurrentOffsetB + Environment.NewLine;
                result += "Calibration/CurrentOFFSETC: " + stats.Calibration.CurrentOffsetC + Environment.NewLine;

                result += "Calibration/PowerCONSTA: " + stats.Calibration.PowerConstA + Environment.NewLine;
                result += "Calibration/PowerCONSTB: " + stats.Calibration.PowerConstB + Environment.NewLine;
                result += "Calibration/PowerCONSTC: " + stats.Calibration.PowerConstC + Environment.NewLine;
                result += "Calibration/PowerOFFSETA: " + stats.Calibration.PowerOffsetA + Environment.NewLine;
                result += "Calibration/PowerOFFSETB: " + stats.Calibration.PowerOffsetB + Environment.NewLine;
                result += "Calibration/PowerOFFSETC: " + stats.Calibration.PowerOffsetC + Environment.NewLine;

                result += "Calibration/KVACONSTA: " + stats.Calibration.KvaConstA + Environment.NewLine;
                result += "Calibration/KVACONSTB: " + stats.Calibration.KvaConstB + Environment.NewLine;
                result += "Calibration/KVACONSTC: " + stats.Calibration.KvaConstC + Environment.NewLine;
                result += "Calibration/KVAOFFSETA: " + stats.Calibration.KvaOffsetA + Environment.NewLine;
                result += "Calibration/KVAOFFSETB: " + stats.Calibration.KvaOffsetB + Environment.NewLine;
                result += "Calibration/KVAOFFSETC: " + stats.Calibration.KvaOffsetC + Environment.NewLine + Environment.NewLine;


                result += "DeltaCalibration/DVoltageCONSTA: " + stats.DeltaCalibration.DVoltageConstA + Environment.NewLine;
                result += "DeltaCalibration/DVoltageCONSTB: " + stats.DeltaCalibration.DVoltageConstB + Environment.NewLine;
                result += "DeltaCalibration/DVoltageCONSTC: " + stats.DeltaCalibration.DVoltageConstC + Environment.NewLine;
                result += "DeltaCalibration/DVoltageOFFSETA: " + stats.DeltaCalibration.DVoltageOffsetA + Environment.NewLine;
                result += "DeltaCalibration/DVoltageOFFSETB: " + stats.DeltaCalibration.DVoltageOffsetB + Environment.NewLine;
                result += "DeltaCalibration/DVoltageOFFSETC: " + stats.DeltaCalibration.DVoltageOffsetC + Environment.NewLine;

                result += "DeltaCalibration/DCurrentCONSTA: " + stats.DeltaCalibration.DCurrentConstA + Environment.NewLine;
                result += "DeltaCalibration/DCurrentCONSTB: " + stats.DeltaCalibration.DCurrentConstB + Environment.NewLine;
                result += "DeltaCalibration/DCurrentCONSTC: " + stats.DeltaCalibration.DCurrentConstC + Environment.NewLine;
                result += "DeltaCalibration/DCurrentOFFSETA: " + stats.DeltaCalibration.DCurrentOffsetA + Environment.NewLine;
                result += "DeltaCalibration/DCurrentOFFSETB: " + stats.DeltaCalibration.DCurrentOffsetB + Environment.NewLine;
                result += "DeltaCalibration/DCurrentOFFSETC: " + stats.DeltaCalibration.DCurrentOffsetC + Environment.NewLine;

                result += "DeltaCalibration/DPowerCONSTA: " + stats.DeltaCalibration.DPowerConstA + Environment.NewLine;
                result += "DeltaCalibration/DPowerCONSTB: " + stats.DeltaCalibration.DPowerConstB + Environment.NewLine;
                result += "DeltaCalibration/DPowerCONSTC: " + stats.DeltaCalibration.DPowerConstC + Environment.NewLine;
                result += "DeltaCalibration/DPowerOFFSETA: " + stats.DeltaCalibration.DPowerOffsetA + Environment.NewLine;
                result += "DeltaCalibration/DPowerOFFSETB: " + stats.DeltaCalibration.DPowerOffsetB + Environment.NewLine;
                result += "DeltaCalibration/DPowerOFFSETC: " + stats.DeltaCalibration.DPowerOffsetC + Environment.NewLine;

                result += "DeltaCalibration/DKVACONSTA: " + stats.DeltaCalibration.DKvaConstA + Environment.NewLine;
                result += "DeltaCalibration/DKVACONSTB: " + stats.DeltaCalibration.DKvaConstB + Environment.NewLine;
                result += "DeltaCalibration/DKVACONSTC: " + stats.DeltaCalibration.DKvaConstC + Environment.NewLine;
                result += "DeltaCalibration/DKVAOFFSETA: " + stats.DeltaCalibration.DKvaOffsetA + Environment.NewLine;
                result += "DeltaCalibration/DKVAOFFSETB: " + stats.DeltaCalibration.DKvaOffsetB + Environment.NewLine;
                result += "DeltaCalibration/DKVAOFFSETC: " + stats.DeltaCalibration.DKvaOffsetC + Environment.NewLine + Environment.NewLine;

                //
                result += "HGDeltaCalibration/HDVoltageCONSTA: " + stats.HGDeltaCalibration.HDVoltageConstA + Environment.NewLine;
                result += "HGDeltaCalibration/HDVoltageCONSTB: " + stats.HGDeltaCalibration.HDVoltageConstB + Environment.NewLine;
                result += "HGDeltaCalibration/HDVoltageCONSTC: " + stats.HGDeltaCalibration.HDVoltageConstC + Environment.NewLine;
                result += "HGDeltaCalibration/HDVoltageOFFSETA: " + stats.HGDeltaCalibration.HDVoltageOffsetA + Environment.NewLine;
                result += "HGDeltaCalibration/HDVoltageOFFSETB: " + stats.HGDeltaCalibration.HDVoltageOffsetB + Environment.NewLine;
                result += "HGDeltaCalibration/HDVoltageOFFSETC: " + stats.HGDeltaCalibration.HDVoltageOffsetC + Environment.NewLine;

                result += "HGDeltaCalibration/HDCurrentCONSTA: " + stats.HGDeltaCalibration.HDCurrentConstA + Environment.NewLine;
                result += "HGDeltaCalibration/HDCurrentCONSTB: " + stats.HGDeltaCalibration.HDCurrentConstB + Environment.NewLine;
                result += "HGDeltaCalibration/HDCurrentCONSTC: " + stats.HGDeltaCalibration.HDCurrentConstC + Environment.NewLine;
                result += "HGDeltaCalibration/HDCurrentOFFSETA: " + stats.HGDeltaCalibration.HDCurrentOffsetA + Environment.NewLine;
                result += "HGDeltaCalibration/HDCurrentOFFSETB: " + stats.HGDeltaCalibration.HDCurrentOffsetB + Environment.NewLine;
                result += "HGDeltaCalibration/HDCurrentOFFSETC: " + stats.HGDeltaCalibration.HDCurrentOffsetC + Environment.NewLine;

                result += "HGDeltaCalibration/HDPowerCONSTA: " + stats.HGDeltaCalibration.HDPowerConstA + Environment.NewLine;
                result += "HGDeltaCalibration/HDPowerCONSTB: " + stats.HGDeltaCalibration.HDPowerConstB + Environment.NewLine;
                result += "HGDeltaCalibration/HDPowerCONSTC: " + stats.HGDeltaCalibration.HDPowerConstC + Environment.NewLine;
                result += "HGDeltaCalibration/HDPowerOFFSETA: " + stats.HGDeltaCalibration.HDPowerOffsetA + Environment.NewLine;
                result += "HGDeltaCalibration/HDPowerOFFSETB: " + stats.HGDeltaCalibration.HDPowerOffsetB + Environment.NewLine;
                result += "HGDeltaCalibration/HDPowerOFFSETC: " + stats.HGDeltaCalibration.HDPowerOffsetC + Environment.NewLine;

                result += "HGDeltaCalibration/HDKVACONSTA: " + stats.HGDeltaCalibration.HDKvaConstA + Environment.NewLine;
                result += "HGDeltaCalibration/HDKVACONSTB: " + stats.HGDeltaCalibration.HDKvaConstB + Environment.NewLine;
                result += "HGDeltaCalibration/HDKVACONSTC: " + stats.HGDeltaCalibration.HDKvaConstC + Environment.NewLine;
                result += "HGDeltaCalibration/HDKVAOFFSETA: " + stats.HGDeltaCalibration.HDKvaOffsetA + Environment.NewLine;
                result += "HGDeltaCalibration/HDKVAOFFSETB: " + stats.HGDeltaCalibration.HDKvaOffsetB + Environment.NewLine;
                result += "HGDeltaCalibration/HDKVAOFFSETC: " + stats.HGDeltaCalibration.HDKvaOffsetC + Environment.NewLine + Environment.NewLine;

                //third party 
                foreach (var thirdparty in stats.ThirdPartyPosting.ThirdParty)
                {
                    result += "Third Party:" + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Activated: " + thirdparty.Activated + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/ActStatus: " + thirdparty.ActStatus + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Attempts: " + thirdparty.Attempts + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Success: " + thirdparty.Success + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Results: " + thirdparty.Results + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/TimeStamp: " + thirdparty.TimeStamp + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Host: " + thirdparty.Host + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/Port: " + thirdparty.Port + Environment.NewLine;
                    result += "ThirdPartyPosting/ThirdParty/URI: " + thirdparty.Uri + Environment.NewLine + Environment.NewLine;
                }

                result += "TimeServer: " + stats.TimeServer + Environment.NewLine;
                result += "EpochTime: " + stats.EpochTime + Environment.NewLine;
                result += "NTPEnabled: " + stats.NtpEnabled + Environment.NewLine;
                result += "SerialID: " + stats.SerialId + Environment.NewLine;
                result += "FWVersion: " + stats.FwVersion + Environment.NewLine;
                result += "UIVersion: " + stats.UiVersion + Environment.NewLine + Environment.NewLine;
            }

            return result;
        }

    }
}
