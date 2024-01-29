using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform; // Assign your camera's Transform component here in the Inspector
    public float transitionDuration = 1.0f;

    [Serializable]
    public struct ButtonPositionAndRotation
    {
        public Button[] buttons;
        public Vector3 position;
        public Vector3 rotation;
    }

    public ButtonPositionAndRotation[] buttonPositions;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float transitionStartTime;

    void Start()
    {
        initialPosition = cameraTransform.position;
        initialRotation = cameraTransform.rotation;

        // Attach button click listeners
        foreach (var buttonPosition in buttonPositions)
        {
            foreach (var button in buttonPosition.buttons)
            {
                button.onClick.AddListener(() => ChangeCameraPositionAndRotation(buttonPosition));
            }
        }
    }

    void Update()
    {
        // Interpolate camera position and rotation
        float t = (Time.time - transitionStartTime) / transitionDuration;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, initialPosition, t);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, initialRotation, t);
    }

    public void ChangeCameraPositionAndRotation(ButtonPositionAndRotation buttonPosition)
    {
        // Set the transition start time
        transitionStartTime = Time.time;

        // Update initial position and rotation based on the selected position
        initialPosition = buttonPosition.position;
        initialRotation = Quaternion.Euler(buttonPosition.rotation);
    }
}
