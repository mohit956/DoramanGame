using UnityEngine;

public class CameraController : MonoBehaviour
{
    // [Header("Touch Input")]
    // [SerializeField] private UI_VirtualTouchZone virtualTouchZone; // Reference to the touch zone

    // [Header("Camera Settings")]
    // [SerializeField] private Transform cameraTransform; // Camera to rotate
    // [SerializeField] private float rotationSpeed = 0.1f; // Sensitivity for rotation

    // private void Update()
    // {
    //     if (virtualTouchZone != null)
    //     {
    //         // Get the drag delta from the touch zone
    //         Vector2 dragDelta = virtualTouchZone.dragDelta;

    //         // Rotate the camera based on the drag delta
    //         if (dragDelta != Vector2.zero)
    //         {
    //             // Rotate horizontally (yaw) and vertically (pitch)
    //             float yaw = dragDelta.x * rotationSpeed;
    //             float pitch = -dragDelta.y * rotationSpeed; // Negative because dragging up should look down

    //             // Apply rotation to the camera
    //             cameraTransform.Rotate(Vector3.up, yaw, Space.World); // Yaw (global Y-axis)
    //             cameraTransform.Rotate(Vector3.right, pitch, Space.Self); // Pitch (local X-axis)
    //         }
    //     }
    // }
}
