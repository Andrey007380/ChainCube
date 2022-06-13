using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int _currentGameScore;
    [SerializeField] private int _currentCubeMaxScore;
    
    public static Action OnNewRecordAchieve;
    
    public static Action<int> RecordAchieveValue;
    
    private void ScoreChanger(int score)
    {
        _currentGameScore += score;
        RecordAchieveValue?.Invoke(_currentGameScore);
        if (_currentCubeMaxScore >= score) 
            return;
        _currentCubeMaxScore = score;
        OnNewRecordAchieve?.Invoke();
    }

    private void OnEnable() => TileCubeSystem.OnScoreChange += ScoreChanger;
    
    private void OnDisable() => TileCubeSystem.OnScoreChange -= ScoreChanger;
}
