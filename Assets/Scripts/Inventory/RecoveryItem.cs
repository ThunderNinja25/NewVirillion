using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new Recovery Item")] 
public class RecoveryItem : ItemBase
{
    [Header("HP")]
    [SerializeField] private int hpAmount;
    [SerializeField] private bool restoreMaxHP;

    public override bool Use(CharacterSystem character)
    {
        if(hpAmount > 0)
        {
            if(character.currentHealth == character.maxHealth)
            {
                return false;
            }

            character.Heal(hpAmount);
        }
        return true;
    }
}
