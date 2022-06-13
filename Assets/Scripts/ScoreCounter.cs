using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _currentGameScore = 0;
    private int _currentCubeMaxScore = 0;
    
    public static Action<int> RecordAchieveValue;
    
    private void ScoreChanger(int score)
    {
        _currentGameScore += score;
        RecordAchieveValue?.Invoke(_currentGameScore);
        if (_currentCubeMaxScore >= score) 
            return;
        _currentCubeMaxScore = score;
    }

    private void OnEnable() => TileCubeSystem.OnScoreChange += ScoreChanger;
    
    private void OnDisable() => TileCubeSystem.OnScoreChange -= ScoreChanger;
}
