using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;
using GD.Services;

namespace GD
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }

        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            string baseUrl = Configuration["Api:BaseUrl"];
            ApiClient.Instance.SetBaseUrl(baseUrl);

            ApplicationConfiguration.Initialize();
            Application.Run(new DangNhap());
        }
    }
}
