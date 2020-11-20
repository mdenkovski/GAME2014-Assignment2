using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// ItemSpawner.cs
/// Last Edit Oct 23, 2020
/// - spawn a random item of the prefabs
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    public GameObject PotionPrefab;

    public GameObject SmallGoldPrefab;
    public GameObject MediumGoldPrefab;
    public GameObject LargeGoldPrefab;



    // Start is called before the first frame update
    void Start()
    {
        //choose a random item that we will spawn
        int spawnObject = Random.Range(0,4);
        switch (spawnObject)
        {
            case 0:
                //spawn a potion at our transform location
                Instantiate(PotionPrefab, transform);
                break;
            case 1:
                //spawn a small gold at our transform location
                Instantiate(SmallGoldPrefab, transform);
                break;
            case 2:
                //spawn a medium gold at our transform location
                Instantiate(MediumGoldPrefab, transform);
                break;
            case 3:
                //sawn a large gold at our transform location
                Instantiate(LargeGoldPrefab, transform);
                break;
            default:
                break;
        }
    }

}
