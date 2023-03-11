using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField] private int Value;
    [SerializeField] TextMeshProUGUI ValueText;

    private SpriteRenderer sprite;
   
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        ValueText.text = GetValue().ToString();
        SetColor(Value);

        if (Value <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.BLOCKS.Remove(this.gameObject);
            GameManager.Instance.ChechWin();
        }

        
            
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("finish");
            GameManager.Instance.SetLoseAction();
        }
        
    }


    public void SetColor(int Value)
    {
        if (Value >= 150 && Value >=100)  // 80 ile 100 arasýnda
            sprite.color = Color.blue;
        
        else if (Value >= 100 && Value >=80)  // 80 ile 100 arasýnda
            sprite.color = Color.red;
        
        else if (Value < 80 && Value >= 40)  // 40 ile 80 arasýnda
            sprite.color = Color.green;
        
        else if (Value < 40 && Value >= 20)  // 40 ile 80 arasýnda
            sprite.color = Color.yellow;
        
        else if (Value < 20 && Value >= 0)  // 40 ile 80 arasýnda
            sprite.color = Color.cyan;

    }

    public float CalculateDistance(GameObject g1,GameObject g2)
    {
        return Vector3.Distance(g1.transform.position, g2.transform.position);
    }

    #region Getters Setters


    public int GetValue()
    {
        return Value;
    }

    public void SetValue(int Value)
    {
        this.Value = Value;
    }

    #endregion
}
