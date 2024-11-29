using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Entity player;
    [SerializeField] private Entity house;
    private TextMeshProUGUI playerHP;
    private TextMeshProUGUI houseHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHP = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        houseHP = transform.Find("House HP").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text = $"HP: {player.Health}";
        houseHP.text = $"House HP: {house.Health}";
    }
}
