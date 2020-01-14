using System;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Material[] farbe; //0=aktuell 1=blockiert 2=Entdeckt 3=pfad 4=start 5=ziel 6=normal 7=clicked
    public int xCoordinate { get; set; }
    public int yCoordinate { get; set; }
    public int zCoordinate { get; set; }
    public Boolean betretbar = true;
    public Boolean entdeckt = false;
    public Boolean clicked = false;
    public Hex previous;
    public bool colorChangeOn = true;
    //public int dijkstra_abstand = int.MaxValue;
    public List<Hex> nachbarn = new List<Hex>();

    int preFarbe;
    int iFarbe = 6;

    void Update()
    {
        //Änderung der farbe und des clicked wertes, wenn geklickt wird
        if (Input.GetMouseButtonDown(0) && colorChangeOn)
        {
            RaycastHit Hitinfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out Hitinfo))
            {
                if (Hitinfo.collider == GetComponent<MeshCollider>())//Wenn der ray auf den Meshcollider getroffen ist
                {
                    clicked = true;             //Hexagon auf angeklickt setzen
                    if (iFarbe == 6)
                        ChangeColor(7);             //Farbe entsprechend anpassen
                    //ShowNeighbors();
                }
                else                            //Wenn der ray was anderes getroffen hat
                {
                    clicked = false;            // Hexagon auf nicht angeklickt setzen
                    if (iFarbe == 7)            // Wenn das material nicht die "angklickt" farbe hat soll die farbe nicht geändert werden(z.B. Wenn Ziel und start schon makiert sind
                        ChangeColor(6);         // farbe entsprechen anpassen 
                }
            }
        }

        //Für Update der Kollision von Hindernissen
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            if (iFarbe == 6)
            {
                betretbar = false;
                ChangeColor(1);
            }
        }
        else
        {
            betretbar = true;
            if (iFarbe == 1)            // Wenn das material nicht die "angklickt" farbe hat soll die farbe nicht geändert werden(z.B. Wenn Ziel und start schon makiert sind
                ChangeColor(6);         // farbe entsprechen anpassen
        }
    }

    //Hex Koordinaten
    public void SetX(int x)
    {
        xCoordinate = x;
    }
    public void SetY(int y)
    {
        yCoordinate = y;
    }
    public void SetZ(int z)
    {
        zCoordinate = z;
    }

    //Hex Nachbarn
    public List<Hex> getNachbarn()
    {
        return nachbarn; //Funktion, die die Nachbarn zurück gibt
    }
    public void addNachbar(Hex nachb)
    {
        if (nachb != null)  //Funktion, die ein übergebenes Hex den Nachbarnarray von diesem Hex hinzufügt
            nachbarn.Add(nachb);
    }

    //Hex Betretbar
    public Boolean getBetretbar()
    {
        return betretbar; //gibt zurück ob das Hex betretbar ist oder nicht
    }
    public void setBetretbar(Boolean betr)
    {
        betretbar = betr;   //je nach dem was übergeben wird setzt die Funktion
        if (betr == false)  //den betreten wert auf false oder true und färbt es entsprechend ein
            ChangeColor(1);
        else
            ChangeColor(0);
    }


    //Hex Vorherigen
    public Hex getPrevious()
    {
        if (previous == null)   //gibt das Hex zurück, das von einem algorithmus als vorheriges
            return null;        //Hex deklariert wurde
        else
            return previous;    // wird von der CreatePath.cs zur Pfaderstellung genutzt
    }
    public void setPrevious(Hex prev)
    {
        previous = prev;       //wird von algorithmen genutzt um ein Vorheriges Hex zu deklarieren
    }


    public Boolean GetEntdeckt()
    {
        return entdeckt;       //Gibt zurück, ob das Hex von einem Algorithmus bereits gefunden wurde
    }
    public void SetEntdeckt(Boolean wert)
    {
        entdeckt = wert;        //wird von einem Algorithmus gesetzt, wenn er das Hex entdeckt hat
        if (wert == true)
            ChangeColor(2);
    }

    public void IsStart()
    {
        ChangeColor(4);         //Setzt die farbe des Hex auf die Start-farbe
    }
    public void IsEnde()
    {
        ChangeColor(5);         //Setzt die Farbe des hex auf die Ziel-farbe
    }
    public void ResetColor()
    {
        ChangeColor(6);         //Setzt die Farbe auf die Normale Farbe zurück
    }

    public void ChangeColor(int c) //Methode, die die Farbe des Hex ändert
    {
        Renderer rend;  //erstellt einen Renderer
        rend = GetComponentInParent<Renderer>();  //Setzt rend auf den Mesh-Renderer des Hexprefab
        rend.sharedMaterial = farbe[c];     //Ändert das Material auf ein anderes Vorgerfertigtes material
        iFarbe = c;    //Hilfswert für Hexfarbe, das zur leichteren Verwendung in der update-Methode benutzt wird
    }

    public void Destroy()
    {
        Destroy(gameObject); //Zerstört das komplette Object
    }

    public void ShowNeighbors() //nicht benutzte funktion, die die Nachbarn des gewählten Hex highlightet
    {
        foreach (Hex g in nachbarn)
            g.ChangeColor(2);
    }

    public Transform getTransform() //Funktion, die die Position des Hex zurück gibt 
    {
        return GetComponent<RectTransform>();
    }

}