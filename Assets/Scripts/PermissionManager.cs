using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;

#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using UnityEngine.iOS;
#endif

public class PermissionManager : MonoBehaviour
{
    //   public GameObject camerapermission;
    //  public GameObject storagepermission;
    //  public GameObject avatarpage;
    //  public GameObject TNCpanel;
    bool isTNCaccepted;
    public static PermissionManager Instance;

    private bool CameraDeclined = false;

    [HideInInspector] public string psid;

    public void disableEverything()
    {

    }
    public void enableEverything()
    {

    }
    private void Awake()
    {
        Instance = this;
        // SaveSystem.LoadUserData();
    }

    void NextScene()
    {
        //StateManager.StateFuncions.setStateMain();
    }
    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}
    private void Start()
    {
        //     camerapermission.SetActive(true);


        //StartPermissions();
        StartCoroutine(StartPermissions());

        Debug.Log("Persistent Data Path = " + Application.persistentDataPath);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //       StartCoroutine(StartPermissions());
    }

    public IEnumerator StartPermissions()
    {
#if UNITY_ANDROID
        yield return null;
        AndroidRuntimePermissions.Permission result;
        AndroidRuntimePermissions.Permission cameraresult = AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA");

        if (cameraresult != AndroidRuntimePermissions.Permission.Granted)
        {
            result = AndroidRuntimePermissions.RequestPermission("android.permission.CAMERA");
            cameraresult = AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA");

        }

        if (cameraresult != AndroidRuntimePermissions.Permission.Granted)
        {
            AskForCameraPermission();
        }
        else
        {
            PlayerPrefs.SetInt("Camera", 1);
            //        camerapermission.SetActive(true);
            StartCoroutine(AskForStoragePermission());
        }

#endif
        yield break;
       
    }

    public IEnumerator AskForStoragePermission()
    {
        //       camerapermission.SetActive(false);
        //       storagepermission.SetActive(true);

        yield return null;
        //AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
        AndroidRuntimePermissions.Permission _result = AndroidRuntimePermissions.CheckPermission("android.permission.READ_EXTERNAL_STORAGE");

        //storagepermission.SetActive(false);

        if (_result != AndroidRuntimePermissions.Permission.Granted)
        {
            AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
            _result = AndroidRuntimePermissions.CheckPermission("android.permission.READ_EXTERNAL_STORAGE");
        }

        if (_result != AndroidRuntimePermissions.Permission.Granted)
        {

        }
        NextScene();
    }


    public void AskForCameraPermission()
    {
#if UNITY_ANDROID
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access CAMERA!");
            PlayerPrefs.SetInt("Camera", 1);
            // camerapermission.SetActive(false);
            // storagepermission.SetActive(true); //uncomment this whenever storage permissions required
            //avatarpage.SetActive(true);
            StartCoroutine(AskForStoragePermission());
        }

        else
        {
            //         camerapermission.SetActive(true);
            Debug.Log("cmra Permission state: " + result);
            CameraDeclined = true;
        }

#endif
    }



    private void OnApplicationPause(bool pause)
    {
#if UNITY_EDITOR
        return;
#endif
        if (!pause && CameraDeclined)
        {
            StartCoroutine(StartPermissions());
            //StartPermissions();
            //AndroidRuntimePermissions.Permission cameraresult = AndroidRuntimePermissions.CheckPermission("android.permission.CAMERA");
            //if (cameraresult != AndroidRuntimePermissions.Permission.Granted)
            //{

            //    camerapermission.SetActive(true);
            //}
            //else
            //{
            //    camerapermission.SetActive(false);
            //    AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");


            //}
        }
    }

    public class AppVersionData
    {
        public AppVersions version { get; set; }
    }

    public class DeviceAppVersionData
    {
        public string latestVersion { get; set; }
        public string lastSupportedVersion { get; set; }
    }

    public class AppVersionResponse
    {
        public AppVersionData data { get; set; }
    }

    public class AppVersions
    {
        public DeviceAppVersionData IOS { get; set; }
        public DeviceAppVersionData ANDROID { get; set; }
    }
}