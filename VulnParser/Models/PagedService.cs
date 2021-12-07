using System.Collections.Generic;
using System.Linq;
using VulnParser.ViwModels;

namespace VulnParser.Models
{
    public class PagedService<T>
    {
        public static List<T> GetCurrentPage(List<T> allItems, int countItems, int curPage, int countPages)
        {
            List<T> tempPage = new List<T>();

            if (curPage >= 0 && curPage <= countPages)
            {
                for (int i = 0; i < countItems; i++)
                {
                    int index = (curPage - 1) * countItems + i;
                    if (index >= allItems.Count)
                        break;

                    tempPage.Add(allItems[index]);
                }
            }

            return tempPage;
        }
    }
}
