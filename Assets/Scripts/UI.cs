using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private GameObject restartPanel;

   private void RestartPanel() => restartPanel.SetActive(true);

    private void OnEnable()
    {
        ScoreCounter.RecordAchieveValue += i => scoreText.text = "Score: " + i;
        Spawner.OnLose += RestartPanel;
    }

    private void OnDisable() => Spawner.OnLose -= RestartPanel;
}
