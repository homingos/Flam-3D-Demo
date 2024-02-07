using UnityEngine;

public class PinchController : MonoBehaviour
{
    // Adjust this value to control the sensitivity of the pinch resize
    public float pinchResizeSpeed = 0.5f;

    // Minimum and maximum size limits
     float minSize = 0.1f;
     float maxSize = 4.0f;

    private Vector2 initialPinchDistance;
    private float initialSize;
    //Vector3 originalPos;
    Vector3 positionOffset;
    GameObject currentExperience;
    Transform baseObjectTransform;

    public ExperienceManager experienceManager;

    private Vector3 initialScale;
    private float minPinchDistance = 0.5f;

    private Transform GetObjectTransform()
    {
        if(experienceManager.currentGameObject!=null)
        return experienceManager.currentGameObject.transform;

        return null;
    }


    Vector2 lastmousePos = Vector2.zero;
    public void TestScaleAndPosition()
    {
        if(Input.GetMouseButton(0))
        {
    //        Debug.Log(Input.mousePosition);
            Vector2 delta2 = (Vector2)(Input.mousePosition) - lastmousePos;
            ScaleTargetByPinch(Vector2.zero, Vector2.zero, Input.mousePosition, delta2);
            lastmousePos = Input.mousePosition;
        }
        else
        {
            lastmousePos = Input.mousePosition;
        }
    }
  

    void Update()
    {
    

        if (GetObjectTransform() == null)
            return;

#if UNITY_EDITOR
 //       TestScaleAndPosition();
#endif
        if (Input.touchCount == 0)
        {
            initialScale = GetObjectTransform().localScale;
        }


        // Check if there are two touches (fingers)
        if (Input.touchCount == 2)
        {
            // Get the first and second touches
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            ScaleTargetByPinch(touch1.position, touch1.deltaPosition, touch2.position, touch2.deltaPosition);
            initialScale = GetObjectTransform().localScale;
        }
    }


    public void ScaleTargetByPinch(Vector2 touch1,Vector2 delta1, Vector2 touch2, Vector2 delta2)
    {
      
        // Calculate current and previous positions of touches
        Vector2 touch1PrevPos = touch1 - delta1;
        Vector2 touch2PrevPos = touch2 - delta2;

        // Calculate previous distance and current distance between touch positions
        float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
        float touchDeltaMag = (touch1 - touch2).magnitude;

        // Calculate the difference in distances to determine pinch amount
        float deltaMagnitudeDiff =  touchDeltaMag - prevTouchDeltaMag;

        // Check if pinch distance is large enough
        if (Mathf.Abs(deltaMagnitudeDiff) > minPinchDistance)
        {
            // Scale the object accordingly
            Vector3 scale = GetObjectTransform().localScale + new Vector3(deltaMagnitudeDiff, deltaMagnitudeDiff, deltaMagnitudeDiff)*0.0001f;
            scale = Vector3.Max(scale, Vector3.zero); // Ensure scale doesn't become negative
            Debug.Log(scale.x );
            if (scale.x > minSize && scale.x < maxSize)
            {
                Debug.Log(scale.x +" "+ minSize +" "+maxSize) ;

                float resizex = scale.x - GetObjectTransform().localScale.x;
                GetObjectTransform().localScale = scale;

           //     GetObjectTransform().position = calcualteNewOrigin(GetObjectTransform().position, resizex, initialScale.x);
            }
        }


    }



    Vector3 calculateOffset()
    {
        baseObjectTransform = GetObjectTransform().Find("BottomOrigin");
        Vector3 offset = GetObjectTransform().position - baseObjectTransform.position;
        return offset;
    }

    Vector3 calcualteNewOrigin(Vector3 originalPos, float delX, float origSize)
    {
        Vector3 newOrigin = originalPos + (delX/origSize)*positionOffset + calculateOffset();
        return newOrigin;
    }
}
