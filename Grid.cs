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
    
    //Definition des Hexagons (Größe)
    float hexWidth = 1.73f;
    float hexHeight = 2.0f;

    //Delay für Suchfunktionen
    public float searchDelay;


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
        //Astar3coords astar = new Astar3coords();

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
                hex.name = "Hexagon" + x + "|" + y+"|"+(x+y);
                HexList.Add(hex.GetComponent<Hex>());
                hex.GetComponent<Hex>().SetX(x);
                hex.GetComponent<Hex>().SetY(y);
                hex.GetComponent<Hex>().SetZ(x + y);
                x++;

            }
        }
        int Width = gridWidth - 1;
        int Height = gridHeight - 1;
        int xStart = 0;
        
        for (int i = 0; i < HexList.Count; i++)
        {
            //Alle zwei Reihen Width um 1 verringern
            if (i % (2 * gridWidth) == 0 && i != 0)
            {
                xStart--;
                Width--;
            }
            //Nachbarn rechts
            if (HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i + 1]);
            //Nachbarn links
            if (HexList[i].xCoordinate > xStart)
                HexList[i].addNachbar(HexList[i - 1]);  
            //Nachbarn unten links bei geradem Y
            if (HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > xStart && HexList[i].yCoordinate < Height)
                HexList[i].addNachbar(HexList[i + Height]);
            //Nachbarn unten links bei ungeradem Y
            if (HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate >= xStart && HexList[i].yCoordinate < Height)
                HexList[i].addNachbar(HexList[i + gridWidth]);
            //Nachbarn unten rechts bei geradem Y
            if (HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate <= Width && HexList[i].yCoordinate < Height)
                HexList[i].addNachbar(HexList[i + gridWidth]);
            //Nachbarn unten rechts bei ungeradem Y
            if (HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width && HexList[i].yCoordinate < Height)
                HexList[i].addNachbar(HexList[i + gridWidth +1]);
            //Nachbarn oben links bei geradem Y > 0
            if (HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > xStart && HexList[i].yCoordinate !=0)
                HexList[i].addNachbar(HexList[i - (gridWidth + 1)]);
            //Nachbarn oben links bei ungeradem Y 
            if (HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate <= Width && HexList[i].yCoordinate != 0)
                HexList[i].addNachbar(HexList[i - gridWidth]);
            //Nachbarn oben rechts bei geradem Y > 0
            if (HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate <= Width && HexList[i].yCoordinate != 0)
                HexList[i].addNachbar(HexList[i - gridWidth]);
            //Nachbarn oben rechts bei ungeradem Y
            if (HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width && HexList[i].yCoordinate != 0)
                HexList[i].addNachbar(HexList[i - Height]);
            
            
        }
    }

    //Grid-funktionen
    public void ClearGrid()
    {
        foreach (Hex g in HexList)
        {
            g.setPrevious(null);
            g.SetEntdeckt(false);
            g.ChangeColor(6);
        }
    }
    public void DestroyGrid()
    {
        foreach (Hex g in HexList)
        {
            Debug.Log("Entferne Hex: " + g.xCoordinate + ", " + g.yCoordinate);
            g.Destroy();
        }
        HexList.Clear();
    }
    //Hexagon-funktionen
    public Hex GetClicked()
    {
        foreach (Hex g in HexList)
        {
            if (g.clicked == true) return g;
        }
        return null;
    }
    public void SetSearchDelay(float delay)
    {
        searchDelay = delay;
    }
}
