using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class New_GameManager : MonoBehaviour
{
    public List<GameObject> onlinePlayerList;

    public EnumAge currentAge = new EnumAge();

    private List<Transform> spawnPoints;
    private int currentScene = 0;
   

    private void Start()
    {
        currentAge = EnumAge.SCI;
        ResetAvailablePlayerColors();
        FindSpawnPoints();
        colorUpdated = new bool[SceneManager.sceneCount];
    }
    private void Update()
    {
        UpdatePlayersSprite();
    }
   
    #region ColorManagment

    public SkinConfigure blueSkinConfigure;
    public SkinConfigure redSkinConfigure;
    public SkinConfigure greenSkinConfigure;
    public SkinConfigure yellowSkinConfigure;

    public List<EnumPlayerColor> availablePlayerColors;

    private bool[] colorUpdated;
    void ResetAvailablePlayerColors()
    {
        availablePlayerColors = new List<EnumPlayerColor>
        {
            EnumPlayerColor.RED, 
            EnumPlayerColor.GREEN, 
            EnumPlayerColor.BLUE,
            EnumPlayerColor.YELLOW
        };
    }
    void SetPlayerColor(GameObject player)
    {
        int randomNumber;
        randomNumber = Random.Range(0, availablePlayerColors.Count);
        
        if(availablePlayerColors[randomNumber] == EnumPlayerColor.RED)
        {
            player.GetComponent<New_Player>().leg = redSkinConfigure.leg;
        }
        if (availablePlayerColors[randomNumber] == EnumPlayerColor.GREEN)
        {
            player.GetComponent<New_Player>().leg = greenSkinConfigure.leg;
        }
        if (availablePlayerColors[randomNumber] == EnumPlayerColor.BLUE)
        {
            player.GetComponent<New_Player>().leg = redSkinConfigure.leg;
        }
        if (availablePlayerColors[randomNumber] == EnumPlayerColor.YELLOW)
        {
           player.GetComponent<New_Player>().leg = yellowSkinConfigure.leg;
        }
        player.GetComponent<New_Player>().color = availablePlayerColors[randomNumber];
        availablePlayerColors.RemoveAt(randomNumber);

    }
    void ReturnPlayerColor(GameObject player)
    {
        EnumPlayerColor color = player.GetComponent<New_Player>().color;
        if (!availablePlayerColors.Contains(color))
        {
            availablePlayerColors.Add(color);
        }
    }
  
    void UpdatePlayersSprite()
    {

        if (!colorUpdated[currentScene])
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            foreach (GameObject player in onlinePlayerList)
            {
                ChangePlayerBodySprite(player);
            }
            colorUpdated[currentScene] = true;
        }
    }
    void ChangePlayerBodySprite(GameObject player)
    {
        SpriteRenderer playerRenderer = player.transform.Find("body").GetComponent<SpriteRenderer>();
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.RED)
        {
            ChangeSkin(playerRenderer,redSkinConfigure);
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.BLUE)
        {
            ChangeSkin(playerRenderer, blueSkinConfigure);
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.GREEN)
        {
            ChangeSkin(playerRenderer, greenSkinConfigure);
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.YELLOW)
        {
            ChangeSkin(playerRenderer, yellowSkinConfigure);
        }
    }
    private void ChangeSkin(SpriteRenderer playerRenderer, SkinConfigure colorSkinConfigure)
    {
        switch (currentAge)
        {
            case EnumAge.SCI:
                playerRenderer.sprite = colorSkinConfigure.sciSprite[Random.Range(0, colorSkinConfigure.sciSprite.Count)];
                break;
            case EnumAge.NOW:
                playerRenderer.sprite = colorSkinConfigure.nowSprite[Random.Range(0, colorSkinConfigure.nowSprite.Count)];
                break;
            case EnumAge.NEA:
                playerRenderer.sprite = colorSkinConfigure.neaSprite[Random.Range(0, colorSkinConfigure.neaSprite.Count)];
                break;
            case EnumAge.REV:
                playerRenderer.sprite = colorSkinConfigure.revSprite[Random.Range(0, colorSkinConfigure.revSprite.Count)];
                break;
            case EnumAge.MID:
                playerRenderer.sprite = colorSkinConfigure.midSprite[Random.Range(0, colorSkinConfigure.midSprite.Count)];
                break;
        }
    }

    #endregion
    #region OnlinePlayerListUpdate
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (!onlinePlayerList.Contains(playerInput.gameObject))
        {
            SetPlayerColor(playerInput.gameObject);
            onlinePlayerList.Add(playerInput.gameObject);
            ChangePlayerBodySprite(playerInput.gameObject);
            SpawnPlayer(playerInput.gameObject);
        }
    }
    public void OnPlayerLeft(PlayerInput playerInput)
    {

        if (!onlinePlayerList.Contains(playerInput.gameObject))
        {
            onlinePlayerList.Remove(playerInput.gameObject);
            ReturnPlayerColor(playerInput.gameObject);
        }
    }
    #endregion
    #region SpawnPlayer
    void SpawnPlayer(GameObject player)
    {
        if (spawnPoints.Count > 0)
        {
            player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        }
    }
    void FindSpawnPoints()
    {
        spawnPoints = new List<Transform>();
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnPoints.Add(spawn.transform);
        }
    }
#endregion

}
