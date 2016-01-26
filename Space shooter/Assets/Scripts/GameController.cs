using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject enemy;
    public GameObject boss;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText = null;
    public GUIText restartText = null;
    public GUIText gameOverText = null;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool bossBattle = false;
    private float bossBattleScore = 100;

    void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnEnemyWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnEnemyWaves() {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            float x = UnityEngine.Random.Range(-spawnValues.x, spawnValues.x);
        for (int i = 0; i < 5; ++i) {

            Vector3 spawnPosition = new Vector3(x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = enemy.transform.rotation;
            GameObject new_enemy = Instantiate(enemy, spawnPosition, spawnRotation) as GameObject;
                new_enemy.AddComponent<MoverEnemy2>();
            yield return new WaitForSeconds(1);
        }
            yield return new WaitForSeconds(5);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

            if (bossBattle)
            {
                break;
            }
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
            if (bossBattle)
            {
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        if (score > bossBattleScore)
        {
            bossBattleScore += 500;
            beginBossBattle();
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void BossOver()
    {
        bossBattle = false;
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnEnemyWaves());
    }
    public void beginBossBattle()
    {
        Instantiate(boss, boss.transform.position, boss.transform.rotation);
        bossBattle = true;
    }
}
