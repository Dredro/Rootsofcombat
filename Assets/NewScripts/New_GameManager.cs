using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class New_GameManager : MonoBehaviour
{
    public List<GameObject> onlinePlayerList;

    public List<Transform> spawnPoints;

    private int currentScene = 0;
   

    private void Start()
    {
        ResetAvailablePlayerColors();
        FindSpawnPoints();
        colorUpdated = new bool[SceneManager.sceneCount];
    }
    private void Update()
    {
        UpdatePlayersSprite();
    }

    #region ColorManagment

    public GameObject bluePrefab;
    public GameObject greenPrefab;
    public GameObject redPrefab;
    public GameObject yellowPrefab;

    public List<Sprite> blueSpriteList;
    public List<Sprite> greenSpriteList;
    public List<Sprite> redSpriteList;
    public List<Sprite> yellowSpriteList;

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
    void LoadPlayer(GameObject player)
    {
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.RED)
        {
            player = Instantiate(redPrefab);
            player.GetComponent<New_Player>().color = EnumPlayerColor.RED;
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.BLUE)
        {
            player = Instantiate(bluePrefab);
            player.GetComponent<New_Player>().color = EnumPlayerColor.BLUE;
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.GREEN)
        {
            player = Instantiate(greenPrefab);
            player.GetComponent<New_Player>().color= EnumPlayerColor.GREEN;
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.YELLOW)
        {
            player = Instantiate(yellowPrefab);
            player.GetComponent<New_Player>().color = EnumPlayerColor.YELLOW;
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
            playerRenderer.sprite = redSpriteList[currentScene];
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.BLUE)
        {
            playerRenderer.sprite = blueSpriteList[currentScene];
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.GREEN)
        {
            playerRenderer.sprite = greenSpriteList[currentScene];
        }
        if (player.GetComponent<New_Player>().color == EnumPlayerColor.YELLOW)
        {
            playerRenderer.sprite = yellowSpriteList[currentScene];
        }
    }
    #endregion
    #region OnlinePlayerListUpdate
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (!onlinePlayerList.Contains(playerInput.gameObject))
        {
            SetPlayerColor(playerInput.gameObject);
            LoadPlayer(playerInput.gameObject);
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
