using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _currentGameScore;
    [SerializeField] private int _currentCubeMaxScore;
    
    public static event Action OnNewRecordAchieve;
    
    public static event Action<int> RecordAchieveValue;
    
    private void ScoreChanger(int score)
    {
        _currentGameScore += score;
        RecordAchieveValue?.Invoke(_currentGameScore);
        if (_currentCubeMaxScore < score)
        {
            _currentCubeMaxScore = score;
            OnNewRecordAchieve?.Invoke();
        }
    }

    private void OnEnable() => TileCubeSystem.OnScoreChange += ScoreChanger;
    
    private void OnDisable() => TileCubeSystem.OnScoreChange -= ScoreChanger;
}
