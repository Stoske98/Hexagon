using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar
{
    private GameObject CanvasHBPrefab;
    public GameObject CanvasHB;
    public GameObject HealthBarGO;
    public List<Image> HealthPoints;
    public Image HealthPrefab;

    public HealthBar(GameObject canvasHBPrefab, Image healthPrefab)
    {
        CanvasHBPrefab = canvasHBPrefab;
        HealthPoints = new List<Image>();
        HealthPrefab = healthPrefab;
    }
    public void Initialize(Unit unit)
    {
        CanvasHB = GameManager.Instance.InstantiatePrefab(CanvasHBPrefab);
        CanvasHB.transform.SetParent(unit.GameObject.transform);
        HealthBarGO = CanvasHB.transform.GetChild(0).gameObject;
        HealthPoints = new List<Image>();
        for (int i = 0; i < unit.MaxHealth; i++)
        {
            Image image = GameManager.Instance.InstantiatePrefabImage(HealthPrefab);
            image.transform.SetParent(HealthBarGO.transform.GetChild(0));
            image.transform.localScale = Vector3.one;
            HealthPoints.Add(image);
            image.color = Color.green;
        }
        CanvasHB.GetComponent<RectTransform>().transform.localPosition = Vector3.zero + Vector3.up * CanvasHB.GetComponent<RectTransform>().transform.localPosition.y;
        CanvasHB.SetActive(false);
    }


    public void HealthBarFiller(Unit unit)
    {
        for (int i = 0; i < HealthPoints.Count; i++)
        {
            HealthPoints[i].enabled = !DisplayHealthPoint(unit.CurrentHealth, i);
        }
    }
    public void ColorChanger(Unit unit)
    {
        foreach (var image in HealthPoints)
        {
            Color healthColor = Color.Lerp(Color.red, Color.green, ((float)unit.CurrentHealth / (float)unit.MaxHealth));
            image.color = healthColor;
        }
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber) >= _health);
    }
}