using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiefensuche
{
    List<Hex> AlgoList = new List<Hex>();
    public Hex Start, Ende;
    Hex current; 
    //Hauptprogramm
	public Tiefensuche(Hex start, Hex goal)
	{
        Start = start;
        Ende = goal;
        current = Start;    
        current.SetEntdeckt(true);   //Erstes Hexagon Makieren
        AddNeighborsToList();    //Nachbarn in Warteschlangeliste eintragen
        SearchGrid();            //Suche starten
        CreatePath path = new CreatePath(goal);
    }
    //Suchfunktion
    private void SearchGrid()
    {         
        while (Ende.GetEntdeckt()==false) //Durchlaufen bis das ende entdeckt ist 
        {   
            current = AlgoList[AlgoList.Count-1]; //aktuelles Hex aktualisieren
            Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate);
            AlgoList.RemoveAt(AlgoList.Count - 1); //erstes Element aus liste holen
            AddNeighborsToList(); // Nachbarn in Liste einfügen 
        }
    }
    //Nachbarn in Warteschlange anfügen
    private void AddNeighborsToList()
    {
        List<Hex> Neighbors = current.getNachbarn(); //neue Hexarray erstellen und Nachbarn vom aktuellen Hex einholen

        foreach (Hex g in Neighbors) 
        {
            if (!g.GetEntdeckt() && g.getBetretbar()) //nachbarn, falls unentdeckt, in liste einfügen
            {
                AlgoList.Add(g);
                g.setPrevious(current); // vorheriges Element setzen
                //Hex-Farbe in wartefarbe ändern
                g.SetEntdeckt(true); //aktuelles Hex als entdeckt makieren  
            }
        }
    }
}


