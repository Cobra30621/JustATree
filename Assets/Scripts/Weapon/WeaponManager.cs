using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
	
	public List<Bullet.BulletProperty> properties = new List<Bullet.BulletProperty>();
	public GameObject bulletPrefab;
	
	public Dictionary<Bullet.BulletType, int> currentWeapon = new Dictionary<Bullet.BulletType, int>();
	public Dictionary<Bullet.BulletType, Coroutine> coroutines = new Dictionary<Bullet.BulletType, Coroutine>();

	// debug
	public GameObject enemyAPrefab;
	
	List<EnemyAI> enemyList;
	float fireTime;

	public void GetWeapon(Bullet.BulletType type)
	{
		if (currentWeapon.ContainsKey(type))
		{
			currentWeapon[type]++;
		}
		else
		{
			currentWeapon[type] = 1;
		}

		if (coroutines.ContainsKey(type))
		{
			StopCoroutine(coroutines[type]);
		}

		coroutines[type] = StartCoroutine(Shoot(type));
	}


	void Update()
	{
	}

	IEnumerator Shoot(Bullet.BulletType bulletType)
	{
		Bullet.BulletProperty property = properties[currentWeapon[bulletType] - 1];
		while (true)
		{
			yield return new WaitForSeconds(property.spawnTime);
			GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
			Bullet bullet = bulletObj.GetComponent<Bullet>();
		
			EnemyAI closestEnemy = null;
			closestEnemy = GetClosestEnemy(bulletObj);
			if (closestEnemy != null)
			{
				bullet.Init(this, closestEnemy, property, bulletType);
			}
			else
			{
				Destroy(bulletObj);
			}
		}
	}

	public EnemyAI GetClosestEnemy(GameObject bulletObj)
	{
		EnemyAI closestEnemy = null;
		float closestDistance = float.MaxValue;
		enemyList = GameManager.Instance.enemyList;
		foreach (EnemyAI enemy in enemyList)
		{
			if (enemy != null)
			{
				float distance = Vector3.Distance(enemy.transform.position, bulletObj.transform.position);
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestEnemy = enemy;
				}
			}
		}

		return closestEnemy;
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(100,100,100,100),"Apple"))
        {
			GetWeapon(Bullet.BulletType.APPLE);
			// Instantiate(enemyAPrefab, transform.position, transform.rotation);
        }
        if (GUI.Button(new Rect(100,200,100,100),"SWORD"))
        {
			GetWeapon(Bullet.BulletType.SWORD);
        }
        if (GUI.Button(new Rect(100,300,100,100),"COLA"))
        {
			GetWeapon(Bullet.BulletType.COLA);
        }
        if (GUI.Button(new Rect(100,400,100,100),"TRIDENT"))
        {
			GetWeapon(Bullet.BulletType.TRIDENT);
        }
    }
}