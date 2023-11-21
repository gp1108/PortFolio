
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerlinNoise : MonoBehaviour
{
    [Header ("Terrain Cubes")]
    //Bloques a instanciar como terreno
    public GameObject cubeGameObjectGrass;
    public GameObject cubeGameObjectWater;
    public GameObject cubeGameObjectHill;
    private int[] rotaciones = { 0, 90, 180, 270 };



    [Header("Perlin Noise")]
    //Dimension de la generacion
    public int worldSizeX;
    public int worldSizeZ;
    private float _gridOffset;
    //Caracteristicas de la perlin noise
    public int noiseHeight; // basicamente como de alto se genera nuestro cubo 
    public float detailScale; //A menos cantidad mas abrupto se ven las coas , valores muy altos pueden dar lugar a superficies muy planas
    public GameObject emptyGameObject;

    private float _perlinNoiseToInt; // creo esta variable para aproximar los valores a enteros y que de la semsacion de minecraft
    private int _randomSeed;

    [Header("Props")]
    //Generacion procedural de arboles, rocas etc.
    private List<GameObject> blockPositions = new List<GameObject>();
    public GameObject[] worldProps;

    void Start()
    {
        worldSizeX= 100;
        worldSizeZ = 100;
        noiseHeight = 6;
        detailScale = 30;
        PerlinNoiseGen();

    }
    private void PerlinNoiseGen()
    {
        // Genera una semilla aleatoria
        _randomSeed = Random.Range(0, 10000);
        

        _gridOffset = 1;


        //detailScale = 30;

        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
                _perlinNoiseToInt = Mathf.RoundToInt(GenerateNoise(x, z, detailScale) * noiseHeight);
                
                Vector3 position = new Vector3(x * _gridOffset, _perlinNoiseToInt, z * _gridOffset); // en el eje y va esto GenerateNoise(x,z,_detailScale) * _noiseHeight

                //Spawn which block depending on Y coordinate
                if (_perlinNoiseToInt == 0)
                {
                    GameObject cube = Instantiate(cubeGameObjectWater, position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, rotaciones[Random.Range(0, rotaciones.Length)], 0)) as GameObject;
                    cube.transform.SetParent(emptyGameObject.transform);

                }
                else if (_perlinNoiseToInt >= 1 && _perlinNoiseToInt <= 1)
                {
                    GameObject cube = Instantiate(cubeGameObjectGrass, position, Quaternion.identity) as GameObject;
                    cube.transform.SetParent(emptyGameObject.transform);

                }
                else
                {
                    GameObject cube = Instantiate(cubeGameObjectHill, position, Quaternion.identity) as GameObject;
                    cube.transform.SetParent(emptyGameObject.transform);

                }

            }

        }
    }
    
    
    public void DeletePerlin()
    {
        
        Transform[] children = new Transform[emptyGameObject.transform.childCount];
        for (int i = 0; i < emptyGameObject.transform.childCount; i++)
        {
            children[i] = emptyGameObject.transform.GetChild(i);
        }

        // Elimina cada hijo
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        PerlinNoiseGen();
    }

    
    /*
    private void SpawnObject()
    {
        //Aqui se puede usar la misma logica de altura para determinar que gameobject se spawnea , yo lo hago aleatorio pero es bastante feo
        for(int i = 0; i<40;i++)
        {
            GameObject toPlaceObject = Instantiate(worldProps[Random.Range(0, worldProps.Length)], ObjectsSpawnLocation(), Quaternion.Euler(0,Random.Range(0,360),0));
            toPlaceObject.transform.SetParent(propsGroup.transform);
        }
    }

    private Vector3 ObjectsSpawnLocation()
    {
        int randomIndex = Random.Range(0, blockPositions.Count);
        Vector3 newPosition = new Vector3(blockPositions[randomIndex].x, blockPositions[randomIndex].y + 1.01f, blockPositions[randomIndex].z);
        blockPositions.RemoveAt(randomIndex);
        return newPosition;
    }
    */
   
    

    private float GenerateNoise(int x, int z, float detailScale)
    {
        float xNoise = _randomSeed +(x + this.transform.position.x) / detailScale;
        float zNoise = _randomSeed +(z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
    
}
