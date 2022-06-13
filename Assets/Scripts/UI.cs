using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _scoreText;
   [SerializeField] private GameObject _restartPanel;
   [SerializeField] private TextMeshProUGUI _startText;

   private void RestartPanel() => _restartPanel.SetActive(true);

   private void StartTextAnimation()
   {
       DOTween.Validate();
       if (_startText != null)
           _startText.transform.DOScale(new Vector3(1.3f, 1.3f, 1), 0.5f).SetLoops(-1, LoopType.Yoyo);
   }

   private void Start() => StartTextAnimation();

   private void OnEnable()
    {
        ScoreCounter.RecordAchieveValue += i => _scoreText.text = "Score: " + i;
        Spawner.OnLose += RestartPanel;
    }

    private void OnDisable() => Spawner.OnLose -= RestartPanel;
}
