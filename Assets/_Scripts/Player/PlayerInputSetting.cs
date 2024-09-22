using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSetting : MonoBehaviour
{
    public float speed;
    [HideInInspector]public Vector3 movement;
    [HideInInspector] public bool isJumpDown;
    [HideInInspector] public bool isJumpHeld;
    private PlayerInput input;
    private Transform playerTransform;

    private Tusi tusi;

    private int index;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        tusi = GetComponent<Tusi>();

        playerTransform = GetComponent<Transform>();
        PlayerMiddle.Instance.GetPlayer(playerTransform);
    }
    private void OnEnable()
    {
        transform.position = new Vector3(-50, 3, 1);
    }
    private void Start()
    {
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMove;

    }

    private void Update()
    {
        OnJumpDown();
        OnJumpHeld();

        OnChangeState();
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

    private void OnJumpHeld()
    {
        if(isJumpDown)
        {
            isJumpHeld = true;
            if(input.actions["Jump"].WasReleasedThisFrame())
            {
                isJumpHeld = false;
            }
        }
    }

    private void OnJumpDown()
    {
        isJumpDown = input.actions["Jump"].WasPressedThisFrame();
    }

    private void OnChangeState()
    {
        if (input.actions["ChangeState"].WasPressedThisFrame())
        {
            tusi.ChangeState();
        }
    }
}
