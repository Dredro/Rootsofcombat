using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public Vector2[] spawnPoints;
    private Stack<Vector2> spawn;

    private void Start()
    {
        ReadSpawnPoints();
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
        SetPlayerPosition(playerInput.transform);
    }

    void SetPlayerPosition(Transform transform)
    {
        transform.position = spawn.Pop();
    }
}
