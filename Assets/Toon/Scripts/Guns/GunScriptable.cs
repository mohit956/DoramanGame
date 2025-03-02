using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMyScriptableObject", menuName = "Guns/AddGun")]

public class GunScriptable : ScriptableObject
{
    public string Name;
    public string ID;
    public float FireRate;
    public float Speed=50;
    public GameObject Bullet;
    public Sprite Icon;

    
}
