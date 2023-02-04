using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
 public class InGamePlayer
{

    public EnumPlayerColor color;
    public Player player;
    public int frags;
    public int deaths;
    public int currentWeapon;

    public InGamePlayer(EnumPlayerColor color,Player player)
    {
        this.color = color;
        this.frags = 0;
        this.deaths = 0;
        this.currentWeapon = 0;
        this.player = player;
    }
    
} 

public class GameManager : MonoBehaviour
{
    public GameObject brazil;
    public List<GameObject> SpawnPoints;
    public int currentLevel=0;

    public List<InGamePlayer> lobby=new List<InGamePlayer>();
   
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Weapons")]
    public List<GameObject> weaponsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PrintScoreboard();
        }
    }
    public void PlayerKilled(EnumPlayerColor killer, EnumPlayerColor victim)
    {
        print(killer + " killed " + victim);
        if(killer!=victim)
        {
            foreach (InGamePlayer i in lobby)
            {
                if(i.color==killer)
                {
                    i.frags++;
                }
            }
        }
        foreach (InGamePlayer i in lobby)
        {
            if(i.color==victim)
            {
                i.deaths++;
                i.player.Reset();
                GameObject obj = i.player.gameObject;
                StartCoroutine(Respawn(2, obj));

            }
        }
    }
    public void PlayerFellOff()
    {

    }
    public void PlayerJoined(EnumPlayerColor color,Player player)
    {
        lobby.Add(new InGamePlayer(color,player));
        print(color + " joined the game");
    }
    public void PrintScoreboard()
    {
        foreach(InGamePlayer i in lobby) 
        {
            print(i.color+" | Kills: "+i.frags+" | Deaths: "+i.deaths);
        }
    }
    IEnumerator Respawn(float time,GameObject obj)
    {
        obj.transform.position=brazil.transform.position;
        yield return new WaitForSeconds(time);
        obj.transform.position=ChooseSpawn().transform.position; 


    }
    private GameObject ChooseSpawn()
    {
        int i = Random.Range(0,SpawnPoints.Count);
        return SpawnPoints[i];
    }
}
