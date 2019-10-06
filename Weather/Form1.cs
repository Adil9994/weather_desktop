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
            // Create the watcher.
            Watcher = new GeoCoordinateWatcher();

            // Catch the StatusChanged event.
            Watcher.StatusChanged += Watcher_StatusChanged;

            // Start the watcher.
            Watcher.Start();
        }
    }
}
