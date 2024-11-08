using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private List<Combo> _combos;
    [SerializeField] private float _inputTimeWindow = 0.5f;

    private readonly List<KeyCode> _inputHistory = new();
    private readonly List<Combo> _currentCombos = new();
    private Coroutine _executeComboTimer;
    private Coroutine _clearHistoryTimer;
    private Hero _hero;
    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            bool stopLoop = false;
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (stopLoop) break;
                if (Input.GetKeyDown(key))
                {
                    switch (key)
                    {
                        case KeyCode.Mouse0:
                        case KeyCode.Mouse1:
                        case KeyCode.LeftShift:
                            RegisterInput(key);
                            stopLoop = true;
                            break;


                    }
                }
            }
        }
    }

    private void RegisterInput(KeyCode key)
    {

        _inputHistory.Add(key);

        if (_inputHistory.Count > 6)
        {
            _inputHistory.Clear();
        }
        if (_clearHistoryTimer != null)
        {
            StopCoroutine(_clearHistoryTimer);
        }

        _clearHistoryTimer = StartCoroutine(ClearHistory());

        CheckCombos();
    }

    private void CheckCombos()
    {
        foreach (Combo combo in _combos)
        {
            if (combo.IsMatching(_inputHistory))
            {
                Debug.Log($"Найдено совпадение с комбо: {combo.comboName}");
                _currentCombos.Add(combo);
                if (_executeComboTimer != null)
                {
                    StopCoroutine(_executeComboTimer);
                }

                _executeComboTimer = StartCoroutine(ComboExecutionTimer());

            }
        }
    }

    private void ExecuteLongestCombo()
    {
        if (_currentCombos.Count == 0) return;


        Combo longestCombo = _currentCombos.OrderByDescending(o => o.keySequence.Count).First();
        Debug.Log($"Выполняется комбо: {longestCombo.comboName}");
        _currentCombos.Clear();
        _inputHistory.Clear();
    }

    private IEnumerator ComboExecutionTimer()
    {
        yield return new WaitForSeconds(_inputTimeWindow);
        ExecuteLongestCombo();
    }
    private IEnumerator ClearHistory()
    {
        yield return new WaitForSeconds(_inputTimeWindow);
        _inputHistory.Clear();
    }
}

[Serializable]
public class Combo
{
    public string comboName;
    public List<KeyCode> keySequence;
    public int score;

    public bool IsMatching(List<KeyCode> _inputHistory)
    {
        int comboCounter = 0;

        if (keySequence.Count == _inputHistory.Count)
        {
            for (int i = 0; i < keySequence.Count; i++)
            {
                if (_inputHistory[i] == keySequence[i]) comboCounter++;
            }
            if (keySequence.Count == comboCounter) return true;
        }
        return false;
    }
}
