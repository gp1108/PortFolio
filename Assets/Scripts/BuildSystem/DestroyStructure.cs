using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStructure : MonoBehaviour
{
    private GameObject canvas;
    private void OnMouseUpAsButton()
    {
        
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas.GetComponent<BuildMenuButton>().destroyModeActive == true)
        {
            
                this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
                Destroy(this.gameObject, 0.15f);
                if(this.gameObject.tag == "BaseTurret")
                {
                    BuildManager.dameReferencia.PriceUpdate(1, false); //basicamente el indice que ocupa en el buildmenu button y falso por que debe disminuir el precio
                }
                else if(this.gameObject.tag == "OtherTurret")
                {
                    BuildManager.dameReferencia.PriceUpdate(2, false);
                }
                else if (this.gameObject.tag == "Taller")
                {
                    BuildManager.dameReferencia.PriceUpdate(3, false);
                }
                
        }
        else
        {
            
            return;
            
        }
    }
}
