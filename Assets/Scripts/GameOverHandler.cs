using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private float restartDelay = 2f;

    [SerializeField] private GameObject enemy;

    private void Update()
    {
        EndGame();
    }

    private void EndGame()
    {
        
        if (enemy.activeSelf) { return; }
        
        scoreSystem.enabled = false;
        Invoke(nameof(RestartLevel), restartDelay);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
        
    }
}
