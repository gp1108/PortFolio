using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMov : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;
    private Vector3 rotacioncamara;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 orbit = Vector3.back * offset.magnitude + new Vector3(0, 1.5f, 0);
        orbit = Quaternion.Euler(0, target.eulerAngles.y, 0) * orbit;

        transform.position = target.position + orbit;

        transform.rotation = target.rotation ;



    }
}
