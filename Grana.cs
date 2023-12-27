using System;

public class Grana
{
    public Cvor odakle;
    public Cvor dokle;
    public int tezina;
    public Grana() { }
	public Grana(Cvor odakle, Cvor dokle, int tezina)
    {
        this.odakle = odakle;
        this.dokle = dokle;
        this.tezina = tezina;
    }
    public void toString()
    {
        Console.WriteLine(odakle.ime + " " + dokle.ime + " " + tezina);
    }
}
