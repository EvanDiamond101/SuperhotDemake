﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [SerializeField] private GameObject inGameScoreUI;
    [SerializeField] private GameObject endGameScoreUI;
    [SerializeField] private GameObject nextLevelPrompt;

    [SerializeField] private AudioSource mainMusicSFX;
    [SerializeField] private AudioSource introSFX;
    [SerializeField] private AudioSource victorySFX;
    [SerializeField] private AudioSource victoryStingSFX;
    [SerializeField] private AudioSource defeatSFX;
    [SerializeField] private AudioSource defeatStingSFX;
    private float totalScore = 0;

    [HideInInspector] public List<float> LevelTargetTime = new List<float>();
    private float timeInLevel = 0.0f;

    public bool canStartGame = false;
    public bool canGoToNextLevel = false;

    /* Monitor the time spent in a level */
    public void Update()
    {
        timeInLevel += Time.deltaTime;
        inGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "LEVEL SCORE:\n" + CalculateLevelScore();
    }

    /* Calculate the current score realtime */
    public int CalculateLevelScore()
    {
        float levelScore = LevelTargetTime[LevelLoader.Instance.GetCurrentLevelIndex()] - timeInLevel;
        if (levelScore <= 0) return 0;
        levelScore *= 10;

        return (int)levelScore;
    }

    /* Add to the score based on time in level */
    public void ClearTimeScore()
    {
        timeInLevel = 0.0f;
        totalScore += CalculateLevelScore();
    }

    /* Clear the total score */
    public void ClearAllScore()
    {
        totalScore = 0;
    }

    /* Get the total score */
    public float GetTotalScore()
    {
        return totalScore;
    }

    /* Show the intro UI */
    public void ShowIntroScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canStartGame = true;

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "SUPER HOT\n\nPRESS B";
        introSFX.Play();
        mainMusicSFX.Stop();
    }
    
    /* Hide the intro UI */
    public void HideIntroScreen()
    {
        inGameScoreUI.SetActive(true);
        endGameScoreUI.SetActive(false);
        nextLevelPrompt.SetActive(false);
        canStartGame = false;

        victorySFX.Stop();
        defeatSFX.Stop();
        introSFX.Stop();
        mainMusicSFX.Play();
    }

    /* Show "next level" ui prompt */
    public void ShowNextLevelPrompt()
    {
        inGameScoreUI.SetActive(false);
        nextLevelPrompt.SetActive(true);
        canGoToNextLevel = true;

        mainMusicSFX.Stop();
    }

    /* Hide "next level" ui prompt */
    public void HideNextLevelPrompt()
    {
        inGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canGoToNextLevel = false;

        mainMusicSFX.Play();
    }

    /* Show the time score UI */
    public void ShowVictoryScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canStartGame = true;

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "YOU WON\n\nSCORE: " + ((int)totalScore-70) + "\n\nPRESS B";
        victorySFX.Play();
        victoryStingSFX.Play();
        mainMusicSFX.Stop();
    }

    /* Show the defeat screen */
    public void ShowDefeatScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canStartGame = true;

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "DEFEAT\n\nSCORE: " + ((int)totalScore-70) + "\n\nPRESS B";
        defeatSFX.Play();
        defeatStingSFX.Play();
        mainMusicSFX.Stop();
    }
}
