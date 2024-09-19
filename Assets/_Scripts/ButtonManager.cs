using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject DoorOpen;
    public GameObject DoorClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision!=null) 
        {
            if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Snow"))
            {
                DoorClose.SetActive(false);
                DoorOpen.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Snow"))
            {
                DoorClose.SetActive(true);
                DoorOpen.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
