using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.player_actions;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System;


public class MissilePrefab : MonoBehaviour
{
    private Vector2 direction;
    private LayerMask obstacleLayer;
    private LayerMask enemyLayer;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxRange = 20f;

    private System.Action onComplete;

    public void Launch(Vector2 direction, LayerMask obstacleLayer, LayerMask enemyLayer, System.Action onComplete)
    {
        this.direction = direction;
        this.obstacleLayer = obstacleLayer;
        this.enemyLayer = enemyLayer;
        this.onComplete = onComplete;
        StartCoroutine(TravelCoroutine());
    }

    private IEnumerator TravelCoroutine()
    {
        Vector2 startPos = transform.position;
        float travelled = 0f;

        while (travelled < maxRange)
        {
            float step = speed * Time.deltaTime;
            transform.position = (Vector2)transform.position + direction * step;
            travelled += step;
            yield return null;
        }
        onComplete?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            Debug.Log("Shot hit a wall");
            onComplete?.Invoke();
            Destroy(gameObject);
            return;
        }

        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            GoblinScript hitEntity = collision.GetComponent<GoblinScript>();
            if (hitEntity != null)
            {
                if (!hitEntity.isBlocking)
                    hitEntity.damage(25);
                else
                    Debug.Log("Shot was blocked!");
            }
            onComplete?.Invoke();
            Destroy(gameObject);
            return;
        }
    }
}