using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Voreinstellungen : MonoBehaviour
{
    //für Gridgröße eintellen (also das Inputfield)
    private int groesse;
    bool istDezimal = true;
    private int auswahl;

    //für InputField
    public string GridChange;
    public GameObject inputField;
        
    public Grid grid;
    
    //Slider für Delay
    public Slider slider;

    //Funktionen für Start- und Zielfestlegung
    public Hex Start;
    public Hex Ziel;

    //"Inventar"
    

    public List<Hex> HexListObject;
    //--------------------------------------------------------------------------------

    //Methoden für das UI

    //wechsel zum Hauptmenü
    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /* - dient zur eingabe der Größe des Grids
     * - prüft ob die Eingabe in das Inputfield nur aus Zahlen besteht
     * - ertellt ein Grid mit der eingegebenen Größe
     */
    public void GridGroesse()
    {
        istDezimal = true;
        GridChange = inputField.GetComponent<Text>().text;   //holt String aus InputField und schreibt ihn auf die Variable
        //überprüft ob alle eingegebenen Zeichen Zahlene sind, weil Unity z.B. "." zulässt
        foreach (char zeichen in GridChange)
        {
            if (!char.IsNumber(zeichen))
            {
                istDezimal = false;
                Debug.Log("Bitte nur Zahlen eingeben");
                break;
            }
        }
        //Grid wird nur erstellt, wenn bei der Eingabe NUR Zahlen eingegeben wurden
        if (istDezimal == true)
        {           
            groesse = int.Parse(GridChange);                    //groesse wird mit dem in Int gewandelten Wert von der Eingabe überschrieben
            grid.Beginn(groesse);                               //die methode Beginn() vom Script Grid wird aufgerufen und der parameter wird übergeben
        }

    }

    /*nach buttonclick wird das Start-Hex übergeben und auf eine Variable geschrieben damit es dem Algorithmus beim Starten übergeben werden kann*/
    public void StartpunktButton()
    {
        if (Start != null)
            Start.ResetColor();
        Start = grid.GetClicked();
        Start.IsStart();        
    }

    /*nach buttonclick wird das Ziel-Hex übergeben und auf eine Variable geschrieben damit es dem Algorithmus beim Starten übergeben werden kann*/
    public void ZielpunktButton()
    {
        if (Ziel != null)
            Ziel.ResetColor();
        Ziel = grid.GetClicked();
        Ziel.IsEnde();
    }

    /*hier wird der Algorithmus ausgewählt und die Auswahl in als Zahl gespeichert, 
     * sodass beim "Play"-drücken der ausgewählte Algorithmus benutzt werden kann
     */
    public void AlgoAuswahl(int val)
    {
        if (val == 0)
        {          
            auswahl = 0;
            Debug.Log("Tiefensuche");
        }
        if (val == 1)
        {
            auswahl = 1;
            Debug.Log("Breitensuche");
        }
        if (val == 2)
        {
            auswahl = 2;
            Debug.Log("A*");
        }
    }

    /*Die Nummer des Ausgewählte Algorithmus wird abgefragt damit der entsprechende Algorithmus, 
     * mit Start- und Ziel-Hex "geladen" werden kann.
     * Der Algorithmus wird dann ausgeführt.
     * der Scenenwechsel erfolg durch Unity, mit dem ein-/ausblenden des jeweiligen Canvas, automatisch
     */
    public void PlayButton()
    {
        switch (auswahl)
        {
            case 0:
                if (Start != null && Ziel != null)
                {
                    //sorgt dafür das wenn der Algorithmus läuft, die Farben der Hexagons nicht verändert werden können
                    foreach (Hex g in grid.HexList) { g.colorChangeOn = false; } 
                    
                    Debug.Log("Tiefensuche wird ausgeführt");
                    grid.GetComponent<Tiefensuche>().enabled = true;
                    grid.GetComponent<Tiefensuche>().Start = Start;
                    grid.GetComponent<Tiefensuche>().Ende = Ziel;
                    grid.GetComponent<Tiefensuche>().Anfang();
                    break;
                }
                else
                {
                    Debug.Log("fehler bei Platzierung von Start- und Zielpunkt");
                    break;
                }
            case 1:
                if (Start != null && Ziel != null)
                {
                    foreach (Hex g in grid.HexList) { g.colorChangeOn = false; }
                    Debug.Log("Breitensuche wird ausgeführt");
                    grid.GetComponent<Breitensuche>().enabled = true;
                    grid.GetComponent<Breitensuche>().Start = Start;
                    grid.GetComponent<Breitensuche>().Ende = Ziel;
                    grid.GetComponent<Breitensuche>().Anfang();
                    break;
                }
                else
                {
                    Debug.Log("fehler bei Platzierung von Start- und Zielpunkt");
                    break;
                }
            case 2:
                Debug.Log("auswahl: A* ");
                break;
                /*if (Start != null && Ziel != null)
                {
                    foreach (Hex g in grid.HexList) { g.colorChangeOn = false; }
                    public class Astar3coords

                    public void Astar(int maxX, int maxY, int objectX, int objectY, int objectZ, int startX, int startY, int startZ)
                    break;
                }
                else
                {
                    Debug.Log("fehler bei Platzierung von Start- und Zielpunkt");
                    break;
                }*/
        }
    }

    //Methode für den Zurückbutton in "InGameUI"
    public void HexagonsWiederFaerbbarMachen()
    {
        //beim Zurückgehen wird "colorChangeOn" auf true gesetzt, damit die felder wieder gefärbt werden können
        foreach (Hex g in grid.HexList) { g.colorChangeOn = true; }
    }

    public void ObjektSpawn()
    {
        //Instantiate(sampleObject, Vector3.up, Quaternion.identity);
        //HexListObject.Add(sampleObject);
    }
    public void ChangeDelay()
    {
        grid.searchDelay = slider.value;
    }

    public void ZurueckButton()
    {
        if (Start != null && Ziel != null)
        {
            Start.IsStart();
            Ziel.IsEnde();
        }
        switch (auswahl)
        {
            case 0:
                grid.GetComponent<Tiefensuche>().run = false;
                grid.GetComponent<Tiefensuche>().enabled = false;
                break;
            case 1:
                grid.GetComponent<Breitensuche>().run = false;
                grid.GetComponent<Breitensuche>().enabled = false;
                break;
        }
    }   
}
