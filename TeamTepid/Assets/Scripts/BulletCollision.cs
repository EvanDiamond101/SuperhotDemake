﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [HideInInspector] public string ignoreCollisionTag = "";

    /* When a bullet hits something, do stuff */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ScoreManager.Instance.canGoToNextLevel) return;

        if (collider.GetType() == typeof(CircleCollider2D))
        {
            return;
        }
        //Kill if we hit a player or AI
        if (collider.CompareTag(ignoreCollisionTag) || collider.CompareTag("Bullet") || collider.CompareTag("Prop"))
        {
            return;
        }

        if (ignoreCollisionTag != "Player" && collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collider.gameObject.GetComponent<ThePlayer>().Kill();
        }
        else if (ignoreCollisionTag != "Ai" && collider.gameObject.CompareTag("Ai"))
        {
            collider.gameObject.GetComponent<EnemyAI>().Kill();
        }

        //Destroy bullet
        Destroy(this.gameObject);
    }
}
