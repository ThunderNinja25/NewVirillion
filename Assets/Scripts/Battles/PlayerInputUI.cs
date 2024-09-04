using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputUI : MonoBehaviour
{
    [SerializeField] private GameObject movesList;
    [SerializeField] private GameObject backpackInventory;

    public void ShowMoves()
    {
        movesList.SetActive(true);
    }

    public void DisableMoves()
    {
        movesList.SetActive(false);
    }

    public void ShowBackpack()
    {
        backpackInventory.SetActive(true);
    }

    public void DisableBackpack()
    {
        backpackInventory.SetActive(false);
    }
}
