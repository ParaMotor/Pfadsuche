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
    List<Hex> HexList = new List<Hex>
    {

    };

    //prinzipelle Main Methode
    void Start()
    {

        CalcStartPos();
        CreateGrid();

        
        Tiefensuche search = new Tiefensuche(HexList[5], HexList[43]);
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
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                //Objekt Hex erstellen, nach Vorlage von HexPrefab
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
                HexList.Add(new Hex() { xCoordinate = x, yCoordinate = y });
            }
        }
        int Width = gridWidth - 1;
        int Height = gridHeight - 1;

        for (int i=0; i<HexList.Count; i++)
        {
            if (HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i + 1]);
            
            if (HexList[i].xCoordinate > 0)
                HexList[i].addNachbar(HexList[i - 1]);

            if (HexList[i].yCoordinate < Height)
                HexList[i].addNachbar(HexList[i + gridWidth]);

            if (HexList[i].yCoordinate < Height && HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i + gridWidth + 1]);

            if (HexList[i].yCoordinate < Height && HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > 0)
                HexList[i].addNachbar(HexList[i + gridWidth - 1]);

            if (HexList[i].yCoordinate > 0)
                HexList[i].addNachbar(HexList[i - gridWidth]);

            if (HexList[i].yCoordinate > 0 && HexList[i].yCoordinate % 2 == 1 && HexList[i].xCoordinate < Width)
                HexList[i].addNachbar(HexList[i - gridWidth + 1]);

            if (HexList[i].yCoordinate > 0 && HexList[i].yCoordinate % 2 == 0 && HexList[i].xCoordinate > 0)
                HexList[i].addNachbar(HexList[i - gridWidth - 1]);
        }
    }
}
