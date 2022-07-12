using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StringFromListWithVars
{
    class Program
    {
        static List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        static string inputString;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter CSV Location:");
            var csvLoc = Console.ReadLine();
            ReadCSV(csvLoc);

            Console.WriteLine("Enter String Input:");
            var stringLoc = Console.ReadLine();
            ReadString(stringLoc);

            Console.WriteLine("Enter Output Location:");
            var outputLoc = Console.ReadLine();
            CreateOutput(outputLoc);
        }

        static void ReadCSV(string loc)
        {
            try
            {
                var lines = File.ReadAllLines(loc);
                var varNames = lines[0].Split(",");
                lines = lines.Skip(1).ToArray();

                foreach (var item in lines)
                {
                    Dictionary<string, string> el = new Dictionary<string, string>();
                    var els = item.Split(",");
                    int i = 0;
                    foreach (var ele in els)
                    {
                        el.Add(varNames[i], ele);
                        i++;
                    }
                    list.Add(el);
                }
                Console.WriteLine($"CSV imported: {lines.Count()} rows, {varNames.Length} variables");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error reading in CSV: " + ex.Message);
            }
        }

        static void ReadString(string loc)
        {
            try
            {
                inputString = File.ReadAllText(loc);
                Console.WriteLine($"String imported");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading in string: " + ex.Message);
            }
        }

        static void CreateOutput(string output)
        {
            foreach (var item in list)
            {
                string tempString = (string)inputString.Clone();
                foreach (var var in item.Keys)
                {
                    tempString = tempString.Replace("{" + var + "}", item[var]);
                }

                File.WriteAllText(Path.Combine(output, "Output_" + item.Values.ElementAtOrDefault(0) + ".txt"), tempString);
            }
        }
    }
}
