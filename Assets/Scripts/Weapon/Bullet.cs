using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
	
	[Serializable]
	public struct BulletProperty
	{
		public int level;
		public int damage;
		public float spawnTime;
		
	}
	
	[Serializable]
	public struct BulletSprite
	{
		public ItemType type;
		public Sprite sprite;
	}

	public List<BulletSprite> bulletSprites = new List<BulletSprite>();
	public float speed = 10;
	[SerializeField]
	SpriteRenderer sprite = null;
	WeaponManager weaponManager;
	EnemyAI target;
	BulletProperty property;

	public void Init(WeaponManager weaponManager, EnemyAI target, BulletProperty property, ItemType type)
	{
		this.weaponManager = weaponManager;
		this.target = target;
		this.property = property;
		
		foreach (BulletSprite bulletSprite in bulletSprites)
		{
			if (bulletSprite.type == type)
			{
				sprite.sprite = bulletSprite.sprite;
				break;
			}
		}
	}

	public void ChangeTarget(EnemyAI target)
	{
		this.target = target;
	}

	public void ChangeProperty(BulletProperty property)
	{
		this.property = property;
	}


	void Update()
	{
		if (target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
			
		}
		else
		{
			target = weaponManager.GetClosestEnemy(gameObject);
			if (target == null)
			{
				Destroy(gameObject);
			}
		}

		transform.Rotate(0, 0, 360 * Time.deltaTime % 360);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		EnemyAI enemy = other.GetComponent<EnemyAI>();
		if (enemy != null)
		{
			enemy.TakeDamage(property.damage);
			AudioManager.Instance.StartPlayOnce(3);
			Destroy(gameObject); 
		}
	}
}