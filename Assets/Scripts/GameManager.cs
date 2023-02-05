using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI textMeshProUGUI;
    int timer=5;

    public GameObject brazil;
    public GameObject[] SpawnPoints;
    public int currentLevel=0;

    public List<InGamePlayer> lobby=new();

    public EnumAge currentAge;

    public GameObject pauseMenu;
    int sceneNumber = 1;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    
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
    public List<GameObject> weaponsList = new();
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
     DontDestroyOnLoad(this.gameObject);
      
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis("Cancel") == 1)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }*/
      

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PrintScoreboard();
        }
        AgeCheck();
    }
    
    void AgeCheck()
    {
        foreach(InGamePlayer i in lobby)
        {
            PlayerController playerController;
            if ((sceneNumber > 0) && (i.player.TryGetComponent<PlayerController>(out playerController)))
            {
                if (playerController.curWeapon != null)
                {
                    if (playerController.curWeapon.GetAge() > currentAge && currentLevel < 4)
                    {
                        currentAge = i.player.gameObject.GetComponent<PlayerController>().curWeapon.GetAge();
                        sceneNumber++;
                        ChangeLevel();
                    }
                    if (currentLevel > 4)
                    {
                        sceneNumber = 0;
                        ChangeLevel();
                    }
                }
            }
        }
    }
    public void ChangeLevel()
    {
        ChooseSpawn();
        /*cameras[currentCamera].SetActive(false);
        currentCamera++;
        cameras[currentCamera].SetActive(true);*/
        currentLevel = sceneNumber - 1;
        switch (currentLevel)
        {
            case 0:
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[0]);
                break;
            case 1:
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[1]);
                break;
            case 2:
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[2]);
                break;
            case 3:
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[3]);
                break;
            case 4:
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[4]);
                break;

        }

        if (lobby.Count > 0)
        {
            if (textMeshProUGUI != null)
                textMeshProUGUI.gameObject.SetActive(true);
            SceneManager.LoadScene(sceneNumber);
            timer = 5;
            Invoke("StartSpawn", 5f);
            StartCoroutine(SpawnPlayers());
        }


    }

    IEnumerator SpawnPlayers() {
        yield return new WaitForSeconds(1f);
        timer--; 
        if (textMeshProUGUI != null)
            textMeshProUGUI.text = timer.ToString();
        if (timer != 0)
            StartCoroutine(SpawnPlayers());
        else
            StartSpawn();
    }
    private void StartSpawn()
    {
        if (timer == 0)
        {
            foreach (InGamePlayer i in lobby)
            {
                /*GameObject obj = i.player.gameObject;
                StartCoroutine(Respawn(0, obj));*/
                i.player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                i.player.gameObject.GetComponent<PlayerController>().ChangeWeapon(weaponsList[i.currentWeapon]);
                i.player.gameObject.transform.position = ChooseSpawn().transform.position;
                textMeshProUGUI.gameObject.SetActive(false);
            }
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
                    i.currentWeapon++;
                    print(i.color + " got his weapons downgraded");
                    i.player.gameObject.GetComponent<PlayerController>().ChangeWeapon(weaponsList[i.currentWeapon]);
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
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        int i = Random.Range(0,SpawnPoints.Length);
        return SpawnPoints[i];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            print("spadanko" + collision.gameObject.name);
            Player player = collision.gameObject.GetComponent<Player>();
            PlayerKilled(player.lastShotBy, player.gameObject.GetComponent<PlayerController>().PlayerColor);
        }
        else
            Destroy(collision.gameObject);
            
    }
}
