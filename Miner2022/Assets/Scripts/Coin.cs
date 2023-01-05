using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] AudioClip collectionSound;

    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            gameManager.AddPointsToScore(points);
            AudioSource.PlayClipAtPoint(collectionSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
