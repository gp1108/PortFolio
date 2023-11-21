using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.AI.Navigation;
/*
public class NodeGenerator : MonoBehaviour
{
    public GameObject planoPrefab;    // El prefab del plano.
    public GameObject prefab; // El prefab que deseas instanciar.
    public int numFilas = 5;  // N�mero de filas de cuadrados.
    public int numColumnas = 5; // N�mero de columnas de cuadrados.
    public float separacion = 0.05f; // Separaci�n entre cuadrados.


    //public NavMeshSurface nodoactivo; 

    void Start()
    {
        // Calcular el tama�o total del cuadrado.
        Vector3 tama�oPrefab = prefab.GetComponent<Renderer>().bounds.size;



        // Iterar a trav�s de las filas y columnas para instanciar los cuadrados.
        for (int fila = 0; fila < numFilas; fila++)
        {
            for (int columna = 0; columna < numColumnas; columna++)
            {
                // Calcular la posici�n de instanciaci�n.
                float x = columna * (tama�oPrefab.x + separacion);
                float z = fila * (tama�oPrefab.z + separacion);

                // Crear una nueva instancia del prefab en la posici�n calculada.
                Vector3 posici�n = new Vector3(x, 0, z);
                Instantiate(prefab, posici�n, Quaternion.identity);
            }
        }

        // Calcular el tama�o del plano para que coincida con el tama�o total de los cuadrados.
        float tama�oTotalX = numColumnas * (tama�oPrefab.x + separacion);
        float tama�oTotalZ = numFilas * (tama�oPrefab.z + separacion);
        Vector3 tama�oPlano = new Vector3(tama�oTotalX/8, 0.02f, tama�oTotalZ/8);

        // Instanciar el plano debajo de los cuadrados.
        Vector3 posici�nPlano = new Vector3(tama�oTotalX/2.5f , -0.01f, tama�oTotalZ/2.25f );
        Instantiate(planoPrefab, posici�nPlano, Quaternion.identity).transform.localScale = tama�oPlano;




    }

    //Cambiar condicion de activacion de maya
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nodoactivo.BuildNavMesh();
        }
    }
}
*/
