using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
     public float speed;
     public float hardModeSpeed;

     private Rigidbody rb;

     void Start()
     {
          if (GameController.hardMode) { 
               rb = GetComponent<Rigidbody>();
          rb.velocity = transform.forward * hardModeSpeed;
          } else { 
          rb = GetComponent<Rigidbody>();
          rb.velocity = transform.forward * speed;
          }
     }
}
    
