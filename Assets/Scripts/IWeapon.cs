using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{

    public  void Fire2();

    public  void Fire1();

    public void DisposeWeapon();

    public void Release();
    public EnumMeleeRanged GetType();
    public Vector3 GetOffset();
    public EnumPlayerColor GetPlayer();
    public void SetPlayer(EnumPlayerColor player);
    
}
