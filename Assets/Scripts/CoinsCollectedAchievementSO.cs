using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "CoinsCollectedAchievementSO", fileName = "ScriptableObject/NewCoinsCollectedAchievementSO")]
public class CoinsCollectedAchievementSO : ScriptableObject
{
    public CoinsCollectedAchievements[] coinscollectedArray;

    [Serializable]
    public class CoinsCollectedAchievements
    {

        public CoinsAchievementsType coinsAchievementsType;
        public int requirement;

    }
}

public enum CoinsAchievementsType
{
    None,
    Commando,
    Prediator,
    DeathBringer
}
