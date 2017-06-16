using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WFA_Filtr
{
    public partial class Dialog_nap : Form
    {
        public Dialog_nap()
        {
            InitializeComponent();
        }

        public String Amplituda
        { 
            get {return textBox1.Text;}
            set { textBox1.Text = value; }        
        }

        public String Czestotliwosc
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }        
        }

        public String Czestotliwosc2
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

//HELP - Amplituda
        private void label1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość amplitudy sinusoidalnego napięcia zasilającego.           Jednostka: Wolt [V]",
                "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
//HELP - Częstotliwość min
        private void label2_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość minimalnej częstotliwości sinusoidalnego napięcia zasilającego.          Jednostka: Hertz [Hz]",
                "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }
//HELP - Częstotliwość max
        private void label3_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość maksymalnej częstotliwości sinusoidalnego napięcia zasilającego.          Jednostka: Hertz [Hz]",
                "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }

        

        


    }
}
