using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TPLSamples.App
{
    class Program
    {

        public class Content
        {
            public string SiteUrl { get; set; }
            public int Len { get; set; }
        }
        public static void doSomething(Task<string> data)
        {
            Console.WriteLine($"result data length : {data.Result.Length}");
        }
        public async static Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //var myTask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(doSomething);

            //Console.WriteLine("Async works");

            //await myTask;

            Console.WriteLine($"Main Thread Id : {Thread.CurrentThread.ManagedThreadId}");

            List<string> urlList = new List<string>
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.cnn.com",
                "https://www.bbc.com"
            };

            List<Task<Content>> tasklist = new List<Task<Content>>();

            urlList.ToList().ForEach(x =>
            {
                tasklist.Add(GetContentAsync(x));
            });

            var contents = await Task.WhenAll(tasklist.ToArray());

            contents.ToList().ForEach(x =>
            {
                Console.WriteLine($"Url : {x.SiteUrl} - Data Length : {x.Len}");
            });


        }

        public static async Task<Content> GetContentAsync(string url)
        {
            Content _content = new Content();
            var data = await new HttpClient().GetStringAsync(url);

            _content.SiteUrl = url;
            _content.Len = data.Length;

            Console.WriteLine($"GetContentAsync Thread Id : {Thread.CurrentThread.ManagedThreadId}");

            return _content;
        }
    }
}
