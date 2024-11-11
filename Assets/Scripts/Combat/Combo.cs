using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Combo
{
    public string comboName;
    public List<ActionType> keySequence;
    public int score;

    public bool IsMatching(List<ActionType> _inputHistory)
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
