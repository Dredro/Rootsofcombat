using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/SkinConfigure")]
public class SkinConfigure : ScriptableObject
{
    [Header("Leg skin")]
    public Skins leg;

    [Header("Sprite with hand")]
    public List<Sprite> sciSprite;
    public List<Sprite> nowSprite;
    public List<Sprite> revSprite;
    public List<Sprite> midSprite;
    public List<Sprite> neaSprite;

    [Header("Sprite without hand")]
    public List<Sprite> sciSpriteWh;
    public List<Sprite> nowSpriteWh;
    public List<Sprite> revSpriteWh;
    public List<Sprite> midSpriteWh;
    public List<Sprite> neaSpriteWh;

}
[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}
