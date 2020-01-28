using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Voreinstellungen : MonoBehaviour
{
//Variablen
    public Grid grid;
    public Hindernisse hindernisse;

    //für Gridgröße eintellen (also das Inputfield)
    private int groesse;
    bool istDezimal = true;
    private int auswahl;

    //für InputField
    public string GridChange;
    public GameObject inputField;

    //Slider für Delay
    public Slider slider;

    //Funktionen für Start- und Zielfestlegung
    public Hex Start;
    public Hex Ziel;

    //Bei PlayButton
    public int geseheneFelder = 0;
    //public GameObject Character;
    public Transform SpawnCharacter;
    public GameObject charClone;
    public Hex before;
    

    
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
        //überprüft ob alle eingegebenen Zeichen Zahlen sind, weil Unity z.B. "." zulässt
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
            if(groesse > 25)
            {
                groesse = 25;
                grid.Beginn(groesse);
            }
            else
            {
                grid.Beginn(groesse);                               //die methode Beginn() vom Script Grid wird aufgerufen und der parameter wird übergeben
            }
            
        }

    }

    /*nach buttonclick wird das Start-Hex als "geclickt" gefärbt
    * dann wird das geclickte Feld wird als "Start" markiert
    * die Farbe des Feldes wird auf die "StartFarbe" gesetzt*/
    public void StartpunktButton()
    {
        if (Start != null)
            Start.ResetColor();
        Start = grid.GetClicked();
        Start.IsStart();
    }

    /*nach buttonclick wird das Ziel-Hex als "geclickt" gefärbt
     * dann wird das geclickte Feld wird als "Ziel" markiert
     * die Farbe des Feldes wird auf die "ZielFarbe" gesetzt*/
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
     * der "Scenenwechsel" erfolgt durch Unity, mit dem ein-/ausblenden des jeweiligen Canvas, automatisch
     */
    public void PlayButton()
    {
        Astar3coords Astar = new Astar3coords();

        hindernisse.SetChangeable(false); // deaktiviert Dragfunktion

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
                    //CharakterSpwan();
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
                    //CharakterSpwan();
                    break;
                }
                else
                {
                    Debug.Log("fehler bei Platzierung von Start- und Zielpunkt");
                    break;
                }
            case 2:
                Debug.Log("auswahl: A* ");
                if (Start != null && Ziel != null)
                {
                    Astar.Astar(Ziel, Start, grid);
                }
                else
                {
                    Debug.Log("fehler bei Platzierung von Start- und Zielpunkt");
                }
                
                break;
        }
    }

    

    //ändert das Delay mit dem der algorithmus durch läuft
    public void ChangeDelay()
    {
        grid.searchDelay = slider.value;
    }

    //Methode für den Zurückbutton in "InGameUI"
    public void HexagonsWiederFaerbbarMachen()
    {
        //beim Zurückgehen wird "colorChangeOn" auf true gesetzt, damit die felder wieder gefärbt werden können
        foreach (Hex g in grid.HexList) { g.colorChangeOn = true; }
    }

    //ZurückButton wenn man "Ingame" ist, also nicht in den Einstellungen
    public void ZurueckButton()
    {
        hindernisse.SetChangeable(true); // Aktieviert das Draggen wieder

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
        //Destroy(charClone);
    }
    //Für charakter Erstellung und Bewegung
    public void CharakterSpwan()
    {
        List<Hex> pfadRev = grid.GetComponent<Tiefensuche>().pathList;
        Vector3 direction;
        Hex before = pfadRev[pfadRev.Count - 1];
        Instantiate(charClone, pfadRev[pfadRev.Count - 1].transform.position, SpawnCharacter.rotation);
        pfadRev.RemoveAt(pfadRev.Count - 1);
        for (int i = pfadRev.Count ; i > 0; i--)
        {
            direction = before.transform.position - pfadRev[pfadRev.Count - 1].transform.position;
            for (int j = 4; j > 0; j--)
            {
                charClone.transform.position = charClone.transform.position + direction * 0.25f;
            }
            before = pfadRev[pfadRev.Count - 1];
            pfadRev.RemoveAt(pfadRev.Count - 1);
        }

    }
}
