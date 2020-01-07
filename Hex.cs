using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Temporär von mir(Paul) erstellte Hexklasse

public class Hex : MonoBehaviour
{
    Boolean discovered=false;
    Hex previous;

    public Hex[] Neighbors; 
    
    public int xCoordinate { get; set; }
    public int yCoordinate { get; set; }    

    public Hex()
    {

    }

    public void setDiscovered()
    {
        discovered = true;
    }

    public Boolean getDiscovered()
    {
        return discovered;
    }

    public Hex[] getNeighbors()
    {
        return Neighbors;
    }    

    public void setPrevious(Hex prev)
    {
        previous = prev; 
    }

    public Hex getPrevious(Hex prev)
    {
        return previous;
    }
}
