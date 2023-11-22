using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;


public class Tank : MonoBehaviour
{
    //Movimiento
    public float velocidad;
    public float velocidadgiro;
    public float sensibilidad;
    private float xRotacion;

    public GameObject cañon;
    public GameObject cabina;

    

   
    void Start()
    {
        //Movimiento tanque y sensibilidad camara
        velocidad = 7;
        velocidadgiro = 70;
        sensibilidad = 350;

        Cursor.lockState = CursorLockMode.Locked;

        // Oculta el cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Movimiento del tanque
        if ( Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(new Vector3(0, 1 * -velocidadgiro, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(new Vector3(0, 1 * velocidadgiro, 0) * Time.deltaTime);
        }
        
      
        // Control de la cabina 
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        cabina.transform.Rotate(Vector3.up * mouseX);


        xRotacion -= mouseY;
        xRotacion = Mathf.Clamp(xRotacion, -25, 5);

        cañon.transform.localRotation = Quaternion.Euler(xRotacion, 0, 0);

        //MENU
        if(Input.GetKeyDown(KeyCode.Escape))
        {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
           

            
        }
    }

    
}
