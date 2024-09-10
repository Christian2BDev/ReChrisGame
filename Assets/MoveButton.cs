using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI buttonText;   // Reference to the TextMeshPro text
    private Vector3 originalPosition;    // Store the original position of the text
    public float moveDistance = 5f;      // How far down you want the text to move

    void Start()
    {
        // Store the original position of the text
        originalPosition = buttonText.rectTransform.localPosition;
    }

    // This method is called when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        // Move the text down by changing its localPosition
        buttonText.rectTransform.localPosition = originalPosition - new Vector3(0, moveDistance, 0);
    }

    // This method is called when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        // Move the text back to its original position
        buttonText.rectTransform.localPosition = originalPosition;
    }
}
