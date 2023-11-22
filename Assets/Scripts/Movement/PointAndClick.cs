using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClick : MonoBehaviour
{
    public GameObject player;
    private Vector3 posicionInicial;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si el rayo intersecta con un objeto en el NavMesh, establece la posición de destino
            if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<NavMeshAgent>() == null)
            {
                player.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                
            }
        }
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        transform.position = posicionInicial;
    }
}
