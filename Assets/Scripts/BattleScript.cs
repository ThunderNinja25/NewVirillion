using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON,LOST }

public class BattleScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerBattleSpawn;
    [SerializeField] private Transform enemyBattleSpawn;

    private CharacterSystem playerSystem;
    private CharacterSystem enemySystem;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private BattleUIScript playerUI;
    [SerializeField] private BattleUIScript enemyUI;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    public void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleSpawn);
        playerSystem = playerGO.GetComponent<CharacterSystem>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemySystem = enemyGO.GetComponent<CharacterSystem>();

        dialogueText.text = enemySystem.characterName + " approaches...";

        playerUI.SetUI(playerSystem);
        enemyUI.SetUI(enemySystem);
    }
    
}
