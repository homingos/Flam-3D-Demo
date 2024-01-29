using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlendShapeChanger : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public BlendShapeSetting[] blendShapeSettings;

    [System.Serializable]
    public class BlendShapeSetting
    {
        public string blendShapeName;
        [Range(0, 100)] public float blendShapeValue = 0f;
    }

    public void ChangeBlendShapes()
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer not assigned!");
            return;
        }

        foreach (BlendShapeSetting setting in blendShapeSettings)
        {
            // Make sure the blend shape exists in the mesh
            if (!BlendShapeExists(setting.blendShapeName))
            {
                Debug.LogError("Blend shape not found: " + setting.blendShapeName);
                continue;
            }

            // Set the blend shape value
            skinnedMeshRenderer.SetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(setting.blendShapeName), setting.blendShapeValue);
        }
    }

    private bool BlendShapeExists(string blendShapeName)
    {
        int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
        return blendShapeIndex != -1;
    }
}
