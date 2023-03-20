using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private List<Scene> sceneSci;
    [SerializeField] private List<Scene> sceneNow;
    [SerializeField] private List<Scene> sceneRev;
    [SerializeField] private List<Scene> sceneMed;
    [SerializeField] private List<Scene> sceneNea;
    [SerializeField] private Scene lobby;

    public void LoadLobby()
    {
        SceneManager.LoadScene(lobby.buildIndex);
    }
            
    public void LoadLevel(Age age)
    {
       switch(age) 
        {
            case Age.Now:
                SceneManager.LoadScene(sceneNow[Random.Range(0,sceneNow.Count)].buildIndex);
                break;
            case Age.Rev:
                SceneManager.LoadScene(sceneRev[Random.Range(0, sceneRev.Count)].buildIndex);
                break;
            
            case Age.Sci:
                SceneManager.LoadScene(sceneSci[Random.Range(0, sceneRev.Count)].buildIndex);
                break;

            case Age.Nea:
                SceneManager.LoadScene(sceneNea[Random.Range(0, sceneNea.Count)].buildIndex);
                break;

            case Age.Med:
                SceneManager.LoadScene(sceneMed[Random.Range(0, sceneMed.Count)].buildIndex);
                break;
        }
    }
}
