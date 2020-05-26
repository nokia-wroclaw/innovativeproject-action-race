using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownNokiaController : MonoBehaviour
{
    public bool collided;


    void OnTriggerEnter2D(Collider2D collision)
    {   
         collided = true;
         Debug.Log("Wykryto kolizję");
              
    }
}
