using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breitensuche : MonoBehaviour
{

    Boolean wait = false;
    List<Hex> AlgoList = new List<Hex>();
    Hex current;
    CreatePath path;

    public Hex Start { get; set; }
    public Hex Ende { get; set; }
    public Double ZeitfürStats { get; set; }
    public Boolean run = false;
    public Transform character;

    //Werte für Einzelschritte
    public Boolean step = true;         //soll Steuern, ob der Stepmodus aktiviert werden soll
    int thisNeighbors;                  //Zählt wie viele Nachbarn das Aktuelle Hex in die Algolist geschrieben hat
    List<List<Hex>> iNeighbors = new List<List<Hex>>();
    List<Hex> StepList = new List<Hex>();
    List<Hex> temp = new List<Hex>();

    //Variablen zur Performancemessung
    Stopwatch stopwatch;
    int delay;
    int setTimer;
    int counter;
    int steps = 0;

    //
    public List<Hex> pathList;
    public int entdeckt;

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
        UnityEngine.Debug.Log(steps + " Schritte wurden benötigt");
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
            stopwatch.Stop();
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
                    path = new CreatePath(Ende); //erstellen des Pfades
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
                current = AlgoList[0]; //aktuelles Hex aktualisieren
                current.ChangeColor(0);
                StepList.Add(current);
                UnityEngine.Debug.Log("current Hex: " + current.xCoordinate + ", " + current.yCoordinate);
                AlgoList.RemoveAt(0); //letztes Element aus liste holen
                AddNeighborsToList(); // Nachbarn in Liste einfügen
                wait = true;
                steps++;
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
        else // Anschluss des Algortihmus
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
        List<Hex> help = new List<Hex>();

        foreach (Hex g in Neighbors)
        {
            if (!g.GetEntdeckt() && g.getBetretbar()) //nachbarn, falls unentdeckt, in liste einfügen
            {
                AlgoList.Add(g);
                help.Add(g);
                g.setPrevious(current); // vorheriges Element setzen
                //Hex-Farbe in wartefarbe ändern
                g.SetEntdeckt(true); //aktuelles Hex als entdeckt makieren  
            }
        }
        iNeighbors.Add(help);    //Anzahl der für dieses Element hinzugefügten Element erhöhen
    }

    //Algoritmus durchlauf zur Performancemessung ohne Darstellung
    private void SearchGridPerformance()
    {
        while (Ende.GetEntdeckt() == false && AlgoList.Count > 0)
        {
            current = AlgoList[0]; //aktuelles Hex     aktualisieren
            AlgoList.RemoveAt(0); //letztes Element aus liste holen
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
        iNeighbors.Clear();
        Ende.IsEnde();
    }

    //Methode um im Stepmode ein Schritt zurück zu gehen
    private void PreviousStep()
    {
        //Die Elemente die zuletzt zur Warteliste hinzugefügt wurden wieder entfernen
        foreach (Hex g in iNeighbors[iNeighbors.Count - 1])
        {
            g.ResetColor();
            g.SetEntdeckt(false);
            AlgoList.Remove(g);
        }

        //aktuelles Element eins zurück setzen
        current.ChangeColor(2);
        current = StepList[StepList.Count - 2];

        //Algolist zurücksetzen
        temp = AlgoList;
        AlgoList.Add(new Hex());
        for (int i = AlgoList.Count - 1; i > 0; i--)
        {
            AlgoList[i] = temp[i - 1];
        }
        AlgoList[0] = StepList[StepList.Count - 1];

        //Entfernt die Letzen Element der Hilfslisten
        iNeighbors.RemoveAt(iNeighbors.Count - 1);
        StepList.RemoveAt(StepList.Count - 1);
    }
}