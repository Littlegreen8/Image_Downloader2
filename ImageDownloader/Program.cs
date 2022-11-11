using System;
using System.Linq;
using System.Threading;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;



namespace ImageDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(100);

            File.Delete(@"C:\Users\62677\source\repos\Downloader2\ImageDownloader\bin\Debug\chromedriver.exe");
            ZipFile.ExtractToDirectory(@"C:\Users\62677\source\repos\Downloader2\ImageDownloader\bin\Debug\chromedriver_win32.zip", @"C:\Users\62677\source\repos\Downloader2\ImageDownloader\bin\Debug");

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--user-data-dir="+@"C:\Users\62677\AppData\Local\Google\Chrome\User Data\");
            var driver = new ChromeDriver(chromeOptions);

            for (int k = 1; k < 100; k++)
            { 
                string LandingUrl = "https://www.xrmnw.cc/plus/search/index.asp?keyword=%E9%B1%BC%E5%AD%90%E9%85%B1fish&searchtype=title&p=" + k;
                driver.Navigate().GoToUrl(LandingUrl);

                Thread.Sleep(100);

                var Project = driver.FindElements(By.TagName("b"));

                Console.WriteLine();

                for (int i = 0; i < 10; i++)
                {
                    Project = driver.FindElements(By.TagName("span"));
                    Project[i].Click();
                    
                    var y = 1;
                    
                    string Name = driver.Title;
                    Name = Name.Remove(Name.Length - 7, 7);
                    var newDirectory = Directory.CreateDirectory(@"E:\\Image Downloaded\\" + Name);
                    Thread.Sleep(5000);

                    var Images = driver.FindElements(By.TagName("img"));

                    for (int x = 0; x < Images.Count()-3; x++)
                    {
                        var ImageUrl = Images[x].GetAttribute("src");

                        WebClient Downloader = new WebClient();
                        Downloader.DownloadFile(ImageUrl, newDirectory.FullName + "\\" + y + ".jpg");
                        y++;
                    } 

                    Thread.Sleep(500);
                    driver.Navigate().Back();
                    Thread.Sleep(100);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
