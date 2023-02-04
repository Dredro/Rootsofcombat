using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private EnumPlayerColor lastShotBy;
    private float health=100;
    public bool weaponInHand=false;

    [Header("Sort by Level 0-first !!!")]
    public List<Sprite> skinWithHand;
    public List<Sprite> skinWithoutHand;

    private SpriteRenderer currentRenderer;
    private GameManager gameManager;
  
    private void Start()
    {
       Reset(); 
        gameManager = GameManager.Instance;
       gameManager.PlayerJoined(lastShotBy, this);
        currentRenderer = transform.Find("body").GetComponent<SpriteRenderer>(); 
    }
    public void Reset()
    {
        health = 100;
        lastShotBy = GetComponent<PlayerController>().PlayerColor;
    }
    private void Update()
    {
        
        if (weaponInHand)
        {
            currentRenderer.sprite = skinWithoutHand[gameManager.currentLevel];
        }
        else
        {
            currentRenderer.sprite = skinWithHand[gameManager.currentLevel];
        }
    }

    public void Hit(Vector2 vel,float damage,EnumPlayerColor player)
    {
        lastShotBy = player;
        health-= damage;
        if(health<=0)
        {
            gameManager.PlayerKilled(player, GetComponent<PlayerController>().PlayerColor);
            
        }
    }

}