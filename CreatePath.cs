using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePath : MonoBehaviour
{
    Hex Start;
    Hex Ende;
    List<Hex> path;
    //temporär erstelltes Creatpath-Script
    public CreatePath(Hex start, Hex goal)
    {
        Start = start;
        Ende = goal;
        StartAlgorithm(); //startet Aptherstellung
        Debug.Log(path);
    }

    void StartAlgorithm()
    {
        if (Ende.getPrevious() == null) //Ende nicht erreichbar/ Suchalgorithmus nicht gestartet
            Debug.Log("Ende nicht erreichbar");
        else
        {
            Hex help = Ende; //Hilfs-Hex
            path.Add(Ende); //Ende in Path einfügen
            while (help != null) //Schleife für rechtlichen Hexs
            {
                path.Add(help.getPrevious()); //Vorgänger wird in Path eingefügt
                //help farbe geben
                help = help.getPrevious(); //Hilfs-Hex wird neu gesetzt
            }  
        }
    }
}
