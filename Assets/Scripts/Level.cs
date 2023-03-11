using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelName;
    public int levelIndex;

    public void Awake()
    {
        levelName = gameObject.name;
        levelIndex = int.Parse(levelName);
    }
    void Start()
    {
        

      
    }

    void Update()
    {
        
    }

    public void ClickButton()
    {
       // LevelManager.Instance.OpenScene(levelName);
        LevelManager.Instance.SetLastLevel(levelIndex+1);

    }
}
