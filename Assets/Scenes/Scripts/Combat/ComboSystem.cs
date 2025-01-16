using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private List<Combo> _combos;
    [SerializeField] private float _inputTimeWindow = 0.5f;

    private readonly List<ActionType> _inputHistory = new();
    private readonly List<Combo> _currentCombos = new();
    private Coroutine _executeComboTimer;
    private Coroutine _clearHistoryTimer;

    public void RegisterAttack(ActionType action)
    {

        _inputHistory.Add(action);

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