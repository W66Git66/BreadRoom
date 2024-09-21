using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
<<<<<<< HEAD
    public MovePlatform movePlatform;
=======
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

>>>>>>> a2aeafe81dcec558212408a9e9367cad7c1cd159
    // Start is called before the first frame update
    void Start()
    {
        target_DoorPos = Pos_DoorClose;
        target_buttonPos = Pos_ButtonOff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision!=null) 
        {
            if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Snow"))
<<<<<<< HEAD
            {
                movePlatform.ChangePosition();
=======
            {
                isDoorOpen = true;
>>>>>>> a2aeafe81dcec558212408a9e9367cad7c1cd159
            }
        }
    }

    private void  OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Snow"))
            {
<<<<<<< HEAD
                movePlatform.ReturnPosition();
=======
               isDoorOpen= false;
>>>>>>> a2aeafe81dcec558212408a9e9367cad7c1cd159
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
