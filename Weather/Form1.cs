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
        AllWeatherInfo AllWeatherInfo;
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

        private void Button2_Click(object sender, EventArgs e)
        {
            String API_KEY = "846f12aa31d2907a0bbb26f484c1c60f";
            String cityName = textBox4.Text;
            String units = "metric";
            String str = "";
            String url = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&units=" + units + "&appid=" + API_KEY;
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            WebResponse response = request.GetResponse();
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader r = new StreamReader(s))
                {
                    str = r.ReadToEnd();
                }
            }
            response.Close();
            AllWeatherInfo = JsonConvert.DeserializeObject<AllWeatherInfo>(str);
            setWind();
            setWeather();
            setMain();
            setSys();
            setClouds();
            setUtility();
        }
        private void setWind()
        {
            label24.Text = AllWeatherInfo.wind.deg.ToString();
            label29.Text = AllWeatherInfo.wind.speed.ToString();
        }
        private void setWeather()
        {
            label11.Text = AllWeatherInfo.weather[0].main.ToString();
            label18.Text = AllWeatherInfo.weather[0].description.ToString();
        }
        private void setMain()
        {
            label12.Text = AllWeatherInfo.main.temp.ToString();
            label27.Text = AllWeatherInfo.main.temp_min.ToString();
            label28.Text = AllWeatherInfo.main.temp_max.ToString();
            label25.Text = AllWeatherInfo.main.pressure.ToString();
            label22.Text = AllWeatherInfo.main.humidity.ToString();
        }
        private void setSys()
        {
            label20.Text = timeGetterForSun(AllWeatherInfo.sys.sunrise).ToString();
            label19.Text = timeGetterForSun(AllWeatherInfo.sys.sunset).ToString();
        }
        private void setClouds()
        {
            label21.Text = AllWeatherInfo.clouds.all.ToString();
        }
        private void setUtility()
        {
            label26.Text = AllWeatherInfo.visibility.ToString();
            label10.Text = AllWeatherInfo.name.ToString();
        }
        private DateTime timeGetterForSun(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
             
        }
    }
}
