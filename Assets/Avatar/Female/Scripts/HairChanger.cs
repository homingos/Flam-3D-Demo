using UnityEngine;
using UnityEngine.UI;

public class HairChanger : MonoBehaviour
{
    public Texture2D assignedTexture1;
    public Texture2D assignedTexture2;
    public Material[] materials;
    public string textureProperty1Name = "_MainTex";
    public string textureProperty2Name = "_SecondTex";
    public string floatProperty1Name = "_FloatValue1";
    public string floatProperty2Name = "_FloatValue2";

    private Button button;
    public float floatValue1 = 0.0f; // Individual float value for each button
    public float floatValue2 = 1.0f; // Individual float value for each button

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        foreach (Material material in materials)
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

                material.SetFloat(floatProperty1Name, floatValue1); // Use the individual float value
                material.SetFloat(floatProperty2Name, floatValue2); // Use the individual float value
            }
        }
    }
}
