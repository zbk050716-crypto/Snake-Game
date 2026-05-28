using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;

    // 分数
    private int score = 0;

    // Level
    private int level = 1;

    // 单例
    public static GameManager Instance;

    private ObstaclePool obstaclePool;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        obstaclePool =
        FindObjectOfType<ObstaclePool>();

        UpdateUI();

        UpdateSnakeSpeed();
    }

    // 增加分数
    public void AddScore(int amount)
    {
        score += amount;

        // 更新Level
        level = score / 5 + 1;

        // 每5分生成一个障碍
        if (score % 5 == 0)
        {
            SpawnObstacle();

            AudioManager.Instance.PlayLevelUpSound();
        }

        // 更新UI
        UpdateUI();

        // 更新速度
        UpdateSnakeSpeed();
    }

    // 更新UI
    void UpdateUI()
    {
        scoreText.text = "Score : " + score;

        levelText.text = "Level : " + level;
    }

    // Level提升后加速
    void UpdateSnakeSpeed()
    {
        SnakeController snake =
            FindObjectOfType<SnakeController>();

        if (snake != null)
        {
            // Level越高速度越快
            snake.moveInterval =
            Mathf.Clamp(
            0.18f - (level - 1) * 0.01f,
            0.06f,
            0.18f);
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle =
            obstaclePool.GetObstacle();

        // 随机位置
        SnakeController snake =
    FindObjectOfType<SnakeController>();

        int x = Random.Range(
            -(int)snake.boundX + 1,
            (int)snake.boundX);

        int z = Random.Range(
            -(int)snake.boundZ + 1,
            (int)snake.boundZ);

        obstacle.transform.position =
            new Vector3(x, 0.5f, z);
    }
}
