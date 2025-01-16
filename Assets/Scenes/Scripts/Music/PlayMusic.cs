using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private void Start()
    {
        TestBackgroundMusic.Play();
    }

    private void OnEnable()
    {
        WaveSystem.StartWave += TestBackgroundMusic.PlayFront;
        WaveSystem.StopWave += TestBackgroundMusic.StopFront;
    }
    private void OnDisable()
    {
        WaveSystem.StartWave -= TestBackgroundMusic.PlayFront;
        WaveSystem.StopWave -= TestBackgroundMusic.StopFront;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        TestBackgroundMusic.PlayFront();
    //    }
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        TestBackgroundMusic.StopFront();
    //    }
    //}
}
