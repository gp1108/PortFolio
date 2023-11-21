using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills : MonoBehaviour
{
    private static Skills _Reference;
    public static Skills giveMeReference
    {
        get
        {
            if (_Reference == null)
            {
                _Reference = FindObjectOfType<Skills>();
                if (_Reference == null)
                {
                    GameObject go = new GameObject("skillManager");
                    _Reference = go.AddComponent<Skills>();
                }
            }
            return _Reference;
        }
    }
    public enum SkillName
    {
        changeColor,
        changeSize,
        shakeCube,
        addPhysics,
        rainbowColor,

    }

    

    // Define el costo de cada habilidad
    public Dictionary<SkillName, int> skillCost = new Dictionary<SkillName, int>
    {
        { SkillName.changeColor, 1 },
        { SkillName.changeSize, 1 },
        { SkillName.shakeCube, 1 },
        { SkillName.addPhysics, 1 },
        { SkillName.rainbowColor, 1 },
    };

    

    //Define si ha sido desbloqueado o no 
    public Dictionary<SkillName, bool> isSkillUnlocked = new Dictionary<SkillName, bool>
    {
        { SkillName.changeColor, false },
        { SkillName.changeSize, false },
        { SkillName.shakeCube, false },
        { SkillName.rainbowColor,false},
        { SkillName.addPhysics,false},
        
    };

    public Dictionary<SkillName, bool> skillCanBeUnlocked = new Dictionary<SkillName, bool>
    {
        { SkillName.changeColor, true },
        { SkillName.changeSize, false },
        { SkillName.shakeCube, false },
        { SkillName.rainbowColor,false},
        { SkillName.addPhysics,false},

    };
    [Header("Skills")]
    public List<GameObject> SkillButtons = new List<GameObject>();

    public Color unlockedSkillColor;
    public Color notEnoughRPcolor;
    public Color defaultColor;
    public Color dependency;
    private GameObject canvas;
    public int skillPoints;

    public GameObject cube;
    public Material[] material;
  
    [Header("Buttons")]
    public GameObject changeColorButton;
    public GameObject changeSizeButton;
    public GameObject shakeCubeButton;
    private void Start()
    {
        
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        SkillsUI[] skillButtons = canvas.GetComponentsInChildren<SkillsUI>(true);
        foreach(SkillsUI skillUIscript in skillButtons)
        {
            SkillButtons.Add(skillUIscript.gameObject);
        }
        foreach (GameObject buttons in SkillButtons)
        {

            buttons.GetComponent<Image>().color = defaultColor;

        }
        UpdateSkillUI();
    }
    
    IEnumerator changeSize()
    {
        while(true)
        {
            int sizeX = Random.Range(25, 30);
            int sizeY = Random.Range(25, 30);
            int sizeZ = Random.Range(25, 30);
            cube.transform.localScale = new Vector3(30, 30, 30);
            yield return new WaitForSeconds(1f);
        }
    }
    public void unlockSkill(SkillName skill)
    {
        if (skillCost[skill] <= skillPoints)
        {

            switch (skill)
            {
                case SkillName.changeColor:

                    skillCanBeUnlocked[SkillName.changeSize] = true;
                    skillCanBeUnlocked[SkillName.shakeCube] = true;
                   
                    UnlockSkillLogic(skill);


                    break;
                case SkillName.changeSize:
                    if (isSkillUnlocked[SkillName.changeColor] == true)
                    {

                        if (isSkillUnlocked[SkillName.changeSize] == true && isSkillUnlocked[SkillName.shakeCube] == true)
                        {
                            skillCanBeUnlocked[SkillName.addPhysics] = true;
                            skillCanBeUnlocked[SkillName.rainbowColor] = true;

                            StartCoroutine("changeSize");

                        }

                        UnlockSkillLogic(skill);
                        

                    }
                    break;
                case SkillName.shakeCube:
                    if (isSkillUnlocked[SkillName.changeColor] == true)
                    {
                        if (isSkillUnlocked[SkillName.changeSize] == true && isSkillUnlocked[SkillName.shakeCube] == true)
                        {
                            skillCanBeUnlocked[SkillName.addPhysics] = true;

                        }
                        

                        UnlockSkillLogic(skill);

            
                    }
                    break;
                case SkillName.addPhysics:
                    if (isSkillUnlocked[SkillName.changeSize] == true && isSkillUnlocked[SkillName.shakeCube] == true)
                    {



                        UnlockSkillLogic(skill);


                    }
                    break;
                case SkillName.rainbowColor:
                    if (isSkillUnlocked[SkillName.changeSize] == true)
                    {



                        UnlockSkillLogic(skill);


                    }
                    break;



            }


        }
   
    }

    private void UnlockSkillLogic(SkillName skill)
    {

        skillPoints -= skillCost[skill];
        isSkillUnlocked[skill] = true;
        UpdateSkillUI();
    }
    
    public void UpdateSkillUI()
    {
        foreach (KeyValuePair<SkillName, int> kvp in skillCost)
        {
            SkillName clave = kvp.Key;
            int valor = kvp.Value;
            
            if(kvp.Value > skillPoints && isSkillUnlocked[kvp.Key] == false && skillCanBeUnlocked[kvp.Key] == true)
            {
                foreach(GameObject skillButtons in SkillButtons)
                {
                    if(skillButtons.gameObject.name == kvp.Key.ToString())
                    {
                        skillButtons.gameObject.GetComponent<Image>().color = notEnoughRPcolor;
                        skillButtons.gameObject.GetComponent<Button>().interactable = true;
                    }
                }
            }
            if(kvp.Value <= skillPoints && isSkillUnlocked[kvp.Key] == false)
            {
                foreach (GameObject skillButtons in SkillButtons)
                {
                    if (skillButtons.gameObject.name == kvp.Key.ToString())
                    {
                        skillButtons.gameObject.GetComponent<Image>().color = defaultColor;
                        skillButtons.gameObject.GetComponent<Button>().interactable = true;
                    }
                }
            }
            if(isSkillUnlocked[kvp.Key] == true)
            {
                
                foreach (GameObject skillButtons in SkillButtons)
                {
                    
                    if (skillButtons.gameObject.name == kvp.Key.ToString())
                    {
                        
                        skillButtons.gameObject.GetComponent<Image>().color = unlockedSkillColor;
                        skillButtons.gameObject.GetComponent<Button>().interactable = false;
                    }
                }
            }
            if (skillCanBeUnlocked[kvp.Key] == false )
            {

                foreach (GameObject skillButtons in SkillButtons)
                {

                    if (skillButtons.gameObject.name == kvp.Key.ToString())
                    {

                        skillButtons.gameObject.GetComponent<Image>().color = dependency;
                        skillButtons.gameObject.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
    }


}
