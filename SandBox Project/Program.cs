using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SandBox_Project
{
    class Program
    {
        static List<Data_row> database = new List<Data_row>();
        static Stopwatch sw = new Stopwatch();

        static void Main(string[] args)
        {
            sw.Start();

            for (int i = 9; i < 10; i++)
            {
                
                database = new List<Data_row>();
                StreamReader sr = new StreamReader("C:/Users/Bence/Source/Repos/C#/BigData Slice/BigData Slice/bin/Debug/data_" + i + ".txt");


                while (!sr.EndOfStream)
                {
                    //    for (int i = 0; i < 1; i++)
                    //{
                    PrepareTxt(sr.ReadLine());
                }
                sr.Close();

                RemoveThatNotEpocOrRandomData();

                DataPrepare.DoPrepare(database,i);

                //Pieces of datas from each number
                //var q = from meausre in database
                //        where meausre.size == 260
                //        orderby meausre.number ascending
                //        group meausre by meausre.number into g
                //        select new
                //        {
                //            Category = g.Key,
                //            count = g.Count()
                //        };


                //foreach (var item in q)
                //{
                //    Console.WriteLine(item.Category + ": " + item.count + "db");
                //}

                //Time pieaces
                //var q = from measure in database
                //        orderby measure.size ascending
                //        group measure by measure.size into g
                //        select new
                //        {
                //            size = g.Key,
                //            count = g.Count()
                //        };
                //foreach (var item in q)
                //{
                //    Console.WriteLine(item.size + ": " + item.count + "db");
                //}


                //Write out database infos
                //int idx = 0;
                // foreach (Data_row item in database)
                // { 
                //     Console.Write(++idx /*+ ". id: " + item.id*/ + ". Event: " + item.event_id  /*+ ", device: " + item.device*/
                //         + ", channel: " + item.channel + ", number: " + item.number + ", Datas: " + item.data.Length + "\n");
                // }

                sw.Stop();
                Console.WriteLine("\n-------------\nTime: " + sw.ElapsedMilliseconds + "ms");
                Console.WriteLine("Measures:(database/14): " + database.Count() / 14 + "db <--EGÉSZ SZÁMNAK KELL LENNIE!");
                Console.Beep(800, 3);
            }
            Console.ReadLine();
        }
        static void PrepareTxt(string row)
        {
            Data_row actual_data = new Data_row();
            string[] data_line = row.Split('\t');
            actual_data.id = int.Parse(data_line[0]);
            actual_data.event_id = int.Parse(data_line[1]);
            actual_data.device = data_line[2];
            actual_data.channel = data_line[3];
            actual_data.number = int.Parse(data_line[4]);
            actual_data.size = int.Parse(data_line[5]);
            actual_data.data = CreateData(data_line[6]);
            database.Add(actual_data);
        }
        //random data's code is -1 (2,226db)
        private static void RemoveThatNotEpocOrRandomData()
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].device != "EP" /*|| database[i].number==-1*/)
                {
                    database.Remove(database[i]);
                }
            }
        }
        private static double[] CreateData(string datas)
        {
            string[] s_array = datas.Split(',');

            double[] d_array = new double[s_array.Length];
            for (int i = 0; i < s_array.Length; i++)
            {
                s_array[i] = s_array[i].Replace('.', ',');
                d_array[i] = double.Parse(s_array[i]);
            }

            return d_array;
        }
    }
}
