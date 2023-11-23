using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class BuildManager : MonoBehaviour
{
    [Header("BUILDMANAGER")]
    private static BuildManager Referencia;
    [SerializeField]private GameObject[] _structures;
    private int _structureIndex;

    //Esta booleana es el nexo entre el preview system y el buildmanager, es decir , aprovecho el cambio de color para asi determinar si se puede o no construir
    private bool _canbuild;
    public bool buildCD;

    [Header("PREVIEW SYSTEM")]
    //PreviewSystem
    [SerializeField] private GameObject[] _previewStructures;
    private GameObject previewPrefab;
    private Vector3 previewPrefabPosition;
    private int lastIndex;
    [SerializeField] private Material aviableInstance;
    [SerializeField] private Material unAviableInstance;
    public GameObject buildPanel;
    private Canvas canvas;

    [Header("Gold Cost")]
    public int goldToPay;

    public int baseTurretCost;
    public int otherTurretCost;
    public int researchStructureCost;
 
    [Header("Gold Texts")]
    public TMP_Text textBaseTurretCost;
    public TMP_Text textOtherTurretCost;
    public TMP_Text textResearchStructureCost;


    public int gold;

    public static BuildManager dameReferencia
    {
        get
        {
           

            if (Referencia == null)
            {
                Referencia = FindObjectOfType<BuildManager>();
                if (Referencia == null)
                {
                    GameObject go = new GameObject("BuildManager");
                    Referencia = go.AddComponent<BuildManager>();
                }
            }
            return Referencia;
        }
    }

    public void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        gold = 2000;
        //Costs
        baseTurretCost = 10;
        otherTurretCost = 50;
        researchStructureCost = 200;
 
        UpdatePriceUI();

        //goldToPay = 5; // igualo aqui al precio de los muros para evitar un bug en el que la partida carga y puedes construir muros sin gastar dinero
    }

    private void Update()
    {
        bool isDestroyModeActive = canvas.GetComponent<BuildMenuButton>().destroyModeActive;
        

        if (previewPrefab != null)
        {
            previewPrefab.transform.position = previewPrefabPosition;


            //Color del prefab
            if (previewPrefab.GetComponent<PreviewPrefabSize>().validposition == true && buildPanel.activeSelf && isDestroyModeActive == false && goldToPay <= gold)
            {
                Renderer[] childRenderers = previewPrefab.GetComponentsInChildren<Renderer>();

                
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.enabled = true;
                    childRenderer.material = aviableInstance;
                    _canbuild = true;
                }

            }
            else if (previewPrefab.GetComponent<PreviewPrefabSize>().validposition == false && buildPanel.activeSelf && isDestroyModeActive == false && goldToPay <= gold)
            {
                Renderer[] childRenderers = previewPrefab.GetComponentsInChildren<Renderer>();

                
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.enabled = true;
                    childRenderer.material = unAviableInstance;
                    _canbuild = false;
                }
                
            }
            else if(!buildPanel.activeSelf || isDestroyModeActive == true)
            {
                Renderer[] childRenderers = previewPrefab.GetComponentsInChildren<Renderer>();
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.enabled = false;
                    _canbuild = false;
                }
            }
            else if( goldToPay > gold)
            {
                Renderer[] childRenderers = previewPrefab.GetComponentsInChildren<Renderer>();
                foreach (Renderer childRenderer in childRenderers)
                {
                    childRenderer.enabled = false;
                    _canbuild = false;
                }
            }
        }
        
    }



    

    public void GetStructurePrefabIndex(int index)
    {
        _structureIndex = index;
        StructureCost();
        

    }

    public void GetGold(int Gold)
    {
        gold += Gold;
    }
    public void StructureCost()
    {

        if (_structureIndex  == 1)
        {
            goldToPay = baseTurretCost;
        }
        else if (_structureIndex  == 2)
        {
            goldToPay = otherTurretCost;
        }
        else if (_structureIndex  == 3)
        {
            goldToPay = researchStructureCost;
        }
        
       
    } //Determina el valor a pagar segun la estructura
    public void PriceUpdate(int Index, bool moreCost) //Aumenta o disminuye el precio de construccion de los objetos
    {
        
        //La variable more hace referencia a si se ha vendido o colocado el objeto. Si se ha vendido el precio debe disminuir , si se ha colocado , aumentar.
        if(moreCost == true)
        {
            if (_structureIndex == 1)
            {
                baseTurretCost += 5;
            }
            else if (_structureIndex == 2)
            {
                otherTurretCost += 10;
            }
            else if (_structureIndex == 3)
            {
                researchStructureCost += 100;
            }
            UpdatePriceUI();
            StructureCost();
        }
        else if(moreCost == false)
        {
            if (Index == 1)
            {
                
                baseTurretCost -= 5;
                GetGold(baseTurretCost);
            }
            else if (Index  == 2)
            {
               
                otherTurretCost -= 10;
                GetGold(otherTurretCost);
            }
            else if (Index  == 3)
            {
                
                researchStructureCost -= 100;
                GetGold(researchStructureCost);
            }
          
            UpdatePriceUI();
            StructureCost();
        }
    }

    public void UpdatePriceUI() //Actualiza los textos de los precios
    {
        textBaseTurretCost.text = baseTurretCost.ToString() + "g";
        textOtherTurretCost.text = otherTurretCost.ToString() + "g";
        textResearchStructureCost.text = researchStructureCost.ToString() + "g";
    }

    public void PlaceStucture(Vector3 position)
    {
        
        if(_canbuild == true && buildCD == false) 
        {
            if(goldToPay <= gold)
            {
                Instantiate(_structures[_structureIndex], position, Quaternion.identity);

                
                buildCD = true;
                GetGold(-goldToPay);
                PriceUpdate(_structureIndex,true);

            }  
        }
        else
        {
            return;
        }
    }
    
    
    //PreviewSystem
    public void GetPreviewPrefabPosition(Vector3 position)
    {
        previewPrefabPosition = position;

    }

    public void SetPreviewGameObject()
    {
        
        if (previewPrefab == null)
        {
            previewPrefab = Instantiate(_previewStructures[_structureIndex], previewPrefabPosition, Quaternion.identity);
            lastIndex = _structureIndex;
        }
        else if(_structureIndex != lastIndex)
        {
            lastIndex = _structureIndex;
            Destroy(previewPrefab);
            previewPrefab = Instantiate(_previewStructures[_structureIndex], previewPrefabPosition, Quaternion.identity);
        }
        else
        {
            return;
        }

    }
    public void DestroyPreviewPrefab()
    {
        Destroy(previewPrefab);
    }
    
}


