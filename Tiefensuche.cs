using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiefensuche
{
    List<Hex> hexaList;
    List<Hex> AlgoList;
    Hex Start, Ende, current;

	public Tiefensuche(Hex start, Hex goal)
	{
        //hexaList = Grid;
        Start = start;
        Ende = goal;
        current = Start;
        Start.setDiscovered();   //Erstes Hexagon Makieren
        AddNeighborsToList();    //Nachbarn in Warteschlangeliste eintragen
        SearchGrid();            //Suche starten
	}

    private void SearchGrid()
    {         
        while (Ende.getDiscovered() == false && AlgoList[0]!=null) //Durchlaufen bis das ende entdeckt ist
        {   
            current = AlgoList[0]; //aktuelles Hex aktualisieren
            AlgoList.RemoveAt(0); //erstes Element aus liste holen
            AddNeighborsToList(); // Nachbarn in Liste einfügen
            current.setDiscovered(); //aktuelles Hex als entdeckt makieren
        }
    }

    private void AddNeighborsToList()
    {
        Hex[] Neighbors; //neue Hexarray erstellen
        Neighbors = current.getNeighbors(); //Nachbarn vom aktuellen Hex einholen

        foreach (Hex g in Neighbors) //nachbarn, falls unentdeckt, in liste einfürgen
            if (!g.getDiscovered())
            {
                AlgoList.Add(g);
                g.setPrevious(current); // vorheriges Element setzen
            }
        
    }
}


