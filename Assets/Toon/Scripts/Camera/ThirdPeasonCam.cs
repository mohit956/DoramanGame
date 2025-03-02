using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPeasonCam : MonoBehaviour
{

    public FixedTouchField touch;
    public CinemachineFreeLook freeLookCamera;
    public float sensitivity=5f;


public float aimFOV=25;
public float normalFOV =60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (freeLookCamera != null)
            {
                freeLookCamera.m_XAxis.Value += touch.TouchDist.x * Time.deltaTime * sensitivity * 200; // Horizontal rotation
                freeLookCamera.m_YAxis.Value -= touch.TouchDist.y * Time.deltaTime * sensitivity; // Vertical rotation




                //change fov
            float targetFOV = GameManager.instance.IsAiming ? aimFOV : normalFOV;
    freeLookCamera.m_Lens.FieldOfView = Mathf.Lerp(freeLookCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * 5f);
            } 
    }
}
