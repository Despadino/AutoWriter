using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoWriter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = ($"Точность {trackBar1.Value}%");
            Accuracy.accuracy = trackBar1.Value;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            trackBar1.Value = Accuracy.accuracy;
            label1.Text = ($"Точность {trackBar1.Value}%");
        }
    }
}
