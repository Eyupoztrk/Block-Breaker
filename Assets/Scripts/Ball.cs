using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;

   
    Vector3 lastVelocity;
  

    public float Angle;
  


    private Vector3 initialVelocity;
   

    [SerializeField]
    private float minVelocity = 10f;

   
    private Rigidbody2D rb;




    void Start()
    {
        
     
        rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(transform.up * Time.deltaTime * ballSpeed);
        rb.velocity = transform.up * Time.deltaTime * ballSpeed;
       //rb.AddForce(gameObject.transform.rotation.eulerAngles *ballSpeed * Time.deltaTime);

    }

    void Update()
    {

        // transform.Translate(Vector3.up * ballSpeed * Time.deltaTime);

        

    }
  

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Square"))
        {
            collision.gameObject.GetComponent<Block>().SetValue(collision.gameObject.GetComponent<Block>().GetValue() - 1);
            GameManager.Instance.BlockSound.Play();


        }

        if(collision.gameObject.CompareTag("Bottom"))
        {

            GameManager.Instance.ballAmount++;
            GameManager.Instance.ballAmountText.text = GameManager.Instance.ballAmount.ToString() + "X";

           

            ShootSystem.Instance.balls.Remove(this.gameObject);
            Destroy(this.gameObject);
            
        }



        if(collision.gameObject.CompareTag("Bomb"))
        {
            Features.Instance.bombIndex = collision.gameObject.GetComponent<Indexes>().bombIndex;
            Features.Instance.BombFeatures();
            ShootSystem.Instance.RemoveAllBalls();
           

        } 
        
        if(collision.gameObject.CompareTag("Bolt"))
        {
            Features.Instance.boltIndex = collision.gameObject.GetComponent<Indexes>().boltIndex;
            Features.Instance.BoltFaetures();
            ShootSystem.Instance.RemoveAllBalls();
           

        } 
        
        if(collision.gameObject.CompareTag("electricity"))
        {
            Features.Instance.electricityIndex = collision.gameObject.GetComponent<Indexes>().electricityIndex;
            Features.Instance.ElectricityFaetures();
            ShootSystem.Instance.RemoveAllBalls();
           

        }
       
    }

    

   
   

}
