using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour, Interactable
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private RecoveryItem recoveryItem;
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        gameObject.SetActive(false);
    }

    
}
