using System;

public class Cvor
{
	public string ime;
	public int index;
	public int komponenta_povezanosti = 0;
	public int najkraci_put = 0;
	public Cvor() { }
	public Cvor(string ime, string razlika)
	{
        this.ime = ime;
        Graf_Matrica.index_cvorova++;
        index = Graf_Matrica.index_cvorova;
    }
	public Cvor(string ime)
	{
		this.ime = ime;
	}
}
