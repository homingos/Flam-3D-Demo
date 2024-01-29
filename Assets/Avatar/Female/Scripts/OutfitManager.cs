using UnityEngine;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    public GameObject[] upperwearOptions;
    public Button[] upperwearButtons;
    public GameObject[] bottomwearOptions;
    public Button[] bottomwearButtons;
    public GameObject[] outfitOptions;
    public Button[] outfitButtons;

    private GameObject activeUpperwear;
    private GameObject activeBottomwear;
    private GameObject activeOutfit;

    private void Start()
    {
        InitializeButtons();
        DeactivateAllOptions();

        // Activate the first upperwear and bottomwear options by default
        if (upperwearOptions.Length > 0)
        {
            activeUpperwear = upperwearOptions[0];
            activeUpperwear.SetActive(true);
        }

        if (bottomwearOptions.Length > 0)
        {
            activeBottomwear = bottomwearOptions[0];
            activeBottomwear.SetActive(true);
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < upperwearButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the delegate
            upperwearButtons[i].onClick.AddListener(() => SelectUpperwear(index));
        }

        for (int i = 0; i < bottomwearButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the delegate
            bottomwearButtons[i].onClick.AddListener(() => SelectBottomwear(index));
        }

        for (int i = 0; i < outfitButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the delegate
            outfitButtons[i].onClick.AddListener(() => SelectOutfit(index));
        }
    }

    public void SelectUpperwear(int upperwearIndex)
    {
        DeactivateOutfit();
        DeactivateAllOptions();

        if (upperwearIndex >= 0 && upperwearIndex < upperwearOptions.Length)
        {
            activeUpperwear = upperwearOptions[upperwearIndex];
            activeUpperwear.SetActive(true);

            if (activeBottomwear != null)
            {
                activeBottomwear.SetActive(true); // Keep the paired bottomwear active
            }
        }
    }

    public void SelectBottomwear(int bottomwearIndex)
    {
        DeactivateOutfit();
        DeactivateAllOptions();

        if (bottomwearIndex >= 0 && bottomwearIndex < bottomwearOptions.Length)
        {
            activeBottomwear = bottomwearOptions[bottomwearIndex];
            activeBottomwear.SetActive(true);

            if (activeUpperwear != null)
            {
                activeUpperwear.SetActive(true); // Keep the paired upperwear active
            }
        }
    }

    public void SelectOutfit(int outfitIndex)
    {
        DeactivateAllOptions();

        if (outfitIndex >= 0 && outfitIndex < outfitOptions.Length)
        {
            activeOutfit = outfitOptions[outfitIndex];
            activeOutfit.SetActive(true);
        }
    }

    private void DeactivateAllOptions()
    {
        foreach (var upperwear in upperwearOptions)
        {
            upperwear.SetActive(false);
        }

        foreach (var bottomwear in bottomwearOptions)
        {
            bottomwear.SetActive(false);
        }

        foreach (var outfit in outfitOptions)
        {
            outfit.SetActive(false);
        }
    }

    private void DeactivateOutfit()
    {
        if (activeOutfit != null)
        {
            activeOutfit.SetActive(false);
            activeOutfit = null;
        }
    }
}
