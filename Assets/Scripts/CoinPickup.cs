using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] private int pointsForCoinPicup = 100;

    private bool hasCollected = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !hasCollected)
        {
            hasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPicup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
