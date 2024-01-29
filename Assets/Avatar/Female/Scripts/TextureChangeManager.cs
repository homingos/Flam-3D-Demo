using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureChangeManager : MonoBehaviour
{
    public Material targetMaterial; // Reference to the material using the shader graph
    public Texture2D[] textureOptions; // Array of texture options

    public void OnButtonPress(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < textureOptions.Length)
        {
            targetMaterial.SetTexture("_TexturePropertyName", textureOptions[buttonIndex]);
        }
    }
}
