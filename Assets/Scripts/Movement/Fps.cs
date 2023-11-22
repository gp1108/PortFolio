using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour
{
    private int moveMentSpeed;
    private bool _saltando;
    private bool _enAire;
    private Rigidbody rb;
    private Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        moveMentSpeed = 7;
        rb = GetComponent<Rigidbody>();
        _saltando = false;
        _enAire = false;
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * moveMentSpeed;

        }

        if (Input.GetKeyDown(KeyCode.Space) && _saltando == false)
        {
            _saltando = true;
            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

        }

        if (rb.velocity.y <= 0 && _enAire == true)
        {
            rb.AddForce(Vector3.down * 6, ForceMode.Impulse);
            _enAire = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Suelo")
        {
            _saltando = false;
            _enAire = false;

        }

    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.collider.tag == "Suelo")
        {
            _enAire = true;

        }

    }

    private void OnEnable()
    {
        transform.position = posicionInicial;
    }
}
