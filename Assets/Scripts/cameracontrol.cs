using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //aaaaa
    }

    // Update is called once per frame
    public float playerspeed; 
    void Update()
    {
                this.transform.position += new Vector3(0f, 0f, playerspeed);
                
                

    }
}
