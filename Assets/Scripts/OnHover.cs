using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject childText; //  or make public and drag

    void Start()
    {
            childText.SetActive(false);
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        childText.SetActive(true);
    }
    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        childText.SetActive(false);
    }
}
