using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BodyMenu : MonoBehaviour
{
    public GameObject[] subMenus;
    public Button[] menuButtons;
    public Color selectedColor;

    public GameObject[] subSubMenus; // Array of sub-sub category canvases
    public Button[] subSubMenuButtons; // Array of sub-sub category buttons
    public Color subSubMenuSelectedColor; // Color for selected sub-sub category

    private Sprite[] originalSprites;
    private Color[] originalColors;

    private Sprite[] originalSubSubMenuSprites; // Store original sprites of sub-sub menu buttons
    private Color[] originalSubSubMenuColors; // Store original colors of sub-sub menu buttons

    private void Start()
    {
        originalSprites = new Sprite[menuButtons.Length];
        originalColors = new Color[menuButtons.Length];

        originalSubSubMenuSprites = new Sprite[subSubMenuButtons.Length];
        originalSubSubMenuColors = new Color[subSubMenuButtons.Length];

        for (int i = 0; i < menuButtons.Length; i++)
        {
            int index = i;
            menuButtons[i].onClick.AddListener(() => OpenSubMenu(index));
            originalSprites[i] = menuButtons[i].image.sprite;
            originalColors[i] = menuButtons[i].image.color;
        }

        for (int i = 0; i < subSubMenuButtons.Length; i++)
        {
            int index = i;
            subSubMenuButtons[i].onClick.AddListener(() => OpenSubSubMenu(index));
            originalSubSubMenuSprites[i] = subSubMenuButtons[i].image.sprite;
            originalSubSubMenuColors[i] = subSubMenuButtons[i].image.color;
        }

        // Select the first option in the main menu
        if (menuButtons.Length > 0)
        {
            OpenSubMenu(0);
        }
    }

    private void OpenSubMenu(int index)
    {
        for (int i = 0; i < subMenus.Length; i++)
        {
            if (i == index)
            {
                subMenus[i].SetActive(true);
                if (menuButtons[i].spriteState.selectedSprite != null)
                {
                    menuButtons[i].image.sprite = menuButtons[i].spriteState.selectedSprite;
                }
                else
                {
                    menuButtons[i].image.color = selectedColor;
                }

                // Close sub-sub menus
                for (int j = 0; j < subSubMenus.Length; j++)
                {
                    subSubMenus[j].SetActive(false);
                    subSubMenuButtons[j].image.sprite = originalSubSubMenuSprites[j];
                    subSubMenuButtons[j].image.color = originalSubSubMenuColors[j];
                }
            }
            else
            {
                subMenus[i].SetActive(false);
                menuButtons[i].image.sprite = originalSprites[i];
                menuButtons[i].image.color = originalColors[i];
            }
        }
    }

    private void OpenSubSubMenu(int index)
    {
        for (int i = 0; i < subSubMenus.Length; i++)
        {
            if (i == index)
            {
                subSubMenus[i].SetActive(true);
                if (subSubMenuButtons[i].spriteState.selectedSprite != null)
                {
                    subSubMenuButtons[i].image.sprite = subSubMenuButtons[i].spriteState.selectedSprite;
                }
                else
                {
                    subSubMenuButtons[i].image.color = subSubMenuSelectedColor;
                }
            }
            else
            {
                subSubMenus[i].SetActive(false);
                subSubMenuButtons[i].image.sprite = originalSubSubMenuSprites[i];
                subSubMenuButtons[i].image.color = originalSubSubMenuColors[i];
            }
        }
    }
}
