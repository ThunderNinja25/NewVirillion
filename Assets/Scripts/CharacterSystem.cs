using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSystem : MonoBehaviour
{
    public string characterName;
    public int characterLevel;

    public int damage;

    public int maxHealth;
    public int currentHealth;

    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void Heal(int healthIncrease)
    {
        currentHealth += healthIncrease;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
