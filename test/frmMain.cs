using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ioh = Cyclone.MciWrapper.IOHelper;

namespace test
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Cyclone.MciWrapper.MciCommon mci = new Cyclone.MciWrapper.MciCommon();

        string file = @"G:\My Music\Waves(wavdest gen)\黑色毛衣.wav";

        private void Form1_Load(object sender, EventArgs e)
        {
            var s = ioh.GetShortPathName(file);
            Console.WriteLine(s);

            mci.SetFile(file);
            mci.Open();
            mci.Play();

            mci.Volume = 1001;

            mci.Rate = 2500;

            Console.WriteLine(mci.Volume);
            Console.WriteLine(TimeSpan.FromMilliseconds(mci.Duration).ToString());
            Console.WriteLine(mci.Position);


        }
    }
}
