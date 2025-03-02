using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.IsAndroid)  // Right mouse button clicked
        {
            fire();
        }
        else if (Input.GetMouseButtonUp(0) && !GameManager.instance.IsAndroid)  // Right mouse button released
        {
            stopFire();
        }
    }

    public void swichFire()
    {
        if (GameManager.instance.IsFireing)
        {
            stopFire();
        }
        else
        {
            fire();
        }
    }

  

    public void fire()
    {
        GameManager.instance.IsFireing = true;
        if (!GameManager.instance.IsAiming)
        {
            
            GameManager.instance.AimScript.aim();
        }

        
    }
    public void stopFire()
    {

        GameManager.instance.IsFireing = false;

    }
}
