using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> BLOCKS;

    public AudioSource BlockSound;
    public int FixAmount;
    public GameObject EnvironmentObject;
    public float speed;


    [Header("******* GAME ********")]
    public int ballAmount;
    public int TurnAmount;
    public int[] ballAmounts;

    public GameObject finishObject;

    [Header("******* BALL ********")]
    public GameObject ball;
    public int ballSpeed;

    public bool isWin,isLose;
    

    [Header("******* UI ********")]
    public TextMeshProUGUI ballAmountText;
    public TextMeshProUGUI NextBallAmountText;
    public GameObject LosePanel;
    public GameObject WinPanel;
    public GameObject winEffect;
    public GameObject loseEffect;

    [Header("Animation")]
    public Animator FinsihAnimator;
        

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

    public float number;
    void Start()
    {
        Screen.SetResolution(1080, 2400, true);
        ballAmount = ballAmounts[0];
        FixAmount = ballAmount;
        number = EnvironmentObject.transform.position.y-2;



        ball.GetComponent<Ball>().ballSpeed = ballSpeed;
        ballAmountText.text = ballAmount.ToString()+"X";


    }
   public float t = 0;
    
    // Update is called once per frame
    void Update()
    {
        
      

        if (ballAmount == FixAmount)
        {

            EnvironmentObject.transform.position = new Vector3(0,  Mathf.Lerp(EnvironmentObject.transform.position.y, number, t), 0);
           
            t += 0.1f * Time.deltaTime;

            if (ShootSystem.Instance.turnCounter -1== TurnAmount)
            {
                isLose = true;
            }

            foreach (var item in BLOCKS)
            {
                item.GetComponent<BoxCollider2D>().isTrigger = false;
            }

            foreach (var item in Features.Instance.bomb)
            {
                item.GetComponent<BoxCollider2D>().isTrigger = false;
            } 
            
            foreach (var item in Features.Instance.bolt)
            {
                item.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            
            foreach (var item in Features.Instance.electricities)
            {
                item.GetComponent<BoxCollider2D>().isTrigger = false;
            }

          
        }

      
        

    }
    private void FixedUpdate()
    {
       // ChechWin();
        //CheckLose();
    }

    private void LateUpdate()
    {
        if (isWin)
            SetWinAction();
        if(isLose)
            SetLoseAction();
    }

    public void ChechWin()
    {
        if (BLOCKS.Count ==0)
        {
            isWin = true; 
        }


    }

    public void CheckLose()
    {
        
    }


    public void TakeDownBlocks()
    {
        
        for (int i = 0; i < 10; i++)
        {
            if (ShootSystem.Instance.shootAmount % 2 == 0)
                EnvironmentObject.transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        }
       

        



    }


 

    public void IncreaseTime(float time)
    {
        Time.timeScale = time;
    }


    #region UI

    public void SetLoseAction()
    {
        isLose = false;
     //   loseEffect.SetActive(true);
        StartCoroutine( SetActive(LosePanel, 1));
       
    }

    public void SetWinAction()
    {
        isWin = false;
       
        ShootSystem.Instance.DropAllBalls();
        StartCoroutine(SetActive(WinPanel, 1.5f));

        

    }

    public IEnumerator SetActive(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(true);
    }
    #endregion
}
