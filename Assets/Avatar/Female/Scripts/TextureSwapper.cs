using UnityEngine;
using UnityEngine.UI;

public class TextureSwapper : MonoBehaviour
{
    public Texture2D assignedTexture1;
    public string textureProperty1Name = "_MainTex";
    public Texture2D assignedTexture2;
    public string textureProperty2Name = "_SecondTex";

    public Material material;

    public float floatValue1 = 0.0f;
    public string floatProperty1Name = "_FloatValue1";
    public float floatValue2 = 1.0f;
    public string floatProperty2Name = "_FloatValue2";
   
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
            if (assignedTexture1 != null)
            {
                material.SetTexture(textureProperty1Name, assignedTexture1);
            }

            if (assignedTexture2 != null)
            {
                material.SetTexture(textureProperty2Name, assignedTexture2);
            }

            material.SetFloat(floatProperty1Name, floatValue1);
            material.SetFloat(floatProperty2Name, floatValue2);
        }
    }
}
