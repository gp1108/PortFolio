using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewPrefabSize : MonoBehaviour
{
    public int prefabSize;
    public bool validposition;
    private int contador;

    //Dimensiones del prefab
    //Si el prefabSize= 0 el prefab tiene de tama�o 1xYx1
    //Si el prefabSize= 1 el prefab tiene de tama�o 2xYx2
    //Si el prefabSize= 3 el prefab tiene de tama�o 3xYx3


    

    void Update()
    {
        if (prefabSize == 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, -transform.up,  out hit ,0.5f ))
            {
                if(hit.collider.CompareTag("Node"))
                {
                    
                    if(hit.collider.gameObject.GetComponent<Nodes>().constructed == false)
                    {
                        
                        validposition = true;
                    }
                    else
                    {
                        validposition = false;

                    }

                }
                
                
            }
            
        }
        else if(prefabSize == 1)
        {
            

        }
        else if(prefabSize ==2)
        {
            
            
            RaycastHit hit1,hit2,hit3,hit4,hit5,hit6,hit7,hit8,hit9;
            if (Physics.Raycast(transform.position, -transform.up, out hit1, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z - 1f), -transform.up, out hit2, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z - 1f), -transform.up, out hit3, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z + 1f), -transform.up, out hit4, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z + 1f), -transform.up, out hit5, 0.4f) &&

                Physics.Raycast(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z + 0f), -transform.up, out hit6, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x + 0f, transform.position.y, transform.position.z + 1f), -transform.up, out hit7, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z + 0f), -transform.up, out hit8, 0.4f) &&
                Physics.Raycast(new Vector3(transform.position.x + 0f, transform.position.y, transform.position.z - 1f), -transform.up, out hit9, 0.4f) )
            {
                
                if (hit1.collider.CompareTag("Node") && 
                    hit2.collider.CompareTag("Node") && 
                    hit3.collider.CompareTag("Node") && 
                    hit4.collider.CompareTag("Node") && 
                    hit5.collider.CompareTag("Node") &&
                    hit6.collider.CompareTag("Node") &&
                    hit7.collider.CompareTag("Node") &&
                    hit8.collider.CompareTag("Node") &&
                    hit9.collider.CompareTag("Node"))
                {
                    
                    if (hit1.collider.gameObject.GetComponent<Nodes>().constructed == false 
                        && hit2.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit3.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit4.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit5.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit6.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit7.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit8.collider.gameObject.GetComponent<Nodes>().constructed == false
                        && hit9.collider.gameObject.GetComponent<Nodes>().constructed == false)
                    {
                        
                        validposition = true;
                    }
                    else
                    {
                        validposition = false;

                    }

                }
                else
                {
                    validposition = false;
                } 


            }
            else
            {
                validposition = false;
            }
        }
    }

    public bool CanConstruct()
    {
        if (prefabSize == 0)
        {
            BuildManager.dameReferencia.buildCD = false;
            return true;
        }
        if (prefabSize == 1)
        {
            contador++;
            
            if (contador == 4)
            {

                contador = 0;
                return true;
               
            }
            else
            {
                return false;
            }

        }
        if(prefabSize == 2)
        {
            contador++;

            if (contador == 9)
            {
                contador=0;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
