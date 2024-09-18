using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON,LOST }

public class BattleScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerBattleSpawn;
    [SerializeField] private Transform enemyBattleSpawn;
    [SerializeField] private Image playerSprite;
    [SerializeField] private Image enemySprite;

    private CharacterSystem playerSystem;
    private CharacterSystem enemySystem;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private BattleUIScript playerUI;
    [SerializeField] private BattleUIScript enemyUI;
    [SerializeField] private PlayerInputUI uiSwitch;

    public bool battleOver;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
    }

    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleSpawn);
        playerSystem = playerGO.GetComponent<CharacterSystem>();
        playerSystem.currentHealth = playerSystem.maxHealth;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemySystem = enemyGO.GetComponent<CharacterSystem>();
        enemySystem.currentHealth = enemySystem.maxHealth;

        //playerSprite.sprite = playerSystem.PlayerSprite;
        enemySprite.sprite = enemySystem.EnemySprite;

        playerUI.SetUI(playerSystem);
        enemyUI.SetUI(enemySystem);

        dialogueText.text = enemySystem.characterName + " approaches...";

        yield return new WaitForSeconds(2);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemySystem.TakeDamage(playerSystem.damage);
        enemyUI.SetHealth(enemySystem.currentHealth);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2);

        if(isDead)
        {
            state = BattleState.WON;
            gameObject.SetActive(false);
            EndBattle();
            yield return new WaitForSeconds(2);
            battleOver = true;
            
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemySystem.characterName + " attacks!";

        yield return new WaitForSeconds(1);

        bool isDead = playerSystem.TakeDamage(enemySystem.damage);

        playerUI.SetHealth(playerSystem.currentHealth);

        yield return new WaitForSeconds(1);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
            yield return new WaitForSeconds(2);
            battleOver = true;
            
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            state = BattleState.START;
            battleOver = true;
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            state = BattleState.START;
            battleOver = true;
        }
        GameManager.Instance.EndBattle();
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
        
    }

    IEnumerator PlayerHeal()
    {
        playerSystem.Heal(5);

        playerUI.SetHealth(playerSystem.currentHealth);
        dialogueText.text = "You have been healed!";

        yield return new WaitForSeconds(2);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
        uiSwitch.DisableMoves();
    }
    
    public void OnMovesButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        uiSwitch.ShowMoves();
    }

    public void OnBackpackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        uiSwitch.ShowBackpack();
        StartCoroutine(PlayerHeal());
        uiSwitch.DisableBackpack();
    }
}
