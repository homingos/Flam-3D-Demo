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
    Vector3 originalPos;
    Vector3 positionOffset;
    GameObject currentExperience;
    Transform baseObjectTransform;

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
                initialPinchDistance = touchZero.position - touchOne.position;
                initialSize = transform.localScale.x;
                if (experienceManager.currentGameObject == null)
                    return;

                currentExperience = experienceManager.currentGameObject;
                originalPos = experienceManager.currentGameObject.transform.position;

                baseObjectTransform = experienceManager.currentGameObject.transform.Find("BottomOrigin");

                positionOffset = calculateOffset(currentExperience.transform, baseObjectTransform);
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float pinchResize = calcualtePinchResize(touchZero.position, touchOne.position);

                // Apply the resize factor to the object
                float newSize = initialSize + pinchResize;
                newSize = Mathf.Clamp(newSize, minSize, maxSize);
                pinchResize = newSize - initialSize;

                GameObject currentExperience = experienceManager.currentGameObject;
                

                if (baseObjectTransform == null) {
                    Debug.Log("The bottom object is null");
                    return;
                }


                currentExperience.transform.localScale = new Vector3(newSize, newSize, newSize);
                currentExperience.transform.position = calcualteNewOrigin(currentExperience.transform, pinchResize, baseObjectTransform, initialSize);
            }
        }
    }

    float calcualtePinchResize(Vector2 pos1, Vector2 pos2)
    {
        // Calculate the current pinch distance
                Vector2 currentPinchDistance = pos1 - pos2;
        float pinchMagnitude = currentPinchDistance.magnitude - initialPinchDistance.magnitude;

        // Calculate the pinch resize amount
        float pinchResize = pinchMagnitude * pinchResizeSpeed * Time.deltaTime;

        return pinchResize;
    }

    Vector3 calculateOffset(Transform gameObj, Transform baseObj)
    {
        Vector3 offset = gameObj.position - baseObj.position;
        return offset;
    }

    Vector3 calcualteNewOrigin(Transform gameObj, float delX, Transform baseObj, float origSize)
    {
        Vector3 newOrigin = originalPos + (delX/origSize)*positionOffset + positionOffset;
        Debug.Log("Size calculations: " + newOrigin + " : " + originalPos);

        return newOrigin;
    }
}
