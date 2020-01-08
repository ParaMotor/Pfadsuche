using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{
    //über start(int Wert) das Grid erstellen lassen, Wert ist dabei die Größe, wir wollen ein quadratisches Feld, dafür reicht eine Variable die Methode nur in die von Play einfügen
    public void PlayButton()
    {
        //start(Wert)
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }

     
}
