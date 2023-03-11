using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Features : MonoBehaviour
{
    // Start is called before the first frame update
    public static Features Instance { get; private set; }

    [Header("***** BOMB *****")]
    public GameObject[] bomb;
    public int bombIndex;
    public ParticleSystem bombEffect;
    public float bombRange;
    public int bombValue;
    public AudioSource bombSound;
    
    [Header("***** BOLT *****")]
    public GameObject[] bolt;
    public int boltIndex;
    public ParticleSystem boltEffect;
    public float boltRange;
    public int boltValue;
    public AudioSource BoltSound;
    
    [Header("***** Electricity  *****")]
    public GameObject[] electricities;
    public int electricityIndex;
    public ParticleSystem electricityEffect;
    public float electricityRangeAmount;
    public int electricityValue;
    public AudioSource electricitySound;


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
       
    }

    void Update()
    {
        
    }

    public void BombFeatures()
    {
        bombSound.Play();
        Instantiate(bombEffect, bomb[bombIndex].transform.position, Quaternion.identity);
        foreach (var item in GameManager.Instance.BLOCKS)
        {
            if (CalcDistance(item.gameObject, bomb[bombIndex].gameObject) <= bombRange)
            {
                item.GetComponent<Block>().SetValue(item.GetComponent<Block>().GetValue() - bombValue);
            }
        }


        bomb[bombIndex].SetActive(false);
       

    }

    public void BoltFaetures()
    {
        BoltSound.Play();
        Instantiate(boltEffect, bolt[boltIndex].transform.position, Quaternion.identity);
        foreach (var item in GameManager.Instance.BLOCKS)
        {
            if (CalcDistance(item.gameObject, bolt[boltIndex].gameObject) <= boltRange)
            {
                item.GetComponent<Block>().SetValue(item.GetComponent<Block>().GetValue() - boltValue);
            }
        }


          bolt[boltIndex].SetActive(false);


    }
    
    public void ElectricityFaetures()
    {
        electricitySound.Play();
       

        for(int i =0;i<electricityRangeAmount;i++)
        {
           int randomIndex = Random.Range(0, GameManager.Instance.BLOCKS.Count - 1);
           GameManager.Instance.BLOCKS[randomIndex].GetComponent<Block>().SetValue(GameManager.Instance.BLOCKS[randomIndex].GetComponent<Block>().GetValue() - electricityValue);
           ParticleSystem fx= Instantiate(electricityEffect, GameManager.Instance.BLOCKS[randomIndex].transform.position, Quaternion.identity);
            fx.transform.SetParent(GameManager.Instance.BLOCKS[randomIndex].transform);
        }


          electricities [electricityIndex].SetActive(false);


    }


    public float CalcDistance(GameObject g1, GameObject g2)
    {
        float distance = Vector2.Distance(g1.transform.position, g2.transform.position);
        return distance;
    }



}
