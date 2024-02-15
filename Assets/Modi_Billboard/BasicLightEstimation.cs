﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// A component that can be used to access the most recently received basic light estimation information
    /// for the physical environment as observed by an AR device.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class BasicLightEstimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The ARCameraManager which will produce frame events containing light estimation information.")]
        ARCameraManager m_CameraManager;

        public float sensitvity;


        public void Increase()
        {
            sensitvity += 0.1f;
       //     m_Light.intensity +=  sensitvity;
        }

        public void Decrease()
        {
            sensitvity -= 0.1f;
          //  m_Light.intensity +=  sensitvity;
        }

        /// <summary>
        /// Get or set the <c>ARCameraManager</c>.
        /// </summary>
        public ARCameraManager cameraManager
        {
            get { return m_CameraManager; }
            set
            {
                if (m_CameraManager == value)
                    return;

                if (m_CameraManager != null)
                    m_CameraManager.frameReceived -= FrameChanged;

                m_CameraManager = value;

                if (m_CameraManager != null & enabled)
                    m_CameraManager.frameReceived += FrameChanged;
            }
        }
        public Color? mainLightColor { get; private set; }

        /// <summary>
        /// The estimated intensity in lumens of main light of the physical environment, if available.
        /// </summary>
        public float? mainLightIntensityLumens { get; private set; }

        /// <summary>
        /// The estimated spherical harmonics coefficients of the physical environment, if available.
        /// </summary>
        public SphericalHarmonicsL2? sphericalHarmonics { get; private set; }
        /// <summary>
        /// The estimated brightness of the physical environment, if available.
        /// </summary>
        public float? brightness { get; private set; }

        /// <summary>
        /// The estimated color temperature of the physical environment, if available.
        /// </summary>
        public float? colorTemperature { get; private set; }

        /// <summary>
        /// The estimated color correction value of the physical environment, if available.
        /// </summary>
        public Color? colorCorrection { get; private set; }
        public Vector3? mainLightDirection { get; private set; }

        void Awake ()
        {
            m_Light = GetComponent<Light>();
        }

        void OnEnable()
        {
            if (m_CameraManager != null)
                m_CameraManager.frameReceived += FrameChanged;
        }

        void OnDisable()
        {
            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;
        }
        
        void FrameChanged(ARCameraFrameEventArgs args)
        {
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                brightness = args.lightEstimation.averageBrightness.Value;
              //  m_Light.intensity = brightness.Value;
                m_Light.intensity = brightness.Value + sensitvity* brightness.Value;
            }
            else
            {
                brightness = null;
            }

            if (args.lightEstimation.averageColorTemperature.HasValue)
            {
                colorTemperature = args.lightEstimation.averageColorTemperature.Value;
                m_Light.colorTemperature = colorTemperature.Value;
            }
            else
            {
                colorTemperature = null;
            }

            if (args.lightEstimation.colorCorrection.HasValue)
            {
                colorCorrection = args.lightEstimation.colorCorrection.Value;
                m_Light.color = colorCorrection.Value;
            }
            else
            {
                colorCorrection = null;
            }

            if (args.lightEstimation.mainLightDirection.HasValue)
            {
                mainLightDirection = args.lightEstimation.mainLightDirection;
                m_Light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
            
            }
         

            if (args.lightEstimation.mainLightColor.HasValue)
            {
                mainLightColor = args.lightEstimation.mainLightColor;
                m_Light.color = mainLightColor.Value;
            }
            else
            {
                mainLightColor = null;
            }

            if (args.lightEstimation.mainLightIntensityLumens.HasValue)
            {
                mainLightIntensityLumens = args.lightEstimation.mainLightIntensityLumens;
                m_Light.intensity = args.lightEstimation.averageMainLightBrightness.Value  +sensitvity * args.lightEstimation.averageMainLightBrightness.Value; ;
            }
            else
            {
                mainLightIntensityLumens = null;
            }

            if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
            {
                sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
                RenderSettings.ambientMode = AmbientMode.Skybox;
                RenderSettings.ambientProbe = sphericalHarmonics.Value;
            }
            else
            {
                sphericalHarmonics = null;
            }
        }

        Light m_Light;
    }
}
