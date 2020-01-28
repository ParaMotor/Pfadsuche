using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    List<Hex> path = new List<Hex>();
    int stepNr;
    Boolean run = false;
    public Boolean abort = false;

    //Geschwindigkeit des Characters
    public float speed;
    public float turnSpeed;
    
    //Initialisierungsfunktion
    public void Init(List<Hex> pathlist)
    {
        path = pathlist;
        path.Reverse();
        run = true;
        GetComponent<Animator>().SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (run && path.Count>0)
        {
            float step = speed * Time.deltaTime; //Berechnung der Entfernung, die sie laufen soll
            float turnStep = turnSpeed * Time.deltaTime;  //Berechnung der Drehgeschwindigkeit
            transform.position = Vector3.MoveTowards(transform.position, path[stepNr].getTransform().position, step); //Bewegungsfunktion

            //Bestimmt in welche Richtung der Character schauen soll
            Vector3 targetDirection = path[stepNr].getTransform().position - transform.position;

            //Rotiert den forward-Vector in die Richtung
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (Vector3.Distance(transform.position, path[stepNr].getTransform().position) < 0.001f)
            {
                stepNr++;
            }

            //Ende erreicht und resetten der Voreinstellungen
            if (Vector3.Distance(transform.position, path[path.Count-1].getTransform().position) < 0.001f || abort)
            {
                run = false;                                            //run deaktivieren
                path.Clear();                                           //Liste leeren
                stepNr = 0;                                             //stepNr zurücksetzen
                GetComponent<Animator>().SetBool("walk", false);
                abort = false;
            }
        }
    }    
}
