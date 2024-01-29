using UnityEngine;
using UnityEngine.UI;

public class FootwearManager : MonoBehaviour
{
    public GameObject[] footwearOptions;
    public Button[] footwearButtons;

    private GameObject activeFootwear;

    private void Start()
    {
        InitializeButtons();
        DeactivateAllFootwear();

        // Activate the first footwear option by default
        if (footwearOptions.Length > 0)
        {
            activeFootwear = footwearOptions[0];
            activeFootwear.SetActive(true);
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < footwearButtons.Length; i++)
        {
            int index = i; // To capture the correct index in the delegate
            footwearButtons[i].onClick.AddListener(() => SelectFootwear(index));
        }
    }

    public void SelectFootwear(int footwearIndex)
    {
        DeactivateAllFootwear();

        if (footwearIndex >= 0 && footwearIndex < footwearOptions.Length)
        {
            activeFootwear = footwearOptions[footwearIndex];
            activeFootwear.SetActive(true);
        }
    }

    private void DeactivateAllFootwear()
    {
        foreach (var footwear in footwearOptions)
        {
            footwear.SetActive(false);
        }
    }
}
