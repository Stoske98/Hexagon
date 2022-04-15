using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //user
    [HideInInspector]
    public string user_nickame;
    [HideInInspector]
    public int user_rank;
    [HideInInspector]
    public string user_specialID;
    [HideInInspector]
    public int selected_class;
    //units
    public List<Unit> units = new List<Unit>();
    public List<GameObject[]> Classes = new List<GameObject[]>();
    public GameObject[] Dark;
    public GameObject[] Light;
    [HideInInspector]
    public List<Unit> deadP1Units = new List<Unit>();
    [HideInInspector]
    public List<Unit> deadP2Units = new List<Unit>();
    public Unit selectedUnit;
    private bool isPlayer1Turn = true;
    private Unit hitUnit = null;
    private Unit currentHoverUnit = null;

    //Hexes
    private Hex hitHex;
    private Hex currentHoverHex;
    private Hex selectedHex;
    public List<Hex> availableMoves = new List<Hex>();
    public List<Hex> specialMoves = new List<Hex>();
    public List<Hex> attackMoves = new List<Hex>();
    public List<Hex> abilityHexes = new List<Hex>();

    //Color
    public Color hoverColor;
    public Color movesColor;
    public Color specialMovesColor;
    public Color abilityColor;

    //Projectil
    public GameObject projectilPrefab;

    private Hex[,] map;

    //Game rule
    [HideInInspector]
    public int numberOfMoves = 1;
    private bool kingOnCentar = false;
    private int moveWhenKingStepOnCenter;
    private int outerFields = 4;

    //Camera
    [SerializeField] public GameObject[] cameraAngles;
    private int cameraIndex = -1;
    public CameraController cameraController;
    // Multiplayer logic
    [HideInInspector]
    public int currentTeam = -1;

    //Health Bar
    [Header("Health Bar")]
    public GameObject CanvasHBPrefab;
    public Image HealthImagePrefab;

    public float EndMoveTimer = 5f;

    private bool TargetableAbilityActive = false;
    private KeyCode KeyCode;
    private bool IsMapCreated;

    public Text txt_moves;

    void Start()
    {
        selected_class = -1;
    }
    public void StartGame()
    {
        map = Map.Instance.map;
        //SpawnUnits();
        Classes.Add(Light); // position 0
        Classes.Add(Dark); // position 1

        SpawnTeam(Classes[(int)ClassType.Light], 0, ClassType.Light);
        SpawnTeam(Classes[(int)ClassType.Dark], 1, ClassType.Dark);

        foreach (var unit in units)
        {
            unit.healthBar = new HealthBar(CanvasHBPrefab, HealthImagePrefab);
            unit.InitializeHealthBar();
        }
        IsMapCreated = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(IsMapCreated)
        {
            Raycast();
            UpdateUnits();
        }
    }

    private void LateUpdate()
    {
        foreach (Unit unit in units)
        {
            unit.UpdateBar(cameraAngles[currentTeam + 1]);
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

        if (selectedUnit != null && (selectedUnit.Team == 0 && isPlayer1Turn && currentTeam == 0 || selectedUnit.Team == 1 && !isPlayer1Turn && currentTeam == 1))
        {
            if (Input.GetKeyDown(KeyCode.Q) && selectedUnit.ability1 != null && selectedUnit.ability1.CoolDown == 0)
            {
                switch (selectedUnit.ability1.AbilityType)
                {
                    case AbilityType.Targetable:
                        RemoveHighlightHexes();
                        abilityHexes = selectedUnit.ability1.GetAbilityHexes(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row));
                        HighlightAbilityMoves();
                        KeyCode = KeyCode.Q;
                        TargetableAbilityActive = true;
                        break;
                    case AbilityType.Instant:

                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.W) && selectedUnit.ability2 != null && selectedUnit.ability2.CoolDown == 0)
            {
                switch (selectedUnit.ability2.AbilityType)
                {
                    case AbilityType.Targetable:
                        RemoveHighlightHexes();
                        abilityHexes = selectedUnit.ability2.GetAbilityHexes(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row));
                        HighlightAbilityMoves();
                        KeyCode = KeyCode.W;
                        TargetableAbilityActive = true;
                        break;
                    case AbilityType.Instant:

                        break;
                }
            }

        }
        if (TargetableAbilityActive)
        {
            if (Input.GetMouseButtonDown(0) && Map.Instance.GetUnit(hitHex) != null)
            {
                switch (KeyCode)
                {
                    case KeyCode.Q:
                        UseTargetAbility(selectedUnit, Map.Instance.GetUnit(hitHex), selectedUnit.Team, 1, selectedUnit.ability1);
                        RemoveHighlightHexes();
                        break;
                    case KeyCode.W:
                        UseTargetAbility(selectedUnit, Map.Instance.GetUnit(hitHex), selectedUnit.Team, 2, selectedUnit.ability2);
                        RemoveHighlightHexes();
                        break;
                        /*case KeyCode.P:
                            UseTargetAbility(selectedUnit, Map.Instance.GetUnit(hitHex), selectedUnit.Team, 1, selectedUnit.ability1);
                            break;*/
                }


            }//else ispis unit is not there
            if (Input.GetMouseButtonDown(1))
            {
                KeyCode = KeyCode.None;
                TargetableAbilityActive = false;
                RemoveHighlightHexes();
                HighlightHexes();


            }
            return;
        }
        if (selectedUnit != null && Input.GetMouseButtonDown(0))
        {
            if ((selectedUnit.Team == 0 && isPlayer1Turn && currentTeam == 0) || (selectedUnit.Team == 1 && !isPlayer1Turn && currentTeam == 1))
            {
                MoveUnit(hitHex);
                RemoveHighlightHexes();
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }

        if (selectedUnit != null && Input.GetMouseButtonDown(1))
            if (selectedUnit.Team == 0 && isPlayer1Turn && currentTeam == 0 || selectedUnit.Team == 1 && !isPlayer1Turn && currentTeam == 1)
                Attack(hitHex);

    }
    private void UseTargetAbility(Unit CastUnit, Unit RecieveUnit, int team, int keycode, Ability ability)
    {
        if (PathFinder.InRange(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row), hitHex, ability.Range))
        {
            ClientJobSystem.UseTargetAbility(CastUnit, RecieveUnit, team, keycode);
            ability.UseAbility(hitHex);
            ability.CoolDown = ability.MaxCooldown;
            EndTurn();
            //Invoke("SwordsmanPassive", EndMoveTimer);
            KeyCode = KeyCode.None;
            TargetableAbilityActive = false;
        }

    }
    private void Attack(Hex hex)
    {
        Unit enemyUnit = Map.Instance.GetUnit(hex);
        if (enemyUnit != null && enemyUnit.Team != selectedUnit.Team && attackMoves.Contains(hitHex))
        {
            ClientJobSystem.AttackUnitRequst(selectedUnit, enemyUnit, selectedUnit.Team);
            selectedUnit.Attack(enemyUnit);
            EndTurn();
           // Invoke("SwordsmanPassive", EndMoveTimer);
            RemoveHighlightHexes();
        }

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
            hitUnit = Map.Instance.GetUnit(currentHoverHex);
            if (hitUnit != null)
            {
                if (currentHoverUnit != hitUnit)
                {
                    if (currentHoverUnit == null)
                        currentHoverUnit = hitUnit;
                    currentHoverUnit.healthBar.CanvasHB.SetActive(false);
                    currentHoverUnit = hitUnit;
                    currentHoverUnit.healthBar.CanvasHB.SetActive(true);
                }

            }
            else
            {
                if (currentHoverUnit != null)
                {
                    currentHoverUnit.healthBar.CanvasHB.SetActive(false);
                    currentHoverUnit = null;
                    hitUnit = null;
                }

            }
        }
    }

    private void SelectUnit()
    {
        selectedUnit = Map.Instance.GetUnit(hitHex);
        if (selectedUnit != null)
        {
            //show stats
            GameUiManager.Instance.ShowUnitStats(selectedUnit);
            if (selectedUnit.Team == 0 && isPlayer1Turn && currentTeam == 0 || selectedUnit.Team == 1 && !isPlayer1Turn && currentTeam == 1)
                GetAllPossibilities();
            else
                RemoveHighlightHexes();
        }
    }

    public void GetAllPossibilities()
    {
        RemoveHighlightHexes();
        if (selectedUnit != null && !selectedUnit.isDeath && selectedUnit.currentState != UnitState.Stuned && selectedUnit.Team == currentTeam)
        {
            selectedHex = map[selectedUnit.Column, selectedUnit.Row];
            availableMoves = selectedUnit.getAvailableMoves(map[selectedUnit.Column, selectedUnit.Row]);
            specialMoves = selectedUnit.getSpecialMoves(map[selectedUnit.Column, selectedUnit.Row]);
            attackMoves = selectedUnit.getAttackMoves(map[selectedUnit.Column, selectedUnit.Row]);
            HighlightHexes();
        }
    }

    public void MoveUnit(Hex hex)
    {
        if (specialMoves.Contains(hex))
        {
            if (!selectedUnit.GetType().IsSubclassOf(typeof(Trikster)))
            {
                ClientJobSystem.MoveUnitRequest(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row), hex, selectedUnit.Team);
                selectedUnit.isSpecialMove = true;
                selectedUnit.SetPath(hex);
                EndTurn();
            }
            else
            {
                Trikster trikster = selectedUnit as Trikster;
                specialMoves.Remove(hex);
                if (specialMoves.Count != 0)
                {
                    Vector2Int hexPos1 = trikster.createIllusion(ref trikster.ilusion1);
                    Vector2Int hexPos2 = trikster.createIllusion(ref trikster.ilusion2);
                    ClientJobSystem.MoveTrikserRequest(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row), hex, selectedUnit.Team, hexPos1, hexPos2);
                } else
                    ClientJobSystem.MoveUnitRequest(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row), hex, selectedUnit.Team);

                specialMoves.Add(hex);
                trikster.isSpecialMove = true;
                trikster.SetPath(hex);
                EndTurn();
            }

        }
        else if (availableMoves.Contains(hex))
        {
            ClientJobSystem.MoveUnitRequest(Map.Instance.GetHex(selectedUnit.Column, selectedUnit.Row), hex, selectedUnit.Team);
            selectedUnit.SetPath(hex);
            EndTurn();
        }

    }
    private void SpawnTeam(GameObject[] unitsPrefabs, int team, ClassType type)
    {
        if (team == 0)
        {
            units.Add(new SpawnTrikset().spawnUnit(3, 0, spawnUnit(UnitType.Trikster, 3, 0, unitsPrefabs), team, type));
            units.Add(new SpawnKing().spawnUnit(4, 0, spawnUnit(UnitType.King, 4, 0, unitsPrefabs), team, type));
            units.Add(new SpawnQueen().spawnUnit(4, 1, spawnUnit(UnitType.Queen, 4, 1, unitsPrefabs), team, type));
            units.Add(new SpawnWizard().spawnUnit(5, 0, spawnUnit(UnitType.Wizzard, 5, 0, unitsPrefabs), team, type));

            // units.Add(new SpawnSwordsman().spawnUnit(0, 2, spawnUnit(UnitType.Swordsman, 0, 2), player1));
            //units.Add(new SpawnTank().spawnUnit(4, 2, spawnUnit(UnitType.Tank, 4, 2, unitsPrefabs), team, type));
            //units.Add(new SpawnSwordsman().spawnUnit(4, 2, spawnUnit(UnitType.Swordsman, 4, 2,  unitsPrefabs), team));
            //units.Add(new SpawnSwordsman().spawnUnit(4, 2, spawnUnit(UnitType.Swordsman, 4, 2, unitsPrefabs), team, type));

            //units.Add(new SpawnTank().spawnUnit(2, 2, spawnUnit(UnitType.Tank, 2, 2, unitsPrefabs), team, type));
          //  units.Add(new SpawnTank().spawnUnit(6, 2, spawnUnit(UnitType.Tank, 6, 2,  unitsPrefabs), team, type));
            // units.Add(new SpawnSwordsman().spawnUnit(8, 2, spawnUnit(UnitType.Swordsman, 8, 2), player1));

            units.Add(new SpawnKnight().spawnUnit(6, 1, spawnUnit(UnitType.Knight, 6, 1, unitsPrefabs), team, type));
            units.Add(new SpawnKnight().spawnUnit(2, 1, spawnUnit(UnitType.Knight, 2, 1, unitsPrefabs), team, type));

            /*units.Add(new SpawnSwordsman().spawnUnit(5, 1, spawnUnit(UnitType.Swordsman, 5, 1, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(3, 1, spawnUnit(UnitType.Swordsman, 3, 1, unitsPrefabs), team, type));


            units.Add(new SpawnArcher().spawnUnit(1, 1, spawnUnit(UnitType.Archer, 1, 1, unitsPrefabs), team, type));
            units.Add(new SpawnArcher().spawnUnit(7, 1, spawnUnit(UnitType.Archer, 7, 1, unitsPrefabs), team, type));*/

            //d
            units.Add(new SpawnTank().spawnUnit(3, 1, spawnUnit(UnitType.Tank, 3, 1, unitsPrefabs), team, type));
            units.Add(new SpawnTank().spawnUnit(5, 1, spawnUnit(UnitType.Tank, 5, 1, unitsPrefabs), team, type));
            units.Add(new SpawnArcher().spawnUnit(1, 1, spawnUnit(UnitType.Archer, 1, 1, unitsPrefabs), team, type));
            units.Add(new SpawnArcher().spawnUnit(7, 1, spawnUnit(UnitType.Archer, 7, 1, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(4, 2, spawnUnit(UnitType.Swordsman, 4, 2, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(2, 2, spawnUnit(UnitType.Swordsman, 2, 2, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(6, 2, spawnUnit(UnitType.Swordsman, 6, 2, unitsPrefabs), team, type));
        }
        else
        {

            units.Add(new SpawnTrikset().spawnUnit(5, 7, spawnUnit(UnitType.Trikster, 5, 7, unitsPrefabs), team, type));
            units.Add(new SpawnKing().spawnUnit(4, 8, spawnUnit(UnitType.King, 4, 8, unitsPrefabs), team, type));
            units.Add(new SpawnQueen().spawnUnit(4, 7, spawnUnit(UnitType.Queen, 4, 7, unitsPrefabs), team, type));
            units.Add(new SpawnWizard().spawnUnit(3, 7, spawnUnit(UnitType.Wizzard, 3, 7, unitsPrefabs), team, type));

            //units.Add(new SpawnSwordsman().spawnUnit(0, 6, spawnUnit(UnitType.Swordsman, 0, 6), player2));
            //units.Add(new SpawnTank().spawnUnit(4, 6, spawnUnit(UnitType.Tank, 4, 6, unitsPrefabs), team, type));
            //units.Add(new SpawnSwordsman().spawnUnit(4, 6, spawnUnit(UnitType.Swordsman, 4, 6,  unitsPrefabs), team, type));

           // units.Add(new SpawnTank().spawnUnit(2, 6, spawnUnit(UnitType.Tank, 2, 6, unitsPrefabs), team, type));
           // units.Add(new SpawnTank().spawnUnit(6, 6, spawnUnit(UnitType.Tank, 6, 6,  unitsPrefabs), team, type));
            //units.Add(new SpawnSwordsman().spawnUnit(8, 6, spawnUnit(UnitType.Swordsman, 8, 6), player2));

            units.Add(new SpawnKnight().spawnUnit(6, 7, spawnUnit(UnitType.Knight, 6, 7, unitsPrefabs), team, type));
            units.Add(new SpawnKnight().spawnUnit(2, 7, spawnUnit(UnitType.Knight, 2, 7, unitsPrefabs), team, type));

            /* units.Add(new SpawnSwordsman().spawnUnit(5, 6, spawnUnit(UnitType.Swordsman, 5, 6, unitsPrefabs), team, type));
             units.Add(new SpawnSwordsman().spawnUnit(3, 6, spawnUnit(UnitType.Swordsman, 3, 6, unitsPrefabs), team, type));

             units.Add(new SpawnArcher().spawnUnit(1, 6, spawnUnit(UnitType.Archer, 1, 6, unitsPrefabs), team, type));
             units.Add(new SpawnArcher().spawnUnit(7, 6, spawnUnit(UnitType.Archer, 7, 6, unitsPrefabs), team, type));*/

            //d
            units.Add(new SpawnTank().spawnUnit(3, 6, spawnUnit(UnitType.Tank, 3, 6, unitsPrefabs), team, type));
            units.Add(new SpawnTank().spawnUnit(5, 6, spawnUnit(UnitType.Tank, 5, 6, unitsPrefabs), team, type));
            units.Add(new SpawnArcher().spawnUnit(1, 6, spawnUnit(UnitType.Archer, 1, 6, unitsPrefabs), team, type));
            units.Add(new SpawnArcher().spawnUnit(7, 6, spawnUnit(UnitType.Archer, 7, 6, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(2, 6, spawnUnit(UnitType.Swordsman, 2, 6, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(4, 6, spawnUnit(UnitType.Swordsman, 4, 6, unitsPrefabs), team, type));
            units.Add(new SpawnSwordsman().spawnUnit(6, 6, spawnUnit(UnitType.Swordsman, 6, 6, unitsPrefabs), team, type));
        }
    }
    public GameObject spawnUnit(UnitType type, int column, int row, GameObject[] units)
    {
        GameObject unit = Instantiate(units[(int)type - 1]);
        if (row > 4)
            unit.transform.eulerAngles = new Vector3(0, 180, 0);
        unit.transform.position = map[column, row].GameObject.transform.position;
        map[column, row].Walkable = false;

        return unit;
    }



    public void EndTurn()
    {
        ++numberOfMoves;
        CleanCC();
        isPlayer1Turn = !isPlayer1Turn;
        CleanArcherSpecial();
        UpdateMoves();
    } 
     
    public void SwordsmanPassive(int team)
    {
        List<Swordsman> swordsmens = new List<Swordsman>();
        foreach (Unit unit in units)
        {
            if (unit.GetType().IsSubclassOf(typeof(Swordsman)) && unit.Team == team)
            {
                if (!unit.isDeath)
                    swordsmens.Add(unit as Swordsman);
            }
        }
        foreach (Swordsman swordsman in swordsmens)
        {
           // Debug.Log("Swordsman team: " + swordsman.Team + "\n postion: " + swordsman.Column + "," + swordsman.Row);
            swordsman.PassiveAttack();
        }

    }

    private void SwordsmanPassiveSendToServer()
    {
        ClientJobSystem.SwordsmanPassive();
    }

    public void InvokeEndTurn()
    {
        Invoke("EndTurn", EndMoveTimer);
        Invoke("SwordsmanPassiveSendToServer", EndMoveTimer);
        Invoke("CheckForAcrhcerSpecialAbility", EndMoveTimer);
        Invoke("CheckToRemoveFields", EndMoveTimer);
        Invoke("GetAllPossibilities", EndMoveTimer);
        Invoke("AbilityCDController", EndMoveTimer);
        
    }

    private void CleanArcherSpecial()
    {
            foreach (Unit unit in units)
            {
                if (unit.GetType().IsSubclassOf(typeof(Archer)))
                {
                    Archer archer = unit as Archer;
                    if (!archer.isDeath && archer.passive)
                    {
                        archer.DeactivateArcherPassive();
                    }
                }
            }
    }

    private void CheckForAcrhcerSpecialAbility()
    {
        Unit[] archers = new Unit[2];
        foreach (Unit archer in units)
        {
            if (archer.GetType().IsSubclassOf(typeof(Archer)))
            {
                if (!archer.isDeath && archer.Team == currentTeam && archer.Column != -1 && archer.Row != -1)
                    if (archers[0] == null)
                        archers[0] = archer;
                    else
                    {
                        archers[1] = archer;
                        break;
                    }
            }
        }
        ClientJobSystem.ArcherSpecialAbilityRequest(archers[0], archers[1]);
    }

    public void KingActivateSpecialAbility()
    {
        if(!kingOnCentar)
        {
            ClientJobSystem.ActivateKingSpecialAbility(numberOfMoves - 1);
        }
    }

    public void ActivateKingSpecialAbility(int nom)
    {
        moveWhenKingStepOnCenter = nom;
        kingOnCentar = true;
    }

    private void CheckToRemoveFields()
    {
        if(kingOnCentar)
        {
            if ((numberOfMoves - moveWhenKingStepOnCenter) == 10 || (numberOfMoves - moveWhenKingStepOnCenter) == 20 || (numberOfMoves - moveWhenKingStepOnCenter) == 30)
            {
                ClientJobSystem.RemoveFields();
            }
            ClientJobSystem.ChalengeRoyalCounter();
        }
    }

    public void RemoveFields()
    {
        ReduceNumberOfFields(outerFields, outerFields - 1);
        outerFields--;
    }

    public void UpdateMoves()
    {
        txt_moves.text = numberOfMoves.ToString();
    }
    private void ReduceNumberOfFields(int of, int inerFields)
    {
        List<Hex> of_hexes = PathFinder.BFS_HexesInRange(Map.Instance.GetHex(4, 4), of);
        List<Hex> if_hexes = PathFinder.BFS_HexesInRange(Map.Instance.GetHex(4, 4), inerFields);
        foreach (Hex hex in if_hexes)
        {
            if (of_hexes.Contains(hex))
                of_hexes.Remove(hex);
        }

        foreach (Hex h in of_hexes)
        {
            Unit unit = Map.Instance.GetUnit(h);
            if(unit != null)
            {
                unit.RecieveDamage(unit.CurrentHealth);
            }
            h.Walkable = false;
            h.GameObject.SetActive(false);
        }
    }

    private void CleanCC()
    {
        Dictionary<Unit, CC> removeCCFromList = new Dictionary<Unit, CC>();
        foreach (Unit unit in units)
        {
            foreach (CC cc in unit.cc)
            {
                if (cc.RanOut())
                    removeCCFromList.Add(unit, cc);
            }
        }
        foreach (var pair in removeCCFromList)
        {
            pair.Key.cc.Remove(pair.Value);
        }
        removeCCFromList.Clear();
    }

    public void AbilityCDController()
    {
        foreach (Unit unit in units)
        {
            if (unit.Team == currentTeam)
            {
                if (unit.ability1 != null && unit.ability1.CoolDown != 0)
                    unit.ability1.CoolDown -= 1;
                if (unit.ability2 != null && unit.ability2.CoolDown != 0)
                    unit.ability2.CoolDown -= 1;
                /* if (unit.ability1 != null && unit.ability1.CoolDown != 0)
                     unit.ability1.CoolDown -= 1;*/
            }
        }
    }


    private void HighlightAbilityMoves()
    {
        for (int i = 0; i < abilityHexes.Count; i++)
            map[abilityHexes[i].Column, abilityHexes[i].Row].Highlight(abilityColor);

    }
    private void HighlightHexes()
    {
        for (int i = 0; i < availableMoves.Count; i++)
            map[availableMoves[i].Column, availableMoves[i].Row].Highlight(movesColor);
        for (int i = 0; i < attackMoves.Count; i++)
            map[attackMoves[i].Column, attackMoves[i].Row].Highlight(Color.red);
        for (int i = 0; i < specialMoves.Count; i++)
            map[specialMoves[i].Column, specialMoves[i].Row].Highlight(specialMovesColor);

        map[selectedHex.Column, selectedHex.Row].Highlight(Color.green);
    }
    private void RemoveHighlightHexes()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            map[availableMoves[i].Column, availableMoves[i].Row].isHighligted = false;
            map[availableMoves[i].Column, availableMoves[i].Row].resetColor();
        }
        for (int i = 0; i < attackMoves.Count; i++)
        {
            map[attackMoves[i].Column, attackMoves[i].Row].isHighligted = false;
            map[attackMoves[i].Column, attackMoves[i].Row].resetColor();
        }
        for (int i = 0; i < specialMoves.Count; i++)
        {
            map[specialMoves[i].Column, specialMoves[i].Row].isHighligted = false;
            map[specialMoves[i].Column, specialMoves[i].Row].resetColor();
        }
        for (int i = 0; i < abilityHexes.Count; i++)
        {
            map[abilityHexes[i].Column, abilityHexes[i].Row].isHighligted = false;
            map[abilityHexes[i].Column, abilityHexes[i].Row].resetColor();
        }
        if (selectedHex != null)
        {
            map[selectedHex.Column, selectedHex.Row].isHighligted = false;
            map[selectedHex.Column, selectedHex.Row].resetColor();
            selectedHex = null;
        }
        attackMoves.Clear();
        specialMoves.Clear();
        availableMoves.Clear();
        abilityHexes.Clear();
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
        return Instantiate(projectil);
    }

    public GameObject InstantiatePrefabOnPosition(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab,position,Quaternion.identity);
    }

    public Image InstantiatePrefabImage(Image image)
    {
        return Instantiate(image);
    }

    public void DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void OnUnitDeath(float time, Unit unit)
    {
        if (selectedUnit == unit)
            RemoveHighlightHexes();
        map[unit.Column, unit.Row].Walkable = true;
        unit.healthBar.CanvasHB.SetActive(false);
        unit.Column = -1;
        unit.Row = -1;
        StartCoroutine(UnitDeathTimer(time, unit));
    }
    

    public void ChangeCamera()
    {
        if (currentTeam == 0)
        {
            foreach (Unit unit in Instance.units)
            {
                KingLight kl = unit as KingLight;
                if (kl != null)
                {
                    selectedUnit = kl;
                    GetAllPossibilities();
                    GameUiManager.Instance.ShowUnitStats(kl);
                    GameUiManager.Instance.PlayerStatsAnimation();
                }
            }
        }
        else
        {
            foreach (Unit unit in units)
            {
                KingDark kd = unit as KingDark;
                if (kd != null)
                {
                    selectedUnit = kd;
                    GetAllPossibilities();
                    GameUiManager.Instance.ShowUnitStats(kd);
                    GameUiManager.Instance.PlayerStatsAnimation();
                }
            }
        }
        Invoke("HideConnectingPanel", 2);
        CameraAngles index = currentTeam == 0 ? CameraAngles.player1 : CameraAngles.player2;
        for (int i = 0; i < cameraAngles.Length; i++)
            cameraAngles[i].SetActive(false);
        cameraIndex = (int)index;
        cameraAngles[(int)index].SetActive(true);
        cameraAngles[(int)index].transform.parent = cameraController.transform;
        cameraController.SetCameraController(cameraAngles[(int)index].transform);
    }

    private void HideConnectingPanel()
    {
        GameUiManager.Instance.connectingPanel.SetActive(false);
    }

    IEnumerator UnitDeathTimer(float time, Unit unit)
    {
        yield return new WaitForSeconds(time);
        if (unit.Team == 0)
        {
            deadP1Units.Add(unit);
            GameUiManager.Instance.UpdateGral(deadP1Units.Count, unit.Team);
            unit.GameObject.transform.position = new Vector3(/*8*/108, 0, -0.5f) + Vector3.forward * deadP1Units.Count * 0.5f;
        }
        else
        {
            deadP2Units.Add(unit);
            GameUiManager.Instance.UpdateGral(deadP2Units.Count, unit.Team);
            unit.GameObject.transform.position = new Vector3(/*-1*/-100, 0, 7.5f) + -Vector3.forward * deadP2Units.Count * 0.5f;
        }
        unit.animator.SetBool("Death", false);
    }

    public void DestroyGameObject(GameObject go, float time = 0)
    {
        Destroy(go, time);
    }

    public GameObject GetPrefabByPath(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public Sprite GetSpriteByPath(string path)
    {
        return Resources.Load<Sprite>(path);
    }

}
public enum CameraAngles
{
    menu = 0,
    player1 = 1,
    player2 = 2
}








