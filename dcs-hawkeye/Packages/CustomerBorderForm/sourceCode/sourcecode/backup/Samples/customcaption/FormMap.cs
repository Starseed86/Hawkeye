using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mapbox.Map;
using MSHTML;
using System.IO;
using CefSharp;
using CefSharp.WinForms;


namespace DCS_Hawkeye
{
    public partial class FormMap : Form
    {
        public string pathJs = @"./GeoJSON/";
        public string pathNTDS = @"./NTDS/";
        public ChromiumWebBrowser browsermapPPI;
        public string JsonData;
        public readonly Timer JsonTimer= new Timer();

        public FormMap()
        {
            InitializeComponent();
            InitBrowser();
        }

        public void InitBrowser()
        {
            if (!Cef.IsInitialized)
            {
                Cef.Initialize(new CefSettings());
            }
            browsermapPPI = new ChromiumWebBrowser(string.Empty);
            //{
            //    Location = new Point(0, 0),
            //    Dock = DockStyle.Fill
            //};
            
            string html = File.ReadAllText(pathJs + "dynamicReference_modded_12.html");
            browsermapPPI.LoadHtml(html);
            this.Controls.Add(browsermapPPI);
            //browsermapPPI.BringToFront();
            //browsermapPPI.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            browsermapPPI.Dock = DockStyle.Fill;
            //JsonData = File.ReadAllText(pathJs + "jsonCaucusus_2.json");
            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //browsermapPPI.RegisterJsObject("jsonData", JsonData);
            //JsonTimer.Interval = 2000;
            //JsonTimer.Tick += new EventHandler(JsonTimer_Tick);
            //JsonTimer.Start();
        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const int WM_MOVING = 0x216;



        private void FormMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
            //JsonTimer.Stop();
        }

        private void JsonTimer_Tick(object sender, EventArgs e)
        {
            Random dice = new Random();
            int die = dice.Next(1, 3);
            if (die == 1)
            {
                JsonData = File.ReadAllText(pathJs + "jsonCaucusus_1.json");
            }
            else
            {
                JsonData = File.ReadAllText(pathJs + "jsonCaucusus_2.json");
            }
        }
    }
}
