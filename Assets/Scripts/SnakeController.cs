using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveInterval = 0.18f;
    public float gridSize = 1f;

    [Header("地图边界")]
    public float boundX = 30f;
    public float boundZ = 30f;

    [Header("身体预制体")]
    public GameObject bodyPrefab;
    private ObjectPool objectPool;

    // 当前方向
    private Vector3 direction = Vector3.forward;

    // 下一次方向
    private Vector3 nextDirection;

    // 计时器
    private float timer;

    // 游戏结束
    private bool isGameOver = false;

    public GameObject gameOverPanel;

    // 身体列表
    private List<Transform> bodyParts = new List<Transform>();

    // 历史位置
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        nextDirection = direction;
        objectPool = FindObjectOfType<ObjectPool>();

        positions.Add(transform.position);

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        HandleInput();

        timer += Time.deltaTime;

        if (timer >= moveInterval)
        {
            Move();

            timer = 0f;
        }
    }

    // =========================
    // 输入控制
    // =========================
    void HandleInput()
    {
        // W
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Vector3.back)
            {
                nextDirection = Vector3.forward;
            }
        }

        // S
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector3.forward)
            {
                nextDirection = Vector3.back;
            }
        }

        // A
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector3.right)
            {
                nextDirection = Vector3.left;
            }
        }

        // D
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Vector3.left)
            {
                nextDirection = Vector3.right;
            }
        }
    }

    // =========================
    // 移动
    // =========================
    void Move()
    {
        // 更新方向
        direction = nextDirection;

        // 旧位置
        Vector3 oldPosition = transform.position;

        // 新位置
        Vector3 newPosition = oldPosition + direction * gridSize;

        // =========================
        // 撞墙检测
        // =========================
        if (newPosition.x > boundX ||
            newPosition.x < -boundX ||
            newPosition.z > boundZ ||
            newPosition.z < -boundZ)
        {
            GameOver();
            return;
        }

        // =========================
        // 撞身体检测（修复版）
        // 从第二节开始检测
        // =========================
        for (int i = 1; i < bodyParts.Count; i++)
        {
            float distance = Vector3.Distance(
                newPosition,
                bodyParts[i].position
            );

            if (distance < 0.1f)
            {
                GameOver();
                return;
            }
        }

        // 移动蛇头
        transform.position = newPosition;

        // 朝向
        transform.forward = direction;

        // 保存历史位置
        positions.Insert(0, oldPosition);

        // 更新身体
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (i < positions.Count)
            {
                bodyParts[i].position = positions[i];
            }
        }

        // 限制历史记录
        if (positions.Count > 1000)
        {
            positions.RemoveAt(positions.Count - 1);
        }
    }

    // =========================
    // 吃食物
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food))
        {
            food.RandomSpawn();

            Grow();

            AudioManager.Instance.PlayEatSound();

            GameManager.Instance.AddScore(1);
        }

        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    // =========================
    // 增长身体
    // =========================
    void Grow()
    {
        Vector3 spawnPosition;

        if (bodyParts.Count == 0)
        {
            spawnPosition = transform.position;
        }
        else
        {
            spawnPosition = bodyParts[bodyParts.Count - 1].position;
        }

        // 从对象池获取
        GameObject body = objectPool.GetObject();

        body.transform.position = spawnPosition;

        body.transform.rotation = Quaternion.identity;

        bodyParts.Add(body.transform);
    }

    // =========================
    // 游戏结束
    // =========================
    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        isGameOver = true;

        AudioManager.Instance.PlayGameOverSound();

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Game Over!");
    }
}