using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public class Funkcije
{
    public static List<Grana> Popuni_i_Sortiraj_Grane(Graf_Lista graf)
    {
        List<Grana> lista = new List<Grana>();
        for (int i = 0; i < graf.broj_grana; i++)
        {
            string[] tren_grana = graf.grane[i].Split(' ');
            Cvor pom1 = new Cvor(tren_grana[0]);
            Cvor pom2 = new Cvor(tren_grana[1]);
            Grana nova = new Grana(pom1, pom2, Convert.ToInt32(tren_grana[2]));
            lista.Add(nova);
        }

        List<Grana> SortedList = lista.OrderBy(o => o.tezina).ToList();

        return SortedList;
    }
    public static List<Grana> Sortiraj_Grane(List<Grana> lista)
    {
        List<Grana> SortedList = lista.OrderBy(o => o.tezina).ToList();

        return SortedList;
    }
    public static void Povezi(Grana grana, int kp, List<Cvor> cvorovi)
    {
        Set_kp(kp, grana.odakle, cvorovi);
        Set_kp(kp, grana.dokle, cvorovi);
    }
    public static List<Grana> Pronadji_Kandidate(Cvor cvor, List<Grana> grane, List<Cvor> cvorovi)
    {
        List<Grana> izabrane_grane = new List<Grana>();
        foreach(Grana grana in grane)
        {
            if (grana.odakle.ime == cvor.ime)
            {
                if (Get_kp(grana.dokle, cvorovi) == 0)
                {
                    izabrane_grane.Add(grana);
                }               
            }
            else if (grana.dokle.ime == cvor.ime)
            {
                if (Get_kp(grana.odakle, cvorovi) == 0)
                {
                    izabrane_grane.Add(grana);
                }
            }
        }

        return izabrane_grane;
    }
    public static int Get_kp(Cvor trazeni_cvor, List<Cvor> cvorovi)
    {
        foreach (Cvor cvor in cvorovi)
        {
            if (cvor.ime == trazeni_cvor.ime)
            {
                return cvor.komponenta_povezanosti;
            }
        }
        return 0;
    }
    public static void Set_kp(int kp, Cvor trazeni_cvor, List<Cvor> cvorovi)
    {
        foreach (Cvor cvor in cvorovi)
        {
            if (cvor.ime == trazeni_cvor.ime)
            {
                cvor.komponenta_povezanosti = kp;
                break;
            }
        }
    }
}
