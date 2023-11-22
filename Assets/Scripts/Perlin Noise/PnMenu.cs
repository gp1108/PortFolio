using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PnMenu : MonoBehaviour
{
    public GameObject perlinNoiseGen;
    public TMP_InputField worldSizeX;
    public TMP_InputField worldSizeZ;
    public Scrollbar noiseHeight;
    public Scrollbar detailScale;


    public void Reload()
    {
        perlinNoiseGen.GetComponent<PerlinNoise>().DeletePerlin();
    }

    public void SetWorldSizeX()
    {

       if(int.TryParse(worldSizeX.text, out int valorEntero))
       {
            perlinNoiseGen.GetComponent<PerlinNoise>().worldSizeX = valorEntero;
       }
    }

    public void SetWorldSizeZ()
    {
        if (int.TryParse(worldSizeZ.text, out int valorEntero))
        {
            perlinNoiseGen.GetComponent<PerlinNoise>().worldSizeZ = valorEntero;
        }

    }

    public void HeightNoise()
    {
        perlinNoiseGen.GetComponent<PerlinNoise>().noiseHeight =Mathf.FloorToInt( noiseHeight.value *10);
    }

    public void DetailScale()
    {
        if (detailScale.value == 0)
        {
            perlinNoiseGen.GetComponent<PerlinNoise>().detailScale = 1;
        }
        else
        {
            perlinNoiseGen.GetComponent<PerlinNoise>().detailScale = Mathf.FloorToInt((detailScale.value) * 40);
        }
        
    }
}
