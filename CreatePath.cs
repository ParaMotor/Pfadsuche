using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePath {

    Hex Ende;

    public List<Hex> path = new List<Hex>();
    //temporär erstelltes Creatpath-Script
    public CreatePath(Hex goal)
    {
        Ende = goal;
        StartAlgorithm(); //startet Aptherstellung
        Debug.Log(path.Count);
    }
        
    void StartAlgorithm()
    {
        if (Ende.getPrevious() == null) //Ende nicht erreichbar/ Suchalgorithmus nicht gestartet
            Debug.Log("Ende nicht erreichbar");
        else
        {
            path.Add(Ende); //Ende in Path einfügen
            Ende.ChangeColor(5);
            Debug.Log(Ende.xCoordinate + ", " + Ende.yCoordinate);
            Hex help = Ende.getPrevious(); //Hilfs-Hex
            while (help != null) //Schleife für rechtlichen Hexs
            {
                path.Add(help); //Vorgänger wird in Path eingefügt
                help.ChangeColor(3);
                Debug.Log(help.xCoordinate + ", " + help.yCoordinate);
                //help farbe geben
                help = help.getPrevious(); //Hilfs-Hex wird neu gesetzt
            } 
        }
    }
}
