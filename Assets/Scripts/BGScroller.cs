
using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour 
{

    public float scrollSpeed;
    public float hardModeScrollSpeed;
    public float tileSizeZ;
    private float t = 0.0f;
    private Vector3 startPosition;

    void Start () 
    {
        startPosition = transform.position;
         
    }

    void Update ()
    {
        if (GameController.hardMode) { 
            float newPosition = Mathf.Repeat (Time.time * Mathf.Lerp(scrollSpeed, hardModeScrollSpeed, t), tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
            t += 0.25f * Time.deltaTime;
          } else { 
            float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
          }
    }
}