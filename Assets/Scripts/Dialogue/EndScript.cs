using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneCanvas;
    [SerializeField] private Animation cutscene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutsceneCanvas.SetActive(true);
        cutscene.Play();
    }
}
