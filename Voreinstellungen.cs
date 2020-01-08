using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Voreinstellungen : MonoBehaviour
{
    //public GameObject inputField;
    public int groesse;
    //über start(int Wert) das Grid erstellen lassen, Wert ist dabei die Größe, wir wollen ein quadratisches Feld, dafür reicht eine Variable die Methode nur in die von Play einfügen
    
    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GridGroesse(string eingabe)
    {
        groesse = int.Parse(eingabe);
        Start(groesse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
