using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArsenal : MonoBehaviour
{
    [SerializeField] private List<WeaponSlot> weapons;

    public List<WeaponSlot> Weapons => weapons;

    public static WeaponArsenal GetArsenal()
    {
        return FindObjectOfType<Rigidbody2D>().GetComponent<WeaponArsenal>();
    }
}

[Serializable]
public class WeaponSlot
{
    [SerializeField] WeaponBase weapon;
    [SerializeField] int damage;

    public WeaponBase Weapon => weapon;

    public int Count => damage;
}
