using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithm
{
    public partial class GenTablo : UserControl
    {
        public GenTablo()
        {
            InitializeComponent();
        }

        public Canli Canli { get; set; }

        public GenTablo(Canli c, int no) : this()
        {
            this.Canli = c;
            label5.Text = no.ToString();
            label8.Text = c.Gen.x1.ToString();
            label9.Text = c.Gen.x2.ToString();
            label3.Text = c.Gen.SphereFormul.ToString();
        }

    }
}
