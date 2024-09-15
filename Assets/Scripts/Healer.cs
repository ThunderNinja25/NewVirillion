using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public IEnumerator Heal(Transform player, Dialogue dialogue)
    {
        yield return DialogueManager.Instance.ShowDialogue(dialogue);

        var playerHeal = player.GetComponent<CharacterSystem>();
        playerHeal.Heal(playerHeal.maxHealth);
    }
}
