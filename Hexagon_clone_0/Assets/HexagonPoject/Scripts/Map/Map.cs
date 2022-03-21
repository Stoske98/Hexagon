using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    #region Map Singleton
    private static Map _instance;

    public static Map Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            map = new Hex[N, N];
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {

                    Hex hex = new Hex(c, r, Instantiate(hexPrefab), Color.white);
                    hex.SetPosition();
                    hex.GameObject.transform.SetParent(transform);
                    hex.setColor(Color.white, false);
                    map[c, r] = hex;

                }
            }

            foreach (Hex hex in map)
            {
                SetNeighbors(hex);
            }

            hexThatIsNotInGame();
        }

    }
    #endregion
    public int N;
    [SerializeField] private GameObject hexPrefab;

    public Hex[,] map;

    public Hex GetHex(int col, int row)
    {
        if (!IsInBounds(col, row))
        {
            return null;
        }

        return map[col, row];
    }
    private bool IsInBounds(int col, int row)

    {
        bool rowInRange = row >= 0 && row < N;
        bool colInRange = col >= 0 && col < N;
        return rowInRange && colInRange;
    }

    public void hexThatIsNotInGame()
    {
        List<Hex> lista = new List<Hex>();
        lista.Add(GetHex(0, 0));
        lista.Add(GetHex(0, 1));
        lista.Add(GetHex(1, 0));
        lista.Add(GetHex(2, 0));
        lista.Add(GetHex(6, 0));
        lista.Add(GetHex(7, 0));
        lista.Add(GetHex(8, 0));
        lista.Add(GetHex(8, 1));

        lista.Add(GetHex(0, 7));
        lista.Add(GetHex(1, 7));
        lista.Add(GetHex(7, 7));
        lista.Add(GetHex(8, 7));
        lista.Add(GetHex(0, 8));
        lista.Add(GetHex(1, 8));
        lista.Add(GetHex(2, 8));
        lista.Add(GetHex(3, 8));
        lista.Add(GetHex(5, 8));
        lista.Add(GetHex(6, 8));
        lista.Add(GetHex(7, 8));
        lista.Add(GetHex(8, 8));
        foreach (var hex in lista)
        {
            hex.Walkable = false;
            hex.GameObject.SetActive(false);
        }

        //GetHex(4, 4).Walkable = false;
    }

    public void SetNeighbors(Hex hex)
    {
        hex.neighbors = new List<Hex>();
        if (hex.Column % 2 == 0)
        {
            foreach (Vector2Int v2 in hex.evenColumnNeighbors)
            {
                if (GetHex(hex.Column + v2.x, hex.Row + v2.y) != null)
                    hex.neighbors.Add(GetHex(hex.Column + v2.x, hex.Row + v2.y));
            }
        }
        else
        {
            foreach (Vector2Int v2 in hex.oddColumnNeighbors)
            {
                if (GetHex(hex.Column + v2.x, hex.Row + v2.y) != null)
                    hex.neighbors.Add(GetHex(hex.Column + v2.x, hex.Row + v2.y));
            }
        }
    }

    public Unit GetUnit(Hex hex)
    {
        if (hex == null)
            return null;
        foreach (Unit unit in GameManager.Instance.units)
        {
            if (unit.Column == hex.Column && unit.Row == hex.Row)
                return unit;
        }
        return null;
    }

    public Hex ReturnHex(GameObject gameObject)
    {
        foreach (Hex hex in Map.Instance.map)
        {

            if (hex.GameObject == gameObject)
            {
                return hex;
            }
        }
        return null;
    }

}
