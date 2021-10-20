using System.Collections.Generic;

public class Knight : Melee
{
    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        //right
        AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 2, hex.Row));
        //left
        AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 2, hex.Row));

        if (hex.Column % 2 == 0)
        {
            //top right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row + 1));
            //top left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row + 1));
            //botom left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row - 2));
            //bottom right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row - 2));
        }
        else
        {
            //top left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row + 2));
            //top right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row + 2));
            //botom left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row - 1));
            //bottom right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row - 1));
        }
        return availableMoves;
    }
    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();

        Hex desiredHex = null;
        Hex betweenHex1 = null;
        Hex betweenHex2 = null;



        if (hex.Column % 2 == 0)
        {
            //right
            desiredHex = Map.Instance.GetHex(hex.Column + 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //left
            desiredHex = Map.Instance.GetHex(hex.Column - 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row - 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row - 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
        }
        else
        {
            //right
            desiredHex = Map.Instance.GetHex(hex.Column + 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //left
            desiredHex = Map.Instance.GetHex(hex.Column - 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row + 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row + 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
        }
            return specialMoves;
    }

    public void addToSpecialMove(Hex desiredHex, Hex betweenHex1, Hex betweenHex2)
    {
        if (Map.Instance.GetUnit(betweenHex1) != null && Map.Instance.GetUnit(betweenHex1).Team != Team || Map.Instance.GetUnit(betweenHex2) != null && Map.Instance.GetUnit(betweenHex2).Team != Team)
            AddHexToSpecialMoveList(desiredHex);
    }
    public override void SetPath(Hex hex)
    {
        path.Add(hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;

    }
}

