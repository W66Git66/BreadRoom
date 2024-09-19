using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : Singleton<CountManager>
{
    [HideInInspector] public int index;//Íæ¼ÒÐòºÅ

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
