using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 2f;
    private Vector3 vector;
    private float x;
    private float y;
    private float timeBeforeDirectionChangement = 2;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vector = new Vector3(x, y, 0);
        vector = vector.normalized * speed * Time.deltaTime;
        timeBeforeDirectionChangement -= Time.deltaTime;  // T.dt is secs since last update
        if (timeBeforeDirectionChangement <= 0)
        {
            timeBeforeDirectionChangement = 2;
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.transform.position + vector);
        //rb.AddForce(Vector2.right * 20,ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Quel objet a été touché 
        /*Debug.Log("collided with " + coll.collider.gameObject.name);

        switch(coll.collider.gameObject.name)
        {
            case "BorderTop":
                y = -1;
                x = Random.Range(-1f, 1f);
                break;
            case "BorderBottom":
                y = 1;
                x = Random.Range(-1f, 1f);
                break;
            case "BorderRight":
                x = -1;
                y = Random.Range(-1f, 1f);
                break;
            case "BorderLeft":
                x = 1;
                y = Random.Range(-1f, 1f);
                break;
        }*/
    }
}
