
using UnityEngine;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform targetRectTransform;  // Reference to the RectTransform you want to move

    private Vector3 originalPosition;  // Original position of the RectTransform
    public MainMenuHandler _menuSounds;

    private void Start()
    {
        _menuSounds=GameObject.FindObjectOfType<MainMenuHandler>();
        // Store the original position of the RectTransform
        originalPosition = targetRectTransform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the position of the target RectTransform to match the button's position
        targetRectTransform.transform.parent = transform;
        targetRectTransform.position = transform.position;
        _menuSounds.ButtonClick();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore the original position of the target RectTransform
        //targetRectTransform.position = originalPosition;
    }
}

