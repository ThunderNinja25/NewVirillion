using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] private Dialogue dialogue;
    Healer healer;

    private void Awake()
    {
        healer = GetComponent<Healer>();
    }
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));

    }

    public IEnumerator Interact(Transform initiator)
    {
        if (healer != null)
        {
           yield return healer.Heal(initiator, dialogue);
        }
    }
}
