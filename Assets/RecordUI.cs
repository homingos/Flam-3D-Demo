using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{

    public Image outerCircle;
    public Image InnerCircle;


    public float senstivity = 2f;
    public bool isPointerDown = false;

    public TakeScreenShot takeScreenShot;
    public RecordingManager recordingManager;

    public void OnPointerDown()
    {
        isPointerDown = true;
        if (recordingStarted)
        {
            //stop recording
            StopRecording();
        }
        else
        {
            StartCoroutine("OnPressStart");
        }

       
    }

    public void OnPointerUp()
    {
        
        isPointerDown = false;
    }


    public void StopRecording()
    {
        InnerCircle.color = Color.white;
        InnerCircle.transform.localScale = Vector3.one;
        recordingStarted = false;
        Debug.Log("recording stopped");
        recordingManager.handleRecording();
    }

    public void StartRecording()
    {
        Debug.Log("start recording");
        Handheld.Vibrate();
        InnerCircle.transform.localScale = Vector3.one;

        recordingManager.handleRecording();
    }

    bool recordingStarted;
    IEnumerator OnPressStart()
    {
        float time = Time.time;
        StartCoroutine(ButtonScale());
        while (isPointerDown)
        {
            Debug.Log("is pointer down while loop");
            //do icon zoom
            if (Time.time > (time + 0.8f) && !recordingStarted)
            {
                InnerCircle.color = Color.red;
                //Start Recording
                StartRecording();
                recordingStarted = true;
            }
            yield return new WaitForEndOfFrame();
        }


        if(!recordingStarted)
        {
            Debug.Log("take screenshot");
            //take screenshot
            takeScreenShot.ProcessScreenShot();
        }
        yield break;
    }

    IEnumerator ButtonScale()
    {  
            Vector3 scaleDown = InnerCircle.transform.localScale *0.8f;
            while (InnerCircle.transform.localScale.x > scaleDown.x)
            {
                if(!isPointerDown || recordingStarted)
                {
                    InnerCircle.transform.localScale = Vector3.one;
                   yield break;
                }
                InnerCircle.transform.localScale = Vector3.Lerp(InnerCircle.transform.localScale, scaleDown, Time.deltaTime* senstivity);
            yield return new WaitForEndOfFrame();
            }      
    }
}
