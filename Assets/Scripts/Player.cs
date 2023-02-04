using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float health;
    public bool weaponInHand;

    [Header("Sort by Level 0-first !!!")]
    public List<Sprite> skinWithHand;
    public List<Sprite> skinWithoutHand;

    private SpriteRenderer currentRenderer;
    private GameManager gameManager;
  
    private void Start()
    {
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
    public void Hit(Vector2 vel)
    {
        GetComponent<Rigidbody2D>().velocity += vel;
    }
}