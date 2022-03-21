using System.Collections.Generic;

public class AllRanks
{
    public  List<Rank> listOfRanks { get; set; }
    public AllRanks()
    {
        listOfRanks = new List<Rank>();
        listOfRanks.Add(new Rank(0, 249,"Rank 1"));
        listOfRanks.Add(new Rank(250, 499, "Rank 2"));
        listOfRanks.Add(new Rank(500, 749, "Rank 3"));
        listOfRanks.Add(new Rank(750, 999, "Rank 4"));
        listOfRanks.Add(new Rank(1000, 1249, "Rank 5"));
        listOfRanks.Add(new Rank(1250, 1449, "Rank 6"));
        listOfRanks.Add(new Rank(1500, 1749, "Rank 7"));
        listOfRanks.Add(new Rank(1750, 1999, "Rank 8"));
    }

    public Rank GetRank(int mmr)
    {
        Rank myRank = null;
        foreach (Rank rank in listOfRanks)
        {
            if(rank.intMinMMR <= mmr && mmr <= rank.intMaxMMR)
            {
                myRank = rank;
                break;
            }
        }
        return myRank;
    }

    public int GetQueuePosition(int mmr)
    {
        int position = -1;
        foreach (Rank rank in listOfRanks)
        {
            ++position;
            if (rank.intMinMMR <= mmr && mmr <= rank.intMaxMMR)
            {
                break;
            }
        }
        return position;
    }
}

