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
    private bool _canMove;
    // Start is called before the first frame update
    void Start()
    {
        moveMentSpeed = 7;
        rb = GetComponent<Rigidbody>();
        _saltando = false;
        _enAire = false;
        posicionInicial = transform.position;
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && _canMove == true)
        {
            transform.position += transform.forward * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.A) && _canMove == true)
        {
            transform.position -= transform.right * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.S) && _canMove == true)
        {
            transform.position -= transform.forward * Time.deltaTime * moveMentSpeed;

        }
        if (Input.GetKey(KeyCode.D) && _canMove == true)
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
            _canMove = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.collider.tag == "Suelo")
        {
            _enAire = true;

        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (_enAire == true)
        {
            _canMove = false;
        }
    }

    private void OnEnable()
    {
        transform.position = posicionInicial;
    }
}
