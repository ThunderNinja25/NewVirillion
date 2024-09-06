using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] string weaponDescription;
    [SerializeField] Sprite weaponIcon;

    public string Name => weaponName;

    public string Description => weaponDescription;

    public Sprite Icon => weaponIcon;
}
