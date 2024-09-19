using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : Singleton<ChangePlayer> 
{
    public GameObject player1;
    public GameObject player2;

    protected override void Awake()
    {
        base.Awake();
        if(CountManager.Instance.index==0)
        {
            player1.gameObject.SetActive(true);
            CountManager.Instance.index++;
        }
        else
        {
            player2.gameObject.SetActive(true);
        }

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
