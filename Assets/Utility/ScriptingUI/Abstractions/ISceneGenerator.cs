using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;



public abstract class ISceneGenerator:MonoBehaviour{
    public static ISceneGenerator Instance=null;
    [HideInInspector]public Camera camera;
    protected UnityEngine.EventSystems.EventSystem eventSystem;
    public void Awake()
    {
        // if(Instance ==null)
        {
            // if (Instance == null)
             {Instance = gameObject.GetComponent<ISceneGenerator>();}
        }
    }

    public static void LoadScene<T>()where T:ISceneGenerator{
        GameObject go=null;
        
        if(Instance ==null) {Instance = GameObject.FindObjectOfType<ISceneGenerator>();
        }
            if(Instance!=null)
           { go =Instance.gameObject;
            Instance.CleanScene();
            go = Instance.gameObject;}
        if(go == null){
        go = new GameObject("SceneGeneratorHolder");
        }
       try{Destroy(Instance);}catch{}
        Instance= go.AddComponent<T>();   
        Debug.Log("instance null displaying 2 ");
        Debug.Log("instance null 2 " + ISceneGenerator.Instance); 

    }

    public virtual void LoadResources(){

    }
    public virtual void Start()
    {
      
        GenerateScene();
        Debug.Log("instance null displaying");
        Debug.Log("instance null " + ISceneGenerator.Instance);
    }

    public  async virtual void GenerateSceneUI(){
        
        Debug.Log("Creating UI");
        CleanSceneUI();
        CreateCamera();
        var go = new GameObject();
        go.name ="EventSystem";
        eventSystem=go.AddComponent<UnityEngine.EventSystems.EventSystem>();
        go.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        await Task.Yield();
    }
    public async virtual void GenerateScene()
    {
        CleanScene();
        LoadResources();
        CreateCamera();
        GenerateSceneUI();
        await Task.Yield();
    }

    public void CreateCamera(){
        camera = GameObject.FindObjectOfType<Camera>();
        if(camera ==null){
            Debug.Log("camera " + camera);
            var cam = new GameObject();
            camera =cam.AddComponent<Camera>();
            cam.tag ="MainCamera";
            cam.transform.position = new Vector3(0,1,3);
            cam.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            cam.name ="camera";
            cam.AddComponent<AudioListener>();
            camera.clearFlags=CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;
        }
    }
    public void CleanScene(){
        CleanSceneUI();
        CleanSceneObjects(); 
    }

    public void CleanSceneObjects()
    {
         GameObject[] topLevelGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
         foreach (GameObject o in topLevelGameObjects) { 
            if(o.GetComponent<ISceneGenerator>() ==null)
            
            if(!Application.isPlaying){
            if(o.tag!="dnd") DestroyImmediate(o.gameObject);}
            else
            {
                 if(o.tag!="dnd")Destroy(o);
            }
            }
    }

    public void CleanSceneUI()
    {
         foreach (Canvas o in Object.FindObjectsOfType<Canvas>(true)){
            if(!Application.isPlaying){
            if(o.tag!="dnd")DestroyImmediate(o.gameObject);}
            else
            {
                 if(o.tag!="dnd")Destroy(o.gameObject);
            }
            }
        foreach (EventSystem o in Object.FindObjectsOfType<EventSystem>(true)){
            if(!Application.isPlaying){
            if(o.tag!="dnd")DestroyImmediate(o.gameObject);}
            else
            {
                 if(o.tag!="dnd")Destroy(o.gameObject);
            }
            }
    }
}   