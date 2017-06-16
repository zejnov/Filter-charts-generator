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
    public partial class Dialog_obw : Form
    {
        public Dialog_obw()
        {
            InitializeComponent();
        }

        public String Rs
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public String Rr
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public String Lr
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public String Cr
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
//HELP - Rs
        private void label1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość rezystancji szeregowej.                                                        Jednostka: Ohm [Ω]",
               "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
//HELP - Rr
        private void label2_HelpRequested(object sender, HelpEventArgs hlpevent)
        { 
            MessageBox.Show("Wartość rezystancji równoległej.                                                       Jednostka: Ohm [Ω]",
               "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);     
        }
//HELP - Lr
        private void label3_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość indukcyjności równoleglej.                                                    Jednostka: Henr [H]; 1 mH=0,001 H",
               "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
//HELP - Cr
        private void label4_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("Wartość pojemności równoleglej.                                                       Jednostka: Farad [F]; 1 uF=0,000 001 F",
               "Parametry HELP", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

       

    }
}
