using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysStatModel
{
    class RAMGraphData
    {
        double[] graphData;

        public RAMGraphData(int size)
        {
            graphData = new double[size];
        }

        public void addValue(double number)
        {
            for (int i = 0; i <= graphData.Length - 2; i++)
            {
                graphData[i] = graphData[i + 1];
            }
            graphData[graphData.Length - 1] = number;
        }

        public double[] getData()
        {
            return graphData;
        }

        public string __print__()
        {
            string str = "";
            foreach (double item in graphData)
            {
                str = str + item.ToString("#.##") + " ";
            }
            return str;
        }
    }
}
