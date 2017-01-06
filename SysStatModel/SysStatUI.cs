using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SysStatModel
{
    public partial class SysStatUI : Form
    {


        public SysStatUI()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            updatePhysMemVal();
            updateVirtMemVal();
            updateGraph();
            getCPUStats();
        }
        /***********************************************************************/
        private void getCPUName()
        {
            tab2Label1.Text = DriverFunctions.getCPUName();
        }
        private void getCPUStats()
        {
            Tuple<double, double> list = DriverFunctions.getCPUStats();
            group3Label2.Text = Convert.ToString(list.Item1);
            group3Label4.Text = Convert.ToString(list.Item2);
        }
        /***********************************************************************/
        private void getRAMInfo()
        {
            int NAME = 0;
            int VALUE = 1;
            List<string> ramList = DriverFunctions.retrieveRAMName();
            List<List<string[]>> ramInfo = DriverFunctions.retrieveRAMInfo();
            for(int i = 0; i < ramList.Count; i++)
            {
                ListViewGroup groupName = new ListViewGroup(ramList[i]);
                listView1.Groups.Add(groupName);
                foreach(string[] item in ramInfo[i])
                {
                    string[] str = new string[] {item[NAME], item[VALUE]};
                    ListViewItem addItem = new ListViewItem(str,groupName);
                    listView1.Items.Add(addItem);
                }
            }
        }
        private void updatePhysMemVal()
        {
            Tuple<double,double,double> list = DriverFunctions.getPhysicalMemoryStats();
            group1Label2.Text = list.Item1.ToString("0.###") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
            group1Label4.Text = list.Item2.ToString("0.###") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
            group1Label6.Text = list.Item3.ToString("0.###") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
        }
        private void updateVirtMemVal()
        {
            Tuple<double, double, double> list = DriverFunctions.getVirtualMemoryStats();
            group2Label2.Text = list.Item1.ToString("0.##") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
            group2Label4.Text = list.Item2.ToString("0.##") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
            group2Label6.Text = list.Item3.ToString("0.##") + " " + SysStatModel.DriverFunctions.CURRENTUNITSTR;
        }
        private void updateGraph()
        {
            double[] xAxis = DriverFunctions.getXaxis();
            double[] data = DriverFunctions.retrievePhysicalGraphData();
            group1Label7.Text = data[data.Length - 1] + " %";
            chart1.Series["Series1"].Points.DataBindXY(xAxis,data);
            data = DriverFunctions.retrieveVirtualGraphData();
            group2Label7.Text = data[data.Length - 1] + " %";
            chart2.Series["Series2"].Points.DataBindXY(xAxis, data);
        }
        /***********************************************************************/
        private void kbRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (kbRadioButton.Checked.Equals(true))
            {
                DriverFunctions.changeToKB();
                forceTimerFire();
            }
        }
        private void mbRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (mbRadioButton.Checked.Equals(true))
            {
                DriverFunctions.changeToMB();
                forceTimerFire();
            }
        }
        private void gbRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (gbRadioButton.Checked.Equals(true))
            {
                DriverFunctions.changeToGB();
                forceTimerFire();
            }
        }
        /***********************************************************************/

        private void forceTimerFire()
        {
            timer_Tick(null, null);
        }

        private void SysStatUI_Load(object sender, EventArgs e)
        {
            //initiate all data values by manuially firing the Timer
            forceTimerFire();
            //initiate all chart properties to host usage-graph
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = DriverFunctions.sizeOfGraphXAxis;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 20;
            this.chart1.Series["Series1"].Color = Color.DarkGreen;
            this.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = 1;
            this.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = DriverFunctions.sizeOfGraphXAxis;
            this.chart2.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            this.chart2.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            this.chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            this.chart2.ChartAreas["ChartArea1"].AxisY.Interval = 20;
            this.chart2.Series["Series2"].Color = Color.DarkGreen;
            //initiate listView to host memory info metadata
            ColumnHeader header1, header2;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header1.Text = "Attribute";
            header1.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Value";
            header2.TextAlign = HorizontalAlignment.Left;
            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            getRAMInfo();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //TAB 2 CPU Name initializa
            getCPUName();

        }
    }

}
