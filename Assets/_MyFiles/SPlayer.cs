using UnityEngine;
using UnityEngine.SceneManagement;

public class SPlayer : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject startPanel;
    [SerializeField] public bool flashlightOn;
    private void Awake()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(true);
            flashlightOn = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            flashlight.SetActive(false);
            flashlightOn = false;
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
