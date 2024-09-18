using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    public float speed;
    private Vector3 movement;
    private PlayerInput input;
    private Transform playerTransform;
    private void Awake()
    {
        playerTransform=GetComponent<Transform>();
        PlayerMiddle.Instance.GetPlayer(playerTransform);
    }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMove;
        input.actions["Jump"].performed += OnJump;
        input.actions["Jump"].canceled += OnJump;
    }

    private void LateUpdate()
    {
        transform.position += speed*Time.fixedDeltaTime*movement;
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            movement = Vector3.zero;
        }
        else
        {
            var vector2 = context.ReadValue<Vector2>();
            movement = new Vector3(vector2.x, vector2.y, 0);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("11");
    }
}
