using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransition : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Sprite overlaySprite;  // Assign the overlay sprite in the Inspector
    public Vector2 overlayResolution = new Vector2(100, 100);  // Set the overlay resolution in the Inspector

    private Image buttonImage;
    private Image overlayImage;
    private bool isSelected = false;

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        // Create an overlay image dynamically
        GameObject overlayObject = new GameObject("OverlayImage");
        overlayObject.transform.SetParent(transform, false);
        overlayImage = overlayObject.AddComponent<Image>();
        overlayImage.sprite = overlaySprite;

        // Set overlay image size
        RectTransform overlayRect = overlayImage.GetComponent<RectTransform>();
        overlayRect.sizeDelta = overlayResolution;

        overlayImage.enabled = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        overlayImage.enabled = true;
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        overlayImage.enabled = false;
        isSelected = false;
    }

    // Optionally, add this if you want to trigger the overlay on mouse hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.enabled = true;
        }
    }

    // Optionally, add this if you want to hide the overlay when the mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.enabled = false;
        }
    }
}
