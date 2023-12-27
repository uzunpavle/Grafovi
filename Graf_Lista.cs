using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Graf_Lista
{
	public Dictionary<string, LinkedList<Cvor>> lista;
	public int broj_cvorova;
	public int broj_grana = 0;
	public string cvorovi;
	public List<Cvor> cvorici = new List<Cvor>();
	public string[] grane = new string[100];
    public Graf_Lista()
	{
		lista = new Dictionary<string, LinkedList<Cvor>>();
	}
	public void DodajCvor(Cvor cvor)
	{
		LinkedList<Cvor> TrenutnaLista = new LinkedList<Cvor>();
		cvorici.Add(cvor);
		TrenutnaLista.AddLast(cvor);
		lista.Add(cvor.ime, TrenutnaLista);
	}
	public void DodajGranu(Grana grana)
	{
		LinkedList<Cvor> TrenutnaLista = lista[grana.odakle.ime];
        LinkedList<Cvor> DodajLista = lista[grana.dokle.ime];
		Cvor Dokle = DodajLista.ElementAt(0);
		TrenutnaLista.AddLast(Dokle);

    }
	public bool ProveriPostojanjeGrane(Cvor odakle, Cvor dokle)
	{
        LinkedList<Cvor> TrenutnaLista = lista[odakle.ime];
        LinkedList<Cvor> DodajLista = lista[dokle.ime];
        Cvor Dokle = DodajLista.ElementAt(0);
		foreach (Cvor cvor in TrenutnaLista)
		{
			if (cvor == Dokle)
			{
				return true;
			}
		}
		return false;
    }
	public void Ispisi()
	{
		foreach (LinkedList<Cvor> TrenutnaLista in lista.Values)
		{
			foreach (Cvor cvor in TrenutnaLista)
			{
				Console.Write(cvor.ime + " -> ");
			}
			Console.WriteLine();
		}
	}
}
