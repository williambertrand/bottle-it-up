using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StressTextController : MonoBehaviour
{

    private struct StressText
    {
        public readonly float MaxStressThreshold;
        public readonly string StressTextVal;

        public StressText(float maxStressThreshold, string stressTextVal)
        {
            MaxStressThreshold = maxStressThreshold;
            StressTextVal = stressTextVal;
        }
    }
    
    private static readonly List<StressText> StressLevelToStressText = new List<StressText>
    {
        new StressText(0.3f , "..."),
        new StressText(0.4f , ".."),
        new StressText(0.5f , "."),
        new StressText(0.7f , "!"),
        new StressText(0.85f , "!!"),
        new StressText(0.95f , "!!!")
    }; 
    
    private PlayerController _playerController;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _playerController = PlayerController.Instance;
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    
    private void Update()
    {
        _text.text = GetStressText(GetStressLevel());
    }

    private float GetStressLevel()
    {
        var isMonster = _playerController.IsMonster;
        var monstrosity = _playerController.MonstrosityLevel;

        return isMonster ? 1 : monstrosity;
    }

    private static string GetStressText(float stressLevel) =>
        StressLevelToStressText
            .First(s => stressLevel <= s.MaxStressThreshold)
            .StressTextVal;
}
