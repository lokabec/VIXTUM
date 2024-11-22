using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private void Start()
    {
        TestBackgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TestBackgroundMusic.PlayFront();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestBackgroundMusic.StopFront();
        }
    }
}
