using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cream : MonoBehaviour
{
    public Vector2 jumpforce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(jumpforce,ForceMode2D.Impulse);
        }
    }
}
