using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace SandBox_Project
{

    public class MinMax
    {
        public double min = 10000;
        public double max = -10000;
        public List<double> rows_avg = new List<double>();
        public List<double> sum = new List<double>();
    }

    public class DataPrepare
    {           

        static MinMax AF3_minmax;
        static MinMax F7_minmax;
        static MinMax F3_minmax;
        static MinMax FC5_minmax;
        static MinMax T7_minmax;
        static MinMax P7_minmax;
        static MinMax O1_minmax;
        static MinMax O2_minmax;
        static MinMax P8_minmax;
        static MinMax T8_minmax;
        static MinMax FC6_minmax;
        static MinMax F4_minmax;
        static MinMax F8_minmax;
        static MinMax AF4_minmax;

         
        static public void DoPrepare(List<Data_row> database, int slice_num)
        {
            
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            List<Data_row> selected_data = database.Where(x => x.size == 256 && x.data.Min()>4000 && x.data.Max()<4400).OrderBy(x => x.number).ToList();

            List<Data_row> zero = new List<Data_row>();
            List<Data_row> one = new List<Data_row>();
            List<Data_row> two = new List<Data_row>();
            List<Data_row> three = new List<Data_row>();
            List<Data_row> four = new List<Data_row>();
            List<Data_row> five = new List<Data_row>();
            List<Data_row> six = new List<Data_row>();
            List<Data_row> seven = new List<Data_row>();
            List<Data_row> eight = new List<Data_row>();
            List<Data_row> nine = new List<Data_row>();

            AF3_minmax = new MinMax();
            F7_minmax  = new MinMax();
            F3_minmax  = new MinMax();
            FC5_minmax  = new MinMax();
            T7_minmax  = new MinMax();
            P7_minmax  = new MinMax();
            O1_minmax  = new MinMax();
            O2_minmax  = new MinMax();
            P8_minmax  = new MinMax();
            T8_minmax  = new MinMax();
            FC6_minmax  = new MinMax();
            F4_minmax  = new MinMax();
            F8_minmax  = new MinMax();
            AF4_minmax = new MinMax();

            for (int i = 0; i < selected_data.Count; i++)
            {
                if (selected_data[i].number == 0)
                {
                    zero.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 1)
                {
                    one.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 2)
                {
                    two.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 3)
                {
                    three.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 4)
                {
                    four.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 5)
                {
                    five.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 6)
                {
                    six.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 7)
                {
                    seven.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 8)
                {
                    eight.Add(selected_data[i]);
                }
                else if (selected_data[i].number == 9)
                {
                    nine.Add(selected_data[i]);
                }
            }

            List<List<Data_row>> prepared_data = new List<List<Data_row>>();
            prepared_data.Add(zero);
            prepared_data.Add(one);
            prepared_data.Add(two);
            //   prepared_data.Add(three);
            //  prepared_data.Add(four);
            //   prepared_data.Add(five);
            //   prepared_data.Add(six);
            //   prepared_data.Add(seven);
            //  prepared_data.Add(eight);
            //  prepared_data.Add(nine);

            //MinMax
            //MinMax(prepared_data);

            //Avg
            // Avgs(prepared_data);

            //ToTxt

            //csv
            int row_idx = 1;
            string[] dataArray = new string[3641]; //csv
            string strFilePath = "C:/Users/Bence/Desktop/data_"+ slice_num+".csv";
            StringBuilder sbOutput = new StringBuilder(); //csv

            int numberCounter = 0;
            foreach (List<Data_row> numberType in prepared_data)
            {
                string num = numberCounter.ToString();
                StreamWriter sw = new StreamWriter("datas/" + "EEG_" + num + ".txt");
                string actual_measure = "";
                int measueIDX = 0;
                row_idx = 1;

                // sbOutput.AppendLine(string.Join(";", new string[] {"label"}));

                foreach (Data_row row in numberType)
                {
                    //csv
                    dataArray[0] = num.ToString();
                    

                    string s = "";
                    measueIDX++;
                      // double row_min = row.data.Min();
                      // double row_max = row.data.Max();
                    for (int i = 0; i < row.data.Length; i++)
                    {
                        double data_item = row.data[i];

                        //csv
                       // data_item = (data_item - row_min) / (row_max - row_min); //csv normalize
                        dataArray[row_idx] = data_item.ToString(nfi); //csv
                        row_idx++; //csv

                        // double output = NormalizeChannel(data_item, row.channel); //Normalize for each channel
                        // double output = (data_item - row_min) / (row_max - row_min);
                        s += data_item.ToString() + " ";
                    }
                    
                    actual_measure += s;
                    
                    if (measueIDX % 14 == 0)//1measure = 1"line"
                    {
                        // if !csv -> uncomment
                        //actual_measure = actual_measure.Remove(actual_measure.Length - 1);
                        //sw.WriteLine(actual_measure);
                        //actual_measure = "";

                        //csv
                        sbOutput.AppendLine(string.Join(";", dataArray));
                        row_idx = 0;
                    }
                }
                
                sw.Close();
                numberCounter++;

                //csv
                File.WriteAllText(strFilePath, sbOutput.ToString());
            }

            Console.WriteLine("Datas saved");
        }

        private static void MinMax(List<List<Data_row>> prepared_data)
        {
            foreach (List<Data_row> numberType in prepared_data)
            {
                foreach (Data_row row in numberType)
                {
                    double min = 10001;
                    double max = -10001;
                    #region MinMaxIF1
                    if (row.channel == "AF3")
                    {
                        min = AF3_minmax.min;
                        max = AF3_minmax.max;
                    }
                    else if (row.channel == "F7")
                    {
                        min = F7_minmax.min;
                        max = F7_minmax.max;
                    }
                    else if (row.channel == "F3")
                    {
                        min = F3_minmax.min;
                        max = F3_minmax.max;
                    }
                    else if (row.channel == "FC5")
                    {
                        min = FC5_minmax.min;
                        max = FC5_minmax.max;
                    }
                    else if (row.channel == "T7")
                    {
                        min = AF3_minmax.min;
                        max = AF3_minmax.max;
                    }
                    else if (row.channel == "P7")
                    {
                        min = P7_minmax.min;
                        max = P7_minmax.max;
                    }
                    else if (row.channel == "O1")
                    {
                        min = O1_minmax.min;
                        max = O1_minmax.max;
                    }
                    else if (row.channel == "O2")
                    {
                        min = O2_minmax.min;
                        max = O2_minmax.max;
                    }
                    else if (row.channel == "P8")
                    {
                        min = P8_minmax.min;
                        max = P8_minmax.max;
                    }
                    else if (row.channel == "T8")
                    {
                        min = T8_minmax.min;
                        max = T8_minmax.max;
                    }
                    else if (row.channel == "FC6")
                    {
                        min = FC6_minmax.min;
                        max = FC6_minmax.max;
                    }
                    else if (row.channel == "F4")
                    {
                        min = F4_minmax.min;
                        max = F4_minmax.max;
                    }
                    else if (row.channel == "F8")
                    {
                        min = F8_minmax.min;
                        max = F8_minmax.max;
                    }
                    else if (row.channel == "AF4")
                    {
                        min = AF4_minmax.min;
                        max = AF4_minmax.max;
                    }
                    #endregion
                    foreach (double data_item in row.data)

                    {
                        if (data_item < min)
                        {
                            min = data_item;
                        }
                        if (data_item > max)
                        {
                            max = data_item;
                        }
                    }
                    #region MinMaxIF2
                    if (row.channel == "AF3")
                    {
                        AF3_minmax.min = min;
                        AF3_minmax.max = max;
                    }
                    else if (row.channel == "F7")
                    {
                        F7_minmax.min = min;
                        F7_minmax.max = max;
                    }
                    else if (row.channel == "F3")
                    {
                        F3_minmax.min = min;
                        F3_minmax.max = max;
                    }
                    else if (row.channel == "FC5")
                    {
                        FC5_minmax.min = min;
                        FC5_minmax.max = max;
                    }
                    else if (row.channel == "T7")
                    {
                        AF3_minmax.min = min;
                        AF3_minmax.max = max;
                    }
                    else if (row.channel == "P7")
                    {
                        P7_minmax.min = min;
                        P7_minmax.max = max;
                    }
                    else if (row.channel == "O1")
                    {
                        O1_minmax.min = min;
                        O1_minmax.max = max;
                    }
                    else if (row.channel == "O2")
                    {
                        O2_minmax.min = min;
                        O2_minmax.max = max;
                    }
                    else if (row.channel == "P8")
                    {
                        P8_minmax.min = min;
                        P8_minmax.max = max;
                    }
                    else if (row.channel == "T8")
                    {
                        T8_minmax.min = min;
                        T8_minmax.max = max;
                    }
                    else if (row.channel == "FC6")
                    {
                        FC6_minmax.min = min;
                        FC6_minmax.max = max;
                    }
                    else if (row.channel == "F4")
                    {
                        F4_minmax.min = min;
                        F4_minmax.max = max;
                    }
                    else if (row.channel == "F8")
                    {
                        F8_minmax.min = min;
                        F8_minmax.max = max;
                    }
                    else if (row.channel == "AF4")
                    {
                        AF4_minmax.min = min;
                        AF4_minmax.max = max;
                    }
                    #endregion

                }
            }
        }
        private static void Avgs(List<List<Data_row>> prepared_data)
        {
            foreach (List<Data_row> numberType in prepared_data)
            {
                foreach (Data_row row in numberType)
                {
                    double rows_avg = row.data.Average();
                    #region AvgChan
                    if (row.channel == "AF3")
                    {
                        AF3_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            AF3_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "F7")
                    {
                        F7_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            F7_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "F3")
                    {
                        F3_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            F3_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "FC5")
                    {
                        FC5_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            FC5_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "T7")
                    {
                        AF3_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            AF3_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "P7")
                    {
                        P7_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            P7_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "O1")
                    {
                        O1_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            O1_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "O2")
                    {
                        O2_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            O2_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "P8")
                    {
                        P8_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            P8_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "T8")
                    {
                        T8_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            T8_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "FC6")
                    {
                        FC6_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            FC6_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "F4")
                    {
                        F4_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            F4_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "F8")
                    {
                        F8_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            F8_minmax.sum.Add(sum);
                        }
                    }
                    else if (row.channel == "AF4")
                    {
                        AF4_minmax.rows_avg.Add(rows_avg);
                        foreach (double sum in row.data)
                        {
                            AF4_minmax.sum.Add(sum);
                        }
                    }
                    #endregion

                }   
            }
        }
        private static double NormalizeChannel(double data_item,string channel)
        {
            //Standard deviation
            #region StdDev
            double avg = 0;
            double sum = 0;
            int count = 0;
            #region ChannelIF
            if (channel == "AF3")
            {
                avg = AF3_minmax.rows_avg.Average();
                count = (AF3_minmax.sum.Count() )-1;
                sum = AF3_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "F7")
            {
                avg = F7_minmax.rows_avg.Average();
                count = (F7_minmax.sum.Count() ) - 1;
                sum = F7_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "F3")
            {
                avg = F3_minmax.rows_avg.Average();
                count = (F3_minmax.sum.Count() ) - 1;
                sum = F3_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "FC5")
            {
                avg = FC5_minmax.rows_avg.Average();
                count = (FC5_minmax.sum.Count() ) - 1;
                sum = AF3_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "T7")
            {
                avg = T7_minmax.rows_avg.Average();
                count = (T7_minmax.sum.Count() ) - 1;
                sum = T7_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "P7")
            {
                avg = P7_minmax.rows_avg.Average();
                count = (P7_minmax.sum.Count() ) - 1;
                sum = P7_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "O1")
            {
                avg = O1_minmax.rows_avg.Average();
                count = (O1_minmax.sum.Count() ) - 1;
                sum = O1_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "O2")
            {
                avg = O2_minmax.rows_avg.Average();
                count = (O2_minmax.sum.Count() ) - 1;
                sum = O2_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "P8")
            {
                avg = P8_minmax.rows_avg.Average();
                count = (P8_minmax.sum.Count() ) - 1;
                sum = P8_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "T8")
            {
                avg = T8_minmax.rows_avg.Average();
                count = (T8_minmax.sum.Count() ) - 1;
                sum = T8_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "FC6")
            {
                avg = FC6_minmax.rows_avg.Average();
                count = (FC6_minmax.sum.Count() ) - 1;
                sum = FC6_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "F4")
            {
                avg = F4_minmax.rows_avg.Average();
                count = (F4_minmax.sum.Count() ) - 1;
                sum = F4_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "F8")
            {
                avg = F8_minmax.rows_avg.Average();
                count = (F8_minmax.sum.Count() ) - 1;
                sum = F8_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            else if (channel == "AF4")
            {
                avg = AF4_minmax.rows_avg.Average();
                count = (AF4_minmax.sum.Count() ) - 1;
                sum = AF4_minmax.sum.Sum(d => Math.Pow(d - avg, 2));
            }
            #endregion
            double v  =  Math.Sqrt((sum) / count);
            return 0;
            #endregion
        }
    }
}