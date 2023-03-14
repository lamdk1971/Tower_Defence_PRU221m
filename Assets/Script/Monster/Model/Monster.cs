using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Monster : MonoBehaviour, IMonster
{
    // C�c th�ng so cua monster
    public float health;
    public float speed;
    public Vector3[] path = new Vector3[10];

    private float currentHealth;
    private Image healthBar;
    private Tween tween;
    // C�c behavior chung cua monster

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBarHandler>().FillAmountImage;
        currentHealth = health;
        this.transform.DOScaleX(1,0);
    }
    public void Move()
    {
        // monster di chuyen den dich
        this.transform
            .DOPath(path, 15, PathType.Linear)
            .OnWaypointChange(MyWaypointChangeHandler)
            .OnComplete(reachTarget);
    }

    // take dmg -> -hp, run animation -> die
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        hitAnimation();
        Debug.Log(currentHealth + "-" + health);
        healthBarGetDamge();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // doing
    public void MyWaypointChangeHandler(int waypointIndex)
    {
        // N?u Tween ?i qua waypoint th? hai
        if (waypointIndex + 1 <= path.Length - 1)
        {
            var direction = (path[waypointIndex] - path[waypointIndex + 1]).normalized;
            Debug.Log(direction);
            if (direction.x > 0)
            {
                Debug.Log("turn left");
                this.transform.DOScaleX(1, 0);
            }
            else 
            {
                Debug.Log("turn right");
                this.transform.DOScaleX(-1, 0);
            }
        }
    }
    // xu li khi monster bi tieu diet
    public void Die()
    {
        //temp
        Destroy(gameObject);

        //To Do: add: + tien
    }

    // xu li khi monster den dich
    public void reachTarget() 
    {
        //temp
        Destroy(gameObject);

        // To Do : - tim <3 
    }

    private void hitAnimation()
    {
        GetComponent<HandleAnimation>().monsterHitted();
    }

    private void healthBarGetDamge() 
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,
            currentHealth / health,
            10f);
    }

    // hanh vi dac biet cua tung loai monster
    public abstract void SpecialAbility();

    
}
