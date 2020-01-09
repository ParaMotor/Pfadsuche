using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiefensuche : Grid
{
    Boolean wait = false;
    List<Hex> AlgoList = new List<Hex>();
    public Hex Start, Ende; 
    Hex current; 
    //Hauptprogramm
	public Tiefensuche(Hex start, Hex goal)
	{
        Start = start;
        Ende = goal;
        current = Start;

        //Stopuhr starten
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //Code beginnt
        current.SetEntdeckt(true);   //Erstes Hexagon Makieren
        AddNeighborsToList();        //Nachbarn in Warteschlangeliste eintragen
        SearchGrid();                //Suche starten
        
        //Stopuhr anhalten
        stopwatch.Stop();
        UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds +"ms");
        CreatePath path = new CreatePath(goal);
    }
    //Suchfunktion
    private void SearchGrid()
    {         
        while (Ende.GetEntdeckt()==false) //Durchlaufen bis das ende entdeckt ist 
        {
            if (wait == false)
            {
                current = AlgoList[AlgoList.Count - 1]; //aktuelles Hex aktualisieren
                UnityEngine.Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate);
                AlgoList.RemoveAt(AlgoList.Count - 1); //erstes Element aus liste holen
                AddNeighborsToList(); // Nachbarn in Liste einfügen 
                //StartCoroutine(WaitRoutine());
            }
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
    
    IEnumerator WaitRoutine()
    {
        wait = true;
        yield return new WaitForSeconds(2.0f);
        wait = false; 
    }
}


