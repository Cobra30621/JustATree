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

    public int nowWave = 0;
    /// <summary>每幾秒鐘出一波敵人 </summary>
    public float nextWaveTime = 5;

    private void Reset()
    {
        nowWave = 0;
    }

    void GameOver()
    {
        //遊戲結束，Reset東西有的沒的
    }
}
