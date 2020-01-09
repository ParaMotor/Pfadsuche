using System;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Material[] farbe; //0=aktuell 1=blockiert 2=Entdeckt 3=pfad 4=start 5=ziel
    public int xCoordinate { get; set; }
    public int yCoordinate { get; set; }
    public Boolean betretbar = true;
    public Boolean entdeckt = false;
    public Hex previous;
    public int dijkstra_abstand = int.MaxValue;
    public List<Hex> nachbarn = new List<Hex>();

    //Hex Koordinaten
    public void SetX(int x)
    {
        xCoordinate = x;
    }
    public void SetY(int y)
    {
        yCoordinate = y;
    }

    //Hex Funktionen
    public List<Hex> getNachbarn()
    {
        return nachbarn;
    }
    public Boolean getBetretbar()
    {
        return betretbar;        
    }
    public Hex getPrevious()
    {
        if (previous == null)
            return null;
        else
            return previous;
    }
    public void setBetretbar(Boolean betr)
    {
        betretbar = betr;
        if (betr == false)
            ChangeColor(1);
    }
    public void setPrevious(Hex prev)
    {
        previous = prev;
    }
    public void addNachbar(Hex nachb)
    {
        if (nachb!=null)
            nachbarn.Add(nachb);
    }
    public Boolean GetEntdeckt()
    {
        return entdeckt;
    }
    public void SetEntdeckt(Boolean wert)
    {
        entdeckt = wert;
        if (wert == true)
            ChangeColor(2);
    }
    public void ChangeColor(int c)
    {
        Renderer rend;
        rend = GetComponentInParent<Renderer>();
        rend.sharedMaterial = farbe[c];
    }
            

    

}