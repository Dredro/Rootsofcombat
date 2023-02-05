using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosis : MonoBehaviour
{
    public float start = 0.05f;
    public float time = 0.6f;
    public float plus = 0.01f;
    public EnumPlayerColor player;
    public float damage = 100;
    public void FixedUpdate()
    {
        start +=plus;
        transform.localScale=new Vector3(start,start,start);
    }
    public void Start()
    {
        StartCoroutine(DestroyAfter());
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
    

                if (col.gameObject.layer == 9)
                    col.gameObject.GetComponent<Player>().Hit(new Vector2(0, 0), damage, player,true);

          
        

    }
}
