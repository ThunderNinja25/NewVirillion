using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject exclamation;
    [SerializeField] private GameObject fov;
    [SerializeField] private BattleScript battleSystem;

    [SerializeField] private GameManager gameManager;

    //public bool battleLost;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartBattle()
    {
        //StartCoroutine(TriggerBattle());
    }

    public IEnumerator TriggerBattle(PlayerMovement player)
    {
        //battleScript = FindObjectOfType<BattleScript>(true);
        exclamation.SetActive(true);
        fov.SetActive(false);
        yield return new WaitForSeconds(2f);
        exclamation.SetActive(false);
        battleSystem.gameObject.SetActive(true);
        gameManager.battleScript = battleSystem;
        gameManager.enemyController = this;
        gameManager.StartBattle();
    }
}
