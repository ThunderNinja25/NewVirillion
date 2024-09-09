using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemList;
    [SerializeField] private ItemSlotUI itemSlotUI;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private int selectedItem = 0;

    private List<ItemSlotUI> slotUIList;

    private Inventory inventory;
    private CharacterSystem characterSystem;
    private void Awake()
    {
        inventory = Inventory.GetInventory();
        characterSystem = GetComponent<CharacterSystem>();
    }

    private void Start()
    {
        UpdateItemList();
    }

    public void UpdateItemList()
    {
        foreach(Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }
        slotUIList = new List<ItemSlotUI>();
        foreach(var itemSlot in inventory.Slots)
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);

            slotUIList.Add(slotUIObj);
        }
        UpdateItemSelection();
    }

    public void HandleUpdate(Action onBack)
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedItem++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedItem--;
        }

        selectedItem = Mathf.Clamp(selectedItem, 0, inventory.Slots.Count - 1);
        if (prevSelection != selectedItem)
        {
            UpdateItemSelection();
        }
        Action onSelected = () =>
        {
            inventory.UseItem(selectedItem, characterSystem);
        };

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onBack?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void UpdateItemSelection()
    {
        for (int i = 0; i < slotUIList.Count; i++)
        {
            if (i == selectedItem)
            {
                slotUIList[i].NameText.color = GlobalSettings.i.HighlightedColor;

            }
            else
            {
                slotUIList[i].NameText.color = Color.black;
            }
        }

        var slot = inventory.Slots[selectedItem];
        itemIcon.sprite = slot.Item.Icon;
        itemDescription.text = slot.Item.Description;
    }
}
