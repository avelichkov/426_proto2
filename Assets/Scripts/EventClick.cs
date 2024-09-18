using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //private MaterialApplier materialApplier;

    private void Awake()
    {
        //materialApplier = GetComponent<MaterialApplier>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //materialApplier.ApplyOther();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //materialApplier.ApplyOriginal();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // GetComponent<Zombie>().OnClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Empty
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Empty
    }
}

