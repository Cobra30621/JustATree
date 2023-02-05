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

    [SerializeField]
    public float itemShowDistance = 10f;

    public Transform headTransform;
    /// <summary>每幾秒鐘出一波敵人 </summary>
    public float nextWaveTime = 15;

    /// <summary>當前生成敵人波次 (超過設定會取用max波次值) </summary>
    public int nowWave = 0;
    [Header("End Prefabs")]
    public GameObject[] EndPrefabs;
    public Transform EndSpawnTransform;

    public float start_time = 1000;
    public float timer;
    public bool gameOver; // once set to true, the microgame will exit

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        Reset();
    }

    private void Reset() //參數初始化
    {
        nowWave = 0;
        timer = start_time;
    }

    void Update()
    {
        Countdown();

        if(Input.GetKeyDown(KeyCode.Q)){
            PlayEnd(EndType.TreeDied);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            PlayEnd(EndType.TimeOut);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            PlayEnd(EndType.Core);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            PlayEnd(EndType.Tentacle);
        }
        if(Input.GetKeyDown(KeyCode.T)){
            PlayEnd(EndType.Sky);
        }
        if(Input.GetKeyDown(KeyCode.Y)){
            PlayEnd(EndType.MuscleProtein);
        }
    }

    void Countdown()
    {
        if (gameOver) { return; }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            gameOver = true;
            PlayEnd(EndType.TimeOut);
        }
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

    public void PlayEnd(EndType endType)
    {
        if (EndPrefabs.Length > (int)endType)
        {
            Instantiate(EndPrefabs[(int)endType], EndSpawnTransform);
            Debug.Log($"播放結局{endType}");
        }
        else
        {
            Debug.Log($"尚未放置結局{endType}的 Prefab");
        }
    }

    public float GetDistanceFromHead(Transform obj)
    {
        return Vector3.Distance(headTransform.position, obj.position);
    }
}

public enum EndType
{
    TreeDied = 0, TimeOut = 1, Core = 2, Tentacle = 3, Sky = 4, MuscleProtein = 5
}

// public class EndClip{
//     
// }