using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiefensuche
{
    List<Hex> AlgoList;
    Hex Start, Ende, current; 
    //Hauptprogramm
	public Tiefensuche(Hex start, Hex goal)
	{
        Start = start;
        Ende = goal;
        current = Start;
        Start.SetEntdeckt(true);   //Erstes Hexagon Makieren
        AddNeighborsToList();    //Nachbarn in Warteschlangeliste eintragen
        SearchGrid();            //Suche starten
	}
    //Suchfunktion
    private void SearchGrid()
    {         
        while (Ende.GetEntdeckt() == false && AlgoList[0]!=null) //Durchlaufen bis das ende entdeckt ist
        {   
            current = AlgoList[0]; //aktuelles Hex aktualisieren
            AlgoList.RemoveAt(0); //erstes Element aus liste holen
            AddNeighborsToList(); // Nachbarn in Liste einfügen
            current.SetEntdeckt(true); //aktuelles Hex als entdeckt makieren            
        }
    }
    //Nachbarn in Warteschlange anfügen
    private void AddNeighborsToList()
    {
        List<Hex> Neighbors; //neue Hexarray erstellen
        Neighbors = current.getNachbarn(); //Nachbarn vom aktuellen Hex einholen

        foreach (Hex g in Neighbors) //nachbarn, falls unentdeckt, in liste einfügen
            if (!g.GetEntdeckt() && g.getBetretbar())
            {
                AlgoList.Add(g);
                g.setPrevious(current); // vorheriges Element setzen
                //Hex-Farbe in wartefarbe ändern
            }        
    }
}


