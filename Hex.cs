using System;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Material[] farbe; //0=aktuell 1=blockiert 2=Entdeckt 3=pfad 4=start 5=ziel 6=normal
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit Hitinfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out Hitinfo))
            {
                if (colorChangeOn)
                {
                    if (Hitinfo.collider == GetComponent<MeshCollider>())
                    {
                        clicked = true;
                        ChangeColor(1);
                    }
                    else
                    {
                        clicked = false;
                        ChangeColor(6);
                    }
                }
            }
        }
        //Für Update der Kollision von Hindernissen
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            betretbar = false;
        }
        else
        {
            betretbar = true;
        }
        if (!betretbar)
        {
            ChangeColor(1);
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
        return nachbarn;
    }
    public void addNachbar(Hex nachb)
    {
        if (nachb != null)
            nachbarn.Add(nachb);
    }

    //Hex Betretbar
    public Boolean getBetretbar()
    {
        return betretbar;
    }
    public void setBetretbar(Boolean betr)
    {
        betretbar = betr;
        if (betr == false)
            ChangeColor(1);
    }


    //Hex Vorherigen
    public Hex getPrevious()
    {
        if (previous == null)
            return null;
        else
            return previous;
    }
    public void setPrevious(Hex prev)
    {
        previous = prev;
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
        //rend.sharedMaterial = farbe[c];
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void ShowNeighbors()
    {
        foreach (Hex g in nachbarn)
            g.ChangeColor(2);
    }
    public Transform getTransform()
    {
        return GetComponent<RectTransform>();
    }

}