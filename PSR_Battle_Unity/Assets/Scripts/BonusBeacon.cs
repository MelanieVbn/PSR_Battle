using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBeacon : MonoBehaviour
{
    public GameObject warriorPrefab;
    private string[] warriorTypes = { "Paper", "Scissor", "Rock" };
    void Awake()
    {
        for (int i = 0; i < 150; i++)
        {
            if(i < 50)
            {
                warriorPrefab.tag = warriorTypes[0];
            }
            if ( i >= 50 && i < 100)
            {
                warriorPrefab.tag = warriorTypes[1];
            }
            if (i >= 100 && i < 150)
            {
                warriorPrefab.tag = warriorTypes[2];
            }
            Instantiate(warriorPrefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject go = coll.collider.gameObject;
        Debug.Log("Bonus Touché");
        //Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);

    }
}
