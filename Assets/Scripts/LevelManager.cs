using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public GameObject[] Levels;
    public int lastLevel;

    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
       if(!PlayerPrefs.HasKey("LastLevel"))
        {
            PlayerPrefs.SetInt("LastLevel", 2);
        }
        SetLastLevel(PlayerPrefs.GetInt("LastLevel"));
        SetInteractable(Levels);
    }

    void Update()
    {
        
    }

    
   
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetLastLevel(int lastLevel)
    {
        PlayerPrefs.SetInt("LastLevel", lastLevel);
        this.lastLevel = PlayerPrefs.GetInt("LastLevel");
    }

    public void SetInteractable(GameObject[] Levels)
    {
        Debug.Log("1");
        foreach (var item in Levels)
        {
            if(lastLevel < item.GetComponent<Level>().levelIndex)
            {
                Debug.Log("2");
                item.GetComponent<Button>().interactable = false;
            }
            else
                item.GetComponent<Button>().interactable = true;
        }

    }
}
