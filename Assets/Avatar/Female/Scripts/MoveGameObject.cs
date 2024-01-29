using UnityEngine;
using UnityEngine.UI;

public class MoveObjectButton : MonoBehaviour
{
    public Transform targetTransform; // Assign your GameObject's transform here in the Inspector

    // This function is called when the button is clicked
    public void OnMoveButtonClick()
    {
        // Check if a target transform is assigned
        if (targetTransform != null)
        {
            // Move the GameObject, e.g., 1 unit to the right
            targetTransform.position += Vector3.right;
        }
        else
        {
            Debug.LogWarning("No target transform assigned.");
        }
    }
}
