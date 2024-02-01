using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;

public class RecordingManager : MonoBehaviour
{
    bool isRecording = false;
    ScreenRecorder screenRecorder;
    public Camera camera;
    public AudioSource audioSource;

    // to handle the recoding
    public void handleRecording()
    {
        if (!isRecording)
        {
            startRecording();
        }
        else {
            stopRecording();
        }

    }

    // Start recording
    void startRecording()
    {

        Debug.Log("Start recording");

        isRecording = true;

        screenRecorder = new ScreenRecorder(camera, audioSource, false, width: 480, height: 480 * Screen.height / Screen.width);
        screenRecorder.StartRecording();
    }

    // Stop recording
    async void stopRecording(string folderName = "MyMedia")
    {
        Debug.Log("Stop recording");

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
