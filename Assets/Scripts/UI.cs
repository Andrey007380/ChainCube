using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI startText;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private GameObject restartPanel;

    private string _languageType;

    private void RestartPanel() => restartPanel.SetActive(true);

    private void OnEnable()
    {
        Score.RecordAchieveValue += i => scoreText.text = "Score: " + i;
        Spawner.OnLose += RestartPanel;
    }

    private void OnDisable() => Spawner.OnLose -= RestartPanel;
}
