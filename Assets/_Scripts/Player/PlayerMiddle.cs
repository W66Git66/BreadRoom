using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiddle : Singleton<PlayerMiddle>
{
    public Transform[] playerTransform;
    private int playerIndex=0;
    private Vector3 middlePosition;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
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
}
