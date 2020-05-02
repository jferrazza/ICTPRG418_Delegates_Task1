using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FileParser;

public delegate List<List<string>> Parser(List<List<string>> data);

namespace Delegate_Exercise
{
    class Delegate_Exercise
    {
        static void Main(string[] args)
        {

            CsvHandler csvHandler = new CsvHandler();
            DataParser dataParser = new DataParser();

            Parser parser = dataParser.StripQuotes;
            parser += dataParser.StripWhiteSpace;
            parser += RemoveHashes;

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Working...");
            sw.Start();

            Func<List<List<string>>, List<List<string>>> parserCastedToFunc = handledStuff => parser(handledStuff); //    https://stackoverflow.com/questions/1906787/cast-delegate-to-func-in-c-sharp
            csvHandler.ProcessCsv("../data.csv", "../processed_data.csv", parserCastedToFunc);
            sw.Stop();
            #region just metric stuff because curious
            Console.WriteLine("Done");
            StreamReader sr1 = new StreamReader(new FileStream("../data.csv", FileMode.Open));
            StreamReader sr2 = new StreamReader(new FileStream("../processed_data.csv", FileMode.Open));
            var r1 = sr1.ReadToEnd();
            var r2 = sr2.ReadToEnd();
            Console.WriteLine($"{sw.Elapsed.TotalSeconds} seconds, from {r1.Length} characters to {r2.Length} characters, {r2.Length - r1.Length} characters.");
            #endregion
            System.Diagnostics.Process.Start("NOTEPAD", "../data.csv");
            System.Diagnostics.Process.Start("NOTEPAD", "../processed_data.csv");

        }

        public static List<List<string>> RemoveHashes(List<List<string>> data)
        {
            foreach (var row in data)
            {
                for (var index = 0; index < row.Count; index++)
                {
                    if (row[index][0] == '#')
                        row[index] = row[index].Remove(0, 1);

                }
            }
            return data;

        }
    }
}
