using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    Camera cam;
    // Start is called before the first frame update
    private Camera GetCamera()
    {
        if (cam == null)
        {
            cam = GameObject.FindAnyObjectByType<Camera>();
        }
      //  Debug.Log(cam.gameObject.name);
        return cam;
    }

    
    // Update is called once per frame
    void Update()
    {
        var lookPos = GetCamera().transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100 );
       // transform.LookAt(GetCamera().transform, Vector3.up);
    }
}
