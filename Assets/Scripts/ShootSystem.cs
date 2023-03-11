using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public static ShootSystem Instance { get; private set; }
    public GameObject spawnPoint;
    public GameObject ball,copyBall;
    public Trajectory trajectory;

    public float shootFrequensy;

    private int BallAmount;

    public GameObject cycle;
    public List<GameObject> balls;


    public int layerMask;

    public bool isDrag;
    public bool canDraging;
    public bool isRemove;
    public float mouseSpeed;

    public int shootAmount;

    public int turnCounter;
    

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
        shootAmount = 0;
        turnCounter = 0;
        
    }

    void Update()
    {
        if (GameManager.Instance.ballAmount >= GameManager.Instance.FixAmount)
        {
            GameManager.Instance.CheckLose();
            canDraging = true;
        }     
        else
            canDraging = false;

        if (GameManager.Instance.ballAmounts.Length > turnCounter+1)
        {
            GameManager.Instance.NextBallAmountText.text = GameManager.Instance.ballAmounts[turnCounter + 1].ToString() + "X";
        }
        else
        {
            
            GameManager.Instance.NextBallAmountText.text = 0 + "X";
        }
           

    }



    public void ShootDown()
    {
        BallAmount = GameManager.Instance.ballAmount;
            if (canDraging)
                isDrag = true;

       
    }

    public void Drag()
    {
            if (canDraging)
                TurnCycle();
        

    }

    public void ShootUp()
    {
        GameManager.Instance.number--;
        GameManager.Instance.t =0;
            if (canDraging)
            {
                isDrag = false;
                StartCoroutine(ShootSys());
            }

            shootAmount++;
            turnCounter++;
        //GameManager.Instance.CheckLose();
        GameManager.Instance.FixAmount = GameManager.Instance.ballAmounts[turnCounter];
        GameManager.Instance.ballAmount = GameManager.Instance.ballAmounts[turnCounter];
            


    }

    



    public IEnumerator ShootSys()
    {
        
        isRemove = false;
        for (int i = 0; i < BallAmount; i++)
        {
            if (!isRemove)
            {
                yield return new WaitForSeconds(shootFrequensy *Time.deltaTime);
                copyBall = Instantiate(ball, spawnPoint.transform.position, spawnPoint.transform.rotation);
                balls.Add(copyBall);
                GameManager.Instance.ballAmount--;
                GameManager.Instance.ballAmountText.text = GameManager.Instance.ballAmount.ToString() + "X";
            }
            

            
        }

        cycle.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TurnCycle()
    {
        cycle.transform.rotation = Quaternion.Euler(0, 0, GetMousePos().x * -30);
    }

    public Vector2 GetMousePos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos;
    }

    public void DropAllBalls()
    {
        isRemove = true;
        canDraging = true;
        

        GameManager.Instance.ballAmountText.text = GameManager.Instance.ballAmount.ToString() + "X";


        foreach (var item in GameManager.Instance.BLOCKS)
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        
        foreach (var item in Features.Instance.bomb)
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
        }


        foreach (var item in Features.Instance.bolt)
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
        }  
        
        foreach (var item in Features.Instance.electricities)
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        foreach (var item in balls)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 20f;
        }

        balls.Clear();
      //  GameManager.Instance.FixAmount = GameManager.Instance.ballAmounts[turnCounter + 1];
      //  GameManager.Instance.ballAmount = GameManager.Instance.FixAmount;

    }

    public void RemoveAllBalls()
    {
        isRemove = true;
        GameManager.Instance.ballAmount = GameManager.Instance.FixAmount;
        foreach (var item in balls)
        {
            Destroy(item.gameObject);
        }
        balls.Clear();

        //GameManager.Instance.FixAmount = GameManager.Instance.ballAmounts[turnCounter];
       
       

        
       
       
    }




}
