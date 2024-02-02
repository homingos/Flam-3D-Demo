using UnityEngine;

public class PinchController : MonoBehaviour
{
    // Adjust this value to control the sensitivity of the pinch resize
    public float pinchResizeSpeed = 0.5f;

    // Minimum and maximum size limits
    public float minSize = 0.5f;
    public float maxSize = 2.0f;

    private Vector2 initialPinchDistance;
    private float initialSize;

    public ExperienceManager experienceManager;

    void Update()
    {
        // Check if there are two touches (fingers)
        if (Input.touchCount == 2)
        {
            // Get the first and second touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Check if touches began in the previous frame
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");
                initialPinchDistance = touchZero.position - touchOne.position;
                initialSize = transform.localScale.x;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Moved");

                // Calculate the current pinch distance
                Vector2 currentPinchDistance = touchZero.position - touchOne.position;
                float pinchMagnitude = currentPinchDistance.magnitude - initialPinchDistance.magnitude;

                // Calculate the pinch resize amount
                float pinchResize = pinchMagnitude * pinchResizeSpeed * Time.deltaTime;

                // Apply the resize factor to the object
                float newSize = initialSize + pinchResize;
                newSize = Mathf.Clamp(newSize, minSize, maxSize);

                float scaleRatio = newSize / initialSize;

                GameObject currentExperience = experienceManager.currentGameObject;
                Transform bottomOriginTransform = experienceManager.currentGameObject.transform.Find("BottomOrigin");

                if (bottomOriginTransform == null) {
                    Debug.Log("The bottom object is null");
                    return;
                }


                currentExperience.transform.localScale = new Vector3(newSize, newSize, newSize);
                //currentExperience.transform.position = calcualteNewOrigin(currentExperience.transform, scaleRatio, bottomOriginTransform);
            }
        }
    }

    Vector3 calculateOffset(Transform gameObj, Transform baseObj)
    {
        Vector3 offset = gameObj.position - baseObj.position;
        return offset;
    }

    Vector3 calcualteNewOrigin(Transform gameObj, float scaleFactor, Transform baseObj)
    {
        Vector3 oldOrigin = gameObj.position;
        Vector3 positionOffset = calculateOffset(gameObj, baseObj);
        Vector3 newOrigin = oldOrigin + positionOffset * (scaleFactor);

        return newOrigin;
    }
}
