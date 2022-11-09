using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    private float current_time;
    private int score;
    [SerializeField] private float spawnIntervals = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        current_time = 60;
        timerText.text = "Time: " + current_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_time > 0 && isGameActive)
        {
            current_time -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(current_time);
        }else if(current_time <= 0)
        {
            GameEnd();
        }
    }
    IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnIntervals);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void ScoreUpdate(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score;
    }
    public void GameEnd()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnIntervals /= difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTargets());
        score = 0;
        ScoreUpdate(0);
    }
}
