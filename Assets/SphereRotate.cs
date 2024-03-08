using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotate : MonoBehaviour
{
    public float startAngle = 360.0f;
    public float angleTuchandX;     //sets in update.

    public Vector2 startPos;
    public Vector3 direction, theSpeed, avgSpeed;

    bool isDragging, stopSlowly;
    float lerpSpeed = 0.5f;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (mousePos.y > Screen.height / 4 && mousePos.y < (Screen.height * 0.75f))
                return;
            startPos = Input.mousePosition; ;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (mousePos.y > Screen.height / 4 && mousePos.y < (Screen.height * 0.75f))
                return;
            isDragging = false;
            stopSlowly = true;
        }

#if UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
           
            Debug.Log("here");


            Vector2 mousePos = Input.mousePosition;
            if (mousePos.y > Screen.height / 4 && mousePos.y < (Screen.height * 0.75f))
              return;
            

            direction = (mousePos) - (startPos);
            isDragging = true;
                                

                if (isDragging)
                {
                    // Something that uses the chosen direction...
                    theSpeed = new Vector3(direction.y,- direction.x, 0.0F);
                    avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
                    Debug.Log("moving " + avgSpeed);
                    transform.eulerAngles = (avgSpeed);
                }
                else
                {
                    if (stopSlowly)
                    {
                        Debug.Log("TESTOUTPUT");
                        theSpeed = avgSpeed;
                        stopSlowly = false;
                    }
                    float i = Time.deltaTime * lerpSpeed;
                    theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);

                    Debug.Log("moved " + avgSpeed);
                    transform.eulerAngles = (avgSpeed);
                }
            
        }
#endif

        if (Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);
            if (touch.position.y > Screen.height / 4 && touch.position.y < (Screen.height * 0.75f))
                return;
            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = (touch.position) - (startPos);
                    isDragging = true;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    isDragging = false;
                    stopSlowly = true;
                    Debug.Log("end");
                    break;
            }
        }
        
            if (isDragging)
            {
                // Something that uses the chosen direction...
                theSpeed = new Vector3(direction.y, -direction.x, 0.0F);
            avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
                Debug.Log("moving " + avgSpeed);
                transform.eulerAngles   = (avgSpeed);
            }
            else
            {
                if (stopSlowly)
                {
                    Debug.Log("TESTOUTPUT");
                    theSpeed = avgSpeed;
                    stopSlowly = false;
                }
                float i = Time.deltaTime * lerpSpeed;
                theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);

                Debug.Log("moved " + avgSpeed);
                transform.eulerAngles = (avgSpeed);
            }
        


        return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch t = Input.GetTouch(0);

            if (t.position.y > Screen.height / 4 && t.position.y < (Screen.height * 0.75f))
                return;

            Debug.Log("rotating start");
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 dir = mp - (Vector2)transform.position;

            angleTuchandX = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            startAngle = 360 + angleTuchandX;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch t = Input.GetTouch(0);

            if (t.position.y > Screen.height / 4 && t.position.y < (Screen.height * 0.75f))
                return;
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 dir = mp - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - startAngle;
            Debug.Log("rotating on going  "+angle);
            if (angle < transform.eulerAngles.z)
            {
                Debug.Log("rotating on going1  " + angle);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }    }
