using ClosedXML.Excel;
using VulnParser.ViewModels;
using System.Collections.Generic;

namespace VulnParser.Models
{
    public class ParseExcelService
    {
        public static List<Vulnerability> GetVulnsList(string filePath)
        {
            List<Vulnerability> vulnsList = new List<Vulnerability>();

            using (var workBook = new XLWorkbook(filePath))
            {
                var rows = workBook.Worksheet(1).RowsUsed();
                foreach (var row in rows)
                {
                    if (row.RowNumber() > 2)
                    {
                        vulnsList.Add(new Vulnerability()
                        {

                            Id = row.Cell(1).Value.ToString(),
                            Name = row.Cell(2).Value.ToString(),
                            Description = row.Cell(3).Value.ToString(),
                            Source = row.Cell(3).Value.ToString(),
                            ImpactObject = row.Cell(5).Value.ToString(),
                            ConfViol = row.Cell(6).GetValue<int>() == 1 ? true : false,
                            IntegrViol = row.Cell(7).GetValue<int>() == 1 ? true : false,
                            AccessViol = row.Cell(8).GetValue<int>() == 1 ? true : false
                        });
                    }
                }
            }
          
            return vulnsList;
        }
    }
}
