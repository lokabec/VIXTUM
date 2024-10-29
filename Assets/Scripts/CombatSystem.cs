using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class CombatSystem : MonoBehaviour
{
    [SerializeField] private List<Combo> _combos;
    [SerializeField] private float _inputTimeWindow = 0.5f; // Время между нажатиями


    private readonly List<(KeyCode key, float time)> _inputHistory = new();

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    RegisterInput(key);
                    break;
                }
            }
        }

        CleanupOldInputs();
    }

    private void RegisterInput(KeyCode key)
    {
        _inputHistory.Add((key, Time.time));

        CheckCombos();
    }

    private void CleanupOldInputs()
    {

        for (int i = _inputHistory.Count - 1; i >= 0; i--)
        {
            if(Time.time - _inputHistory[i].time > _inputTimeWindow)
            {
                _inputHistory.RemoveAt(i);
            }
        }
    }

    private void CheckCombos()
    {
        Combo bestMuch = null;
        int bestMatchLength = 0;

        foreach (Combo combo in _combos) 
        {
            if (IsComboMach(combo))
            {
                if(combo.keySequence.Count > bestMatchLength)
                {
                    bestMuch = combo;
                    bestMatchLength = combo.keySequence.Count;
                }
            }
        }

        if (bestMuch != null) 
        {
            ExecuteCombo(bestMuch);
            _inputHistory.Clear();
            
        }
    }

    private void ExecuteCombo(Combo combo)
    {
        Debug.Log($"Комбо {combo.comboName} выполнено!");
    }

    private bool IsComboMach(Combo combo)
    {
        if(_inputHistory.Count < combo.keySequence.Count) return false;

        int startIndex = _inputHistory.Count - combo.keySequence.Count;

        for (int i = 0; i < combo.keySequence.Count; i++)
        {
            if (_inputHistory[startIndex + i].key != combo.keySequence[i])
            {
                return false; 
            }
        }
        return true;
    }
}

[Serializable]
public class Combo
{
    public string comboName;
    public List<KeyCode> keySequence; 
    //public float maxInputTime; 
   

    [HideInInspector] public int currentStep = 0;
}
