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
    public Transform _sprite;

    public Vector2 platform;
    public Vector2 normal;

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
            _capsuleCollider2D.enabled = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _rigidbody2D.excludeLayers = 0;
            _sprite.Translate(new Vector3(0,-0.45f,0));
            gameObject.layer = 6;
            sprite.sprite = platformState;
            sprite.size = platform;
            
        }
        //变回吐司
        else
        {
            tusiState = 0;
            _collider2D.enabled = false;
            playerController.enabled = true;
            _capsuleCollider2D.enabled = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.excludeLayers = 8;
            _sprite.Translate(new Vector3(0, 0.45f, 0));
            gameObject.layer = 7;
            sprite.sprite = normalState;
            sprite.size=normal;
            
        }
    }
}
