using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FootballTeamSpin
{
    public partial class MainPage : ContentPage
    {
        private readonly List<string> Teams = new List<string>();
        public MainPage()
        {
            string jsonFileName = "LogoLink.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                Teams = JsonConvert.DeserializeObject<List<string>>(jsonString);
            }
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                int time = 4000;
                Random rand = new Random();
                while (time > 0)
                {
                    Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        ImageBox.Source = Teams[rand.Next(0, Teams.Count)];
                    });
                    Thread.Sleep(100);
                    time -= 100;
                }
            });
        }

    }
}
