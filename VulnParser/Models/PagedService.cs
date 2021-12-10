using System.Collections.Generic;

namespace VulnParser.Models
{
    public class PagedService<T>
    {
        public static List<T> GetCurrentPage(List<T> allItems, int countItems, int curPage, int countPages)
        {
            List<T> tempPage = new List<T>();

            if (curPage >= 1 && curPage <= countPages)
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
