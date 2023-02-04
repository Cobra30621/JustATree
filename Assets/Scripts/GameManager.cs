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
    /// <summary>�C�X�����X�@�i�ĤH </summary>
    public float nextWaveTime = 100;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Reset()
    {
        nowWave = 0;
    }

    void GameOver()
    {
        //�C�������AReset�F�観���S��
    }
}
