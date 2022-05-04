using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private const int ValueOfMultiplication = 100;
    
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _multiplieButton;
    [SerializeField] private int _currentScore;
    
    private void Start() => _multiplieButton.interactable = true;

    private void ScoreChanger(int score)
    {
        _currentScore += score;
        ScoreUpdater();
    }
    
    public void MultiplierClick()
    {
        if (_currentScore >= 100)
        {
            _currentScore -= ValueOfMultiplication;
            ScoreUpdater();
        }
    }

    private void ScoreUpdater()
    {
        _scoreText.text = _currentScore.ToString();
    }

    private void OnEnable() => TileCubeSystem.OnScoreChange += ScoreChanger;
    private void OnDisable() => TileCubeSystem.OnScoreChange -= ScoreChanger;
}
