using System.Collections;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
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

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(timeCooldown);
        ComboMultiplire = 1;
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
}
