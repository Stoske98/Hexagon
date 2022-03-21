using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    // Column + Row + S = 0
    public int Column { set; get; }
    public int Row { set; get; }
    public bool Walkable { set; get; }
    public bool isHighligted { set; get; }
    public int Weight { get; set; }
    public int Cost { get; set; }
    public Hex PrevTile { get; set; }
    public int S { set; get; }

    public GameObject GameObject { set; get; }
    public List<Hex> neighbors { set; get; }

    private Color startColor;
    private Color previousColor;
    private Material material;
    private MeshRenderer mesh;

    const float HEIGHT = 1;
    const float WIDTH = 0.5773502050459253f;
    const float OFFSET = 1.05f;

    public readonly List<Vector2Int> evenColumnNeighbors = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(-1, -1), new Vector2Int(1, -1) };
    public readonly List<Vector2Int> oddColumnNeighbors = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(1, 0) };

    public void setColor(Color color,bool withMesh = false)
    {
        if(withMesh)
            mesh.enabled = true;
        startColor = color;
    }
    public void resetColor()
    {
        if (isHighligted)
        {
            mesh.enabled = true;
            material.SetColor("_BaseColor", previousColor);

        }
        else
        {
            mesh.enabled = false;
            material.color = startColor;
            material.SetColor("_BaseColor", startColor); 
        }
          
    }

    public void Highlight(Color color)
    {
        isHighligted = true;
        mesh.enabled = true;
        previousColor = color;
        material.color = color;
        material.SetColor("_BaseColor", color);
    }

    public Hex(int col, int row, GameObject gameObject, Color color)
    {
        this.Column = col;
        this.Row = row;
        S = -Column - Row;
        Walkable = true;
        Weight = 1;
        GameObject = gameObject;
        mesh = GameObject.GetComponentInChildren<MeshRenderer>();
        material = GameObject.GetComponentInChildren<Renderer>().material;
        material.color = startColor = color;
    }
    public Hex()
    {

    }
    public void SetPosition()
    {
        GameObject.name = "Tile[" + Column + "][" + Row + "]";
        if (this.Column % 2 == 0)
        {
            GameObject.transform.localPosition = new Vector3(this.Column * getHorizontalSpacing(), 0, this.Row * getVerticalSpacing()) * OFFSET;
        }
        else
        {
            GameObject.transform.localPosition = new Vector3(this.Column * getHorizontalSpacing(), 0, (this.Row * getVerticalSpacing() + (getVerticalSpacing() / 2))) * OFFSET;
        }
    }


    private float getHorizontalSpacing()
    {
        return WIDTH * 0.75F * 2;
    }
    private float getVerticalSpacing()
    {
        return HEIGHT;
    }
}
