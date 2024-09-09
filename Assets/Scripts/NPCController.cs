using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] private Dialogue dialogue;
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}
