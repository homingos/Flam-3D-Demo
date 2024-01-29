using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class BoneScaleOverride : MonoBehaviour
{
    [System.Serializable]
    public class BoneScaleData
    {
        public Transform bone;
        public Vector3 scaleOverride = Vector3.one;
    }

    public BoneScaleData[] boneScaleDataArray;

    public void ApplyBoneScaleOverrides()
    {
        foreach (BoneScaleData boneScaleData in boneScaleDataArray)
        {
            boneScaleData.bone.localScale = boneScaleData.scaleOverride;
        }
    }

    // Attach this method to your UI button's OnClick event
    public void OnApplyButtonClicked()
    {
        ApplyBoneScaleOverrides();
    }
}
