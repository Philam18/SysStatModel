using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
/**
 * CIMData is a class that takes 2 variables.
 * pType (property) and cType (class)
 * to query a "property" search in the "class" library.
 * 
 * It uses the .NET WindowsManagementInfrastructure
 * (WMI) class library to 
 * retrieve metadata on different components, such as
 * RAM, CPU, IO, and other controllers.
 */
namespace SysStatModel
{
    class RAMData
    {
        private string propertyType;
        private string classType;
        private List<List<string[]>> infoList;
        private Microsoft.VisualBasic.Devices.ComputerInfo compInfo;
        /*********************************************************************************************************************/
        /*
         * For RAM data retrieval using the VisualBasic.Devices library
         */
        public RAMData()
        {
            compInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
        }
        public Tuple<double,double,double> getPhysicalMemoryStats()
        {
            double totalMemory = this.compInfo.TotalPhysicalMemory;
            double availableMemory = this.compInfo.AvailablePhysicalMemory;
            double total = totalMemory / DriverFunctions.CURRENTUNIT;
            double available = availableMemory / DriverFunctions.CURRENTUNIT;
            double used = total - available;
            double value = Math.Round((used / total * 100), 2);
            DriverFunctions.physicalGraph.addValue(value);
            return Tuple.Create(total, available, used);
        }
        public Tuple<double,double,double> getVirtualMemoryStats()
        {
            double totalMemory = this.compInfo.TotalVirtualMemory;
            double availableMemory = this.compInfo.AvailableVirtualMemory;
            double total = totalMemory / DriverFunctions.CURRENTUNIT;
            double available = availableMemory / DriverFunctions.CURRENTUNIT;
            double used = total - available;
            double value = Math.Round((used / total * 100), 2);
            DriverFunctions.virtualGraph.addValue(value);
            return Tuple.Create(total, available, used);
        }
        /*********************************************************************************************************************/


        /*********************************************************************************************************************/
        /*
         * For RAM data retrieval using the System.Management library
         */
        public RAMData(string propertyType, string classType)
        {
            infoList = new List<List<string[]>>();
            this.propertyType = propertyType;
            this.classType = classType;
            //BEGIN QUERY
            string query = "SELECT " + propertyType + " FROM " + classType;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            buildDeviceList(searcher);
            //FINISH QUERY
        }
        private void buildDeviceList(ManagementObjectSearcher searcher)
        {
            foreach (ManagementObject queryObj in searcher.Get())
            {
                List<String[]> deviceInfo = new List<string[]>();
                foreach(PropertyData data in queryObj.Properties)
                {
                    string[] array = new string[2];
                    array[0] = Convert.ToString(data.Name);
                    array[1] = Convert.ToString(data.Value);
                    deviceInfo.Add(array);
                }
                infoList.Add(deviceInfo);
            }
        }
        public List<string> getRAMName()
        {
            List<string> deviceList = new List<string>();
            foreach(List<string[]> listOfArrays in infoList)
            {
                string name = "DeviceLocator";
                string value = "";
                foreach(string[] array in listOfArrays)
                {
                    if (array[0].Equals(name))
                    {
                        value = array[1];
                    }
                }
                deviceList.Add(value);
            }
            return deviceList;
        }
        public List<List<string[]>> getRAMInfo()
        {
            return infoList;
        }
        /*********************************************************************************************************************/

    }
}
