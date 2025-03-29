using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform buttonAnchor;
    [SerializeField] GameObject inventoryButton;
    List<ItemData> addedItems = new List<ItemData>();
    List<InventoryButton> addedButtons = new List<InventoryButton>();

    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName, itemDescription;
    private void Awake()
    {
        EmpireManager.Instance.onStorageEdit += (ItemData item, int count) =>
        {
            if (!addedItems.Contains(item)) CreateButton(item);
        };
        gameObject.SetActive(false);
    }
    void CreateButton(ItemData item)
    {
        InventoryButton tmp = Instantiate(inventoryButton, buttonAnchor).GetComponent<InventoryButton>();
        tmp.Set(item);
        tmp.onInventoryButtonClick += ItemDescription;
        addedButtons.Add(tmp);
        addedItems.Add(item);
        addedItems.Sort((ItemData a, ItemData b) =>
        {
            return a.name.CompareTo(b.name);
        });
        foreach(InventoryButton i in addedButtons)
        {
            i.transform.SetSiblingIndex(addedItems.IndexOf(i.item));
        }
    }
    public void ItemDescription(ItemData item)
    {
        itemImage.sprite = item.image;
        itemName.text = item.Name;
        itemDescription.text = item.Describe();
    }
}
