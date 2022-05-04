using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _currentScore;
    
    public static event Action OnNewRecordAchiew;

    private void ScoreChanger(int score)
    {
        if (_currentScore < score)
        {
            _currentScore = score;
            OnNewRecordAchiew?.Invoke();
        }
    }

    private void OnEnable() => TileCubeSystem.OnScoreChange += ScoreChanger;
    private void OnDisable() => TileCubeSystem.OnScoreChange -= ScoreChanger;
}
