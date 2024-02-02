using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders;
using NatSuite.Recorders.Inputs;
using System.Threading.Tasks;


public class ScreenRecorder
{
    private MP4Recorder recorder;
    int frameRate = 30;
    int max_frames;
    int frame = 0;
    CameraInput cameraInput;
    AudioInput audioInput;
    IClock Clock;
    AudioSource listener;
    bool autoTick = true;
    bool mute = true;

    bool realtime;

    public string path;
    public bool videoStop = false;
    public bool audioStop = false;

    public bool isRecording = false;

    Camera camera;

    int videoWidth = Screen.width;
    int videoHeight = Screen.height;

    public ScreenRecorder(Camera camera, AudioSource listener, bool mute = true, int width = 480, int height = 854)
    {
        this.camera = camera;
        this.listener = listener;
        this.videoHeight = height;
        this.videoWidth = width;
        this.realtime = true;
        createRecorder();
    }

    void createRecorder()
    {
        //if (videoHeight % 2 != 0) videoHeight--;
        //if (videoWidth % 2 != 0) videoWidth--;

        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, AudioSettings.outputSampleRate, (int)AudioSettings.speakerMode, audioBitRate: 64_000, keyframeInterval: 20);
    }

    public bool StartRecording(bool autoRec = true)
    {
        var sampleRate = AudioSettings.outputSampleRate;
        var channelCount = (int)AudioSettings.speakerMode;

        Clock = new RealtimeClock();
        

        videoStop = false;
        audioStop = false;

        cameraInput = new CameraInput(recorder, Clock, camera);
        if(listener !=null) audioInput = new AudioInput(recorder, Clock, listener, mute);

        autoTick = autoRec;
        isRecording = true;

        return true;
    }

    public async Task<string> StopRecording()
    {
        isRecording = false;
        videoStop = true;
        audioStop = true;
        try
        {
            StopVideo();
            if (mute && listener != null) listener.mute = true;
            audioInput?.Dispose();
            path = await recorder.FinishWriting();
        }
        catch
        {
            Debug.Log("cant save a video");
            return path = "";
        }
        return path;
    }

    public void StopAudio()
    {
        audioStop = true;
        if (listener != null)
            audioInput?.Dispose();
    }
    public void StopVideo()
    {
        videoStop = true;
        try { cameraInput?.Dispose(); } catch { }
    }
}
