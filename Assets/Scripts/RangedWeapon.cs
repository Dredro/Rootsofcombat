using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour,IWeapon
{
    public GameObject projectile;
    public int maxAmmo=0;
    public EnumAge age;
    public EnumFiretype firetype;
    public float spread=0;
    public float projectileSpeed=0;
    public float offsetX = 0;
    public float offsetY = 0;
    public void Fire1()
    {

        GameObject projectileObject=Instantiate(projectile,this.transform);
        projectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed,0);

    }

    public void Fire2()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
