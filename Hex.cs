using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hex
{
    public int xCoordinate { get; set; }
    public int yCoordinate { get; set; }
    public Boolean betretbar = true;
    public Boolean entdeckt = false;
    public Hex previous;
    public List<Hex> nachbarn = new List<Hex>();


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
    }
}
