using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] private int sceneSelection;
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] private Transform spawnPoint;
    PlayerMovement player;
    FadeInOut fade;

    private void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator ChangeScene()
    {
        DontDestroyOnLoad(gameObject);

        fade.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneSelection);

        var destinationPortal = FindObjectsOfType<SceneSwitch>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.transform.position = destinationPortal.SpawnPoint.position;
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;

    public void OnPlayerTriggered(PlayerMovement player)
    {
        this.player = player;
        StartCoroutine (ChangeScene());
    }
}

public enum DestinationIdentifier { A, B, C, D };
