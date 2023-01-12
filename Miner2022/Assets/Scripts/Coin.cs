using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int points = 10;

    GameManager gameManager;
    SoundManager soundManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            gameManager.AddPointsToScore(points);
            soundManager.PlayCoinCollectedSFX();
            Destroy(gameObject);
        }
    }
}
