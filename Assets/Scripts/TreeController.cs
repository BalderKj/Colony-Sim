using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    // Start is called before the first frame update

    public float woodCount = 30f;
    public bool occupied = false;
    void Start()
    {
        
    }

    public void KillTree()
    {
        // put tree fall animation here
        Destroy(gameObject,0.5f);
    }

}
