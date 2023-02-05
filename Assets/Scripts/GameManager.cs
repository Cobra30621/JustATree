using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManger").AddComponent<GameManager>();
            }
            return instance;
        }
    }
    private static GameManager instance;
    [SerializeField]
    public List<EnemyAI> enemyList = new List<EnemyAI>();

    /// <summary>每幾秒鐘出一波敵人 </summary>
    public float nextWaveTime = 15;

    /// <summary>當前生成敵人波次 (超過設定會取用max波次值) </summary>
    public int nowWave = 0;
    [Header("End Prefabs")] 
    public GameObject[] EndPrefabs;

    public Transform EndSpawnTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Reset() //參數初始化
    {
        nowWave = 0;
    }

    public void GameOver()
    {
        //遊戲結束，Reset東西有的沒的
    }

    public EndType testEnd;
    [ContextMenu("test")]
    public void Test()
    {
        PlayEnd(testEnd);
    }
    
    public void PlayEnd(EndType endType )
    {
        switch (endType)
        {
            case EndType.TreeDied:
                Instantiate(EndPrefabs[0], EndSpawnTransform);
                break;
            case EndType.TimeOut:
                Instantiate(EndPrefabs[1], EndSpawnTransform);
                break;
        }
    }
}

public enum EndType
{
    TreeDied, TimeOut, Core, Tentacle, Sky, MuscleProtein
}

// public class EndClip{
//     
// }