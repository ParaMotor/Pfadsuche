using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;

    public int gridWidth = 5;
    public int gridHeight = 5;
    //public int griddiagonale = 0;             //Da das Hexagonförmige Grid aktuell keine Lust hat, erstmal ein Viereckiges, das wird behoben !!

    //Definition des Hexagons (Größe)
    float hexWidth = 1.73f;
    float hexHeight = 2.0f;

    Vector3 startPos;

    //prinzipelle Main Methode
    void Start()
    {
        //Liste, in der die Koordinaten der Hexagone gespeichert werden sollen
        var HexList = new List<Hex>()
         {
             new Hex() { xCoordinate = 2, yCoordinate = 3 },
         };


        CalcStartPos();
        CreateGrid();
        //Das ist nur was zum testen :D
        print(HexList[0]);      
        print(HexList.Count);
    }

    //berechnet aktuelle Position, um weitere Hexagone einzufügen
    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }


    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
                //HexList.Add(new Hex() { xCoordinate = x, yCoordinate = y });
            }
        }
    }
}



