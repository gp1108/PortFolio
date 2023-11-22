using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    private bool _isCameraSwaped;
    public GameObject[] cameras;
    public GameObject[] playerMovement;
    private int index;
    void Start()
    {
        index = 0;
    }

  
    void Update()
    {
        Debug.Log(index);
        if(Input.GetKeyDown(KeyCode.Q) && _isCameraSwaped == false)
        {
            foreach(GameObject allcameras in cameras)
            {
                allcameras.gameObject.SetActive(false);

            }
            if(index == cameras.Length-1)
            {
                index = 0;
                cameras[index].gameObject.SetActive(true);
            }
            else
            {
                index++;
                cameras[index].gameObject.SetActive(true);
                
            }
        }
    }
}
