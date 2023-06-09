﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private int damage;
    [SerializeField] private Material material;
    [SerializeField] private float size;
    [SerializeField] private Renderer sphereRenderer;

    void Update()
    {
        StartCoroutine(DisableAfterTime());
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        ObjectPool.Instance.ReturnToPool(Constant.TAG_BULLET, gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_TRAP))
        {
            ObjectPool.Instance.ReturnToPool(Constant.TAG_BULLET, gameObject);
        }
        else if (other.CompareTag(Constant.TAG_COLUMN))
        {
            ObjectPool.Instance.ReturnToPool(Constant.TAG_BULLET, gameObject);
            CylindricalTrap hurtTrap = other.gameObject.GetComponent<CylindricalTrap>();
            hurtTrap.TakeDamage(damage);
          
        }
        else if (other.CompareTag(Constant.TAG_FINISH))
        {
            ObjectPool.Instance.ReturnToPool(Constant.TAG_BULLET , gameObject);
        }
        else if (other.CompareTag(Constant.TAG_MONSTER))
        {
            ObjectPool.Instance.ReturnToPool(Constant.TAG_BULLET, gameObject);
            MonsterHealth monsterHealth = other.gameObject.GetComponent<MonsterHealth>();
            monsterHealth.TakeDamage(damage);
        }
    }

    public void SetBulletProperties(BulletData bulletData)
    {
        damage = bulletData.damage;
        size = bulletData.size;
        material = bulletData.material;
        //Debug.Log("Bullet Properties: " + damage + "  " + size + " " + material);

        transform.localScale = Vector3.one * size;
        sphereRenderer.material = material;
    }
}