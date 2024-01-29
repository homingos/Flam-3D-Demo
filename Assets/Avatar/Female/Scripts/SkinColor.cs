using UnityEngine;
using UnityEngine.UI;

public class SkinColor : MonoBehaviour
{
    public Material material; // Assign your Shader Graph material here
    public string[] texturePropertyNames = new string[5];
    public Texture2D[] assignedTextures = new Texture2D[5];
    public string[] colorPropertyNames = new string[2];
    public Color[] assignedColors = new Color[2];
    public string floatPropertyName = "_SkinTone";
    public float skinToneValue = 0.5f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (material != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < assignedTextures.Length && assignedTextures[i] != null)
                {
                    material.SetTexture(texturePropertyNames[i], assignedTextures[i]);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (i < assignedColors.Length)
                {
                    material.SetColor(colorPropertyNames[i], assignedColors[i]);
                }
            }

            material.SetFloat(floatPropertyName, skinToneValue);
        }
    }
}
