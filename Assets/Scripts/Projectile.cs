using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private EnumPlayerColor player;
    public float force = 0;
    public float damage = 0;
    public float secDeleteAfter = 2;
    public EnumAmmotype ammotype;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DelayCoroutine());
    }
    public void Set(float force, float damage, EnumPlayerColor player)
    {
        this.player = player;
        this.force = force;
        this.damage = damage;
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(0.02f);
        this.AddComponent<BoxCollider2D>();
        yield return new WaitForSeconds(secDeleteAfter);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (ammotype)
        {
            case EnumAmmotype.NORMAL:

                if (col.gameObject.layer == 9)
                    col.gameObject.GetComponent<Player>().Hit(new Vector2(force, 0), damage, player);
                Destroy(gameObject);
                break;
            case EnumAmmotype.GRENADE:

                break;
        }
    }
}
