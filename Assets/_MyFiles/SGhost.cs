using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assemblies;

public class SGhost : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private GameObject scarecrow;
    [SerializeField] private GameObject scarecrow2;
    [SerializeField] private GameObject witch;
    [SerializeField] private GameObject pumpkin2;
    [SerializeField] private GameObject skull;
    [SerializeField] private Transform[] positions;
    [SerializeField] private SObjectPossession[] possessedObject;
    private Coroutine ghostCoro;

    [SerializeField] private bool isHiding = true;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float minTime = 2f;
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int currecntPos = 0;

    [SerializeField] SPlayer player;

    private void Update()
    {
        if(player != null && player.flashlightOn && isMoving)
        {
            Hide();
        }

    }

    private void Start()
    {
        ghost.transform.position = positions[0].position;
        ghostCoro = StartCoroutine(IGhostLoop());
    }


    private IEnumerator IGhostLoop()
    {
        while (true)
        {
            isHiding = true;
            isMoving = false;

            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            if (player.flashlightOn)
            {
                continue;
            }

            if(currecntPos >= positions.Length - 1)
            {
                yield break;
            }

            isHiding = false;
            isMoving = true;
            ghost.SetActive(false);
            Transform target = positions[currecntPos + 1];


            while(Vector3.Distance(ghost.transform.position, target.position) > 0.05f)
            {
                if (player.flashlightOn)
                {
                    Hide();
                    yield break;
                }

                ghost.transform.position = Vector3.MoveTowards(ghost.transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            if (isHiding)
            {
                continue;
            }
            ghost.transform.position = target.position;
            currecntPos++;

            SetPossess(currecntPos);

            ghost.SetActive(false);
            isMoving = false;
            isHiding = true;
        }
    }
   

    private void Hide()
    {
        if (!isMoving)
        {
            return;
        }
        if(ghostCoro != null)
        {
            StopCoroutine(IGhostLoop());
        }
        SetPossess(-1);
        isHiding = true;
        isMoving = false;
        currecntPos = Mathf.Max(0, currecntPos);

        if(positions.Length > 0)
        {
            ghost.transform.position = positions[currecntPos].position;
        }
        ghost.SetActive(true);
        ghostCoro = StartCoroutine(IGhostLoop());
        
    }

    private void SetPossess(int index)
    {
        foreach(SObjectPossession obj in possessedObject)
        {
            if(obj != null)
            {
                obj.StopPossess();
            }
        }

        if(index >= 0 && index < possessedObject.Length && possessedObject[index] != null)
        {
            possessedObject[index].Possess();
        }
    }
}
