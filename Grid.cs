using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;
    public Transform WallPrefab;
    public Transform oCamera;
    public Transform pCamera;

    //Definition Gridgröße 
    public int gridWidth;
    public int gridHeight;

    //Abmessungen der Hex für Berechungen
    float hexWidth = 1.73f;
    float hexHeight = 2.0f;

    //Delay für Suchfunktionen
    public float searchDelay;


    Vector3 startPos;

    //Liste, in der die Koordinaten der Hexagone gespeichert werden sollen
    public List<Hex> HexList = new List<Hex>
    {

    };
    List<Transform> WallList = new List<Transform>();

    //prinzipelle Main Methode
    public void Beginn(int groesse)
    {
        gridWidth = groesse;
        gridHeight = groesse;

        //CalcStartPos();
        startPos = new Vector3(0, 0, 0);
        CreateGrid();

        oCamera.localPosition = HexList[HexList.Count - 1].getTransform().localPosition / 2 + new Vector3(0, 1, 0); //position für die orthographische Kamera bestimmen
        oCamera.GetComponent<Camera>().orthographicSize = gridHeight;
        pCamera.localPosition = HexList[HexList.Count - 1].getTransform().position / 2; // position für die perspektivische Kamera bestimmen
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
                hex.name = "Hexagon" + x + "|" + y + "|" + (x + y);
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
        float addx = 0;

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
                HexList[i].addNachbar(HexList[i + gridWidth + 1]);
            //Nachbarn oben links bei geradem Y > 0
            if (HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > xStart && HexList[i].yCoordinate != 0)
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


            //Platzierung der Wände

            //Positionsfindung im Grid und Unity Coordinatensystem
            Vector2 gridPos = new Vector2(HexList[i].xCoordinate, HexList[i].yCoordinate);
            Vector3 hexPos = CalcWorldPos(gridPos);
            //Vektoren für Rotationen der Wände
            Vector3 rotaLiOb = new Vector3(0, 0, 60);
            Vector3 rotaReOb = new Vector3(0, 0, 120);
            Vector3 rotaLiUn = new Vector3(0, 0, -60);
            Vector3 rotaReUn = new Vector3(0, 0, 240);
            Vector3 rotaLi = new Vector3(0, 0, 0);
            Vector3 rotaRe = new Vector3(0, 0, 180);

            //Variablen für Positionierung der Wände
            float xr = hexPos.x + 0.44f;
            float xl = hexPos.x - 0.44f;
            float zu = hexPos.z - 0.755f;
            float zo = hexPos.z + 0.755f;

            //Alle 2 Reihen einen zusätzlichen Faktor zur Platzierung 
            if (i % (2 * gridWidth) == 0 && HexList[i].yCoordinate > 0)
            {
                addx = (HexList[i].yCoordinate / 2) * 1.73f;
            }

            // Oberste Reihe von links nach rechts ohne erstes und letztes Element
            if (HexList[i].yCoordinate == 0 && HexList[i].xCoordinate > 0)
            {
                //Wand oben links                 
                WallInit(xl, hexPos.y, zo, rotaLiOb, i, 1);

                //Wand oben rechts
                WallInit(xr, hexPos.y, zo, rotaReOb, i, 2);
            }
            //Für Hex 0/0/0 oben rechts
            if (HexList[i].yCoordinate == 0 && HexList[i].xCoordinate == 0)
            {
                //Wand oben rechts
                WallInit(xr, hexPos.y, zo, rotaReOb, i, 2);
            }
            //Linke Seite, gerades Y 
            if (HexList[i].xCoordinate == xStart && HexList[i].yCoordinate % 2 == 0)
            {
                //Wand oben links
                WallInit((xl + addx), hexPos.y, zo, rotaLiOb, i, 1);

                //Wand links
                WallInit((hexPos.x - 0.865f + addx), hexPos.y, hexPos.z, rotaLi, i, 6);

                //Wand unten links
                WallInit((xl + addx), hexPos.y, zu, rotaLiUn, i, 5);
            }
            //Linke Seite, ungerades Y
            if (HexList[i].xCoordinate == xStart && HexList[i].yCoordinate % 2 == 1)
            {
                //Wand links
                WallInit((hexPos.x - 0.865f + addx), hexPos.y, hexPos.z, rotaLi, i, 6);
            }

            //Rechte Seite, gerades Y 
            if (HexList[i].xCoordinate == Width && HexList[i].yCoordinate % 2 == 0)
            {
                //Wand rechts
                WallInit((hexPos.x + 0.865f + addx), hexPos.y, hexPos.z, rotaRe, i, 3);
            }
            //Rechts Seite, ungerades Y
            if (HexList[i].xCoordinate == Width && HexList[i].yCoordinate % 2 == 1)
            {
                //Wand rechts
                WallInit((hexPos.x + 0.865f + addx), hexPos.y, hexPos.z, rotaRe, i, 3);

                //Wand oben rechts                
                WallInit((xr + addx), hexPos.y, zo, rotaReOb, i, 2);

                //Wand unten rechts
                WallInit((xr + addx), hexPos.y, zu, rotaReUn, i, 4);
            }

            //Wände letzte Reihe
            if (HexList[i].yCoordinate == (gridHeight - 1) && HexList[i].xCoordinate < Width)
            {
                //Wand unten links 
                WallInit((xl + addx), hexPos.y, zu, rotaLiUn, i ,5);
                
                //Wand unten rechts                
                WallInit((xr + addx), hexPos.y, zu, rotaReUn, i, 4);
            }
            //Wand letztes Hex unten rechts
            if (HexList[i].yCoordinate == (gridHeight - 1) && HexList[i].xCoordinate == Width)
            {
                //Wand unten links 
                WallInit((xl + addx), hexPos.y, zu, rotaLiUn, i, 5);                
            }
            //Wenn GridWidth ungerade, letztes Hex unten rechts
            if (gridWidth % 2 == 1 && HexList[i].yCoordinate == Height && HexList[i].xCoordinate == Width)
            {
                //Wand unten rechts
                WallInit((xr + addx), hexPos.y, zu, rotaReUn, i, 4);
            }

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
            //Debug.Log("Entferne Hex: " + g.xCoordinate + ", " + g.yCoordinate);
            g.Destroy();
        }
        foreach (Transform w in WallList)
            Destroy(w.gameObject);
        WallList.Clear();
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

    //Wand-funktion
    void WallInit(float x, float y, float z, Vector3 Rotation, int i, int position)
    {
        Transform wall = Instantiate(WallPrefab) as Transform;              //WallPrefab als Objekt erstellen
        wall.position = new Vector3(x, y, z);                               //Wall an Stelle xl, hexPos.y, zo platzieren
        wall.Rotate(Rotation, Space.Self);                                  //Rotation um eigene Achse, nach Vektor
        wall.name = "Wall " + i + "." + position;                           //Bennenung Wand Oben links mit i.1 und weiter im Uhrzeigersinn
        WallList.Add(wall);
    }
}
