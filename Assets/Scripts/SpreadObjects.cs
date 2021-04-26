using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadObjects : MonoBehaviour
{
    public GameObject itemToSpread;

    // Numbers of objects to spread over an area
    public int numItemsToSpawn = 10;

    // Limitation ot area
    public float itemXSpread = 10;

    public float itemYSpread = 0;

    public float itemZSpread = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < numItemsToSpawn; i++)
        {
            SpreadObject();
        }
    }

    void SpreadObject()
    {
        // Random position in Space
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread),
            Random.Range(-itemYSpread, itemYSpread)
            , Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        // + transform.position creates an offset from world space
        GameObject clone = Instantiate(itemToSpread, randPosition, Quaternion.identity);
    }
}