using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1f;
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myBoxCollider;
    private int currentDirection = 1;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(enemySpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemySpeed = -enemySpeed;
        currentDirection = -currentDirection;
        transform.localScale = new Vector2(currentDirection, 1f);
    }
}
