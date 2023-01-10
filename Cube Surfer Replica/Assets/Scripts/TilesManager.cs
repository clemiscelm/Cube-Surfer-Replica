using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    [SerializeField] HeroStackController heroStackController;
    private List<GameObject> activeTiles;
    public GameObject[] _bonusTiles;
    public GameObject[] _malusTiles;

    public float tileLength = 10;
    public int numberOfTiles = 8;
    public int totalNumOfTiles = 9;
    public int _nbOfBonus = 0;

    public float zSpawn = 0;

    private Transform playerTransform;

    private int previousIndex;
    int random = 0;

    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile();
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  
    }
    void Update()
    {
        random = Random.Range(3, 7);
        if (playerTransform.position.z - tileLength >= zSpawn - (numberOfTiles * tileLength) && _nbOfBonus <= random)
        {
            int index = Random.Range(0, totalNumOfTiles);
            while (index == previousIndex)
                index = Random.Range(0, totalNumOfTiles);

            DeleteTile();
            SpawnTile(index);
        }
        else if(playerTransform.position.z - tileLength >= zSpawn - (numberOfTiles * tileLength) && _nbOfBonus > random)
        {
            int index = Random.Range(0, totalNumOfTiles);
            while (index == previousIndex)
                index = Random.Range(0, totalNumOfTiles);

            DeleteTile();
            SpawnMalusTile(index);
        }

    }

    public void SpawnTile(int index = 0)
    {

        Instantiate(_bonusTiles[index], transform.forward * zSpawn, transform.rotation);
        GameObject tile = _bonusTiles[index];
        tile.SetActive(true);
        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
        _nbOfBonus++;
    }

    private void DeleteTile()
    {
        activeTiles.RemoveAt(0);
        //PlayerManager.score += 3;
    }

    public void SpawnMalusTile(int index = 0)
    {

        Instantiate(_malusTiles[index], transform.forward * zSpawn, transform.rotation);
        GameObject tile = _malusTiles[index];
        tile.SetActive(true);
        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
        _nbOfBonus = 0;
    }

    
}
