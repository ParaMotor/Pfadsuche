using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiefensuche : MonoBehaviour
{

    Boolean wait = false;
    List<Hex> AlgoList = new List<Hex>();
    Hex current;
    CreatePath path;

    public int entdeckt = 0;

    //Zu übergebende Start- und Zielobjekte
    public Hex Start { get; set; }
    public Hex Ende { get; set; }
    public Boolean run = false;
    public Transform character;

    //Werte für Einzelschritte
    public Boolean step = true;         //soll Steuern, ob der Stepmodus aktiviert werden soll
    int thisNeighbors;                  //Zählt wie viele Nachbarn das Aktuelle Hex in die Algolist geschrieben hat
    List<int> iNeighbors = new List<int>();
    List<Hex> StepList = new List<Hex>();
    public List<Hex> pathList;

    //Variablen zur Performancemessung
    Stopwatch stopwatch;
    public Double ZeitfürStats { get; set; }
    int delay;
    int setTimer;
    int counter;
    int steps = 0;
    

    //Hauptprogramm
    public void Anfang()
    {
        delay = (int)GetComponent<Grid>().searchDelay;
        setTimer = counter = delay * 3;
        //Stopuhr starten
        stopwatch = new Stopwatch();
        stopwatch.Start(); //Starten der Stopuhr zur Einsicht der Performance

        InitStart(); //Initialisieren der Startwerte
        SearchGridPerformance();    //Tiefensuche ohne verzögerung Starten

        stopwatch.Stop(); //Stopuhr anhalten

        //Debug zur Dauer des Algorithmus
        //UnityEngine.Debug.Log(steps + " Schritte wurden benötigt");
        ZeitfürStats = stopwatch.ElapsedTicks / 10000.0;
        UnityEngine.Debug.Log(ZeitfürStats + " ms");


        //neu Initialisieren
        GetComponent<Grid>().ClearGrid(); //Zurücksetzen des Grids
        AlgoList.Clear();   //Leeren der Warteliste
        steps = 0;          //Steps wieder auf null setzen
        InitStart();        //Startwerte neu setzen

        //Code beginnt
        run = true; //updatefunktion auf true setzen und somit starten
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (step)
                step = false;
            else
                step = true;
        }

        if (Start != null && run && !step)
        {
            SearchGrid();
            steps++;
        }

        //Abbruch des Algorithmus
        if (Start != null && !run)
        {
            Start = null;
            AlgoList.Clear();
            UnityEngine.Debug.Log("Suchalgorithmus abgebrochen");
        }

        if (step)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Ende.GetEntdeckt() == false)
                {
                    wait = false;
                    SearchGrid();
                    steps++;
                }
                else if (Ende.GetEntdeckt() == true)
                {
                    path = new CreatePath(Ende); //erstellen des Pfades
                    pathList = path.path;
                    character.GetComponent<CharacterScript>().Init(pathList);
                }
                else
                    UnityEngine.Debug.Log("Ende nicht erreichbar");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (current.getPrevious() != null)
                {
                    PreviousStep();
                    steps--;
                }
            }
        }

    }

    //Suchfunktion
    private void SearchGrid()
    {
        if (Ende.GetEntdeckt() == false && AlgoList.Count > 0)
        {
            if (wait == false)
            {
                current = AlgoList[AlgoList.Count - 1]; //aktuelles Hex aktualisieren
                current.ChangeColor(0);
                StepList.Add(current);
                UnityEngine.Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate);
                AlgoList.RemoveAt(AlgoList.Count - 1); //letztes Element aus liste holen
                AddNeighborsToList(); // Nachbarn in Liste einfügen
                wait = true;
            }
            else //wartefunktiion
            {
                if (counter <= 0) //warten beenden
                {
                    wait = false;
                    counter = setTimer;
                }
                else //warte zähler um eins veringern
                {
                    counter--;
                }

            }
        }
        else // Abschluss des Algortihmus
        {
            if (Ende.GetEntdeckt() == true)
            {
                path = new CreatePath(Ende); //erstellen des Pfades
                pathList = path.path;
                character.GetComponent<CharacterScript>().Init(pathList);
            }
            else
                UnityEngine.Debug.Log("Ende nicht erreichbar");
            foreach (Hex g in GetComponent<Grid>().HexList)
                if (g.GetEntdeckt())
                    entdeckt++;
            UnityEngine.Debug.Log("Endeckte Felder " + entdeckt);
            AlgoList.Clear(); //Liste leeren
            Start = null; // Start und Ende nullen
            run = false; //Updatefunktion auf false setzen
            steps = 0;
        }
    }
    //Nachbarn in Warteschlange anfügen
    private void AddNeighborsToList()
    {
        List<Hex> Neighbors = current.getNachbarn(); //neue Hexarray erstellen und Nachbarn vom aktuellen Hex einholen
        thisNeighbors = 0;

        foreach (Hex g in Neighbors)
        {
            if (!g.GetEntdeckt() && g.getBetretbar()) //nachbarn, falls unentdeckt, in liste einfügen
            {
                AlgoList.Add(g);
                g.setPrevious(current); // vorheriges Element setzen
                //Hex-Farbe in wartefarbe ändern
                g.SetEntdeckt(true); //aktuelles Hex als entdeckt makieren  
                thisNeighbors++;    //Anzahl der für dieses Element hinzugefügten Element erhöhen
            }
        }
        iNeighbors.Add(thisNeighbors);
        UnityEngine.Debug.Log(thisNeighbors);
    }

    //Algoritmus durchlauf zur Performancemessung ohne Darstellung
    private void SearchGridPerformance()
    {
        while (Ende.GetEntdeckt() == false && AlgoList.Count > 0)
        {
            current = AlgoList[AlgoList.Count - 1]; //aktuelles Hex aktualisieren
            AlgoList.RemoveAt(AlgoList.Count - 1); //letztes Element aus liste holen
            AddNeighborsToList(); // Nachbarn in Liste einfügen
            steps++;
        }
    }

    //Methode zur Startinitialisierung
    private void InitStart() //Methode um die Tiefensuche zu Initialisieren
    {
        current = Start;
        current.SetEntdeckt(true);   //Erstes Hexagon Makieren
        current.IsStart();
        AddNeighborsToList();        //Nachbarn in Warteschlangeliste eintragen      
        StepList.Add(current);
        steps++;
        Ende.IsEnde();
    }

    //Methode um im Stepmode ein Schritt zurück zu gehen
    private void PreviousStep()
    {
        current.ChangeColor(2);
        AlgoList.Add(current);
        if (StepList[StepList.Count - 2] != null)
            current = StepList[StepList.Count - 2];

        if (StepList[StepList.Count - 1].getPrevious() == current || iNeighbors[iNeighbors.Count - 1] != 0)
            for (int i = 0; i < iNeighbors[iNeighbors.Count - 1]; i++)
            {
                AlgoList[AlgoList.Count - 2].SetEntdeckt(false);
                AlgoList[AlgoList.Count - 2].ResetColor();
                AlgoList.RemoveAt(AlgoList.Count - 2);
            }
        iNeighbors.RemoveAt(iNeighbors.Count - 1);
        StepList.RemoveAt(StepList.Count - 1);
    }


}

