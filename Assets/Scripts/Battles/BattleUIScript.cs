using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider healthSlider;

    public void SetUI(CharacterSystem system)
    {
        nameText.text = system.characterName;
        levelText.text = "Lvl " + system.characterLevel;
        healthSlider.maxValue = system.maxHealth;
        healthSlider.value = system.currentHealth;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
}
