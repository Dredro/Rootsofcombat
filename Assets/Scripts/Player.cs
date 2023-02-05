using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public EnumPlayerColor lastShotBy;
    public float health=100;
    public PlayerController controller;
    public bool weaponInHand=false;
    public bool imReady = false;

    [Header("Sort by Level 0-first !!!")]
    public List<Sprite> skinWithHand;
    public List<Sprite> skinWithoutHand;

    private SpriteRenderer currentRenderer;
    private GameManager gameManager;

    public ParticleSystem blood;
    public ParticleSystem bloodDead;

    private void Start()
    {
        controller= GetComponent<PlayerController>();
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
        if (player != controller.PlayerColor)
        {
            lastShotBy = player;
            health -= damage;
            blood.Play();
            if (health <= 0)
            {
                bloodDead.Play();
                gameManager.PlayerKilled(player, gameObject.GetComponent<PlayerController>().PlayerColor);

            }
        }
    }
    public void Hit(Vector2 vel, float damage, EnumPlayerColor player,bool self)
    {

            lastShotBy = player;
            health -= damage;
            if (health <= 0)
            {
                gameManager.PlayerKilled(player, gameObject.GetComponent<PlayerController>().PlayerColor);

            }
        
    }


}