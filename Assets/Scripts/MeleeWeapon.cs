using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour,IWeapon
{
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 0;
    public void DisposeWeapon()
    {
        Destroy(gameObject);
    }

    public void Fire1()
    {
        
    }

    public void Fire2()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetOffset()
    {
        return new Vector3(offsetX, offsetY, offsetZ);
    }

    public void Release()
    {
        
    }

    EnumMeleeRanged IWeapon.GetType()
    {
        return EnumMeleeRanged.MELEE;
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
