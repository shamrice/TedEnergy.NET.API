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
                        WriteToFile(endOfFileName, csvBuilder.Build("Now", "TDY", "MTD", "Avg", "Proj", "Voltage", "Phase"));

                    WriteToFile(endOfFileName, csvBuilder.Build(dashData.Now, dashData.Tdy, dashData.Mtd, dashData.Avg,
                        dashData.Proj, dashData.Voltage, dashData.Phase));
                }

                if (null != rate)
                {
                    string endOfFileName = "Rate.csv";
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                        WriteToFile(endOfFileName, csvBuilder.Build("Time", "Value", "Tier", "TOU", "MeterReadDate", "DaysLeft",
                            "PlanType", "TOUDesc", "DemandCharge_InUse", "DemandCharge_DemandBase", "DemandCharge_Power",
                            "DemandCharge_Cost"));

                    WriteToFile(endOfFileName, csvBuilder.Build(rate.Time, rate.Value, rate.Tier, rate.Tou, rate.MeterReadDate,
                        rate.DaysLeft, rate.PlanType, rate.TouDescription, rate.DemandCharge.InUse, rate.DemandCharge.DemandBase,
                        rate.DemandCharge.Power, rate.DemandCharge.Cost));
                }

                if (null != sysOverview)
                {
                    string endOfFileName = "SystemOverview.csv";
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                    {
                        string header = string.Empty;
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

                    string data = string.Empty;
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
                    bool fileExists = File.Exists(this.fileLocation + "\\" + this.baseFileName + endOfFileName);
                    if (!fileExists)
                        WriteToFile(endOfFileName, csvBuilder.Build());

                    WriteToFile(endOfFileName, csvBuilder.Build());
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
