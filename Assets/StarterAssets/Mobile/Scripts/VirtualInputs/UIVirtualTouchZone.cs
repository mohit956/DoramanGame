using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIVirtualTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [System.Serializable]
    public class Event : UnityEvent<Vector2> { }

    [Header("Rect References")]
    public RectTransform containerRect;
    public RectTransform handleRect;

    [Header("Settings")]
    public float magnitudeMultiplier = 1f;  // Adjust sensitivity
    public bool invertXOutputValue;
    public bool invertYOutputValue;

    // Stored Pointer Values
    private Vector2 pointerDownPosition;  // Initial touch position
    private Vector2 lastPointerPosition;  // Last touch position
    private bool isDragging = false;      // Tracks if the user is dragging

    [Header("Output")]
    public Event touchZoneOutputEvent; // Event to output drag delta

    void Start()
    {
        SetupHandle();
    }

    private void SetupHandle()
    {
        if (handleRect)
        {
            SetObjectActiveState(handleRect.gameObject, false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Record the initial touch position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

        lastPointerPosition = pointerDownPosition; // Initialize last position
        isDragging = true; // Start tracking drag

        if (handleRect)
        {
            SetObjectActiveState(handleRect.gameObject, true);
            UpdateHandleRectPosition(pointerDownPosition);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Get the current touch position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out Vector2 currentPointerPosition);

        // Calculate the drag delta since the last frame
        Vector2 dragDelta = currentPointerPosition - lastPointerPosition;

        // Apply inversion and magnitude multiplier
        Vector2 outputPosition = ApplyInversionFilter(dragDelta) * magnitudeMultiplier;

        // Output the drag delta for this frame
        OutputPointerEventValue(outputPosition);

        // Update the last pointer position for the next frame
        lastPointerPosition = currentPointerPosition;

        // Optionally update the handle position
        if (handleRect)
        {
            UpdateHandleRectPosition(currentPointerPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset all input values when the touch is lifted
        pointerDownPosition = Vector2.zero;
        lastPointerPosition = Vector2.zero;
        isDragging = false;

        // Output zero as the drag has ended (stops rotation)
        OutputPointerEventValue(Vector2.zero);

        if (handleRect)
        {
            SetObjectActiveState(handleRect.gameObject, false);
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    private void OutputPointerEventValue(Vector2 pointerPosition)
    {
        // Send the touch drag delta to the event
        touchZoneOutputEvent.Invoke(pointerPosition);
    }

    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        if (handleRect)
        {
            handleRect.anchoredPosition = newPosition;
        }
    }

    private void SetObjectActiveState(GameObject targetObject, bool newState)
    {
        if (targetObject)
        {
            targetObject.SetActive(newState);
        }
    }

    private Vector2 ApplyInversionFilter(Vector2 position)
    {
        if (invertXOutputValue)
        {
            position.x = InvertValue(position.x);
        }

        if (invertYOutputValue)
        {
            position.y = InvertValue(position.y);
        }

        return position;
    }

    private float InvertValue(float value)
    {
        return -value;
    }
}


// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.Events;

// public class UIVirtualTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
// {
//     [System.Serializable]
//     public class Event : UnityEvent<Vector2> { }

//     [Header("Rect References")]
//     public RectTransform containerRect;
//     public RectTransform handleRect;

//     [Header("Settings")]
//     public bool clampToMagnitude;
//     public float magnitudeMultiplier = 1f;
//     public bool invertXOutputValue;
//     public bool invertYOutputValue;

//     //Stored Pointer Values
//     private Vector2 pointerDownPosition;
//     private Vector2 currentPointerPosition;

//     [Header("Output")]
//     public Event touchZoneOutputEvent;

//     void Start()
//     {
//         SetupHandle();
//     }

//     private void SetupHandle()
//     {
//         if(handleRect)
//         {
//             SetObjectActiveState(handleRect.gameObject, false); 
//         }
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {

//         RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

//         if(handleRect)
//         {
//             SetObjectActiveState(handleRect.gameObject, true);
//             UpdateHandleRectPosition(pointerDownPosition);
//         }
//     }

//     public void OnDrag(PointerEventData eventData)
//     {

//         RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);
        
//         Vector2 positionDelta = GetDeltaBetweenPositions(pointerDownPosition, currentPointerPosition);

//         Vector2 clampedPosition = ClampValuesToMagnitude(positionDelta);
        
//         Vector2 outputPosition = ApplyInversionFilter(clampedPosition);

//         OutputPointerEventValue(outputPosition * magnitudeMultiplier);
//     }

//     public void OnPointerUp(PointerEventData eventData)
//     {
//         pointerDownPosition = Vector2.zero;
//         currentPointerPosition = Vector2.zero;

//         OutputPointerEventValue(Vector2.zero);

//         if(handleRect)
//         {
//             SetObjectActiveState(handleRect.gameObject, false);
//             UpdateHandleRectPosition(Vector2.zero);
//         }
//     }

//     void OutputPointerEventValue(Vector2 pointerPosition)
//     {
//         touchZoneOutputEvent.Invoke(pointerPosition);
//     }

//     void UpdateHandleRectPosition(Vector2 newPosition)
//     {
//         handleRect.anchoredPosition = newPosition;
//     }

//     void SetObjectActiveState(GameObject targetObject, bool newState)
//     {
//         targetObject.SetActive(newState);
//     }

//     Vector2 GetDeltaBetweenPositions(Vector2 firstPosition, Vector2 secondPosition)
//     {
//         return secondPosition - firstPosition;
//     }

//     Vector2 ClampValuesToMagnitude(Vector2 position)
//     {
//         return Vector2.ClampMagnitude(position, 1);
//     }

//     Vector2 ApplyInversionFilter(Vector2 position)
//     {
//         if(invertXOutputValue)
//         {
//             position.x = InvertValue(position.x);
//         }

//         if(invertYOutputValue)
//         {
//             position.y = InvertValue(position.y);
//         }

//         return position;
//     }

//     float InvertValue(float value)
//     {
//         return -value;
//     }
    
// }
