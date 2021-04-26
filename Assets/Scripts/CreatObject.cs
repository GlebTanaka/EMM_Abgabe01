using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatObject : MonoBehaviour
{
    public GameObject myPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
       SpawnItem(); 
    }

    void SpawnItem()
    {
       GameObject clone = Instantiate(myPrefab, transform.position, Quaternion.identity);
    }

}
