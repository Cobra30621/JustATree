using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

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

        if (timer >= GameManager.Instance.nextWaveTime) //生怪時間到，開始生成敵人
        {
            timer = 0;
            //出下一波怪
        }
    }
    public void SpawnWaveEnemy()
    {
        WaveData nowWaveData = waveDatas[GameManager.Instance.nowWave];

        for (int i = 0; i < nowWaveData.enemySpawnData.Length; i++)
        {
            EnemySpawnData spawnData = nowWaveData.enemySpawnData[i];
            StartCoroutine(DelaySpawnWaveEnemy(spawnData.spawnPoint.position, spawnData.count, spawnData.enemyType));
        }
    }
    IEnumerator DelaySpawnWaveEnemy(Vector3 spawnPosition, int count, int enemyType, float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemies(spawnPosition, count, enemyType);
        yield return null;
    }
    void SpawnEnemies(Vector3 spawnPosition, int count, int enemyType)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity);
            enemy.transform.position = spawnPosition + new Vector3(i * 2, 0, 0);
        }
    }
}