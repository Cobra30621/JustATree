using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int nowWave = 0;
    /// <summary>每幾秒鐘出一波敵人 </summary>
    public float nextWaveTime = 100;

    public GameObject[] EndPrefabs;
    public Transform EndSpawnTransform;
    
    public float start_time = 10;
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

    private void Reset()
    {
        nowWave = 0;
        timer = start_time;
    }

    void Update()
    {
        Countdown();
    }
    
    void Countdown(){
        if(gameOver){return;}

        timer -= Time.deltaTime;
        if(timer < 0){
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
    
    public void PlayEnd(EndType endType )
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
}

public enum EndType
{
    TreeDied = 0, TimeOut = 1, Core = 2, Tentacle = 3, Sky= 4, MuscleProtein=5
}

// public class EndClip{
//     
// }