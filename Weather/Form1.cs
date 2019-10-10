using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private GeoCoordinateWatcher Watcher = null;
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    textBox1.Text = "Cannot find location data";
                }
                else
                {
                    GeoCoordinate location =
                        Watcher.Position.Location;
                    textBox1.Text = location.Latitude.ToString();
                    textBox2.Text = location.Longitude.ToString();
                    Watcher.Stop();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            String API_KEY = "846f12aa31d2907a0bbb26f484c1c60f";
            String cityName = "Moscow";
            String units = "metric";
            String url = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&units=" + units + "&appid=" + API_KEY;
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            WebResponse response = request.GetResponse();
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader r = new StreamReader(s))
                {
                    textBox3.Text = r.ReadToEnd();
                }
            }
            response.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Create the watcher.
            Watcher = new GeoCoordinateWatcher();

            // Catch the StatusChanged event.
            Watcher.StatusChanged += Watcher_StatusChanged;

            // Start the watcher.
            Watcher.Start();
        }
    }
}
