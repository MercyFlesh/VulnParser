using ClosedXML.Excel;
using VulnParser.ViewModels;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Reflection;
using System.IO;

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

        public static void SaveVulnsList(List<Vulnerability> vulns)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("WhiteSheet");

                ws.Range(ws.Cell(1, 1), ws.Cell(1, 5)).Merge();
                ws.Range(ws.Cell(1, 6), ws.Cell(1, 8)).Merge();
                ws.Cell(1, 1).Value = "Общая информация";
                ws.Cell(1, 6).Value = "Последствия";
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 6).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "Идентификатор угрозы";
                ws.Cell(2, 2).Value = "Наименование угрозы";
                ws.Cell(2, 3).Value = "Описание угрозы";
                ws.Cell(2, 4).Value = "Источник угрозы";
                ws.Cell(2, 5).Value = "Объект воздействия угрозы";
                ws.Cell(2, 6).Value = "Нарушение конфиденциальности";
                ws.Cell(2, 7).Value = "Нарушение целостности";
                ws.Cell(2, 8).Value = "Нарушение доступности";

                for (int i = 0; i < vulns.Count; i++)
                {
                    ws.Cell(i + 3, 1).Value = vulns[i].Id;
                    ws.Cell(i + 3, 2).Value = vulns[i].Name;
                    ws.Cell(i + 3, 3).Value = vulns[i].Description;
                    ws.Cell(i + 3, 4).Value = vulns[i].Source;
                    ws.Cell(i + 3, 5).Value = vulns[i].ImpactObject;
                    ws.Cell(i + 3, 6).Value = vulns[i].ConfViol;
                    ws.Cell(i + 3, 7).Value = vulns[i].IntegrViol;
                    ws.Cell(i + 3, 8).Value = vulns[i].AccessViol;

                    ws.Cell(i + 3, 1).WorksheetColumn().Width = 10;
                    ws.Cell(i + 3, 2).WorksheetColumn().Width = 20;
                    ws.Cell(i + 3, 3).WorksheetColumn().Width = 20;
                    ws.Cell(i + 3, 4).WorksheetColumn().Width = 20;
                    ws.Cell(i + 3, 5).WorksheetColumn().Width = 20;
                    ws.Cell(i + 3, 6).WorksheetColumn().Width = 15;
                    ws.Cell(i + 3, 7).WorksheetColumn().Width = 15;
                    ws.Cell(i + 3, 8).WorksheetColumn().Width = 15;
                }

                ws.Rows().Height = 15;
                ws.Rows().Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                
                var dlg = new SaveFileDialog()
                {
                    Filter = "Книга Excel (*.xlsx)|*.xlsx",
                    InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                };

                if (dlg.ShowDialog() == true)
                    wb.SaveAs(dlg.FileName);
            }
        }
    }
}
