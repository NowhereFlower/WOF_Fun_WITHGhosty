using Unity.Hierarchy;
using UnityEngine;

public class SObjectPossession : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private bool turnToPlayer = true;
    [SerializeField] private bool shake = true;
    [SerializeField] private float turnSpeed = 2f;
    [SerializeField] private float shakeSpeed = 5f;
    [SerializeField] private float shakeAmnt = 0.05f;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isPossessed;


    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    private void Update()
    {
        if (!isPossessed)
        {
            return;
        }

        if(turnToPlayer && player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if(direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
            }
        }

        if (shake)
        {
            float offsett = Mathf.Sin(Time.time * shakeSpeed) * shakeAmnt;

            transform.position = originalPos + Vector3.up * offsett;
        }
    }

    public void Possess()
    {
        isPossessed = true;
        
    }

    public void StopPossess()
    {
        
        isPossessed = false;
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
