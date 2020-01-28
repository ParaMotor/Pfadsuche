using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hindernisse : MonoBehaviour
{
    //"Inventar"
    public Transform Schreibtisch;
    public Transform Chair;
    public Transform Sofa;
    public Transform Regal;
    public List<Transform> hindernissListe = new List<Transform>();
    
    //Inventar: Erzeugung der Hindernisse
    public void SpawnRegal()
    {
        Transform hinderniss = Instantiate(Regal);
        AddComponents(hinderniss);
    }
    public void SpawnSchreibtisch()
    {
        Transform hinderniss = Instantiate(Schreibtisch);
        AddComponents(hinderniss);
    }
    public void SpawnSofa()
    {
        Transform hinderniss = Instantiate(Sofa);
        AddComponents(hinderniss);
        hinderniss.gameObject.AddComponent<BoxCollider>();
    }
    public void SpawnChair()
    {
        Transform hinderniss = Instantiate(Chair);
        AddComponents(hinderniss);
        hinderniss.gameObject.AddComponent<BoxCollider>();
    }

    private void AddComponents(Transform T)
    {
        T.SetParent(this.transform);
        T.gameObject.AddComponent<Drag>();
        T.gameObject.AddComponent<Zerstoeren>();
        hindernissListe.Add(T);
    }

    public void SetChangeable(Boolean b)
    {
        foreach(Transform T in hindernissListe)
        {
            T.GetComponent<Drag>().drag = b;
            T.GetComponent<Zerstoeren>().destroyable = b;
        }
    }
}
