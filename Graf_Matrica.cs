using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Graf_Matrica
{
	public int[,] matrica;
	public static int index_cvorova = -1;
	public LinkedList<Cvor> cvorovi;
	public Graf_Matrica(int br)
	{
		cvorovi = new LinkedList<Cvor>();
		matrica = new int[br, br];
	}
	public void DodajCvor(Cvor cvor)
	{
		cvorovi.AddLast(cvor);
	}
	public void DodajGranu(int odakle, int dokle, int tezina)
	{
		matrica[odakle, dokle] = tezina;
	}
	public bool ProveriPostojanjeGrane(int odakle, int dokle)
	{
		if (matrica[odakle, dokle] != 0)
		{
			return true;
		}
		else
		{
            return false;
        }
	}
	public void Ispisi()
	{
		Console.Write("  ");
		foreach (Cvor cvor in cvorovi)
		{
			Console.Write(cvor.ime + " ");
		}
		Console.WriteLine();
		
		for (int i = 0; i < index_cvorova + 1; i++)
		{
			Console.Write(cvorovi.ElementAt(i).ime + " ");
			for (int j = 0; j < index_cvorova + 1; j++)
			{
				Console.Write(matrica[i, j] + " ");
			}
			Console.WriteLine();
		}
	}
}
