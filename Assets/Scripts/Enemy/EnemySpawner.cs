using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    [SerializeField]
    Transform enemyRoot = null;

    [System.Serializable]
    public struct WaveData
    {
        public EnemySpawnData[] enemySpawnData;
    }

    [System.Serializable]
    public struct EnemySpawnData
    {
        public Transform spawnPoint;
        public int count;
        public int enemyType;
    }

    //生怪資料設定
    public List<WaveData> waveDatas;

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;

        //時間到，生成下一波怪
        if (timer >= GameManager.Instance.nextWaveTime)
        {
            timer = 0;
            SpawnWaveEnemy();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            EnemyAI e = Instantiate(enemyPrefabs[0], Vector3.right * 10, Quaternion.identity).GetComponent<EnemyAI>();

            GameManager.Instance.enemyList.Add(e);
        }
    }
    public void SpawnWaveEnemy()
    {
        WaveData nowWaveData;
        //超出範圍
        if (GameManager.Instance.nowWave > waveDatas.Count - 1)
        {
            nowWaveData = waveDatas[waveDatas.Count - 1];
        }
        else
            nowWaveData = waveDatas[GameManager.Instance.nowWave];

        for (int i = 0; i < nowWaveData.enemySpawnData.Length; i++)
        {
            EnemySpawnData spawnData = nowWaveData.enemySpawnData[i];
            SpawnEnemies(spawnData);
        }
        GameManager.Instance.nowWave++;
    }
    //IEnumerator DelaySpawnWaveEnemy(Vector3 spawnPosition, int count, int enemyType, float delay = 0.1f)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SpawnEnemies(spawnPosition, count, enemyType);
    //    yield return null;
    //}
    void SpawnEnemies(EnemySpawnData spawnData)
    {
        Vector3 spawnPosition = spawnData.spawnPoint.position;
        int count = spawnData.count;
        int enemyType = spawnData.enemyType;

        int dir = spawnPosition.x > 0 ? 1 : -1;  //判斷怪物生成在左邊還是右邊

        for (int i = 0; i < count; i++)
        {
            EnemyAI enemy = Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity).GetComponent<EnemyAI>();
            enemy.transform.parent = enemyRoot;
            enemy.transform.position = spawnPosition + new Vector3(i * dir, 0, 0);

            GameManager.Instance.enemyList.Add(enemy);
        }
    }
}