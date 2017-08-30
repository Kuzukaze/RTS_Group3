using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "MainMenu";
    void Awake()
    {
        GameObject gm = GameObject.Find("GameManager");
        if (!gm)
        {
            DontDestroyOnLoad(this.transform.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            GameObject gm = GameObject.Find("GameManager");
            gm.GetComponent<GameManager>().SetStartScene(sceneToLoad);
            Destroy(this.gameObject);
        }
    }
}
