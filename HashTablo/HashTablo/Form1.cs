using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using HashTablo;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HashTablo;

public partial class Form1 : Form
{
    ChainingClass yeniChaining = new ChainingClass(15);
    LinearProbing yenilinearProbing = new LinearProbing(15);
    QuadraticProbingClass Yeni= new QuadraticProbingClass(15);

    private void DosyaOkuEkle()
    {
        string dosyaYolu = "Ayi.txt";

        try
        {
            using (StreamReader sr = new StreamReader(dosyaYolu))
            {
                string satir;

                while ((satir = sr.ReadLine()) != null)
                {
                    string[] elemanlar = satir.Split(new char[] { ' ', '.', ',' },StringSplitOptions.RemoveEmptyEntries);

                    foreach (string eleman in elemanlar)
                    {
                        Yeni.Ekle(eleman);
                        yenilinearProbing.Ekle(eleman);
                        yeniChaining.Ekle(eleman);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Hata: " + e.Message);
        }
    }
    public void YazdirLinearProbing()
    {
        int j = 0;
        for (int i = 0; i < yenilinearProbing.hash; i++)
        {
            System.Windows.Forms.Button button = new System.Windows.Forms.Button
            {
                Text = yenilinearProbing.intdizi[i].deger.ToString(),
                Location = new System.Drawing.Point(0, 25 * i),
                Size = new System.Drawing.Size(60, 25)
            };

            panelVeri.Controls.Add(button);
        }
    }
    public void YazdirQuadraticProbing()
    {
        int j = 0;
        for (int i = 0; i < Yeni.hash; i++)
        {
            System.Windows.Forms.Button button = new System.Windows.Forms.Button
            {
                Text = Yeni.intdizi[i],
                Location = new System.Drawing.Point(0, 25 * i),
                Size = new System.Drawing.Size(60, 25)
            };

            panelVeri.Controls.Add(button);
        }
    }

    public void YazdirChaining()
    {
        for (int i = 0; i < yeniChaining.hash; i++)
        {
            HashClass.Node node = yeniChaining.hashTable[i];
            int j = 0;
            node = node.next;
            while (node != null)
            {
                System.Windows.Forms.Button button = new System.Windows.Forms.Button
                {
                    Text = node.deger.ToString(),
                    Location = new System.Drawing.Point(60 * j, 25 * i),
                    Size = new System.Drawing.Size(60, 25)
                };

                panelVeri.Controls.Add(button);
                j++;
                node = node.next;
            }
        }
    }
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        
        DosyaOkuEkle();
        YazdirChaining();
        YazdirLinearProbing();
        YazdirQuadraticProbing();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        //DosyaOkuEkle();
    }
}