using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

// creation de la roue d'inventaire
public class RadialMenu : MonoBehaviour
{
    // VARS
    [SerializeField] GameObject entryPrefab;

    [SerializeField] float Radius = 300f;

    [SerializeField] List<Texture> Icons;

    [SerializeField] SpriteRenderer TargetIcon;

    List<RadialMenuEntry> Entries;

    [SerializeField] InventoryScriptable inventory;


    private void Start()
    {
        Entries = new List<RadialMenuEntry>();
    }

    // ajouter une entrée au tableau selon le Open()
    void AddEntry(string pLabel, Sprite pIcon, RadialMenuEntry.RadialMenuEntryDelegate pCallback)
    {
        GameObject entry = Instantiate(entryPrefab, transform);

        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        rme.SetCallback(pCallback);

        Entries.Add(rme);
    }

    // ouvre la roue d'inventaire
    public void Open()
    {
       
        for (int i = 0; i < inventory.ItemList.Count; i++)
        {
            ItemData currentItem = inventory.ItemList[i];
            //AddEntry("Button" + i.ToString(), Icons[i], SetTargetIcon);
            AddEntry(currentItem.Name, currentItem.Icon, SetTargetIcon);
        }
        Rearrange();
    }

    // ferme la roue d'inventaire
    public void Close()
    {
 
        for (int i = 0; i < Entries.Count; i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;

            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
                delegate()
                {
                    Destroy(entry);
                };
        }
        // clear les entries de la roue
        Entries.Clear();
    }

    // securite
    public void Toggle()
    {
        if (Entries.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    // rearrange le tableau si jamais j'ajoute une arme ou quand je veux augmenter la taille ou n'importe quoi qui necessiterai de refresh
    void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for (int i = 0; i < Entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * Radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * Radius;

            RectTransform rect = Entries[i].GetComponent<RectTransform>();

            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
        }
    }

    // set une icone
    void SetTargetIcon(RadialMenuEntry pEntry)
    {
        TargetIcon.sprite = pEntry.GetIcon();
    }
}