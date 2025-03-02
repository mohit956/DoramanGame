using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform targetBoll; // Target position for debugging
    public Camera mainCamera; // Reference to the main camera
    public Transform firePoint; // Point from where the bullet will be fired

    public float shootCooldown = 0.5f; // Time between shots (in seconds)
    private float lastShootTime = 0f; // Time when the last shot was fired
    private GunScriptable Guninfo;

    RaycastHit hitInfo;
    private void Awake() {
        Guninfo=GameManager.instance.SelectedGun;
    }

    void Update()
    {


        if (GameManager.instance.IsAiming)
        {
            PerformRaycast();

            if (!GameManager.instance.IsAndroid && Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void PerformRaycast()
    {
        if (mainCamera == null)
        {
            return;
        }



        // Ray origin is from the camera position, and direction is forward
        Vector3 rayOrigin = mainCamera.transform.position;
        Vector3 rayDirection = mainCamera.transform.forward;

        // Perform the raycast

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            targetBoll.position = hitInfo.point; // Update target position for debugging
        }

       
    }

    public void Shoot()
    {
        if (GameManager.instance.IsAiming && Time.time >= lastShootTime + Guninfo.FireRate)
        {

            lastShootTime = Time.time; // Update the last shoot time
            // If the ray hits something, calculate the direction to the hit point
            Vector3 shootDirection = (hitInfo.point - firePoint.position).normalized;
            // Instantiate the bullet at the fire point
            GameObject bullet = Instantiate(Guninfo.Bullet, firePoint.position, Quaternion.LookRotation(shootDirection));

            // Apply force to the bullet (if it has a Rigidbody)
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.AddForce(shootDirection * Guninfo.Speed, ForceMode.Impulse); // Adjust force as needed
            }

        }
    }
}

