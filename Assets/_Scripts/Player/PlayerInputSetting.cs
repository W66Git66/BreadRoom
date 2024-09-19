using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSetting : MonoBehaviour
{
    public float speed;
    [HideInInspector]public Vector3 movement;
    [HideInInspector] public bool isJump;
    private PlayerInput input;
    private Transform playerTransform;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        playerTransform = GetComponent<Transform>();
        PlayerMiddle.Instance.GetPlayer(playerTransform);
    }

    private void Start()
    {
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMove;
        input.actions["Jump"].performed += OnJump;
        input.actions["Jump"].canceled += OnJump;
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
        isJump = context.ReadValueAsButton();
    }
}
