using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour,IWeapon
{
    public EnumPlayerColor player;
    public Vector3 handPoint;
    [Header("Audio")]
    public AudioClip sound;
    private Animator animator;
    private AudioSource audioSource;
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void DisposeWeapon()
    {
        Destroy(gameObject);
    }

    public void Fire1()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            animator.Play("attack", 0, 0);
            if(audioSource != null)
            audioSource.PlayOneShot(sound);
        }
    }

    public void Fire2()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetOffset()
    {
        return new Vector3(handPoint.x,handPoint.y,handPoint.z);
    }

    public void Release()
    {
        
    }

    EnumMeleeRanged IWeapon.GetType()
    {
        return EnumMeleeRanged.MELEE;
    }

    public EnumPlayerColor GetPlayer()
    {
        return player;
    }

    public void SetPlayer(EnumPlayerColor player)
    {
        this.player = player;
    }
}
