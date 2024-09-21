using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public MovePlatform movePlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision!=null) 
        {
            if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Snow"))
            {
                movePlatform.ChangePosition();
            }
        }
    }

    private void  OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Snow"))
            {
                movePlatform.ReturnPosition();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
