using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   

    public void ProceduralGenration()
    {
        SceneManager.LoadScene(1);
    }
    public void SkillTree()
    {
        SceneManager.LoadScene(2);
    }
    public void BuildSystem()
    {
        SceneManager.LoadScene(3);
    }
    public void Guns()
    {
        SceneManager.LoadScene(4);
    }
}