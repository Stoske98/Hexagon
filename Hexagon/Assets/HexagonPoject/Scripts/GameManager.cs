using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region GameManager Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    #endregion
    //units
    [SerializeField] private GameObject[] unitsPrefab;
    public List<Unit> units = new List<Unit>();
    private List<Unit> deadP1Units = new List<Unit>();
    private List<Unit> deadP2Units = new List<Unit>();
    private Unit selectedUnit;
    private bool isPlayer1Turn = true;


    //Hexes
    private Hex hitHex;
    private Hex currentHoverHex;
    private List<Hex> availableMoves = new List<Hex>();
    private List<Hex> specialMoves = new List<Hex>();

    //Color
    public Color hoverColor;
    public Color movesColor;
    public Color specialMovesColor;

    //Projectil
    public GameObject projectilPrefab;

    private Hex[,] map;
    void Start()
    {
        SpawnUnits();
        map = Map.Instance.map;
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        UpdateUnits();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            foreach (var hex in map)
            {
                if (!hex.Walkable)
                {
                    hex.GameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", Color.black);
                }
            }
        }
    }

    private void Raycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Hex")))
        {
            OnHitHex(hit);
        }
    }

    private void OnHitHex(RaycastHit hit)
    {
        hitHex = Map.Instance.ReturnHex(hit.transform.parent.gameObject);

        Hover();
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }
        if (selectedUnit != null && Input.GetMouseButtonDown(1))
        {
            MoveUnit(hitHex);
            Attack(hitHex);
            RemoveHighlightHexes();
        }

    }

    private void Attack(Hex hex)
    {
        Unit enemyUnit = Map.Instance.GetUnit(hex);
        if (enemyUnit != null && enemyUnit.Team != selectedUnit.Team)
            selectedUnit.Attack(enemyUnit);
    }

    private void Hover()
    {
        if (currentHoverHex != hitHex)
        {
            if (currentHoverHex != null)
            {
                map[currentHoverHex.Column, currentHoverHex.Row].resetColor();
            }

            map[hitHex.Column, hitHex.Row].setColor(hoverColor, true);
            currentHoverHex = hitHex;
        }
    }

    private void SelectUnit()
    {
        RemoveHighlightHexes();
        selectedUnit = Map.Instance.GetUnit(hitHex);
        if (Input.GetMouseButtonDown(0) && selectedUnit != null)
        {
            availableMoves = selectedUnit.getAvailableMoves(map[selectedUnit.Column, selectedUnit.Row]);
            specialMoves = selectedUnit.getSpecialMoves(map[selectedUnit.Column, selectedUnit.Row]);
            HighlightHexes();
        }
    }

    public void MoveUnit(Hex hex)
    {
        if(specialMoves.Contains(hex))
            selectedUnit.SetPath(hex);
        else if (availableMoves.Contains(hex)) 
            selectedUnit.SetPath(hex);
        
    }

    private void SpawnUnits()
    {
        int player1 = 0, player2 = 1;

        // player 1 team
        units.Add(new SpawnTrikset().spawnUnit(3, 0, spawnUnit(UnitType.Trikster, 3, 0), player1));
        units.Add(new SpawnKing().spawnUnit(4, 0, spawnUnit(UnitType.King, 4, 0), player1));
        units.Add(new SpawnQueen().spawnUnit(4, 1, spawnUnit(UnitType.Queen, 4, 1), player1));
        units.Add(new SpawnWizard().spawnUnit(5, 0, spawnUnit(UnitType.Wizzard, 5, 0), player1));

        units.Add(new SpawnSwordsman().spawnUnit(0, 2, spawnUnit(UnitType.Swordsman, 0, 2), player1));
        units.Add(new SpawnSwordsman().spawnUnit(2, 2, spawnUnit(UnitType.Swordsman, 2, 2), player1));
        units.Add(new SpawnSwordsman().spawnUnit(4, 2, spawnUnit(UnitType.Swordsman, 4, 2), player1));
        units.Add(new SpawnSwordsman().spawnUnit(6, 2, spawnUnit(UnitType.Swordsman, 6, 2), player1));
        units.Add(new SpawnSwordsman().spawnUnit(8, 2, spawnUnit(UnitType.Swordsman, 8, 2), player1));

        units.Add(new SpawnArcher().spawnUnit(6, 1, spawnUnit(UnitType.Archer, 6, 1), player1));
        units.Add(new SpawnArcher().spawnUnit(2, 1, spawnUnit(UnitType.Archer, 2, 1), player1));

        units.Add(new SpawnTank().spawnUnit(5, 1, spawnUnit(UnitType.Tank, 5, 1), player1));
        units.Add(new SpawnTank().spawnUnit(3, 1, spawnUnit(UnitType.Tank, 3, 1), player1));

        units.Add(new SpawnKnight().spawnUnit(1, 1, spawnUnit(UnitType.Knight, 1, 1), player1));
        units.Add(new SpawnKnight().spawnUnit(7, 1, spawnUnit(UnitType.Knight, 7, 1), player1));

        // player 2 team
        units.Add(new SpawnTrikset().spawnUnit(5, 7, spawnUnit(UnitType.Trikster, 5, 7), player2));
        units.Add(new SpawnKing().spawnUnit(4, 8, spawnUnit(UnitType.King, 4, 8), player2));
        units.Add(new SpawnQueen().spawnUnit(4, 7, spawnUnit(UnitType.Queen, 4, 7), player2));
        units.Add(new SpawnWizard().spawnUnit(3, 7, spawnUnit(UnitType.Wizzard, 3, 7), player2));

        units.Add(new SpawnSwordsman().spawnUnit(0, 6, spawnUnit(UnitType.Swordsman, 0, 6), player2));
        units.Add(new SpawnSwordsman().spawnUnit(2, 6, spawnUnit(UnitType.Swordsman, 2, 6), player2));
        units.Add(new SpawnSwordsman().spawnUnit(4, 6, spawnUnit(UnitType.Swordsman, 4, 6), player2));
        units.Add(new SpawnSwordsman().spawnUnit(6, 6, spawnUnit(UnitType.Swordsman, 6, 6), player2));
        units.Add(new SpawnSwordsman().spawnUnit(8, 6, spawnUnit(UnitType.Swordsman, 8, 6), player2));

        units.Add(new SpawnArcher().spawnUnit(6, 7, spawnUnit(UnitType.Archer, 6, 7), player2));
        units.Add(new SpawnArcher().spawnUnit(2, 7, spawnUnit(UnitType.Archer, 2, 7), player2));

        units.Add(new SpawnTank().spawnUnit(5, 6, spawnUnit(UnitType.Tank, 5, 6), player2));
        units.Add(new SpawnTank().spawnUnit(3, 6, spawnUnit(UnitType.Tank, 3, 6), player2));

        units.Add(new SpawnKnight().spawnUnit(1, 6, spawnUnit(UnitType.Knight, 1, 6), player2));
        units.Add(new SpawnKnight().spawnUnit(7, 6, spawnUnit(UnitType.Knight, 7, 6), player2));

    }

    private GameObject spawnUnit(UnitType type, int column, int row)
    {
        GameObject unit = Instantiate(unitsPrefab[(int)type - 1]);
        unit.transform.position = Map.Instance.map[column, row].GameObject.transform.position;
        Map.Instance.map[column, row].Walkable = false;

        return unit;
    }

    private void HighlightHexes()
    {
        for (int i = 0; i < availableMoves.Count; i++)
            map[availableMoves[i].Column, availableMoves[i].Row].Highlight(movesColor);
        for (int i = 0; i < specialMoves.Count; i++)
            map[specialMoves[i].Column, specialMoves[i].Row].Highlight(specialMovesColor);
    }
    private void RemoveHighlightHexes()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            map[availableMoves[i].Column, availableMoves[i].Row].isHighligted = false;
            map[availableMoves[i].Column, availableMoves[i].Row].resetColor();
        }
        for (int i = 0; i < specialMoves.Count; i++)
        {
            map[specialMoves[i].Column, specialMoves[i].Row].isHighligted = false;
            map[specialMoves[i].Column, specialMoves[i].Row].resetColor();
        }

        specialMoves.Clear();
        availableMoves.Clear();
    }

    private void UpdateUnits()
    {
        foreach (Unit unit in units)
        {
            unit.Update();
        }
    }

    public GameObject InstantiatePrefab(GameObject projectil)
    {
        //Instantiate(unitsPrefab[(int)type - 1]);
        return Instantiate(projectil); ;
    }

    public void DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

}








