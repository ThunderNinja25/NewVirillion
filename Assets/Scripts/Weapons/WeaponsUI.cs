using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField] private GameObject weaponList;
    [SerializeField] private WeaponSlotUI weaponSlotUI;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponDescription;

    private int selectedItem = 0;

    private List<WeaponSlotUI> weaponSlotUIList;

    private WeaponArsenal weaponArsenal;
    private void Awake()
    {
        weaponArsenal = WeaponArsenal.GetArsenal();
    }

    private void Start()
    {
        UpdateWeaponList();
    }

    public void UpdateWeaponList()
    {
        foreach(Transform child in weaponList.transform)
        {
            Destroy(child.gameObject);
        }
        weaponSlotUIList = new List<WeaponSlotUI>();
        foreach(var weaponSlot in weaponArsenal.Weapons)
        {
            var slotUIWeapon = Instantiate(weaponSlotUI, weaponList.transform);
            slotUIWeapon.SetData(weaponSlot);

            weaponSlotUIList.Add(slotUIWeapon);
        }
        UpdateWeaponSelection();
    }

    public void HandleUpdate(Action onBack)
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedItem++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) { selectedItem--; }

        selectedItem = Mathf.Clamp(selectedItem, 0, weaponArsenal.Weapons.Count - 1);
        if(prevSelection != selectedItem) 
        {
            UpdateWeaponSelection();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onBack?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void UpdateWeaponSelection()
    {
        for(int i = 0; i < weaponSlotUIList.Count; i++)
        {
            if(i == selectedItem)
            {
                weaponSlotUIList[i].WeaponText.color = GlobalSettings.i.HighlightedColor;
            }
            else
            {
                weaponSlotUIList[i].WeaponText.color = Color.black;
            }
        }

        var slot = weaponArsenal.Weapons[selectedItem];
        weaponIcon.sprite = slot.Weapon.Icon;
        weaponDescription.text = slot.Weapon.Description;
    }
}
