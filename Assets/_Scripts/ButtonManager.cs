using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private bool isDoorOpen = false;

    public Transform Pos_DoorClose;
    public Transform Pos_DoorOpen;

    public Transform target_DoorPos;

    public float moveSpeed = 5f;

    public Transform Pos_ButtonOff;
    public Transform Pos_buttonOn;

    public Transform target_buttonPos;

    public GameObject Door;
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        target_DoorPos = Pos_DoorClose;
        target_buttonPos = Pos_ButtonOff;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision!=null) 
        {
            if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Snow"))
            {
                isDoorOpen = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Snow"))
            {
               isDoorOpen= false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isDoorOpen)
        {
            target_DoorPos = Pos_DoorOpen;
            target_buttonPos = Pos_buttonOn;
        }else
        {
            target_DoorPos = Pos_DoorClose;
            target_buttonPos = Pos_ButtonOff;
        }

        Door.transform.position = Vector2.MoveTowards(Door.transform.position, target_DoorPos.position, moveSpeed * Time.deltaTime);
        Button.transform.position = Vector2.MoveTowards(transform.position, target_buttonPos.position, moveSpeed * Time.deltaTime);
    }

}
