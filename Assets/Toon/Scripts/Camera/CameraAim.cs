using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // Import Cinemachine namespace

public class CameraAim : MonoBehaviour
{


    private void Update() {
        if(Input.GetMouseButtonDown(1) && !GameManager.instance.IsAndroid){
            aim();

        }

        else if(Input.GetMouseButtonUp(1)&& !GameManager.instance.IsAndroid)
        {
            RevertAim();
        }
    }

    public void switchAim()
    {
        if (GameManager.instance.IsAiming)
        {
            RevertAim();
        }
        else
        {
            aim();
        }
    }

    public void aim()
    {
        GameManager.instance.IsAiming = true;
        GameManager.instance.thirdPersonController.rigWeight=1;
     

    }
    public void RevertAim()
    {
        GameManager.instance.IsAiming = false;
        GameManager.instance.thirdPersonController.rigWeight=0;

        
    }
}

