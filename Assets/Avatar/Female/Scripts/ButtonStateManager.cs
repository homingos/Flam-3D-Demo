using UnityEngine;
using UnityEngine.UI;

public class ButtonStateManager : MonoBehaviour
{
    public Button[] buttons; // An array to hold the UI buttons
    public Sprite[] selectedSprites; // An array to hold the selected sprites for each button
    public Sprite[] defaultSprites; // An array to hold the default sprites for each button

    private Button selectedButton; // The currently selected button

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Store the current index for the closure
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Set the first button as the initially selected button
        if (buttons.Length > 0)
        {
            selectedButton = buttons[0];
            selectedButton.image.sprite = selectedSprites[0];
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        // If a button is already selected, reset its sprite to default
        if (selectedButton != null)
        {
            int selectedIndex = System.Array.IndexOf(buttons, selectedButton);
            selectedButton.image.sprite = defaultSprites[selectedIndex];
        }

        // Set the newly clicked button as the selected button
        selectedButton = buttons[buttonIndex];
        selectedButton.image.sprite = selectedSprites[buttonIndex];
    }
}
