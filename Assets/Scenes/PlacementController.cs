using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementController : MonoBehaviour
{
    public ExperienceManager experienceManager;
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager arRaycastManager;

    Vector2 touchStartPosition;
    Vector3 objectStartPosition;
    Vector3 planeNormal;
    //   GameObject currentExperience;
    Vector3 initOffset;
    [SerializeField]
    float sensitivity = 0.1f;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    RecordUI GetRecordUI()
    {
        return GameObject.FindObjectOfType<RecordUI>();
    }

    void Start()
    {
        InitPlaneManager();
    }

    void InitPlaneManager()
    {
        if (arPlaneManager == null) arPlaneManager = FindObjectOfType<ARPlaneManager>();
        if (arRaycastManager == null) arRaycastManager = FindObjectOfType<ARRaycastManager>();

        arPlaneManager.planesChanged += OnPlanesChanged;

        Debug.Log("Found ARPlane manager: " + (arPlaneManager == null));
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        // Loop through added or updated planes
        foreach (var plane in eventArgs.added)
        {
            Debug.Log("Added Plane ID: " + plane.trackableId);
            planeNormal = plane.normal;
            // Access other properties of the plane as needed
        }

        // Loop through removed planes
        foreach (var plane in eventArgs.removed)
        {
            Debug.Log("Removed Plane ID: " + plane.trackableId);
        }
    }

    void MoveExperiencePosition(Vector3 touchPos)
    {
        if (arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the hit point on the detected AR plane
            Vector3 hitPoint = hits[0].pose.position;
            Debug.Log("Hit point on AR plane: " + hitPoint);
            //    SetExperience();
            CalculateOffset();
            GetExperience().transform.position = hitPoint + initOffset;
            Debug.Log("New offset posisoin: " + hitPoint + initOffset);

        }
    }

    void MoveExperienceMousePosition(Vector3 touchPos)
    {
        //   if (arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            Camera camera = GameObject.FindObjectOfType<Camera>();
            // Get the hit point on the detected AR plane
            Vector3 hitPoint = camera.ScreenToWorldPoint(touchPos);
            Debug.Log("Hit point on AR plane: " + hitPoint);

            //     SetExperience();
            CalculateOffset();
            //    currentExperience = SelectExperience();
            // if (currentExperience == null)
            //      return;
            GetExperience().transform.position = hitPoint + initOffset;
            Debug.Log("New offset posisoin: " + hitPoint + " " + initOffset);

        }
    }

    public void MoveAvtar(Vector2 Touchdelta)
    {
        Touchdelta = Touchdelta * 0.002f;
        Vector3 move3d = (Camera.main.transform.forward) * Touchdelta.y + (Camera.main.transform.right) * Touchdelta.x;
        GetExperience().transform.position = GetExperience().transform.position + new Vector3(move3d.x, 0, move3d.z);

        //       GetExperience().transform.position
        //         += (Camera.main.transform.forward)* Touchdelta.y;
        //    GetExperience().transform.position
        //      += (Camera.main.transform.right) * Touchdelta.x;

        //    GetExperience().transform.localPosition
        //       -= (GetExperience().transform.forward)* Touchdelta.y;
        //  GetExperience().transform.position
        //    -= (GetExperience().transform.right) * Touchdelta.x;
    }

    bool initTouch;
    float prevTime;
    void Update()
    {

        //  Debug.Log("sceenwid " + Screen.width);
        //   Debug.Log("sceenwid1 " + Screen.height);


        //       if (Input.GetMouseButton(0))
        {
            //  Touch touch = Input.GetTouch(0);

            //    if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast from the screen touch position
                //           MoveExperienceMousePosition(Input.mousePosition);
            }

        }
        if (Input.touchCount == 1 && GetExperience() != null && GetRecordUI() != null && !GetRecordUI().isPointerDown)
        {


            if (!initTouch)
            {
                initTouch = true;
                prevTime = Time.time;
            }

            //        if (Time.time > prevTime + 0.8f)
            {
                Touch touch = Input.GetTouch(0);

                //     Debug.Log("sceenwid1 " + touch.position);//    if (touch.phase == TouchPhase.Began)
                {
                    // Perform a raycast from the screen touch position
                    //      MoveExperiencePosition(touch.position);
                    if (touch.position.y > Screen.height / 4)
                        MoveAvtar(touch.deltaPosition);
                }
            }
        }

        else
        {
            initTouch = false;
        }
    }

    void CreateCube(Vector3 position)
    {
        // Instantiate a cube at the specified position
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.one / 30;
        cube.transform.position = position;
    }

    void CalculateOffset()
    {
        Transform baseGameobjectTransform = GetExperience().transform.Find("BottomOrigin");
        Debug.Log("Found baseObject: " + baseGameobjectTransform.position);
        initOffset = GetExperience().transform.position - baseGameobjectTransform.position;
    }

    //void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            // Define the raycast hits array
    //            RaycastHit hit;
    //            Ray ray = Camera.main.ScreenPointToRay(touch.position);

    //            // Raycast to check if the touch hits an existing plane
    //            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //            {
    //                // Get the hit position on the plane
    //                //Vector3 hitPosition = hit.point;
    //                //planeNormal = hit.normal;

    //                // Spawn or select a GameObject at the hit position
    //                selectedObject = SelectExperience();
    //                if (selectedObject != null)
    //                {
    //                    // Store the touch position and object's initial position
    //                    touchStartPosition = touch.position;
    //                    objectStartPosition = selectedObject.transform.position;
    //                }
    //            }
    //        }
    //        else if (touch.phase == TouchPhase.Moved && selectedObject != null)
    //        {
    //            // Calculate the touch delta
    //            Vector2 touchDelta = touch.position - touchStartPosition;

    //            // Project touch delta onto the plane's local axes
    //            Vector3 moveDelta = selectedObject.transform.up * touchDelta.y + selectedObject.transform.right * touchDelta.x;

    //            // Project the movement vector onto the plane's normal to maintain the object's height
    //            float dot = Vector3.Dot(moveDelta, planeNormal);
    //            Vector3 projectedDelta = moveDelta - dot * planeNormal;

    //            // Apply sensitivity to reduce the impact of touch movement
    //            projectedDelta *= sensitivity;

    //            // Move the object along the plane
    //            selectedObject.transform.position = objectStartPosition + projectedDelta;
    //        }
    //    }
    //}

    GameObject SelectExperience()
    {
        if (experienceManager == null)
        {
            experienceManager = FindObjectOfType<ExperienceManager>();
        }
        //      currentExperience = experienceManager.currentGameObject;


        CalculateOffset();
        return experienceManager.currentGameObject;
    }


    GameObject GetExperience()
    {
        if (experienceManager == null)
        {
            experienceManager = FindObjectOfType<ExperienceManager>();
        }
        //      currentExperience = experienceManager.currentGameObject;


        //    CalculateOffset();
        if (experienceManager != null)
            return experienceManager.currentGameObject;
        else return null;
    }




}
