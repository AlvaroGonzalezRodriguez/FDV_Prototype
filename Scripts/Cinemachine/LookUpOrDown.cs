using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LookUpOrDown : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera playerCamera, upCamera, downCamera;
    public Transform positionCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        positionCamera.position = GameObject.Find("MainCharacter").gameObject.transform.position;

        if(Input.GetKeyDown(KeyCode.N))
        {
            upCamera.Priority = playerCamera.Priority + 2;
        }
        if(Input.GetKeyUp(KeyCode.N))
        {
            upCamera.Priority = playerCamera.Priority - 2;
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            downCamera.Priority = playerCamera.Priority + 1;
        }
        if(Input.GetKeyUp(KeyCode.M))
        {
            downCamera.Priority = playerCamera.Priority - 1;
        }
    }
}
