using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public List<Vector2> spawnPoints; 

    public void OnPlayerJoined(PlayerInput playerInput) {
        SetPlayerPosition(playerInput.transform);
    }

    void SetPlayerPosition(Transform transform)
    {
        transform.position = spawnPoints[0];
    }
}
