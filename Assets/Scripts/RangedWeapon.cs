using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{


    private bool isHeld = false;
    private bool isCooldown = false;
    private bool isReload = false;
    private int charges = 0;

    [Header("Objects")]
    public GameObject barrel;
    public GameObject projectile;

    [Header("Ammunition")]
    public float curAmmo = 0;
    public int maxAmmo = 1;
    public float projectileSpeed = 0;

    [Header("Weapon Stats")]
    public EnumAge age;
    public EnumFiretype firetype;
    public float spread = 0;
    public float secBetweenFire = 0.1f;
    public float damage = 0;
    public float secReload = 1;

    [Header("Weapon Type Related Settings")]
    public int additionalSpreadBullets = 4;
    public float chargeTime = 10;//decy sekundy

    [Header("Offsets")]
    public float offsetX = 0;
    public float offsetY = 0;

    public void DisposeWeapon()
    {
        Destroy(gameObject);

    }
    private void Fire()
    {

        curAmmo--;
        float randomSpread = Random.Range(-spread, spread);
        GameObject projectileObject;
        projectileObject = Instantiate(projectile, barrel.transform.position, Quaternion.Euler(Vector3.forward * randomSpread));
        projectileObject.SetActive(true);
        projectileObject.GetComponent<Rigidbody2D>().velocity = Rotate(new Vector2(projectileSpeed, randomSpread),transform.eulerAngles.z);
        print(curAmmo);
    }

    public void Fire1()
    {
      
        IEnumerator WeaponCooldown(float time)
        {
            isCooldown = true;

            yield return new WaitForSeconds(time);

            isCooldown = false;

        }
        IEnumerator ReloadCooldown(float time)
        {
            isReload= true;
            yield return new WaitForSeconds(time);
            curAmmo = maxAmmo;
            isReload= false;

        }
        IEnumerator SpreadCooldown(float time)
        {
            yield return new WaitForSeconds(time);
            Fire();

        }



        if (curAmmo < 1)
        {
            if (!isReload)
                StartCoroutine(ReloadCooldown(secReload));
        }
        else
        if (!isCooldown)
        {
            switch (firetype)
            {
                case EnumFiretype.AUTO:
                    Fire();

                    break;
                case EnumFiretype.SEMI:
                    if (!isHeld)
                    {
                        isHeld = true;
                        Fire();
                    }
                    break;
                case EnumFiretype.SPREAD:
                    if (!isHeld)
                    {
                        isHeld = true;
                        Fire();
                        for(int i=0;i<additionalSpreadBullets;i++)
                        StartCoroutine(SpreadCooldown(0.01f*i));


                    }
                    break;
                case EnumFiretype.CHARGED:
                    if (charges < chargeTime)
                        charges++;
                    print(charges);

                    break;
                default:
                    print("error null weapon type");
                    break;
            }
            StartCoroutine(WeaponCooldown(secBetweenFire));

        }

    }

    public void Fire2()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        isHeld = false;
        if(charges>chargeTime-1)
        {

            Fire();
        }
        charges = 0;

    }

    // Start is called before the first frame update
    void Awake()
    {
        curAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public  Vector2 Rotate( Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
