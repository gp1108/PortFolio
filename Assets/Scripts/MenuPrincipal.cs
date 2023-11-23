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
        SceneManager.LoadScene(4);
    }
    public void Guns()
    {
        SceneManager.LoadScene(5);
    }

    public void Movement()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
