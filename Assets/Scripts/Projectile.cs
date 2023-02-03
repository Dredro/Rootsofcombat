using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DelayCoroutine());
    }
    
    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        this.AddComponent<BoxCollider2D>();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
