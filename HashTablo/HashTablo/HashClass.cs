using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Policy;

namespace HashTablo
{
    internal class HashClass
    {
        public int hash;
        public class Node
        {
            public string deger;
            public Node next { get; set; }
        }
        public int ASCIIHesapla(string kelime)
        {
            int toplam = 0;
            foreach (char s in kelime)
            {
                toplam += (int)s;
            }
            return toplam;
        }
        public int hashFonksiyonu(string number)
        {
            return (ASCIIHesapla(number) % hash);
        }
    }
    internal class LinearProbing :HashClass
    {
        
        public Node[] intdizi;
        public LinearProbing(int hash)
        {
            this.hash = hash;
            intdizi = new HashClass.Node[hash];
            for (int i = 0; i < hash; i++)
            {
                intdizi[i] = new HashClass.Node();
                intdizi[i].deger = "-1";
            }
            
        }
        
        public void Ekle(string number)
        {
            int indeks = hashFonksiyonu(number);
            if (intdizi[indeks].deger.Equals("-1"))
            {
                intdizi[indeks].deger = number;
            }
            else
            {
                bool eklendi = false;
                for (int i = indeks + 1; i != indeks; i = (i + 1) % hash)
                {
                    if (intdizi[i].deger.Equals("-1"))
                    {
                        intdizi[i].deger = number;
                        eklendi = true;
                        break;
                    }
                }
                if (!eklendi)
                {
                    MessageBox.Show("Karma tablosu dolu!");
                }
            }
        }
        public void Yazdir()
        {
            for (int i = 0; i < hash; i++)
            {
                Console.WriteLine("İntDizi[{0}]: {1} ", i, intdizi[i].deger);
            }
        }
        public void Bul(string number)
        {
            if (intdizi[hashFonksiyonu(number)].deger.Equals(number))
                Console.WriteLine("Eleman Dizide {0}. sırada", hashFonksiyonu(number));
            else
            {
                bool Isfind = false;
                for (int i = hashFonksiyonu(number) + 1; i != hashFonksiyonu(number); i = (i + 1) % hash)
                {
                    if (intdizi[i].deger == number)
                    {
                        Console.WriteLine("Eleman Dizide {0}. sırada", i);
                        Isfind = true;
                        break;
                    }
                }
                if (!Isfind)
                {
                    Console.WriteLine("Eleman bulunamadı!");
                }
            }
        }
        public void Sil(int index)
        {
            if (index < intdizi.Length)
            {
                intdizi[index].deger = "-1";
            }
            else
                Console.WriteLine("İndex dizin sınırlarının dışında!");
        }
        public void Sil()
        {
            intdizi[intdizi.Length - 1].deger = "-1";
        }
    }
    internal class ChainingClass: HashClass
    {
        public Node[] hashTable;
        private Node node = new Node();

        public ChainingClass(int hash)
        {
            this.hash = hash;
            hashTable = new Node[hash];
            for (int i = 0; i < hashTable.Length; i++)
            {
                hashTable[i] = new Node();
                hashTable[i].next = null;
            }

        }
        public void Ekle(string number)
        {
            int indeks = hashFonksiyonu(number);
            node = hashTable[indeks];
            while (node.next!= null)
                node = node.next;
            node.next = new Node();
            node.next.deger = number;
        }
        public void Yazdir()
        {
            for (int i = 0; i < hash; i++)
            {
                Console.Write("İntDizi[{0}]: ", i);
                node = hashTable[i];
                while (node.next != null)
                {
                    Console.Write("{0} ", node.deger);
                    node = node.next;
                }
                Console.WriteLine();
            }
        }
        public bool IsThere(string number, int index)
        {
            node = hashTable[index];
            while (node.next != null)
            {
                if (node.next.deger.Equals("-1"))
                    return true;
                node = node.next;
            }
            return false;
        }
        public void Bul(string number)
        {
            int indeks = hashFonksiyonu(number);
            if (IsThere(number, indeks))
            {
                Console.WriteLine("Eleman Dizide {0}. indexte bulundu", indeks);
            }
            else
            {
                Console.WriteLine("Eleman bulunamadı!");
            }
        }
        public void Sil(string veri)
        {
            int indeks = hashFonksiyonu(veri);
            if (IsThere(veri, indeks))
            {
                node = hashTable[indeks];
                while (node.deger.Equals(veri))
                {
                    node = node.next;
                }
                while (node.next != null)
                {
                    node.deger = node.next.deger;
                }
                node.next = null;
            }

            else
                Console.WriteLine("Eleman Bulunamadı.");
        }
    }
    internal class QuadraticProbingClass: HashClass
    {
        public string[] intdizi;
        public QuadraticProbingClass(int hash)
        {
            this.hash = hash;
            intdizi = new string[hash];
        }
        public void Ekle(string number)
        {
            int indeks = hashFonksiyonu(number);
            if (intdizi[indeks] == null)
            {
                intdizi[indeks] = number;
            }
            else
            {
                bool eklendi = false;
                int i = 1;
                int index = (indeks + i * i) % hash;
                while (intdizi[index] != null)
                {
                    i++;
                    index = (indeks + i * i) % hash;
                }
                intdizi[index] = number;
                eklendi = true;

                if (!eklendi)
                {
                    Console.WriteLine("Karma tablosu dolu!");
                }
            }
        }
        public void Yazdir()
        {
            for (int i = 0; i < hash; i++)
            {
                Console.WriteLine("İntDizi[{0}]: {1} ", i, intdizi[i]);
            }
        }
        public void Bul(string number)
        {
            if (intdizi[hashFonksiyonu(number)].Equals(number))
                Console.WriteLine("Eleman Dizide {0}. sırada", hashFonksiyonu(number));
            else
            {
                bool Isfind = false;
                int indeks = hashFonksiyonu(number);
                int i = 1;
                int index = (indeks + i * i) % hash;
                while (intdizi[index] != null)
                {
                    if (intdizi[index] == number)
                    {
                        Console.WriteLine("Eleman Dizide {0}. sırada", index);
                        Isfind = true;
                        break;
                    }
                    i++;
                    index = (indeks + i * i) % hash;
                }
                if (!Isfind)
                {
                    Console.WriteLine("Eleman bulunamadı!");
                }
            }
        }
        public void Sil(int index)
        {
            if (index < intdizi.Length)
            {
                intdizi[index] = null;
            }
            else
                MessageBox.Show("İndex dizin sınırlarının dışında!");
        }
        public void Sil()
        {
            intdizi[intdizi.Length - 1] = null;
        }
    }
    internal class DoubleHashingClass: HashClass
    {
        public string[] intdizi;
        public DoubleHashingClass(int hash)
        {
            this.hash = hash;
            intdizi = new string[hash];
        }
        public int hashFonksiyonu2(string number)
        {
            return (ASCIIHesapla(number) % 7);
        }
        public int hashing(string number, int attempt)
        {
            int hash1 = hashFonksiyonu(number);
            int hash2 = hashFonksiyonu2(number);
            return (hash1 + attempt * hash2) % hash;
        }
        public void Ekle(string number)
        {
            int indeks = hashFonksiyonu(number);
            if (intdizi[indeks] == null)
            {
                intdizi[indeks] = number;
            }
            else
            {
                bool eklendi = false;
                int attempt = 1;
                int index = hashing(number, attempt);
                while (intdizi[index] != null)
                {
                    attempt++;
                    index = hashing(number, attempt);
                }
                intdizi[index] = number;
                eklendi = true;

                if (!eklendi)
                {
                    Console.WriteLine("Karma tablosu dolu!");
                }
            }
        }
        public void Yazdir()
        {
            for (int i = 0; i < hash; i++)
            {
                Console.WriteLine("İntDizi[{0}]: {1} ", i, intdizi[i]);
            }
        }
        public void Bul(string number)
        {
            if (intdizi[hashFonksiyonu(number)].Equals(number))
                Console.WriteLine("Eleman Dizide {0}. sırada", hashFonksiyonu(number));
            else
            {
                bool Isfind = false;
                int indeks = hashFonksiyonu(number);
                int attempt = 1;
                int index = hashing(number, attempt);
                while (intdizi[index] != null)
                {
                    if (intdizi[index] == number)
                    {
                        Console.WriteLine("Eleman Dizide {0}. sırada", index);
                        Isfind = true;
                        break;
                    }
                    attempt++;
                    index = hashing(number, attempt);
                }
                if (!Isfind)
                {
                    Console.WriteLine("Eleman bulunamadı!");
                }
            }
        }
        public void Sil(int index)
        {
            if (index < intdizi.Length)
            {
                intdizi[index] = null;
            }
            else
                Console.WriteLine("İndex dizin sınırlarının dışında!");
        }
        public void Sil()
        {
            intdizi[intdizi.Length - 1] = null;
        }
    }
}
