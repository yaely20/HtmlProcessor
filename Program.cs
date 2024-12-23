using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlProcessor
{
    internal class Program
    {
    static async Task Main(string[] args)
    {
      HtmlLoader loader = new HtmlLoader();
      string url = "https://www.example.com";

      string htmlContent = await loader.Load(url);

      Console.WriteLine(htmlContent);
    }
     
    }  
}
