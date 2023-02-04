using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerColorController : MonoBehaviour
{
    private Stack<Sprite> bodyStack;
    private Stack<AnimationClip> animationClipStack;
    private PlayerManager playerManager;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        LoadSfPlayer();
    }

    private void LoadSfPlayer()
    {
        LoadAnim();
        LoadBodySf();
    }

    void LoadBodySf()
    {
        bodyStack = new Stack<Sprite>();
        foreach(var sprite in playerManager.bodySpriteSf)
        {
            bodyStack.Push(sprite);
        }
    }
    void LoadAnim()
    {
        animationClipStack= new Stack<AnimationClip>();
        
    }
    private void ChangeWalkAnim(Animator animator)
    {
        
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        spriteRenderer = playerInput.transform.Find("body").GetComponent<SpriteRenderer>();
        animator = playerInput.GetComponent<Animator>();
        ChangeBody(spriteRenderer);
        ChangeWalkAnim(animator);
    }
    
    private void ChangeBody(SpriteRenderer body)
    {
       body.sprite = bodyStack.Pop();
    }
}

