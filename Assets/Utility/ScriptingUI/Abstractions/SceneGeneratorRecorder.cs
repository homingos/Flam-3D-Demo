using UnityEngine;
using System.Threading.Tasks;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders;
using NatSuite.Recorders.Inputs;


public class SceneGeneratorRecorder<T> where T: ISceneGenerator{

    #region  recordVariables
    //Record variables
    private  MP4Recorder recorder;
    int frameRate=30;
    int max_frames;
    int frame=0;
    CameraInput cameraInput;
    AudioInput audioInput;
    IClock Clock;
    AudioSource listener;
    bool autoTick=true;
    bool mute=true;
    
    bool realtime;
    #endregion recordVariables

    public string path;
    public bool videStop =false;
    public bool audioStop =false;

    public bool isRecording = false;

    T scene ;
    int videoWidth = Screen.width;
    int videoHeight = Screen.height;

    public  SceneGeneratorRecorder(T scene,AudioSource listne  ,int sample=0,int width=480,int height =854){
        this.scene =scene;
        this.realtime =false;
        listener=listne;
         this .videoHeight =height;
        this.videoWidth =width;
        createRecorder();

    }
    public  SceneGeneratorRecorder(T scene,AudioSource listne,bool mute =true,int width=480,int height = 854){
        this.scene =scene;
        this.mute=mute;
        listener=listne;
        this .videoHeight =height;
        this.videoWidth =width;
        this.realtime =true;
        createRecorder();
    }
    void createRecorder(){

        if(videoHeight%2!=0)videoHeight--;
        if(videoWidth%2!=0)videoWidth--;
        if(!realtime)
        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, AudioSettings.outputSampleRate,  (int)AudioSettings.speakerMode, audioBitRate: 64_000, keyframeInterval: 20);
        else
        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, AudioSettings.outputSampleRate,  (int)AudioSettings.speakerMode, audioBitRate: 64_000, keyframeInterval: 20);
        
    }

    public bool StartRecording(bool autoRec =true){
        /// Future data
        // var sampleRate =  AudioSettings.outputSampleRate ;
        // var channelCount =  (int)AudioSettings.speakerMode ;

        var sampleRate =  AudioSettings.outputSampleRate;
        var channelCount =  (int)AudioSettings.speakerMode;
        if(!realtime)
        {
            if(autoRec)
                Clock = new FixedIntervalClock(frameRate);
            else
                Clock = new FixedIntervalClock(frameRate,false);}
            else{
                Clock =new RealtimeClock();
        }
       
        videStop =false;
        audioStop =false;
       
        cameraInput = new CameraInput(recorder, Clock, scene.camera);
        if(!realtime)audioInput = new AudioInput(recorder, new RealtimeClock(), listener, mute ) ;
        else audioInput = new AudioInput(recorder, Clock, listener, mute) ;
        autoTick = autoRec;
        isRecording = true;
        return true;
    }

    public void RecordFrame(){
        if(realtime)return;
        {if(autoTick)
        return;
        else
        ((FixedIntervalClock)Clock).Tick();}
    }
    public async Task<string> StopRecording(){
        isRecording = false;
        videStop =true;
        audioStop =true;
        try {StopVideo();
        if(mute) listener.mute =true;
        audioInput?.Dispose();   
        path = await recorder.FinishWriting();}catch{
         Debug.Log("cant save a video");
         return path="";
        }
        return path;

        
    }

    public void StopAudio()
    {
        audioStop=true;
        audioInput?.Dispose();
    }
    public void StopVideo()
    {
        videStop=true;
        try{cameraInput?.Dispose();}catch{}
    }



}