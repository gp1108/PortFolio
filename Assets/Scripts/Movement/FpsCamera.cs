using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
    public float velocidad;
    public float salto;
    private bool _saltando;
    private bool _enAire;
    private Rigidbody rb;
    private Vector3 posicionInicial;
    //camara
    public Camera camarajugador;
    public float sensibilidad;
    private float xRotacion;

    void Awake()
    {
        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        _saltando = false;
        _enAire = false;
        posicionInicial = transform.position;
        //movimiento
        velocidad = 5;
        salto = 7;
        sensibilidad = 350.0f;
    }

    void Update()
    {
        //MOVIMIENTO 

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Vector3 localVelocity = rb.velocity;
        localVelocity.x = Input.GetAxis("Horizontal") * velocidad;
        localVelocity.z = Input.GetAxis("Vertical") * velocidad;
        rb.velocity = gameObject.transform.TransformDirection(localVelocity);
        if (Input.GetKeyDown(KeyCode.Space) && _saltando == false)
        {
            _saltando = true;
            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

        }

        if (rb.velocity.y <= 0 && _enAire == true)
        {
            rb.AddForce(Vector3.down * 3, ForceMode.Impulse);
            _enAire = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //CAMARA
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotacion -= mouseY;
        xRotacion = Mathf.Clamp(xRotacion, -45, 45);

        camarajugador.transform.localRotation = Quaternion.Euler(xRotacion, 0, 0);
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
