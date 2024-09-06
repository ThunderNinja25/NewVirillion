using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSlotUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponText;
    [SerializeField] TextMeshProUGUI countText;
     
    public TextMeshProUGUI WeaponText => weaponText;

    public TextMeshProUGUI CountText => countText;

    public void SetData(WeaponSlot weaponSlot)
    {
        weaponText.text = weaponSlot.Weapon.Name;
        countText.text = $"{weaponSlot.Count} HP";
    }
}
