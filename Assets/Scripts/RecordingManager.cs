using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;

public class RecordingManager : MonoBehaviour
{
    bool isRecording = false;
    ScreenRecorder screenRecorder;
    public Camera camera;
    public AudioSource audioSource;
    public TMP_Text recordButtonText;

    public GameObject recordingBtn;

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


    }

    // to handle the recoding
    public void handleRecording()
    {
        //try get the audio Source
        if (!isRecording)
        {
            startRecording();
        }
        else {
            stopRecording();
        }

    }

    bool isRecordingButtonVisible = false;

    public void Update()
    {
       if(isRecordingButtonVisible  != (experienceManager.currentGameObject!=null))
        {
            isRecordingButtonVisible = (experienceManager.currentGameObject != null);

            //update button
            recordingBtn.SetActive(isRecordingButtonVisible);
        }
    }



    // Start recording
    void startRecording()
    {
        Debug.Log("ttt s-1");
       
        Debug.Log("ttt s0");
        

        audioSource = experienceManager.currentGameObject.GetComponentInChildren<AudioSource>();
        Debug.Log((audioSource == null) + "ttt s1");
        Debug.Log("Start recording");
        recordButtonText.SetText("Stop");
        Debug.Log("ttt s2");

        isRecording = true;

        screenRecorder = new ScreenRecorder(camera, audioSource, false, width: 480, height: 480 * Screen.height / Screen.width);
        Debug.Log("ttt s3");
        screenRecorder.StartRecording();
        Debug.Log("ttt s4");
    }

    // Stop recording
    async void stopRecording(string folderName = "MyMedia")
    {
        Debug.Log("Stop recording");
        recordButtonText.SetText("Start");

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
