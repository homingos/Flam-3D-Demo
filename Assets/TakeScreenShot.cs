using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TakeScreenShot : MonoBehaviour
{

  public RawImage rawImage;
  public GameObject screenShotWindow;
  Texture t;
  private string currentPhotoName;

    public GameObject canvas;
  public void ProcessScreenShot()
  {

        StartCoroutine("ScreenShotProcess");
  }



    public void OnTogglePlanes(bool flag)
    {
        foreach (GameObject plane in GameObject.FindGameObjectsWithTag("arplane"))
        {
            Renderer r = plane.GetComponent<Renderer>();
            ARPlaneMeshVisualizer t = plane.GetComponent<ARPlaneMeshVisualizer>();
            r.enabled = flag;
            t.enabled = flag;
        }
    }
    public IEnumerator ScreenShotProcess()
    {
        currentPhotoName = ExperienceManager.activeExpName + DateTime.UtcNow + ".png";
        canvas.SetActive(false);
        OnTogglePlanes(false);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        var tex = ScreenCapture.CaptureScreenshotAsTexture();
        ScreenCapture.CaptureScreenshot(currentPhotoName);
        yield return new WaitForEndOfFrame();

        OnTogglePlanes(true);
        canvas.SetActive(true);
        rawImage.texture = tex;
        t = tex;
        screenShotWindow.SetActive(true);
    }

    public void OnShare()
    {
       var ns = new NativeShare();
      ns.AddFile(Path.Combine(Application.persistentDataPath,currentPhotoName));
        ns.Share();
    }

    public void OnCancel()
    {
        screenShotWindow.SetActive(false);
    }
}
