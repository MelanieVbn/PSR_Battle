using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBeacon : MonoBehaviour
{
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
        Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);

    }
}
