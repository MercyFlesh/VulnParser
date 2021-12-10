using System;
using ExcelDataReader;
using System.IO;
using VulnParser.ViewModels;
using System.Collections.Generic;

namespace VulnParser.Models
{
    public class ParseExcelService
    {
        public static List<Vulnerability> GetVulnsList(string filePath)
        {
            List<Vulnerability> vulnsList = new List<Vulnerability>();

            using (var istream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(istream))
                {
                    while (reader.Read())
                    {
                        if (reader.Depth > 1)
                        {
                            vulnsList.Add(new Vulnerability()
                            {

                                Id = reader.GetDouble(0).ToString(),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Source = reader.GetString(3),
                                ImpactObject = reader.GetString(4),
                                ConfViol = reader.GetDouble(5).ToString() == "1",
                                IntegrViol = reader.GetDouble(6) == 1,
                                AccessViol = reader.GetDouble(7) == 1
                            });
                        }
                    }
                }
            }
            
            return vulnsList;
        }
    }
}
