using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RotateObject : MonoBehaviour
{
    private Vector3 rotationCenter;  // Center point of rotation
    private bool isRotating = false; // Flag to track if rotation is in progress
    private float previousMouseX; // Store the previous mouse/touch X position
    public float rotationSpeed = 1.0f; // Adjustable rotation speed
    public bool clockwiseRotation = false; // Clockwise or counterclockwise rotation flag

    void Start()
    {
        // Calculate the rotation center as the center of the GameObject's transform
        rotationCenter = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // Check if the mouse button is pressed or a touch has begun
            if (!IsPointerOverUIObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Cast a ray to check if it hits the GameObject
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    isRotating = true;
                    previousMouseX = Input.mousePosition.x;
                }
            }
        }

        if (isRotating)
        {
            // Calculate the current mouse/touch X position
            float currentMouseX = Input.mousePosition.x;

            // Calculate the rotation angle based on the mouse/touch movement
            float angle = (currentMouseX - previousMouseX) * rotationSpeed;

            // Apply clockwise or counterclockwise rotation based on the flag
            if (clockwiseRotation)
            {
                angle = -angle;
            }

            // Calculate the pivot point
            Vector3 pivot = rotationCenter;

            // Rotate the object around the pivot point on the Y-axis only
            transform.RotateAround(pivot, Vector3.up, angle);

            // Update the previous mouse/touch X position
            previousMouseX = currentMouseX;

            if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                // Check if the mouse button is released or touch ends
                isRotating = false;
            }
        }
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
