﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField]
    int playerLives = 3;
    [SerializeField]
    int score = 0;

    [SerializeField]
    Text livesText;
    [SerializeField]
    Text scoreText;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        }else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}
	
    public void ProcessPlayerDeath() {
        if(playerLives > 1) {
            TakeLife();

        }else {
            ResetGameSession();
        }
    }

    private void TakeLife() {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddScore(int scoreIn) {
        score += scoreIn;
        scoreText.text = score.ToString();
    }
}
