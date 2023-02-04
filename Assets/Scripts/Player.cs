using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 foreOnPlayer;
    private float health=100;
    public bool weaponInHand;

    [Header("Sort by Level 0-first !!!")]
    public List<Sprite> skinWithHand;
    public List<Sprite> skinWithoutHand;

    private SpriteRenderer currentRenderer;
    private GameManager gameManager;
    private Rigidbody2D rbody2D;
  
    private void Start()
    {
        rbody2D= GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        currentRenderer = transform.Find("body").GetComponent<SpriteRenderer>(); 
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
    private void FixedUpdate()
    {
        Move();
    }
    public void Hit(Vector2 vel,float damage)
    {
        health-= damage;
        print(health);
        //GetComponent<Rigidbody2D>().velocity= new Vector2(100,0);
      GetComponent<Rigidbody2D>().AddForce(new Vector2(damage,0), ForceMode2D.Force);
    }
    private void Move()
    {
        
        rbody2D.AddForce(foreOnPlayer/5);
        foreOnPlayer-= foreOnPlayer / 5;
        
    }
}