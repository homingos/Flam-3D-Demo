using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EffectManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Renderer videoCanvas;
    public float videoDelay = 0;

    private void OnEnable()
    {
        videoPlayer.targetMaterialRenderer = videoCanvas;
        StartCoroutine(Enable());
    }

    // Start is called before the first frame update
    IEnumerator Enable()
    {
        yield return new WaitForSeconds(videoDelay);
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
