using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force = 0;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DelayCoroutine());
    }
    
    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(0.02f);
        this.AddComponent<BoxCollider2D>();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
            col.gameObject.GetComponent<Player>().Hit(new Vector2(GetComponent<Rigidbody2D>().velocity.x,0));
        Destroy(gameObject);
    }
}
