using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlatformStart : MonoBehaviour
{
    int i=0;
    public PlayerInputManager playerInputManager;
    public GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       i++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        i--;
    }
  
  
private void Update()
    {
        if (playerInputManager.playerCount == i)
        {
            gameManager.ChangeCamera();
        }
    }
}
