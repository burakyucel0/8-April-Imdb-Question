using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HelloCSharp
{
    class Program
    {
        static void Main(string[] args)
        {




            List<string> films = new List<string>();
            List<string> movieAndDate = new List<string>();

            using (var webClient = new System.Net.WebClient())
            {
                string result = webClient.DownloadString("https://www.imdb.com/chart/top/");
                webClient.Headers.Add("Accept-Language", "en-us");
                string tBody = "<tbody class=\"lister-list\">";
                int tbodyStartIndex = result.IndexOf(tBody);

                int tBodyEndIndex = result.IndexOf("</tbody>");


                int tbodyLength = result.Length - tbodyStartIndex;

                string tBodyContent = result.Substring(tbodyStartIndex + tBody.Length, (tBodyEndIndex - tbodyStartIndex));

                string[] trList = tBodyContent.Split("</tr>");


                foreach (var item in trList)
                {
                    string[] tdList = item.Split("</td>");

                    if (tdList.Length > 3)
                    {
                        for (int i = 0; i < tdList.Length; i++)
                        {
                            //i == 1 se yani 2. hücreye geçtiysem ( Film ismini oradan alacağım.)
                            if (i == 1)
                            {
                                string tdContent = tdList[i];
                                int aEnd = tdContent.IndexOf("</a>");

                                string filmSubContent = tdContent.Substring(0, aEnd);
                                int upperLastIndex = filmSubContent.LastIndexOf(">") + 1;


                                string filmTitle = filmSubContent.Substring(upperLastIndex, filmSubContent.Length - upperLastIndex);

                                films.Add(filmTitle);

                                int dStart = tdContent.IndexOf("<span class=\"secondaryInfo\">(");  

                                string tempDate = tdContent.Substring(dStart + 29, 4);
                               
                                movieAndDate.Add(filmTitle + " / " + tempDate);
                            }
                         
                        }
                    }
                }

            }



            var orderedList = movieAndDate.OrderBy(v => int.Parse(v.Split(" / ")[1])).ToList();

            foreach (var item in orderedList) Console.WriteLine(item);

            Console.Read();


        }

    }
}



