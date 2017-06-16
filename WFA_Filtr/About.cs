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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            label1.Text = "Autor: inż. Mateusz Szwaba (119888)";
            label2.Text = "Filtr nr 3. Schemat:";

            label3.Text = "Zrealizowano:";
            label4.Text = "KLIKNIJ DWUKROTNIE ABY KONTYNUOWAĆ";

            label5.Text = "1. Wczytywanie parametrów filtru i napięcia oraz zakresu częstotliwosci w oknach dialogowych";
            label6.Text = "2. Weryfikacja formatu wprowadzonych danych + opcja HELP (?) z informacjami o parametrze";
            label7.Text = "3. Prezentacja graficzna transmitancji filtru, prądu zasilającego, widma fazowego";
            label8.Text = "oraz impedancji filtru w zależności od wyboru użytkownika";
            label9.Text = "4. Pasek stanu wspomagający użytkownika w czasie pracy programu";
            label11.Text = "5. Możliwość obsługi programu wyłącznie za pomocą klawiatury";

            label10.Text = "Aby powrócić do tego okna kliknij 'O Programie...'";            
        }

        private void About_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
