using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScript
{
    public class ResultWriter
    {
        public void WriteResults<T>(List<T> results, DateTime dateTime)
        {
            var filename = dateTime.ToString("MMM-dd__HH-mm-ss_", CultureInfo.InvariantCulture) + typeof(T).Name;
            using (var writer = new StreamWriter(filename + ".csv") { NewLine = "\r\n" })
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.WriteHeader(typeof(T));
                csvWriter.NextRecord();
                csvWriter.WriteRecords(results);
            }
        }
    }
}
