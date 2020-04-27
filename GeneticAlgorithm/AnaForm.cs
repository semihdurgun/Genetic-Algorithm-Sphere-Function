using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;


namespace GeneticAlgorithm
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {

        }
        private bool isRunning = false;

        private bool Kontrol()
        {
            if (isRunning)
            {
                isRunning = false;
                button1.Text = "Başla";
            }
            else
            {
                button1.Text = "Dur";
                isRunning = true;
            }

            return isRunning;
        }
       
        private Series GenSeries()
        {

            flowLayoutPanel1.Controls.Clear();
            this.chart1.Series.Clear();
            Series series = this.chart1.Series.Add("Sonuç");
            chart1.IsSoftShadows = false;

            series.ChartType = SeriesChartType.Area;
            series.BorderWidth = 3;
            series.Color = Color.Red;
            return series;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Kontrol()) return;
            Series series = GenSeries();
            chart1.Visible = true;
            groupBox.Visible = true;
            groupBox2.Visible = true;
            int popSayi = (int)numericUpDown1.Value;
            double carazlamaOran = (double)numericUpDown2.Value / 100;
            double mutasyonOran = (double)numericUpDown3.Value / 100;
            int iterasyonSayısı = (int)numericUpDown4.Value;
            int elitPop = (int)numericUpDown5.Value;
            int ms = (int)numericUpDown6.Value;

            GenetikHesaplamalar GenHesabı = new GenetikHesaplamalar(popSayi);
            GenHesabı.elitPop = elitPop;


            for (int j = 0; j < iterasyonSayısı; j++)
            {
                await Task.Delay(ms);
                GenHesabı.Elitizm();
                GenHesabı.TurnuvaCiftiOlustur();
                GenHesabı.Caprazla(carazlamaOran);
                GenHesabı.Mutasyon(mutasyonOran);

                ElitizmFlowLayoutEkle(GenHesabı.EniyiCanli());

                var eniyiSkor = GenHesabı.EniyiCanli().Gen.SphereFormul * 10000;
                series.Points.AddXY(j, eniyiSkor);
                label8.Text = GenHesabı.EniyiCanli().Gen.x1.ToString();
                label9.Text = GenHesabı.EniyiCanli().Gen.x2.ToString();


                if (!isRunning) break;
                if (j == iterasyonSayısı - 1) Kontrol();
            }
        }

        public bool ElitizmFlowLayoutEkle(Canli c)
        {
            foreach (var elitizm in flowLayoutPanel1.Controls.OfType<GenTablo>())
                if (c.Gen.SphereFormul == elitizm.Canli.Gen.SphereFormul)
                    return false;

            label11.Text = "Toplam Gen Sayısı:" + (flowLayoutPanel1.Controls.Count + 1);
            var comp = new GenTablo(c, flowLayoutPanel1.Controls.Count + 1);

            comp.Click += (s, arg) =>
            {
                var canli = ((s as Control).Parent.Parent.Parent as GenTablo).Canli;
                var list = new List<Canli>();
                list.Add(canli);
            };
            flowLayoutPanel1.Controls.Add(comp);
            return true;
        }
    }
    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen,
            float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
            float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }
    }
}
