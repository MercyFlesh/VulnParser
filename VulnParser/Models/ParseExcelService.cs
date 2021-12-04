using System;
using System.IO;
using System.Windows;
using ExcelDataReader;
using VulnParser.ViwModels;
using System.Collections.ObjectModel;

namespace VulnParser.Models
{
    public class ParseExcelService
    {
        public static ObservableCollection<Vulnerability> GetVulnsList(string filePath)
        {
            ObservableCollection<Vulnerability> vulnsList = new ObservableCollection<Vulnerability>();

            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            
            return vulnsList;
        }
    }
}
