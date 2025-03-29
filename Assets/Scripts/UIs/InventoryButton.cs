using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour
{
    [SerializeField] TMP_Text countText, nameText;
    [SerializeField] Image itemImage;
    ItemData setItem;
    public ItemData item { get { return setItem; } }

    public delegate void InventoryButtonClickCallback(ItemData item);
    public event InventoryButtonClickCallback onInventoryButtonClick;
    public void Set(ItemData setItem)
    {
        this.setItem = setItem;
        GetComponent<Button>().onClick.AddListener(() => { onInventoryButtonClick.Invoke(setItem); });
        EmpireManager.Instance.onStorageEdit += (ItemData item, int amount) =>
        {
            if (item == setItem) countText.text = "x" + amount;
        };
        nameText.text = setItem.Name;
        itemImage.sprite = setItem.image;
        countText.text = "x" + EmpireManager.Instance.Get(setItem);
    }
}
