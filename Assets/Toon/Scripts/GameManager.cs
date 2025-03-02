using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Script Refrence")]

    public ThirdPersonController thirdPersonController;
    public Fire fire;
    public CameraAim AimScript;
    public ThirdPeasonCam camrotate;

    [Header("Game Status")]
    public bool IsAiming = false;
    public bool IsAndroid = false;
    public bool IsFireing = false;





    [Header("Scriptable")]
    public List<GunScriptable> gunsInfo;
    [HideInInspector] public GunScriptable SelectedGun;
    [Header("UI")]
    public GameObject firebtn;


    [Header("Object Refrence")]
    public CinemachineFreeLook freeLookCamera;






    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Make sure the GameManager persists across scenes

        SelectedGun = gunsInfo[1];
    }
    private void Start()
    {
        if (IsAndroid)
        {
            freeLookCamera.m_XAxis.m_InputAxisName = ""; // Clear the input axis name
            freeLookCamera.m_YAxis.m_InputAxisName = ""; // Clear the input axis name

        }
    }
}
