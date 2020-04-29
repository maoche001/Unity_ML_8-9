using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public static bool complete;
    public GameObject eny;
    public GameObject gun;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "gun")
        {
            complete = true;
            //Destroy(eny);
            //Destroy(gun);
        }
    }
}
