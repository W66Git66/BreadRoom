using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMiddle : Singleton<PlayerMiddle>
{
    public Transform[] playerTransform;
    private int playerIndex=0;
    private Vector3 middlePosition;

    public CinemachineVirtualCamera virtualCamera;
    public float maxSize;
    public float mainSize;
    protected override void Awake()
    {
        base.Awake();
        ChangeCameraSize();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Update()
    {
        ChangeCameraSize();
    }
    private void LateUpdate()
    {
        getMiddle();
        transform.position = middlePosition;
        
    }

    public void GetPlayer(Transform transform)
    {
        playerTransform[playerIndex] = transform;
        playerIndex++;
    }

    private void getMiddle()
    {
        if (playerIndex==0)
        {
            middlePosition = Vector3.zero;
        }
        else if(playerIndex==1)
        {
            middlePosition = playerTransform[0].position;
        }
        else
        {
            middlePosition = (playerTransform[0].position+playerTransform[1].position)/2;
        }
    }

    public void ChangeCameraSize()
    {
        
        virtualCamera.m_Lens.OrthographicSize += Mathf.Lerp(0,5,0.1f);
    }

}
