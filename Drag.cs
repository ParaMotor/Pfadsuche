using System;
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
    public Boolean drag = true;
    // Plane auf der derzeitig gezogen wird
    private Plane dragPlane;

    //Offset für die DIfferenz der Position von Objekt und Maus 
    private Vector3 offset;

    //Variable für spätere Abfrage
    private RaycastHit hit;

    //Camera für Rays
    private Camera myMainCamera;

    void Start()
    {
        Camera myMainCamera = Camera.main; //erstes Einlesen
    }

    void OnMouseDown()
    {
        if (drag)
        {
            Camera myMainCamera = Camera.main;                            //Kamera Einlesen
            dragPlane = new Plane(myMainCamera.transform.forward, transform.position); //Plane für derzeitige Position
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);//Ray zwischen Mauszeiger und Kamera

            float planeDist;            //Raycast auf Plane für Abstand
            dragPlane.Raycast(camRay, out planeDist);
            offset = transform.position - camRay.GetPoint(planeDist);//Berechnung des Abstandes zum ziehen
        }
    }

    void OnMouseDrag()
    {
        if (drag)
        {
            //Erneutes Kameraeinlesen und Erstellen des Rays
            Camera myMainCamera = Camera.main;
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            transform.position = camRay.GetPoint(planeDist) + offset;
            transform.position = transform.position + new Vector3(0f, (-transform.position.y + 0.1f), 0f);


            Ray rayDown = new Ray(transform.position, -Vector3.up);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                //print("Found an object : " + hit.collider.transform);
                if(!Input.GetKey(KeyCode.LeftAlt))
                    transform.position = hit.collider.transform.position + new Vector3(0f, 0.1f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                transform.RotateAround(transform.position, Vector3.up, 60);
            }

            if (Input.GetKeyDown(KeyCode.Comma))
            {
                transform.RotateAround(transform.position, Vector3.up, -60);
            }
        }
    }
}
