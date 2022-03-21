public class Rank
{
    public int intMinMMR { get; set; }
    public int intMaxMMR { get; set; }
    public string nameMmr { get; set; }

    public Rank(int minMMR, int maxMMR, string name)
    {
        intMinMMR = minMMR;
        intMaxMMR = maxMMR;
        nameMmr = name;
    }

}

