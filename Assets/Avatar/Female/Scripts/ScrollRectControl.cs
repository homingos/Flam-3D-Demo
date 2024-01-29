using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollRectControl : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TMP_InputField decelerationRateInput;
    public TMP_InputField scrollSensitivityInput;

    private void Start()
    {
        // Initialize input fields with current values
        decelerationRateInput.text = scrollRect.decelerationRate.ToString();
        scrollSensitivityInput.text = scrollRect.scrollSensitivity.ToString();
    }

    public void ChangeDecelerationRate()
    {
        if (float.TryParse(decelerationRateInput.text, out float newDecelerationRate))
        {
            scrollRect.decelerationRate = newDecelerationRate;
        }
        else
        {
            // Handle invalid input
            Debug.LogError("Invalid input for Deceleration Rate");
        }
    }

    public void ChangeScrollSensitivity()
    {
        if (float.TryParse(scrollSensitivityInput.text, out float newScrollSensitivity))
        {
            scrollRect.scrollSensitivity = newScrollSensitivity;
        }
        else
        {
            // Handle invalid input
            Debug.LogError("Invalid input for Scroll Sensitivity");
        }
    }
}
