using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake() {
        if(FindObjectsOfType<GameManager>().Length >1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        scoreText.text = score.ToString();
    }

    public void AddPointsToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
