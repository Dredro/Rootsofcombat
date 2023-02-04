using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> players;
    public Vector2[] spawnPoints;
    private Stack<Vector2> spawn;
    private Stack<GameObject> playersStack;
    private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager= GetComponent<PlayerInputManager>();
        ReadSpawnPoints();
        ReadPlayers();

        //First Join
        playerInputManager.playerPrefab = playersStack.Pop();
    }

    private void ReadPlayers()
    {
        playersStack = new Stack<GameObject>();
        foreach (var player in players)
        {
            playersStack.Push(player);
        }
    }
    private void ReadSpawnPoints()
    {
        spawn = new Stack<Vector2>();
        foreach (var spawnPoint in spawnPoints)
        {
            spawn.Push(spawnPoint);
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        playerInputManager.playerPrefab = playersStack.Pop();
        SetPlayerPosition(playerInput.transform);
    }


    void SetPlayerPosition(Transform transform)
    {
        transform.position = spawn.Pop();
    }
}
