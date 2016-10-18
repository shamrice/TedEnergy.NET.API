using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.DataObjects.Eec;
using TedEnergy.Web.API.DataObjects.Ted500;

namespace DataExporter.Core
{
    public class Exporter
    {
        private readonly string fileLocation;
        private readonly TedEnergyWebApi webApi;
        private string baseFileName;

        private void SetCurrentBaseFileName()
        {
            this.baseFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_" + webApi.Type.ToString() + "-";
        }

        private void WriteToFile(string endOfFileName, string text)
        {
            using (StreamWriter sw = new StreamWriter(this.fileLocation + "\\" + this.baseFileName + endOfFileName, append: true))
            {
                sw.WriteLine(text);
                sw.Close();
            }
        }

        public Exporter(string fileExportLocation, TedEnergyWebApi apiToExport)
        {
            if (null == apiToExport)
                throw new NullReferenceException("TedEnergyWebApiBuilder returned a null value attempting to build TedEnergyApi using the " +
                   "configuration suppled.");

            this.fileLocation = fileExportLocation;
            this.webApi = apiToExport;
        }

        public void ExportToCsv()
        {
            CsvStringBuilder csvBuilder = new CsvStringBuilder();

            SetCurrentBaseFileName();
            webApi.RefreshDataObjectCache();

            if (webApi.Type == ServiceType.EEC_POLLING)
            {
                DashData dashData = webApi.GetDataObjectCache().OfType<DashData>().SingleOrDefault();
                Rate rate = webApi.GetDataObjectCache().OfType<Rate>().SingleOrDefault();
                SystemOverview sysOverview = webApi.GetDataObjectCache().OfType<SystemOverview>().SingleOrDefault();

                if (null != dashData)
                {
                    string endOfFileName = "DashData.csv";
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                        WriteToFile(endOfFileName, csvBuilder.Build("Date", "Now", "TDY", "MTD", "Avg", "Proj", "Voltage", "Phase"));

                    WriteToFile(endOfFileName, csvBuilder.Build(DateTime.Now.ToString(), dashData.Now, dashData.Tdy, dashData.Mtd, dashData.Avg,
                        dashData.Proj, dashData.Voltage, dashData.Phase));
                }

                if (null != rate)
                {
                    string endOfFileName = "Rate.csv";
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                        WriteToFile(endOfFileName, csvBuilder.Build("Date", "Time", "Value", "Tier", "TOU", "MeterReadDate", "DaysLeft",
                            "PlanType", "TOUDesc", "DemandCharge_InUse", "DemandCharge_DemandBase", "DemandCharge_Power",
                            "DemandCharge_Cost"));

                    WriteToFile(endOfFileName, csvBuilder.Build(DateTime.Now.ToString(), rate.Time, rate.Value, rate.Tier, rate.Tou, rate.MeterReadDate,
                        rate.DaysLeft, rate.PlanType, rate.TouDescription, rate.DemandCharge.InUse, rate.DemandCharge.DemandBase,
                        rate.DemandCharge.Power, rate.DemandCharge.Cost));
                }

                if (null != sysOverview)
                {
                    string endOfFileName = "SystemOverview.csv";
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                    {
                        string header = "Date,";
                        foreach (var mtu in sysOverview.MtuVal.Mtus)
                        {
                            string mtuPrefix = "MTU" + mtu.MtuNumber + "_";
                            header += csvBuilder.Build(mtuPrefix + "Value", mtuPrefix + "KVA", mtuPrefix + "PF", mtuPrefix + "Voltage"
                                , mtuPrefix + "Phase", mtuPrefix + "Conn", mtuPrefix + "PhaseCurrent-A", mtuPrefix + "PhaseCurrent-B"
                                , mtuPrefix + "PhaseCurrent-C", mtuPrefix + "PhaseVoltage-A", mtuPrefix + "PhaseVoltage-B",
                                mtuPrefix + "PhaseVoltage-C");
                        }
                        WriteToFile(endOfFileName, header);
                    }

                    string data = DateTime.Now.ToString() + ",";
                    foreach (var mtu in sysOverview.MtuVal.Mtus)
                    {
                        string mtuPrefix = "MTU" + mtu.MtuNumber + "_";
                        data += csvBuilder.Build(mtu.Value, mtu.Kva, mtu.Pf, mtu.Voltage
                            , mtu.Phase, mtu.Conn, mtu.PhaseCurrent.A, mtu.PhaseCurrent.B
                            , mtu.PhaseCurrent.C, mtu.PhaseVoltage.A, mtu.PhaseVoltage.B,
                            mtu.PhaseVoltage.C);
                    }
                    WriteToFile(endOfFileName, data);
                    
                }

            }
            else if (webApi.Type == ServiceType.TED500_POLLING)
            {
                Stats tedStats = webApi.GetDataObjectCache().OfType<Stats>().SingleOrDefault();

                if (null != tedStats)
                {
                    string endOfFileName = "Stats.csv";

                    //write header if new file.
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                    {
                        string header = "Date,";
                        foreach (var mtu in tedStats.MtuVal.Mtus)
                        {
                            string mtuPrefix = "MTU" + mtu.MtuNumber + "_";
                            header += csvBuilder.Build(mtuPrefix + "PowerNow", mtuPrefix + "VoltageNow",
                                mtuPrefix + "KvaNow", mtuPrefix + "MtuRec", mtuPrefix + "MtuSkp", mtuPrefix + "MinCount",
                                 mtuPrefix + "ManCalPower", mtuPrefix + "ManCalVoltage", mtuPrefix + "MtuId",
                                  mtuPrefix + "MtuVer", mtuPrefix + "Timestamp", mtuPrefix + "MtuCumulative",
                                   mtuPrefix + "PfAvg", mtuPrefix + "Uptime", mtuPrefix + "PhaseType", mtuPrefix + "Modulation",
                                    mtuPrefix + "SQ", mtuPrefix + "PhaseCurrent.A", mtuPrefix + "PhaseCurrent.B",
                                     mtuPrefix + "PhaseCurrent.C", mtuPrefix + "PhaseVoltage.A", mtuPrefix + "PhaseVoltage.B",
                                      mtuPrefix + "PhaseVoltage.C", mtuPrefix + "UseTcp");
                        }
                        int thirdPartyNum = 1;
                        foreach (var thirdParty in tedStats.ThirdPartyPosting.ThirdParty)
                        {
                            string thirdPartyPrefix = "ThirdParty" + thirdPartyNum + "_";

                            header += csvBuilder.Build(thirdPartyPrefix + "Activated", thirdPartyPrefix + "ActStatus", thirdPartyPrefix + "Attempts"
                            , thirdPartyPrefix + "Success", thirdPartyPrefix + "Results", thirdPartyPrefix + "TimeStamp"
                            , thirdPartyPrefix + "Host", thirdPartyPrefix + "Port", thirdPartyPrefix + "URI");

                            thirdPartyNum++;
                        }

                        header += csvBuilder.Build("ThirdParty_Date", "ThirdParty_Uptime", "ThirdParty_LastMTUID");

                        header += csvBuilder.Build("Bootloader_Uploaded", "Bootloader_Sent");
                        header += csvBuilder.Build("USB_Mode", "USB_Status", "USB_LastError", "USB_ErrorCount", "USB_RxCount", "USB_TxCount");
                        
                        WriteToFile(endOfFileName, header);
                    }

                    //write data.
                    string data = DateTime.Now.ToString() + ",";
                    foreach (var mtu in tedStats.MtuVal.Mtus)
                    {
                        data += csvBuilder.Build(mtu.PowerNow, mtu.VoltageNow,
                                mtu.KvaNow, mtu.MtuRec, mtu.MtuSkp, mtu.MinCount,
                                 mtu.ManCalPower, mtu.ManCalVoltage, mtu.MtuId,
                                  mtu.MtuVer, mtu.Timestamp, mtu.MtuCumulative,
                                   mtu.PfAvg, mtu.Uptime, mtu.PhaseType, mtu.Modulation,
                                    mtu.SQ, mtu.PhaseCurrent.A, mtu.PhaseCurrent.B,
                                     mtu.PhaseCurrent.C, mtu.PhaseVoltage.A, mtu.PhaseVoltage.B,
                                      mtu.PhaseVoltage.C, mtu.UseTcp);                        
                    }

                    foreach (var thirdParty in tedStats.ThirdPartyPosting.ThirdParty)
                    {
                        data += csvBuilder.Build(thirdParty.Activated,thirdParty.ActStatus,thirdParty.Attempts,thirdParty.Success,
                            thirdParty.Results,thirdParty.TimeStamp,thirdParty.Host,thirdParty.Port,thirdParty.Uri);
                    }

                    data += csvBuilder.Build("NULL", "NULL", "NULL");
                    data += csvBuilder.Build(tedStats.Bootloader.Uploaded, tedStats.Bootloader.Sent);
                    data += csvBuilder.Build(tedStats.Usb.Mode, tedStats.Usb.Status, tedStats.Usb.LastError, tedStats.Usb.ErrorCount
                        , tedStats.Usb.RxCount, tedStats.Usb.TxCount);

                    WriteToFile(endOfFileName, data);
                }
            }
        }



        public string DebugTests()
        {
            webApi.RefreshDataObjectCache();
            string result = string.Empty;

            DashData dashData = webApi.GetDataObjectCache().OfType<DashData>().SingleOrDefault();
            Rate rate = webApi.GetDataObjectCache().OfType<Rate>().SingleOrDefault();
            SystemOverview sysOverview = webApi.GetDataObjectCache().OfType<SystemOverview>().SingleOrDefault();

            Stats tedStats = webApi.GetDataObjectCache().OfType<Stats>().SingleOrDefault();

            if (null != dashData)
                result += dashData.ToString();

            if (null != rate)
                result += rate.ToString();

            if (null != sysOverview)
                result += sysOverview.ToString();

            if (null != tedStats)
                result += tedStats.ToString();

            return result;
        }
    }
}
