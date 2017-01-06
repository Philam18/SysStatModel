using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace SysStatModel
{
    class CPUData
    {
        private ManagementObjectSearcher cpuInfo;
        private PerformanceCounter cpuPerformance;
        private string propertyType;
        private string classType;
        
        public CPUData()
        {

        }
        public CPUData(string propertyType, string classType)
        {
            this.propertyType = propertyType;
            this.classType = classType;
            string query = "SELECT " + propertyType + " FROM " + classType;
            cpuInfo = new ManagementObjectSearcher(query);
        }
        public string retrieveCPUName()
        {
            string name = "";
            string query = "SELECT Name FROM Win32_Processor";
            cpuInfo = new ManagementObjectSearcher(query);
            foreach (ManagementObject item in cpuInfo.Get())
            {
                foreach (PropertyData data in item.Properties)
                {
                    name = Convert.ToString(data.Value);
                }
            }
            return name;
        }
        public Tuple<double,double> getCPUStats()
        {
            double maxClock = 0;
            double currentClock = 0;
            string query;
            query = "SELECT MaxClockSpeed FROM Win32_Processor";
            cpuInfo = new ManagementObjectSearcher(query);
            foreach(ManagementObject item in cpuInfo.Get())
            {
                maxClock = Convert.ToDouble(item.Properties["MaxClockSpeed"].Value);
            }
            query = "SELECT CurrentClockSpeed FROM Win32_Processor";
            cpuInfo = new ManagementObjectSearcher(query);
            foreach(ManagementBaseObject item in cpuInfo.Get())
            {
                currentClock = Convert.ToDouble(item.Properties["CurrentClockSpeed"].Value);
            }

            return Tuple.Create(maxClock,currentClock);
        }

        /*
         foreach (ManagementObject queryObj in searcher.Get())
            {
                Console.WriteLine("-----------------------------------");
                foreach (PropertyData data in queryObj.Properties)
                {
                    Console.WriteLine(data.Name + "\t" + data.Value);
                }
            }
        */





    }
}
