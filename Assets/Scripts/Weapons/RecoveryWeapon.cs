using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Create new Recovery Weapon")]
public class RecoveryWeapon : WeaponBase
{
    [Header("Damage")]
    [SerializeField] int damageDealt;
}
