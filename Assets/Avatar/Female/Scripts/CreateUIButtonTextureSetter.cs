using UnityEngine;
using UnityEngine.UI;

public class CreateUIButtonTextureSetter : MonoBehaviour
{
    public Texture2D assignedTexture1; // Assign the first texture for this button in the Inspector
    public Material material1; // Assign the first Shader Graph material here
    public string texturePropertyName1 = "_BaseMap"; // Texture property name for the first material

    public Texture2D assignedTexture2; // Assign the second texture for this button in the Inspector
    public Material material2; // Assign the second Shader Graph material here
    public string texturePropertyName2 = "_BaseMap"; // Texture property name for the second material

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (button.interactable)
        {
            if (material1 != null && assignedTexture1 != null)
            {
                material1.SetTexture(texturePropertyName1, assignedTexture1);
            }
            if (material2 != null && assignedTexture2 != null)
            {
                material2.SetTexture(texturePropertyName2, assignedTexture2);
            }
        }
    }
}
