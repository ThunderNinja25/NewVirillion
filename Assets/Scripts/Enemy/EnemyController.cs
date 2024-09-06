using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject exclamation;

    public SceneLoader loader;

    public IEnumerator TriggerBattle(PlayerMovement player)
    {
        exclamation.SetActive(true);
        yield return new WaitForSeconds(2f);
        exclamation.SetActive(false);
        loader.BattleStart();
    }
}
