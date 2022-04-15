using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUiManager : MonoBehaviour
{
    #region GameUiManager Singleton
    private static GameUiManager _instance;

    public static GameUiManager Instance { get { return _instance; } }


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

    [Header("Canvas")]
    public Canvas canvas;

    [Header("Connecting Panel")]
    public GameObject connectingPanel;

    [Header("Player Stats")]
    public Animator playerStatsAnim;
    public Image heroImage;
    public TextMeshProUGUI heroDamage;
    public TextMeshProUGUI heroHealth;

    [Header("List Of Heroes Top Images")]
    public Image[] heroTopImages;
    public Color deathColor;
    private int[] numberOfEachUnitsOnField = new int[] { 3,2,2,2,1,1,1,1 }; // swordsmans, tanks, knights, archers, jester, wizard, queen, king

    [Header("Gral")]
    public Transform gralLight;
    public Transform gralDark;

    [Header("Chalenge Royal")]
    public GameObject chalengeRoyalGO;
    public TextMeshProUGUI counterChalengeRoyal;
    int counter = 30;

    [Header("Death Counter")]
    public TextMeshProUGUI lightDeaths;
    public TextMeshProUGUI darkDeaths;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerStatsAnimation()
    {
        if (playerStatsAnim.GetBool("IsOut"))
            playerStatsAnim.SetBool("IsOut",false);
        else
            playerStatsAnim.SetBool("IsOut", true);
    }

    public void UpdateChalengeRoyal()
    {
        if(counter >= 0)
        {
            counter--;
            counterChalengeRoyal.text = counter.ToString() + "x";
        }
    }

    public void ShowUnitStats(Unit unit)
    {
        heroImage.sprite = unit.sprite;
        heroDamage.text = unit.Damage.ToString();
        heroHealth.text = unit.CurrentHealth.ToString();
    }

    public void UpdateTopBarStatus(Unit unit)
    {
        int numberOfUnits = (int)unit.unitType - 1;
        AreAllDead(unit.unitType, unit.Team, numberOfEachUnitsOnField[numberOfUnits]);
    }

    public void UpdateGral(int numberOfDeath, int team)
    {
        if (numberOfDeath >= 9)
        {
            if(team == 0)
            {
                gralLight.GetChild(0).gameObject.SetActive(false);
                gralLight.GetChild(1).gameObject.SetActive(false);
                gralLight.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                gralDark.GetChild(0).gameObject.SetActive(false);
                gralDark.GetChild(1).gameObject.SetActive(false);
                gralDark.GetChild(2).gameObject.SetActive(true);
            }
        }else if(numberOfDeath >= 6)
        {

            if (team == 0)
            {
                gralLight.GetChild(0).gameObject.SetActive(false);
                gralLight.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                gralDark.GetChild(0).gameObject.SetActive(false);
                gralDark.GetChild(1).gameObject.SetActive(true);
            }
        }else if(numberOfDeath >= 3)
        {
            if (team == 0)
            {
                gralLight.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gralDark.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (team == 0)
            lightDeaths.text = "x" + numberOfDeath;
        else
            darkDeaths.text = numberOfDeath + "x" ;
    }

    private void AreAllDead(UnitType type, int team, int numberOfThatClassUnits)
    {
        int counter = 0;
        foreach (Unit unit in GameManager.Instance.units)
        {
            if (unit.unitType == type && unit.Team == team && unit.isDeath)
            {
                counter++;
                if (counter == numberOfThatClassUnits)
                {
                    if(team == 0)
                    {
                        heroTopImages[(int)unit.unitType - 1].color = deathColor;
                    }
                    else
                    {
                        heroTopImages[(int)unit.unitType - 1 + 8].color = deathColor;
                    }
                }
            }
        }
            
    }
}
