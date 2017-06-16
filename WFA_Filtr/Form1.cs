using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Numerics;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace WFA_Filtr
{
    public partial class Form1 : Form
    {

        double Am = 0;        
        double Rs = 0;
        double Rr = 0;
        double Lr = 0;
        double Cr = 0;        
        int fs = 0;
        int fs2 = 0;

        //zmienne 'przykładowe'
        double pRs = 3;
        double pRr = 2;
        double pLr = 150;
        double pCr = 100;
        double pAm = 230;
        int pfs = 1;
        int pfs2 = 30000;

        int a;  //pomocnicza

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Witam :)  Wprowadź dane napięcia i obwodu bądź wprowadź przykładowe dane."; //Status
            About info = new About();
            info.ShowDialog();

        }

        //Zapobieganie niepożądanemu zamknięciu
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Czy jesteś pewny?", this.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            switch (dr)
            {
                case DialogResult.Yes:
                    {
                        MessageBox.Show("Żegnam.");
                        break;
                    }
                case DialogResult.No:
                    {
                        e.Cancel = true;
                        break;
                    }
            
            }//switch
        }

        //Wywołanie "zamknij"
        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //================Obsługa buttonów dolnych============================    
        //obsługa buttona - przykładowe dane napięcia i obwodu
        private void btn_1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Przykładowe dane wczytane poprawnie. Wciśnij OBLICZ! bądź dokonaj edycji parametrów"; //Status

            par_rs.Text = pRs.ToString();
            Rs = pRs;
            par_rr.Text = pRr.ToString();
            Rr = pRr;
            par_l.Text = pLr.ToString();
            Lr = pLr;
            par_c.Text = pCr.ToString();
            Cr = pCr;

            par_amp.Text = pAm.ToString();
            Am = pAm;
            par_freq.Text = pfs.ToString();
            fs = pfs;
            par_freq2.Text = pfs2.ToString();
            fs2 = pfs2;
        }
        //obsługa buttona - wczytywanie danych napięcia zasilającego
        private void btn_3_Click(object sender, EventArgs e)
        {
            Dialog_nap dlg_nap = new Dialog_nap();

            dlg_nap.Amplituda = Am.ToString();
            dlg_nap.Czestotliwosc = fs.ToString();
            dlg_nap.Czestotliwosc2 = fs2.ToString();

            if (dlg_nap.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Dane napięcia wczytane poprawnie."; //Status

                if(!Double.TryParse(dlg_nap.Amplituda, out Am))
                {
                    MessageBox.Show("Błędna wartość amplitudy", "Parametry ERROR", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;  
                }

              //  if(!Int
                if(!int.TryParse(dlg_nap.Czestotliwosc, out fs))
                {
                    MessageBox.Show("Błędna wartość częstotliwości min", "Parametry ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(!int.TryParse(dlg_nap.Czestotliwosc2 ,out fs2))
                {
                    MessageBox.Show("Błędna wartość częstotliwości max", "Parametry ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (fs > fs2)  //zamiana w przypadko podania wartości częstotliwości w odwrotnych polach
                {
                    a = fs;
                    fs = fs2;
                    fs2 = a;

                    par_amp.Text = Am.ToString();
                    par_freq.Text = fs.ToString();
                    par_freq2.Text = fs2.ToString();
                }
                else
                {
                    par_amp.Text = Am.ToString();
                    par_freq.Text = fs.ToString();
                    par_freq2.Text = fs2.ToString();
                }
                
            }
        }
        //obsługa buttona - wczytywanie danych obwodu
        private void btn_2_Click(object sender, EventArgs e)
        {
            Dialog_obw dlg_obw = new Dialog_obw();

            dlg_obw.Rs = Rs.ToString();
            dlg_obw.Rr = Rr.ToString();
            dlg_obw.Lr = Lr.ToString();
            dlg_obw.Cr = Cr.ToString();


            if (dlg_obw.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Dane obwodu wczytane poprawnie."; //Status

                if (!Double.TryParse(dlg_obw.Rs, out Rs))
                {
                    MessageBox.Show("Błędna wartość Rs", "Parametry ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);                
                }

                if (!Double.TryParse(dlg_obw.Rr, out Rr))
                {
                    MessageBox.Show("Błędna wartość Rr", "Parametry ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!Double.TryParse(dlg_obw.Lr, out Lr))
                {
                    MessageBox.Show("Błędna wartość Lr", "Parametry ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!Double.TryParse(dlg_obw.Cr, out Cr))
                {
                    MessageBox.Show("Błędna wartość Cr", "Parametry ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                par_rs.Text = Rs.ToString();
                par_rr.Text = Rr.ToString();
                par_l.Text = Lr.ToString();
                par_c.Text = Cr.ToString();

            
            }//ShowDialog


        }
        
        //obsługa buttona - OBLICZ <<<<<<<<<<<<<<<<<SERCE programu>>>>>>>>>>>
        private void btn_4_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            toolStripStatusLabel1.Text = "Czy wiesz, że? Możesz wybrać zawartość wykresu i ponownie wcisnąć OBLICZ!"; //Status

            DataTable dTable,dTable2,dTable3,dTable4;
            dTable = new DataTable();
            dTable2 = new DataTable();
            dTable3 = new DataTable();
            dTable4 = new DataTable();

            DataView dView,dView2,dView3,dView4;
            DataColumn column;
            DataRow row0;
            //------------------Częstotliwości do poszczególnych dTable----------------
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Czestotliwosc";
            dTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Czestotliwosc2";
            dTable2.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Czestotliwosc3";
            dTable3.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Czestotliwosc4";
            dTable4.Columns.Add(column);

            //--------------------------

            column = new DataColumn();          //Transmitancja
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Transmitancja";
            dTable.Columns.Add(column);         

            column = new DataColumn();          //Prąd
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Prad";
            dTable2.Columns.Add(column);

            column = new DataColumn();          //Widmo_Fazowe
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Widmo_Fazowe";
            dTable3.Columns.Add(column);
            
            column = new DataColumn();          //Impedancja
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Impedancja";
            dTable4.Columns.Add(column);
            //-----------------------------------------------

            //=========obliczenia=============================         
            
            for (int f_temp = fs; f_temp <= fs2; f_temp++)
            { 
                //przyśpieszenie oblcizeń przy dużym zakresie częstotliwosci
                if (fs2 - fs > 5000)
                {
                    if (fs2 - fs > 50000)
                    {
                        if (fs2 - fs > 500000)
                        {
                            f_temp += 999;
                        }
                        else 
                        {
                            f_temp += 999;
                        }                       
                    }
                    else
                    {
                        f_temp += 9;
                    }
                }


                //impedancja cewki i kondensatora
                Complex Xl_temp = new Complex(0, (2.0 * 3.14159265 * f_temp * Lr * 0.001));
                Complex Xc_temp = new Complex(0, (2.0 * 3.14159265 * f_temp * Cr * 0.000001));

                Complex Z_z = new Complex(1, 1); //impedancja
                Complex bufor_z = new Complex(1, 1);
                Complex transmitancja = new Complex(1, 1);
                
                //impedancja filtru
                Z_z = Rs + (Rr * Xl_temp * Xc_temp / (1.0 * Xc_temp * Xl_temp + (Xc_temp + Xl_temp) * Rr));
                //napięcie U1
                Complex U1_z = new Complex(Am, 0);
                //prąd pobierany
                Complex I1_z = new Complex(0, 0);
                I1_z = U1_z / Z_z;

                //zmienna pomocnicza
                bufor_z = Xc_temp * Rr / (1.0 * Rr * Xc_temp);

                //prąd z dzielnika prądu, CEWKA
                Complex I2_z = new Complex(0, 0);
                I2_z = I1_z * (bufor_z / (1.0 * bufor_z + Xl_temp));

                //napięcie U2 - wyjsciowe
                Complex U2_z = new Complex(1, 1);
                U2_z = I2_z * Xl_temp;

                transmitancja = U2_z / (1.0 * U1_z);
                
            //++++++++++++++++++++ Wprowadzanie danych do tabel
                row0 = dTable.NewRow();
                row0["Czestotliwosc"] = f_temp;                
                row0["Transmitancja"] = Complex.Abs(transmitancja);               
                dTable.Rows.Add(row0);

                row0 = dTable2.NewRow();
                row0["Czestotliwosc2"] = f_temp;
                row0["Prad"] = Complex.Abs(I1_z);
                dTable2.Rows.Add(row0);

                row0 = dTable3.NewRow();
                row0["Czestotliwosc3"] = f_temp;
                row0["Widmo_Fazowe"] = transmitancja.Phase *180/Math.PI;
                dTable3.Rows.Add(row0);

                row0 = dTable4.NewRow();
                row0["Czestotliwosc4"] = f_temp;
                row0["Impedancja"] = Complex.Abs(Z_z);
                dTable4.Rows.Add(row0);            
            }//for od 'f'

            //++++++++++++++++++++
            dView = new DataView(dTable);
            dView2 = new DataView(dTable2);
            dView3 = new DataView(dTable3);
            dView4 = new DataView(dTable4);

            chart1.Series.Clear(); //wyczyszczenie danych z poprzedniego wykresu
            chart1.Titles.Clear();

            chart1.Legends[0].Enabled = false;  //legenda nie jest konieczna, 1 seria danych na wykres, opis w tytule wykresu
            
            //+++++++++++++++Radio_Buttons++++++++++++++++++++++
            //wykres - transmitancja filtru U2/U1
            if (radioButton1.Checked)
            {                              
                chart1.DataBindTable(dView, "Czestotliwosc");
                chart1.Series["Transmitancja"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;               

                chart1.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
                chart1.ChartAreas[0].AxisY.Title = "Transmitancja [V/V]";
                chart1.Titles.Add("Transmitancja filtru (U2/U1)");
            }
            
            //wykres - prąd zasilający
            if (radioButton2.Checked)
            {
                chart1.DataBindTable(dView2, "Czestotliwosc2");
                chart1.Series["Prad"].ChartType 
                    = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
                chart1.ChartAreas[0].AxisY.Title = "Prąd [A]";
                chart1.Titles.Add("Prąd pobierany ze źródła");              
            }
            //wykres - widmo fazowe
            if (radioButton3.Checked)
            {
                chart1.DataBindTable(dView3, "Czestotliwosc3");
                chart1.Series["Widmo_Fazowe"].ChartType 
                    = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
                chart1.ChartAreas[0].AxisY.Title = "Kąt [stopnie]";
                chart1.Titles.Add("Widmo fazowe - kąt (faza) transmitancji");
            }
            //wykres - impedancja Z_z
            if (radioButton4.Checked)
            {
                chart1.DataBindTable(dView4, "Czestotliwosc4");
                chart1.Series["Impedancja"].ChartType 
                    = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.ChartAreas[0].AxisX.Title = "Częstotliwość [Hz]";
                chart1.ChartAreas[0].AxisY.Title = "Impedancja [Ω]";
                chart1.Titles.Add("Impedancja");
            }

            //Ogólne formatowanie wykresu
            chart1.ChartAreas[0].BackColor = Color.Honeydew;
            chart1.ChartAreas[0].AxisX.LineWidth = 2;
            chart1.ChartAreas[0].AxisY.LineWidth = 2;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{#0}";
            chart1.ChartAreas[0].AxisX.TitleFont =
              new System.Drawing.Font("Arial", 12F, FontStyle.Bold);

            chart1.ChartAreas[0].AxisY.TitleFont =
                new System.Drawing.Font("Arial", 12F, FontStyle.Bold);

            chart1.Titles[0].Font =
                new System.Drawing.Font("Times New Roman", 12F, FontStyle.Bold);

            chart1.Titles[0].ForeColor = Color.DodgerBlue;
            
        }
        //wywoływanie okna informacyjnego
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About info = new About();
            info.ShowDialog();

            //Dialog_obw dlg_obw = new Dialog_obw();
        }
        //=====================================================================

        //================Obsługa wyboru wykresy z ToolStripMenu===============
        private void transmitancjaFiltruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void ącyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void widmoFazoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
        }

        private void impedancjaFiltruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
        }
        //=====================================================================      
    }//public partial class Form1 : Form
}//namespace WFA_Filtr
