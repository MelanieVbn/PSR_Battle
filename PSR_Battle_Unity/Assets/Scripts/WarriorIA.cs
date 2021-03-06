using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarriorIA : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Transform target;//set target from inspector instead of looking in Update
    private float speed = 1f;
    private string enemyTag;
    private string baseTag;
    private Boolean noEnemies = false;
    private float x;
    private float y;
    private float timeBeforeDirectionChangement = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        switch (gameObject.tag){
            case "Rock":
                sr.color = Color.grey;
                enemyTag = "Scissor";
                break;
            case "Paper":
                enemyTag = "Rock";
                sr.color = Color.white;
                break;
            case "Scissor":
                enemyTag = "Paper";
                sr.color = Color.blue;
                break;
        }
        baseTag = gameObject.tag;

        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(baseTag != gameObject.tag)
        {
            baseTag = gameObject.tag;
            switch (gameObject.tag)
            {
                case "Rock":
                    sr.color = Color.grey;
                    enemyTag = "Scissor";
                    break;
                case "Paper":
                    enemyTag = "Rock";
                    sr.color = Color.white;
                    break;
                case "Scissor":
                    enemyTag = "Paper";
                    sr.color = Color.blue;
                    break;
            }
        }
    }
    void FixedUpdate()
    {
        GameObject clostestEnemy = FindClosestEnemy();
        if (!noEnemies)
        {
            if (clostestEnemy != null)
            {
                target = clostestEnemy.transform;
                //rotateLookAt();
                Vector3 direction = (target.transform.position - rb.transform.position).normalized;
                //rotate to look at the player
                //rb.transform.LookAt(target);
                //rb.transform.Rotate(new Vector3(0, 90, 0), Space.Self);//correcting the original rotation


                //move towards the player
                if (Vector3.Distance(rb.transform.position, target.position) > 0)
                {
                    //rb.MovePosition(rb.transform.position + (target.position.normalized * speed * Time.deltaTime));
                    rb.MovePosition(rb.transform.position + direction * speed * Time.fixedDeltaTime);
                    target = null;
                }
            }
        }
        else
        {
            RandomMovement();
        }
        
    }

    public GameObject FindClosestEnemy()
    {
        if(target == null || (Vector3.Distance(transform.position, target.position) > 0.5f))
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            if(gos.Length > 0)
            {
                noEnemies = false;
                foreach (GameObject go in gos)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
                if (closest != null)
                {
                    Debug.Log("L'enemi le plus proche de " + gameObject.tag + " est : " + closest.tag);
                }
                return closest;
            }
            else
            {
                noEnemies = true;
            }
        }
        return null;
    }

    void rotateLookAt()
    {
        Vector3 dir = target.position - rb.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void RandomMovement()
    {
        Vector3 vector = new Vector3(x, y, 0);
        vector = vector.normalized * speed * Time.deltaTime;
        /*timeBeforeDirectionChangement -= Time.deltaTime;  // T.dt is secs since last update
        if (timeBeforeDirectionChangement <= 0)
        {
            timeBeforeDirectionChangement = 2;
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
        }*/

        rb.MovePosition(rb.transform.position + vector);
    }
   
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Quel objet a été touché 
        //Debug.Log("collided with " + coll.collider.gameObject.name);

        if(coll.collider.gameObject.tag == enemyTag)
        {
            Debug.Log(gameObject.tag+" ! : Collision avec l'ennemi ! -> "+enemyTag);
            coll.collider.gameObject.tag = gameObject.tag;
        }

        switch (coll.collider.gameObject.name)
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
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == enemyTag)
        {
            Debug.Log(gameObject.tag + " ! : Collision avec l'ennemi ! -> " + enemyTag);
            coll.collider.gameObject.tag = gameObject.tag;
        }

        switch (coll.collider.gameObject.name)
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
        }
    }
}
