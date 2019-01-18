using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Editor_titulek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] radky;


        #region Metody událostí

        /// <summary>
        /// Uložit soubor
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if (openFileDialog1.FileName != "")
            {
                for (int i = 0; i < radky.Length - 1; i++)
                {
                    if (radky[i].Length > 3) //kontrola jestli je na řádku více než 2 znaky
                    {
                        if (radky[i][2] == ':')//kontrola jestli je řádek časový údaj
                        {
                            
                            radky[i] = prepis(radky[i], Convert.ToInt16(numericUpDown1.Value), Convert.ToInt16(numericUpDown2.Value), Convert.ToInt16(numericUpDown3.Value), Convert.ToInt16(numericUpDown4.Value));//zapamatování si indexu řádku
                        }
                    }
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(saveFileDialog1.FileName, radky, Encoding.Default);
                    MessageBox.Show("Hotovo");
                }
            }
            else
            {
                MessageBox.Show("Musíte nejdřív vybrat soubor!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vybrat cestu k titulkovému souboru
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                radky = File.ReadAllLines(textBox1.Text = openFileDialog1.FileName, Encoding.Default);
            }
        }


#endregion

        #region Return metody
        private string prepis(string radek,int pocet_milisekund, int pocet_sekund, int pocet_minut, int pocet_hodin)
        {
            //počáteční časový údaj

            int hour = 0;
            int minute = 0;
            int second = 0;
            int milisecond = 0;
            
            for(int i = 0; i < 2; i++)
                   hour = hour * 10 + int.Parse(radek[i].ToString());

            for (int i = 3; i < 5; i++)
                minute = minute * 10 + int.Parse(radek[i].ToString());

            for (int i = 6; i < 8; i++)
                second = second * 10 + int.Parse(radek[i].ToString());

            for(int i = 9; i < 12; i++)
                    milisecond = milisecond * 10 + int.Parse(radek[i].ToString());

            
            //kontrola časového údaje
            if (milisecond + pocet_milisekund >= 1000) // časový údaj je v šedeátkové soustavě, takže tohle je taková kontrola
            {
                milisecond -= 1000;
                second++;
            }

            if ((milisecond + pocet_milisekund) < 0)//časový údaj nemlže obsahovat záporná //čísla kontrola záporných čísel
            {
                milisecond = 1000 + milisecond;
                second--;
                
            }

            if (second + pocet_sekund >= 60)
            {
                second -= 60;
                minute++;
            }

            if ((second + pocet_sekund) < 0)//čísla kontrola záporných čísel
            {
                second = 60 + second;
                minute--;
                
            }

            if (minute + pocet_minut >= 60)
            {
                minute -= 60;
                hour++;
            }

            if ((minute + pocet_minut) < 0)//čísla kontrola záporných čísel
            {
                minute = 60 + minute;
                hour--;
                
            }
            if ((hour + pocet_hodin) < 0)//čísla kontrola záporných čísel
            {
                hour = 0;
                pocet_hodin = 0;
            }

            
            DateTime time1 = new DateTime(2013, 7, 4, (hour + pocet_hodin), (minute + pocet_minut), (second + pocet_sekund),(milisecond + pocet_milisekund)); //zařazení prvního časového úseku 
            string vysledek = time1.ToString("HH:mm:ss") + "," + preformatovane_milisekundy(milisecond, pocet_milisekund) + " --> ";
            hour = minute = second = milisecond = 0; // nulování
            
            
            
            //koncový časový údaj
            for (int i = 12; i < radek.Length; i++)
            {
                if (radek[i] > 47 & radek[i] < 58) //zjistí kde se nachází další časový úsek
                {
                    for (int soucet = 0; soucet < 2; soucet++)
                        hour = hour * 10 + int.Parse(radek[i + soucet].ToString());

                    for (int soucet = 3; soucet < 5; soucet++)
                        minute = minute * 10 + int.Parse(radek[i + soucet].ToString());

                    for (int soucet = 6; soucet < 8; soucet++)
                        second = second * 10 + int.Parse(radek[i + soucet].ToString());

                    for (int soucet = 9; soucet < 12; soucet++)
                            milisecond = milisecond * 10 + int.Parse(radek[i + soucet].ToString());

                    break;//příkaz sloužící k zastavení cyklu, který hledá čísla, kdyby to tu nebylo čísla by se chybně přepisovala
                    
                }
            }


            if (milisecond + pocet_milisekund >= 1000) // časový údaj je v šedeátkové soustavě, takže tohle je taková kontrola
            {
                milisecond -= 1000;
                second++;
            }

            if ((milisecond + pocet_milisekund) < 0)//časový údaj nemlže obsahovat záporná //čísla kontrola záporných čísel
            {
                milisecond = 1000 + milisecond;
                second--;
                
            }

            if (second + pocet_sekund >= 60)
            {
                second -= 60;
                minute++;
            }

            if ((second + pocet_sekund) < 0)//čísla kontrola záporných čísel
            {
                second = 60 + second;
                minute--;
                
            }

            if (minute + pocet_minut >= 60)
            {
                minute -= 60;
                hour++;
            }

            if ((minute + pocet_minut) < 0)//čísla kontrola záporných čísel
            {
                minute = 60 + minute;
                hour--;
                
            }
            if ((hour + pocet_hodin) < 0)//čísla kontrola záporných čísel
            {
                hour = 0;
                pocet_hodin = 0;
            }

            DateTime time2 = new DateTime(2013, 7, 4,(hour + pocet_hodin), (minute + pocet_minut), (second + pocet_sekund), (milisecond + pocet_milisekund));
            vysledek += time2.ToString("HH:mm:ss") + "," + preformatovane_milisekundy(milisecond,pocet_milisekund);
            //MessageBox.Show(vysledek);
            return vysledek;
        
        }

        private string preformatovane_milisekundy(int miliseknudy, int pocet_milisekund)
        {
            //formátování (přidávání nul před méně než 3 ciferná čísla
            string vysledek = Convert.ToString(miliseknudy + pocet_milisekund);
            while (vysledek.Length != 3)
                vysledek = "0" + vysledek;

            return vysledek;
        }


        
        #endregion

    }
}
