using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] private int sceneSelection;
    [SerializeField] public DestinationIdentifier destinationPortal;
    [SerializeField] public Transform spawnPoint;
    public PlayerMovement player;

    private void Start()
    {
        GameObject.Find("GameManager");
    }

    public Transform SpawnPoint => spawnPoint;

    public void OnPlayerTriggered(PlayerMovement player)
    {
        this.player = player;
        GameManager.Instance.StartChangeScene(sceneSelection, destinationPortal);
    }
}

public enum DestinationIdentifier { A, B, C, D };
