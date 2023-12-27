using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Grafovi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pozdrav! Dobro dosli u zemlju grafova. \nPred vama su razne opcije sa kojima se mozete igrati i upravljati grafovima.");
            int akcija = 1;
            Graf_Lista graf = new Graf_Lista();

            while (akcija != 0)
            {
                Console.WriteLine("Unesite broj pored akcije koju zelite da izvrsite:\n1 Unos grafa\n2 Snimanje grafa u fajl\n3 Ucitavanje grafa iz fajla" +
                    "\n4 Komponenta povezanosti\n5 Orijentacija grafa\n6 Dajkstrin algoritam\n7 Belman-Fordov algoritam\n8 Primov alogritam" +
                    "\n9 Kruskalov algoritam\n0 Izadji");
                akcija = Convert.ToInt32(Console.ReadLine());

                if (akcija == 1)
                {
                    Console.WriteLine("Koliko ce vas graf da ima cvorova?");
                    graf.broj_cvorova = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Unesite cvorove u jednom redu odvojene razmacima.");
                    graf.cvorovi = Console.ReadLine();
                    string[] cvorovi = graf.cvorovi.Split(' ');
                    for (int i = 0; i < graf.broj_cvorova; i++)
                    {
                        Cvor trenutni_cvor = new Cvor(cvorovi[i], "novi");
                        graf.DodajCvor(trenutni_cvor);
                    }
                    Console.WriteLine("Koliko ce vas graf imati grana?");
                    graf.broj_grana = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < graf.broj_grana; i++)
                    {
                        Console.WriteLine("Unesite granu, prvo odakle krece, potom dokle ide i na kraju tezinu grane. Sve odvojite razmacima.");
                        graf.grane[i] = Console.ReadLine();
                        string[] grane = graf.grane[i].Split(' ');
                        Cvor pom1 = new Cvor(grane[0]);
                        Cvor pom2 = new Cvor(grane[1]);
                        Grana trenutna_grana = new Grana(pom1, pom2, Convert.ToInt32(grane[2]));
                        graf.DodajGranu(trenutna_grana);
                    }
                    graf.Ispisi();
                }
                if (akcija == 2)
                {
                    const string putanja = "Graf.txt";
                    StreamWriter pisac = new StreamWriter(putanja);
                    pisac.WriteLine(graf.broj_cvorova);
                    pisac.WriteLine(graf.cvorovi);
                    pisac.WriteLine(graf.broj_grana);
                    for (int i = 0; i < graf.broj_grana; i++)
                    {
                        pisac.WriteLine(graf.grane[i]);
                    }
                    pisac.Close();
                }
                if (akcija == 3)
                {
                    const string putanja = "Graf.txt";
                    StreamReader citac = new StreamReader(putanja);

                    graf.broj_cvorova = Convert.ToInt32(citac.ReadLine());
                    string line_cvorovi = citac.ReadLine();
                    string[] cvorovi = line_cvorovi.Split(' ');
                    for (int i = 0; i < cvorovi.Length; i++)
                    {
                        Cvor trenutni_cvor = new Cvor(cvorovi[i], "novi");
                        graf.DodajCvor(trenutni_cvor);
                    }

                    graf.broj_grana = Convert.ToInt32(citac.ReadLine());
                    for (int i = 0; i < graf.broj_grana; i++)
                    {
                        string line = citac.ReadLine();
                        graf.grane[i] = line;
                        string[] tokens = line.Split(' ');
                        Cvor cvor1 = new Cvor(tokens[0]);
                        Cvor cvor2 = new Cvor(tokens[1]);
                        Grana trenutna_grana = new Grana(cvor1, cvor2, Convert.ToInt32(tokens[2]));
                        graf.DodajGranu(trenutna_grana);
                    }

                    citac.Close();
                    graf.Ispisi();
                }
                if (akcija == 4)
                {
                    List<Grana> grane = Funkcije.Popuni_i_Sortiraj_Grane(graf);
                    foreach (Cvor cvor in graf.cvorici)
                    {
                        cvor.komponenta_povezanosti = 0;
                    }
                    int trenutna_komponenta = 0;
                    int ukupno_komponenta = trenutna_komponenta;

                    while (grane.Count > 0)
                    {
                        Grana trenutna_grana = grane[0];
                        int tren1 = 0;
                        int tren2 = 0;
                        string ime1 = "";
                        string ime2 = "";
                        foreach (Cvor cvor in graf.cvorici)
                        {
                            if (trenutna_grana.odakle.ime == cvor.ime)
                            {
                                tren1 = cvor.komponenta_povezanosti;
                                ime1 = cvor.ime;
                            }
                            if (trenutna_grana.dokle.ime == cvor.ime)
                            {
                                tren2 = cvor.komponenta_povezanosti;
                                ime2 = cvor.ime;
                            }
                        }

                        if ((tren1 == 0) && (tren2 == 0))
                        {
                            ukupno_komponenta++;
                            trenutna_komponenta++;
                            foreach (Cvor cvor in graf.cvorici)
                            {
                                if (cvor.ime == ime1)
                                {
                                    cvor.komponenta_povezanosti = ukupno_komponenta;
                                }
                                if (cvor.ime == ime2)
                                {
                                    cvor.komponenta_povezanosti = ukupno_komponenta;
                                }
                            }
                        }
                        else
                        {
                            if (((tren1 == 0) && (tren2 != 0)) || ((tren1 != 0) && (tren2 == 0)))
                            {
                                if (tren1 == 0)
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (ime1 == cvor.ime)
                                        {
                                            cvor.komponenta_povezanosti = tren2;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (ime2 == cvor.ime)
                                        {
                                            cvor.komponenta_povezanosti = tren1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((tren1 != 0) && (tren2 != 0) && (tren1 != tren2))
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (cvor.komponenta_povezanosti == tren2)
                                        {
                                            cvor.komponenta_povezanosti = tren1;
                                        }
                                    }
                                    trenutna_komponenta--;
                                }
                            }
                        }
                        grane.Remove(trenutna_grana);
                    }
                    foreach (Cvor cvor in graf.cvorici)
                    {
                        cvor.komponenta_povezanosti = 0;
                    }
                    Console.WriteLine("Ukupno ima " + trenutna_komponenta + " razlicite komponente povezanosti.");
                }
                if (akcija == 5)
                {
                    List<Grana> grane = Funkcije.Popuni_i_Sortiraj_Grane(graf);
                    Graf_Matrica graf_matrica = new Graf_Matrica(graf.broj_cvorova);
                    int prvobitni_index = Graf_Matrica.index_cvorova;
                    Graf_Matrica.index_cvorova = -1;

                    foreach (Cvor cvor in graf.cvorici)
                    {
                        graf_matrica.DodajCvor(cvor);
                    }

                    while (grane.Count > 0)
                    {
                        Grana trenutna_grana = grane[0];

                        int index1 = -1;
                        int index2 = -1;

                        foreach (Cvor cvor in graf_matrica.cvorovi)
                        {
                            if (cvor.ime == trenutna_grana.odakle.ime)
                            {
                                index1 = cvor.index;
                            }
                            if (cvor.ime == trenutna_grana.dokle.ime)
                            {
                                index2 = cvor.index;
                            }
                        }

                        graf_matrica.matrica[index1, index2] = trenutna_grana.tezina;

                        grane.Remove(trenutna_grana);
                    }

                    bool orijentisan = true;

                    for (int v = 0; v < graf.broj_cvorova - 1; v++)
                    {
                        int x = v + 1;
                        for (int k = x; k < graf.broj_cvorova; k++)
                        {
                            if (graf_matrica.matrica[v,k] != graf_matrica.matrica[k,v])
                            {
                                orijentisan = false;
                            }
                        }
                    }

                    if (orijentisan == true)
                    {
                        Console.WriteLine("Graf je orijentisan.");
                    }
                    else
                    {
                        Console.WriteLine("Graf je neorijentisan.");
                    }

                    Graf_Matrica.index_cvorova = prvobitni_index;
                }
                if (akcija == 6)
                {
                    Console.WriteLine("Zao nam je ali nismo jos uvek napravili ovu opciju. Izaberite nesto drugo.");
                }
                if (akcija == 7)
                {
                    List<Grana> grane = Funkcije.Popuni_i_Sortiraj_Grane(graf);
                    List<Cvor> cvorovi = new List<Cvor>();
                    foreach (Cvor cvor in graf.cvorici)
                    {
                        cvorovi.Add(cvor);
                    }
                    Console.WriteLine("Unesite ime pocetnog cvora.");
                    string ime = Console.ReadLine();
                    foreach (Cvor cvor in cvorovi)
                    {
                        if (cvor.ime != ime)
                        {
                            cvor.najkraci_put = int.MaxValue;
                        }
                    }
                    bool promena = false;

                    while (true)
                    {
                        promena = false;
                        foreach(Grana grana in grane)
                        {
                            Cvor odakle = new Cvor();
                            Cvor dokle = new Cvor();
                            foreach (Cvor cvor in cvorovi)
                            {
                                if (cvor.ime == grana.odakle.ime)
                                {
                                    odakle.najkraci_put = cvor.najkraci_put;
                                }
                                if (cvor.ime == grana.dokle.ime)
                                {
                                    dokle.najkraci_put = cvor.najkraci_put;
                                }
                            }
                            if (odakle.najkraci_put != int.MaxValue)
                            {
                                if ((odakle.najkraci_put + grana.tezina) < dokle.najkraci_put)
                                {
                                    foreach (Cvor cvor in cvorovi)
                                    {
                                        if (cvor.ime == grana.dokle.ime)
                                        {
                                            cvor.najkraci_put = odakle.najkraci_put + grana.tezina;
                                            promena = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (promena == false)
                        {
                            break;
                        }
                    }
                    foreach (Cvor cvor in cvorovi)
                    {
                        Console.WriteLine("Najkraci put do cvora " + cvor.ime + " je " + cvor.najkraci_put);
                    }
                }
                if (akcija == 8)
                {
                    List<Grana> grane = Funkcije.Popuni_i_Sortiraj_Grane(graf);
                    List<Grana> stablo = new List<Grana>();
                    List<Grana> kandidati = new List<Grana>();
                    const int kp = 1;
                    kandidati.Add(grane.First());
                    int ukupna_tezina = 0;

                    while (kandidati.Count > 0)
                    {
                        Grana tekuca_grana = kandidati.First();
                        kandidati.RemoveAt(0);
                        if ((Funkcije.Get_kp(tekuca_grana.odakle, graf.cvorici) != kp) || (Funkcije.Get_kp(tekuca_grana.dokle, graf.cvorici) != kp))
                        {
                            Funkcije.Povezi(tekuca_grana, kp, graf.cvorici);
                            stablo.Add(tekuca_grana);
                            kandidati.AddRange(Funkcije.Pronadji_Kandidate(tekuca_grana.odakle, grane, graf.cvorici));
                            kandidati.AddRange(Funkcije.Pronadji_Kandidate(tekuca_grana.dokle, grane, graf.cvorici));
                            kandidati = Funkcije.Sortiraj_Grane(kandidati);
                        }                
                    }
                    Console.WriteLine("Graneko koje ulaze u stablo:");
                    foreach (Grana grana in stablo)
                    {
                        grana.toString();
                        ukupna_tezina += grana.tezina;
                    }
                    Console.WriteLine("Po Primovom algoritmu, najmanje razapinjuce stablo ima tezinu " + ukupna_tezina);
                }
                if (akcija == 9)
                {
                    List<Grana> grane = Funkcije.Popuni_i_Sortiraj_Grane(graf);
                    foreach (Cvor cvor in graf.cvorici)
                    {
                        cvor.komponenta_povezanosti = 0;
                    }
                    int ukupan_put = 0;
                    int trenutna_komponenta = 0;

                    Console.WriteLine("Grane koje ulaze u stablo: ");

                    while (grane.Count > 0)
                    {
                        Grana trenutna_grana = grane[0];
                        int tren1 = 0;
                        int tren2 = 0;
                        string ime1 = "";
                        string ime2 = "";
                        foreach (Cvor cvor in graf.cvorici)
                        {
                            if (trenutna_grana.odakle.ime == cvor.ime)
                            {
                                tren1 = cvor.komponenta_povezanosti;
                                ime1 = cvor.ime;
                            }
                            if (trenutna_grana.dokle.ime == cvor.ime)
                            {
                                tren2 = cvor.komponenta_povezanosti;
                                ime2 = cvor.ime;
                            }
                        }

                        if ((tren1 == 0) && (tren2 == 0))
                        {
                            trenutna_komponenta++;
                            foreach (Cvor cvor in graf.cvorici)
                            {
                                if (cvor.ime == ime1)
                                {
                                    cvor.komponenta_povezanosti = trenutna_komponenta;
                                }
                                if (cvor.ime == ime2)
                                {
                                    cvor.komponenta_povezanosti = trenutna_komponenta;
                                }
                            }
                            trenutna_grana.toString();
                            ukupan_put = ukupan_put + trenutna_grana.tezina;
                        }
                        else
                        {
                            if (((tren1 == 0) && (tren2 != 0)) || ((tren1 != 0) && (tren2 == 0)))
                            {
                                if (tren1 == 0)
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (ime1 == cvor.ime)
                                        {
                                            cvor.komponenta_povezanosti = tren2;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (ime2 == cvor.ime)
                                        {
                                            cvor.komponenta_povezanosti = tren1;
                                        }
                                    }
                                }
                                trenutna_grana.toString();
                                ukupan_put = ukupan_put + trenutna_grana.tezina;
                            }
                            else
                            {
                                if ((tren1 != 0) && (tren2 != 0) && (tren1 != tren2))
                                {
                                    foreach (Cvor cvor in graf.cvorici)
                                    {
                                        if (cvor.komponenta_povezanosti == tren2)
                                        {
                                            cvor.komponenta_povezanosti = tren1;
                                        }
                                    }
                                    trenutna_grana.toString();
                                    ukupan_put = ukupan_put + trenutna_grana.tezina;
                                }
                            }
                        }
                        grane.Remove(trenutna_grana);
                    }

                    Console.WriteLine("Po Kruksalovom algoritmu, najmanje razapinjuce stablo ima tezinu " + ukupan_put);
                }
                if (akcija == 0)
                {
                    break;
                }
                if ((akcija != 1) && (akcija != 2) && (akcija != 3) && (akcija != 4) && (akcija != 5) && (akcija != 6) && (akcija != 7) && (akcija != 8) && (akcija != 9))
                {
                    Console.WriteLine("Uneli ste nepostojeci broj akcije.");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Drago nam je sto ste koristili nas program. Nadamo se da ste uzivali.\nDodjite nam ponovo.\nZbogom!");

        }
    }
}
