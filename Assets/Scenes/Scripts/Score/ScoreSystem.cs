using System.Collections;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int lastProcessedComboCounter = -1;
    public int ComboCounter;
    public int ComboMultiplire = 1;
    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score += value * ComboMultiplire;
            if (cooldownForScore != null) StopCoroutine(cooldownForScore);
            if(scoreDown != null) StopCoroutine(scoreDown);
            cooldownForScore = StartCoroutine(nameof(CoolDown));
        }
    }
    private Coroutine cooldownForScore;
    private Coroutine scoreDown;
    [SerializeField] private float timeCooldown;
    [SerializeField] private float scoreDownSpeed;

    private void Update()
    {
        if (ComboCounter > 0 && ComboCounter % 10 == 0 && ComboCounter != lastProcessedComboCounter)
        {
            ComboAdd();
            lastProcessedComboCounter = ComboCounter;
        }
    }
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(timeCooldown);
        ComboMultiplire = 1;
        ComboCounter = 0;
        scoreDown = StartCoroutine(nameof(ScoreDown));
    }
    private IEnumerator ScoreDown() 
    {
        while (true) 
        {
            score--;
            yield return new WaitForSeconds(scoreDownSpeed);
        }
    }

    private void ComboAdd()
    {
        if (ComboMultiplire < 5)
        {
            ComboMultiplire++;
        }
    }
}
