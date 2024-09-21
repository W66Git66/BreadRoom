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
    public Camera mainCamera;
    public float maxSize;
    public float minSize;
    public float duration;
    Vector2 worldPosLeftBottom;
    Vector2 worldPosTopRight;
    void Start()
    {

    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Update()
    {
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
            LimitPosition(playerTransform[0]);
        }
        else
        {
            middlePosition = (playerTransform[0].position+playerTransform[1].position)/2;
            LimitPosition(playerTransform[0]);
            LimitPosition(playerTransform[1]);
        }
    }

    /// <summary>
    /// 将角色位置限制在屏幕内
    /// </summary>
    /// <param name="trNeedLimit">角色的transform</param>
    public void LimitPosition(Transform trNeedLimit)
    {
        worldPosLeftBottom = mainCamera.ViewportToWorldPoint(Vector2.zero);
        worldPosTopRight = mainCamera.ViewportToWorldPoint(Vector2.one);
        trNeedLimit.position = new Vector3(Mathf.Clamp(trNeedLimit.position.x, worldPosLeftBottom.x, worldPosTopRight.x),
                                           Mathf.Clamp(trNeedLimit.position.y, worldPosLeftBottom.y, worldPosTopRight.y),
                                           trNeedLimit.position.z);
    }

     public IEnumerator ChangeToMaxCameraSize()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime+= Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(minSize, maxSize, elapsedTime/duration) ;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = maxSize;
    }

    public IEnumerator ChangeToMinCameraSize()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(maxSize, minSize, elapsedTime / duration);
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = minSize;
    }
}
