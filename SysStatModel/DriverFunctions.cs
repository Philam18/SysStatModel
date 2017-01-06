using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysStatModel
{
    class DriverFunctions
    {
        public const int sizeOfGraphXAxis = 60;
        private const double KILOBYTE = (1 << 10);
        private const double MEGABYTE = (1 << 20);
        private const double GIGABYTE = (1 << 30);
        public static double CURRENTUNIT = KILOBYTE;
        public static string CURRENTUNITSTR = "KB";
        public static RAMGraphData virtualGraph = new RAMGraphData(sizeOfGraphXAxis);
        public static RAMGraphData physicalGraph = new RAMGraphData(sizeOfGraphXAxis);

        /*******************************************    RAM STUFF     ********************************************************/
        public static Tuple<double, double, double> getPhysicalMemoryStats()
        {
            RAMData RAMPhysicalData= new RAMData();
            return RAMPhysicalData.getPhysicalMemoryStats();
        }
        public static Tuple<double, double, double> getVirtualMemoryStats()
        {
            RAMData RAMVirtualData = new RAMData();
            return RAMVirtualData.getVirtualMemoryStats();
        }
        public static double[] retrievePhysicalGraphData()
        {
            return physicalGraph.getData();
        }
        public static double[] retrieveVirtualGraphData()
        {
            return virtualGraph.getData();
        }
        public static double[] getXaxis()
        {
            RAMGraphData xAxis = new RAMGraphData(sizeOfGraphXAxis);
            for (int i = 1; i <= sizeOfGraphXAxis; i++)
            {
                xAxis.addValue(i);
            }
            return xAxis.getData();
        }
        public static  List<string> retrieveRAMName()
        {
            RAMData device = new RAMData("*", "Win32_PhysicalMemory");
            List<string> deviceList = new List<string>();
            deviceList = device.getRAMName();
            return deviceList;
        }
        public static List<List<string[]>> retrieveRAMInfo()
        {
            RAMData device = new RAMData("*", "Win32_PhysicalMemory");
            List<List<string[]>> deviceInfo = new List<List<string[]>>();
            deviceInfo = device.getRAMInfo();
            return deviceInfo;
        }
        /*******************************************    CPU  STUFF     ********************************************************/
        public static string getCPUName()
        {
            CPUData cpu = new CPUData();
            return cpu.retrieveCPUName();
        }
        public static Tuple<double,double> getCPUStats()
        {
            CPUData cpuData = new CPUData();
            return cpuData.getCPUStats();
        }
        /****************************************  CONVERSION METRIC   ********************************************************/
        public static void changeToKB()
        {
            CURRENTUNIT = KILOBYTE;
            CURRENTUNITSTR = "KB";
        }
        public static void changeToMB()
        {
            CURRENTUNIT = MEGABYTE;
            CURRENTUNITSTR = "MB";
        }
        public static void changeToGB()
        {
            CURRENTUNIT = GIGABYTE;
            CURRENTUNITSTR = "GB";
        }
        /*********************************************************************************************************************/
    }
}
