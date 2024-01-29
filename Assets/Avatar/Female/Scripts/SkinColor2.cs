using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MaterialTextureData
{
    public string[] texturePropertyNames = new string[5];
    public Texture2D[] assignedTextures = new Texture2D[5];
}

[System.Serializable]
public class SkinData
{
    public string[] skinColorPropertyNames = new string[2]; // Skin color property names
    public Color[] skinColor = new Color[2]; // Skin color
    public string floatPropertyName = "_SkinTone"; // Float property name
    public float skinToneValue = 0.5f; // Skin tone value
}

public class SkinColor2 : MonoBehaviour
{
    public Material material1; // Assign your first Shader Graph material here
    public MaterialTextureData material1Textures = new MaterialTextureData();

    public Material material2; // Assign your second Shader Graph material here
    public MaterialTextureData material2Textures = new MaterialTextureData();

    public SkinData skinData = new SkinData(); // Skin color and float data

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SetMaterialTextures(material1, material1Textures);
        SetMaterialTextures(material2, material2Textures);

        SetSkinProperties(material1, skinData);
        SetSkinProperties(material2, skinData);
    }

    private void SetMaterialTextures(Material material, MaterialTextureData textureData)
    {
        if (material != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < textureData.assignedTextures.Length && textureData.assignedTextures[i] != null)
                {
                    material.SetTexture(textureData.texturePropertyNames[i], textureData.assignedTextures[i]);
                }
            }
        }
    }

    private void SetSkinProperties(Material material, SkinData skin)
    {
        if (material != null)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i < skin.skinColor.Length)
                {
                    material.SetColor(skin.skinColorPropertyNames[i], skin.skinColor[i]);
                }
            }

            material.SetFloat(skin.floatPropertyName, skin.skinToneValue);
        }
    }
}
