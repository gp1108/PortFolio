using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;


public class BuildMenuButton : MonoBehaviour
{
    public GameObject buildMenuPanel;
    [Header("Destroy Function")]
    public bool destroyModeActive;
    public GameObject destroyButton;
    [Header("Buttons")]
    public GameObject tallerButton;
    public GameObject turretButton;
    public GameObject otherTurretButton;

    

    public void Start()
    {

        StartCoroutine("GoldCheck");

        destroyModeActive = false;
    }

    public void DestroyStructure()
    {
        destroyModeActive = !destroyModeActive;

        if(destroyModeActive == true)
        {
            destroyButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            destroyButton.GetComponent<Image>().color = Color.white;
        }
    }


    public void SetBaseTurretIndex()
    {
        BuildManager.dameReferencia.GetStructurePrefabIndex(0);
        if (destroyModeActive == true)
        {
            destroyModeActive = false;
            destroyButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void OtherTurretIndex()
    {
        BuildManager.dameReferencia.GetStructurePrefabIndex(1);
        if (destroyModeActive == true)
        {
            destroyModeActive = false;
            destroyButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetResearchStructureIndex()
    {
        BuildManager.dameReferencia.GetStructurePrefabIndex(2);
        if (destroyModeActive == true)
        {
            destroyModeActive = false;
            destroyButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void AddGold()
    {

        BuildManager.dameReferencia.gold += 100;
        BuildManager.dameReferencia.UpdatePriceUI();
    }


    IEnumerator GoldCheck()
    {
        while (true)
        {

            if(BuildManager.dameReferencia.baseTurretCost > BuildManager.dameReferencia.gold)
            {
                turretButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                turretButton.GetComponent<Button>().interactable = true;
            }

            if (BuildManager.dameReferencia.otherTurretCost > BuildManager.dameReferencia.gold)
            {
                otherTurretButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                otherTurretButton.GetComponent<Button>().interactable = true;
            }
            
            if (BuildManager.dameReferencia.researchStructureCost > BuildManager.dameReferencia.gold)
            {
                tallerButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                tallerButton.GetComponent<Button>().interactable = true;
            }

            yield return new WaitForSeconds(0.3f);
        }
        
    }
}
