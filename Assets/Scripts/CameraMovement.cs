using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

     [Range(0,1)]public float Speed = .5f;
     [Range (0,100)]public float zoomSpeed = 50f;
    
     void Update()
     {
         float xAxisValue = Input.GetAxis("Horizontal") * Speed;
         float zAxisValue = Input.GetAxis("Vertical") * Speed;
         float scroll = Input.GetAxis("Mouse ScrollWheel");
 
         transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
         transform.Translate(0,0, scroll * zoomSpeed, Space.Self);
        
     }
}
