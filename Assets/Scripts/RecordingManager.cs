using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARFoundation;

public class RecordingManager : MonoBehaviour
{
    bool isRecording = false;
    ScreenRecorder screenRecorder;
    public Camera camera;
    public AudioSource audioSource;
   // public TMP_Text recordButtonText;

  //  public GameObject recordingBtn;

    public ExperienceManager experienceManager; 

    public void Start()
    {
        Init();
    }

    public void Init()
    {

#if UNITY_EDITOR
        camera = GameObject.Find("Camera").GetComponent<Camera>();
#else
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
#endif
        ARPlacementInteractable arPlacementInteractable;

        arPlacementInteractable = GetComponent<ARPlacementInteractable>();

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

    // to handle the recoding
    public void handleRecording()
    {
        //try get the audio Source
        if (!isRecording)
        {
            OnTogglePlanes(false);


            startRecording();
        }
        else {

     
            stopRecording();

            OnTogglePlanes(true);
        }

    }

    bool isRecordingButtonVisible = false;

    public void Update()
    {
       if(isRecordingButtonVisible  != (experienceManager.currentGameObject!=null))
        {
            isRecordingButtonVisible = (experienceManager.currentGameObject != null);

            //update button
     //       recordingBtn.SetActive(isRecordingButtonVisible);
        }
    }



    // Start recording
    void startRecording()
    { 
        audioSource = experienceManager.currentGameObject.GetComponentInChildren<AudioSource>();
      //  recordButtonText.SetText("Stop");
        isRecording = true;
        screenRecorder = new ScreenRecorder(camera, audioSource, false, width: 480, height: 480 * Screen.height / Screen.width);
        screenRecorder.StartRecording();
    }

    // Stop recording
    async void stopRecording(string folderName = "MyMedia")
    {
        Debug.Log("Stop recording");
     //   recordButtonText.SetText("Start");

        isRecording = false;

        screenRecorder.StopAudio();
        screenRecorder.StopVideo();

        var path = await screenRecorder.StopRecording();
        saveVideoToGallery(path, folderName);

    }

    void saveVideoToGallery(string path, string folderName)
    {
        Guid uuid = Guid.NewGuid();
        string extension = Path.GetExtension(path);
        var name = uuid.ToString().Replace("-", "");
        Debug.Log(path +"Calling Save Video" +path);
        NativeGallery.SaveVideoToGallery(path, folderName, name + extension);
        Debug.Log("saved at " + path);
    }

   
}
