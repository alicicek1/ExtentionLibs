using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UsefulExtensions
{
    class Program
    {
        public class TestEmp
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        static void Main(string[] args)
        {

            string testStr = null;
            testStr = StringExtension.FirstUpperCase(testStr);
            string[] arr = StringExtension.StringToArray(testStr);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();

            var list = configuration.GetSection("TestEmp").Get<List<TestEmp>>();

            foreach (var item in list.DistinctById(a => a.Id))
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("*****************");
            foreach (var item in list.DistinctByIdWithoutYield(a => a.Id))
            {
                Console.WriteLine(item.Name);
            }


            Console.WriteLine(StringExtension.ToLowerCase("ASDFSDFDFwww.googleASDFASDFAWSDF.com", new CultureInfo("tr")));
        }
    }
}
