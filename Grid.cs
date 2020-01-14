using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;

    //Definition Gridgröße 
    public int gridWidth;
    public int gridHeight;
    //public int griddiagonale = 0;             //Da das Hexagonförmige Grid aktuell keine Lust hat, erstmal ein Viereckiges, das wird behoben !!

    //Definition des Hexagons (Größe)
    float hexWidth = 1.73f;
    float hexHeight = 2.0f;

    Vector3 startPos;

    //Liste, in der die Koordinaten der Hexagone gespeichert werden sollen
    public List<Hex> HexList = new List<Hex>
    {

    };

    //prinzipelle Main Methode
    public void Beginn(int groesse)
    {
        gridWidth = groesse;
        gridHeight = groesse;

        CalcStartPos();
        CreateGrid();

        
        //Das ist nur was zum testen :D
        print(HexList[0].xCoordinate + "|" + HexList[0].yCoordinate);
        print(HexList.Count);
    }

    //berechent Startposition
    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }

    //berechnet aktuelle Position, um weitere Hexagone einzufügen
    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    //Grid erstellen
    void CreateGrid()
    {
        int x = 0;
        int xtemp = 0;
        Astar3coords astar = new Astar3coords();

        for (int y = 0; y < gridHeight; y++)
        {
            xtemp = y / 2;
            x = 0 - xtemp;
            for (int xsize = 0; xsize < gridWidth; xsize++)
            {
                //Objekt Hex erstellen, nach Vorlage von HexPrefab
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(xsize, y);
                hex.position = CalcWorldPos(gridPos);
                hex.SetParent(this.transform);
                hex.name = "Hexagon" + x + "|" + y;
                HexList.Add(hex.GetComponent<Hex>());
                hex.GetComponent<Hex>().SetX(x);
                hex.GetComponent<Hex>().SetY(y);
                hex.GetComponent<Hex>().SetZ(x + y);
                x++;

            }
        }
        int Width = gridWidth - 1;
        int Height = gridHeight - 1;
        int counter = -1;
        int row = 0;

        for (int i = 0; i < HexList.Count; i++)
        {
            if (HexList[i].xCoordinate < Width - row)
                HexList[i].addNachbar(HexList[i + 1]);

            if (HexList[i].xCoordinate > 0 - row)
                HexList[i].addNachbar(HexList[i - 1]);

            if (HexList[i].yCoordinate < Height) //abwechselnd unten links/unten rechts
                HexList[i].addNachbar(HexList[i + gridWidth]);

            if (HexList[i].yCoordinate < Height && HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i + gridWidth + 1]);

            if (HexList[i].yCoordinate < Height && HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > 0)
                HexList[i].addNachbar(HexList[i + gridWidth - 1]);

            if (HexList[i].yCoordinate > 0) //abwechselnd oben links/oben rechts
                HexList[i].addNachbar(HexList[i - gridWidth]);

            if (HexList[i].yCoordinate > 0 && HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i - gridWidth + 1]);

            if (HexList[i].yCoordinate > 0 && HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > 0)
                HexList[i].addNachbar(HexList[i - gridWidth - 1]);

            counter++;
            row = counter / gridWidth;
        }
    }

    public void ClearGrid()
    {
        foreach(Hex g in HexList)
        {
            g.setPrevious(null);
            g.ChangeColor(6); 
        }
    }

    public void DestroyGrid()
    {
        foreach(Hex g in HexList)
        {
            Debug.Log("Entferne Hex: " + g.xCoordinate + ", " + g.yCoordinate);
            g.Destroy();
        }
        HexList.Clear();
    }

    public Hex GetClicked()
    {
        foreach(Hex g in HexList)
        {
            if (g.clicked == true) return g;
        }
        return null;
    }
}
