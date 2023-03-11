using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    RaycastHit2D hit;
    public LineRenderer lr;
    
    public GameObject[] points;
    public int TotalBounce;
    public float LineOffset = 0.01f;

    private void Start()
    {
        
        lr.positionCount = TotalBounce;
        

       
        lr.SetPosition(0, points[0].transform.position);
        //lr.SetPosition(1, points[1].transform.position);
        lr.enabled = true;

       


    }

    private void Update()
    {
        if (ShootSystem.Instance.isDrag)
        {
            lr.gameObject.SetActive(true);
            Draw();
        }
        else
            lr.gameObject.SetActive(false);
          
        
    }

    public void Draw()
    {
        Vector2 direction = transform.up;
        Vector2 origin = (Vector2)transform.position + LineOffset * direction;

        for (int i = 1; i < TotalBounce; i++)
        {
            hit = Physics2D.Raycast(origin, direction);
            Debug.DrawLine(origin, hit.point);
           
            lr.SetPosition(i, hit.point);
           

            direction = Vector2.Reflect(direction.normalized, hit.normal);
            origin = hit.point + LineOffset * direction;
        }
    }
}
