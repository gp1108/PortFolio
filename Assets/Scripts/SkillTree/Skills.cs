using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
        spinAround,
        rainbowColor,

    }

    

    // Define el costo de cada habilidad
    public Dictionary<SkillName, int> skillCost = new Dictionary<SkillName, int>
    {
        { SkillName.changeColor, 1 },
        { SkillName.changeSize, 2 },
        { SkillName.shakeCube, 3 },
        { SkillName.spinAround, 4 },
        { SkillName.rainbowColor, 2 },
    };

    

    //Define si ha sido desbloqueado o no 
    public Dictionary<SkillName, bool> isSkillUnlocked = new Dictionary<SkillName, bool>
    {
        { SkillName.changeColor, false },
        { SkillName.changeSize, false },
        { SkillName.shakeCube, false },
        { SkillName.rainbowColor,false},
        { SkillName.spinAround,false},
        
    };

    public Dictionary<SkillName, bool> skillCanBeUnlocked = new Dictionary<SkillName, bool>
    {
        { SkillName.changeColor, true },
        { SkillName.changeSize, false },
        { SkillName.shakeCube, false },
        { SkillName.rainbowColor,false},
        { SkillName.spinAround,false},

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
    private bool canShake;
    private bool canRotate;
    public TMP_Text skillpointText;
  
    private void Start()
    {
        skillPoints = 0;
        canShake = false;
        canRotate = false;

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
            int sizeY = Random.Range(25, 50);
            int sizeZ = Random.Range(25, 40);
            cube.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator rainbowColor()
    {
        while(true)
        {
            for(int i = 0; i< material.Length; i++)
            {
                cube.GetComponent<MeshRenderer>().material = material[i];
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void Update()
    {
        if(canShake == true)
        {
            float traslationY = 2f * Mathf.Sin(10f * Time.time) + 567;
            cube.transform.position = new Vector3(cube.transform.position.x, traslationY, cube.transform.position.z);
        }
        if (canRotate == true)
        {
            cube.transform.Rotate(Vector3.up, 20 * Time.deltaTime);
        }
        skillpointText.text = "Skill points: " + skillPoints;
    }

    public void addSkillPoints()
    {
        skillPoints += 1;
        UpdateSkillUI();
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

                    cube.GetComponent<MeshRenderer>().material = material[6];

                    UnlockSkillLogic(skill);


                    break;
                case SkillName.changeSize:
                    if (isSkillUnlocked[SkillName.changeColor] == true)
                    {

                        skillCanBeUnlocked[SkillName.rainbowColor] = true;
                        StartCoroutine("changeSize");
                            
                       
                        UnlockSkillLogic(skill);
                        

                    }
                    break;
                case SkillName.shakeCube:
                    if (isSkillUnlocked[SkillName.changeColor] == true)
                    {

                        canShake = true;

                        UnlockSkillLogic(skill);

            
                    }
                    break;
                case SkillName.spinAround:
                    if (isSkillUnlocked[SkillName.changeSize] == true && isSkillUnlocked[SkillName.shakeCube] == true)
                    {

                        
                        canRotate = true;
                        UnlockSkillLogic(skill);


                    }
                    break;
                case SkillName.rainbowColor:
                    if (isSkillUnlocked[SkillName.changeSize] == true)
                    {

                       
                        StartCoroutine("rainbowColor");
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
        if (isSkillUnlocked[SkillName.changeSize] == true && isSkillUnlocked[SkillName.shakeCube] == true)
        {
            skillCanBeUnlocked[SkillName.spinAround] = true;

        }
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
