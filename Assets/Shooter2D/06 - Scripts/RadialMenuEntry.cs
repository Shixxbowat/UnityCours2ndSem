using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

// Script pour le menu (les infos) de la roue d'inventaire
public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RadialMenuEntryDelegate(RadialMenuEntry pEntry);
    [SerializeField] TextMeshProUGUI Label;

    [SerializeField] Image Icon;

    RectTransform Rect;
    RadialMenuEntryDelegate Callback;

    private void Start()
    {
        Rect = Icon.GetComponent<RectTransform>();
    }

    public void SetLabel(string pText)
    {
        Label.text = pText;
    }

    public void SetIcon(Sprite pIcon)
    {
        Icon.GetComponent<Image>().sprite = pIcon;
    }

    public Sprite GetIcon()
    {
        return (Icon.GetComponent<Image>().sprite);
    }

    public void SetCallback(RadialMenuEntryDelegate pCallback)
    {
        Callback = pCallback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Callback?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Rect.DOComplete();
        Rect.DOScale(Vector3.one * 1.5f, .3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Rect.DOComplete();
        Rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);
    }
}