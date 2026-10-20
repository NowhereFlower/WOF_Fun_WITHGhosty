using UnityEngine;

public class SPlayer : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] public bool flashlightOn;
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
}
