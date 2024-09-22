using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TarodevController;
public class Tusi : MonoBehaviour
{
    public PlayerController playerController;

    public SpriteRenderer sprite;
    public Sprite platformState;
    public Sprite normalState;

    private CapsuleCollider2D _capsuleCollider2D;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _collider2D;
    private int tusiState;//0为吐司，1为平台
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        _collider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    public void ChangeState()
    {
        //变成平台
        if(tusiState==0)
        {
            tusiState = 1;
            _collider2D.enabled = true;
            playerController.enabled = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            sprite.sprite = platformState;
            _capsuleCollider2D.enabled = false;
        }
        //变回吐司
        else
        {
            tusiState = 0;
            _collider2D.enabled = false;
            playerController.enabled = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            sprite.sprite = normalState;
            _capsuleCollider2D.enabled = true;
        }
    }
}
