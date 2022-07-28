using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{
    public static AchievementController instance;


    #region Events
    public event Action OnPlayerCollectedCoins;
    public event Action OnEnemyKilled;
    #endregion

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void InvokeOnPlayerCollectedCoinstEvent()
    {
        OnPlayerCollectedCoins?.Invoke();
    }

    public void InvokeOnEnemyEvent()
    {
        OnEnemyKilled?.Invoke();
    }
}
