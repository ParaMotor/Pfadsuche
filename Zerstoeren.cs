using System;
using System.Collections.Generic;
using UnityEngine;

public class Zerstoeren : MonoBehaviour
{
    public Boolean destroyable;
    
    RaycastHit Hitinfo;

    void Update()
    {
        if (destroyable)
            if (Input.GetMouseButtonDown(1))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out Hitinfo))
                {
                    if (Hitinfo.collider == GetComponent<BoxCollider>())//Wenn der ray auf den Meshcollider getroffen ist
                    {
                        Destroy(this.gameObject);
                        GetComponentInParent<Hindernisse>().hindernissListe.Remove(this.transform);
                    }
                }

            }
    }

}
