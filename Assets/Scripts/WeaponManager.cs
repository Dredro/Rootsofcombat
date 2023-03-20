using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using UnityEngine;

public class WeaponManager:MonoBehaviour
{
    private List<IWeapons> weaponsSCI = new List<IWeapons>();
    private List<IWeapons> weaponsNOW = new List<IWeapons>();
    private List<IWeapons> weaponsREV = new List<IWeapons>();
    private List<IWeapons> weaponsMID = new List<IWeapons>();
    private List<IWeapons> weaponsNEA = new List<IWeapons>();
    private List<IWeapons> cur = new List<IWeapons>();

    public int gunsAmount;
    public IWeapons GetNextWeapon(int index)
    {
        return cur[index];
    }

    public void GenerateWeaponList(int gunsAmount)
    {
        cur.Clear();
        System.Random random = new System.Random();
        int randNum;
        //losowanie 3 broni z weaponsSCI
       for(int i = 0; i < gunsAmount; i++)
        {
            randNum = random.Next(weaponsSCI.Count);
            if (cur.Contains(weaponsSCI[randNum]))
                i--;
            else
            cur.Add(weaponsSCI[randNum]);
        }
        //losowanie 3 broni z weaponsNOW
        for (int i = 0; i < gunsAmount; i++)
        {
            randNum = random.Next(weaponsNOW.Count);
            if (cur.Contains(weaponsNOW[randNum]))
                i--;
            else
                cur.Add(weaponsNOW[randNum]);
        }
        //losowanie 3 broni z weaponsREV
        for (int i = 0; i < gunsAmount; i++)
        {
            randNum = random.Next(weaponsREV.Count);
            if (cur.Contains(weaponsREV[randNum]))
                i--;
            else
                cur.Add(weaponsREV[randNum]);
        }
        //losowanie 3 broni z weaponsMID
        for (int i = 0; i < gunsAmount; i++)
        {
            randNum = random.Next(weaponsMID.Count);
            if (cur.Contains(weaponsMID[randNum]))
                i--;
            else
                cur.Add(weaponsMID[randNum]);
        }
        //losowanie 3 broni z weaponsNEA
        for (int i = 0; i < gunsAmount; i++)
        {
            randNum = random.Next(weaponsNEA.Count);
            if (cur.Contains(weaponsNEA[randNum]))
                i--;
            else
                cur.Add(weaponsNEA[randNum]);
        }
    }
}


