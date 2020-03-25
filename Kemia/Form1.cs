using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Kemia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string elsoSor = "";
        List<KemiaiElem> elemLista = new List<KemiaiElem>();
        int okor = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("felfedezesek.txt", Encoding.UTF8))
            {
                elsoSor = sr.ReadLine();
                columnHeader1.Text = elsoSor.Split('\t')[0];
                columnHeader2.Text = elsoSor.Split('\t')[1];
                columnHeader3.Text = elsoSor.Split('\t')[2];
                columnHeader4.Text = elsoSor.Split('\t')[3];
                columnHeader5.Text = elsoSor.Split('\t')[4];

                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] tomb = sor.Split('\t');
                    ListViewItem lvi = new ListViewItem(tomb[0]);
                    lvi.SubItems.Add(tomb[1]);
                    lvi.SubItems.Add(tomb[2]);
                    lvi.SubItems.Add(tomb[3]);
                    lvi.SubItems.Add(tomb[4]);
                    listView1.Items.Add(lvi);
                    KemiaiElem ke = new KemiaiElem(tomb[0], tomb[1], tomb[2], Int32.Parse(tomb[3]), tomb[4]);
                    elemLista.Add(ke);
                }
            }

            label2.Text = $"3. Feladat: Elemek száma: {elemLista.Count}";

            

            foreach (var item in elemLista)
            {
                if (item.FelfedezesEve == "Ókor")
                {
                    okor++;
                }
            }

            label3.Text = $"4. Feladat: Felfedezések száma az ókorban: {okor}";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[A-Z,a-z]{1,2}$";
            string mit = textBox1.Text;

            Regex rgx = new Regex(pattern);

            if (!rgx.IsMatch(mit))
            {
                MessageBox.Show("Nem jó! Kérem próbálkozzon újra!", "Figyelmeztetés", MessageBoxButtons.OK);
            }
            else
            {
                label5.Visible = true;
                label5.Text = "6. Feladat: ";
                textBox2.Visible = true;
                string kiir = "Nincs ilyen elem az adatforrásban!";

                foreach (var item in elemLista)
                {
                    if (item.Vegyjel.ToUpper() == mit.ToUpper())
                    {
                        kiir = $"Az elem vegyjele: {item.Vegyjel}" + 
                                      $"\r\nAz elem neve: {item.ElemNeve}" +
                                      $"\r\nRendszáma: {item.Rendszam}" +
                                      $"\r\nFelfedezés éve: {item.FelfedezesEve}" +
                                      $"\r\nFelfedező: {item.Felfedezo}";
                    }
                }

                textBox2.Text = kiir;
            }

            int max = 0;

            for (int i = 9; i < elemLista.Count - 1; i++)
            {
                if (Int32.Parse(elemLista[i + 1].FelfedezesEve) - Int32.Parse(elemLista[i].FelfedezesEve) >= max)
                {
                    max = Int32.Parse(elemLista[i + 1].FelfedezesEve) - Int32.Parse(elemLista[i].FelfedezesEve);
                }
            }

            label6.Visible = true;
            label6.Text = $"7. Feladat: {max} év volt a leghosszabb időszak két elem felfedezése között.";

            label7.Visible = true;
            label7.Text = "8. Feladat: Statisztika";
            textBox3.Visible = true;

            Dictionary<string, int> stat = new Dictionary<string, int>();

            for (int i = okor; i < elemLista.Count; i++)
            {
                string ev = elemLista[i].FelfedezesEve;
                int darab = 0;

                foreach (KeyValuePair<string, int> item in stat)
                {
                    if (item.Key == ev)
                    {
                        darab = item.Value;
                        stat.Remove(ev);
                        break;
                    }
                }

                stat.Add(ev, ++darab);
            }

            string kiir1 = "";
            foreach (KeyValuePair<string, int> item in stat)
            {
                if (item.Value >= 4)
                {
                    kiir1 += $"{item.Key}: {item.Value} db\r\n";
                }
            }

            textBox3.Text = kiir1;
        }
    }
}
