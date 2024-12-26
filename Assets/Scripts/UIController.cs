using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Entity player;
    [SerializeField] private Entity house;
    [SerializeField] private ScoreSystem scoreSystem;
    private TextMeshProUGUI playerHP;
    private TextMeshProUGUI houseHP;
    private TextMeshProUGUI score;
    private TextMeshProUGUI combo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHP = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        houseHP = transform.Find("House HP").GetComponent<TextMeshProUGUI>();
        score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        combo = transform.Find("Combo").GetComponent<TextMeshProUGUI>();
        combo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text = $"HP: {player.Health}";
        houseHP.text = $"House HP: {house.Health}";
        score.text = $"Score: {scoreSystem.Score:0000}";
        combo.text = $"Combo: {scoreSystem.ComboMultiplire}";
        if (scoreSystem.ComboMultiplire > 1)
        {
            combo.enabled = true;
        }
        else
        {
            combo.enabled = false;
        }
        
    }
}
