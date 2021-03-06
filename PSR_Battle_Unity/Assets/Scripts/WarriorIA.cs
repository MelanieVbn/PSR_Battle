using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarriorIA : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Transform target;//set target from inspector instead of looking in Update
    private Vector3 targetDir;
    private float speed = 2f;
    private string enemyTag;
    private string hunterTag;
    private string baseTag;
    private Boolean runAway = false;
    private Boolean noEnemies = false;
    private float x;
    private float y;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        switch (gameObject.tag){
            case "Rock":
                sr.color = Color.grey;
                enemyTag = "Scissor";
                hunterTag = "Paper";
                break;
            case "Paper":
                enemyTag = "Rock";
                hunterTag = "Scissor";
                sr.color = Color.white;
                break;
            case "Scissor":
                enemyTag = "Paper";
                hunterTag = "Rock";
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
                    hunterTag = "Paper";
                    break;
                case "Paper":
                    enemyTag = "Rock";
                    hunterTag = "Scissor";
                    sr.color = Color.white;
                    break;
                case "Scissor":
                    enemyTag = "Paper";
                    hunterTag = "Rock";
                    sr.color = Color.blue;
                    break;
            }
        }

        GameObject clostestElement = FindClosestElement();

        if (clostestElement != null)
        {
            target = clostestElement.transform;

            if (target.tag == hunterTag && (Vector3.Distance(rb.transform.position, target.position) < 1))
            {
                runAway = true;
            }
            else
            {
                runAway = false;
            }

            if (!noEnemies)
            {
                x = target.position.x;
                y = target.position.y;
                //vector = vector.normalized * speed * Time.deltaTime;
                //rotateLookAt();
                if (target.tag == enemyTag)
                {
                    targetDir = (new Vector3(x, y, 0) - rb.transform.position).normalized * (speed * 0.5f) * Time.deltaTime;
                }

                if (target.tag == hunterTag && runAway)
                {
                    targetDir = (rb.transform.position - new Vector3(x, y, 0)).normalized * (speed * 0.5f) * Time.deltaTime;
                }
            }
            else
            {
                if (target.tag == hunterTag && runAway)
                {
                    x = target.position.x;
                    y = target.position.y;
                    targetDir = (rb.transform.position - new Vector3(x, y, 0)).normalized * (speed * 0.5f) * Time.deltaTime;
                }
                else
                {
                    targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                }
            }
        }
        else
        {
            targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
        }
    }
    void FixedUpdate()
    { 
        rb.MovePosition(rb.transform.position + targetDir);     
    }

    public GameObject FindClosestElement()
    {
        if(target == null || (Vector3.Distance(transform.position, target.position) > 0.5f))
        {
            List<GameObject> gos = new List<GameObject>();
            List<GameObject> enemies;
            enemies = GameObject.FindGameObjectsWithTag(enemyTag).ToList();
            List<GameObject> hunters;
            hunters = GameObject.FindGameObjectsWithTag(hunterTag).ToList();
            gos.AddRange(enemies);
            gos.AddRange(hunters);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            if(gos.Count() > 0)
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

                if (enemies.Count() == 0)
                {
                    noEnemies = true;
                }
                return closest;
            }
        }
        return null;
    }
   
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Quel objet a été touché 
        Debug.Log("collided with " + coll.collider.gameObject.name);

        if(coll.collider.gameObject.tag == enemyTag)
        {
            Debug.Log(gameObject.tag+" ! : Collision avec l'ennemi ! -> "+enemyTag);
            coll.collider.gameObject.tag = gameObject.tag;
        }

        //vector = vector.normalized * speed * Time.deltaTime;
        //rotateLookAt();
        
        switch (coll.collider.gameObject.name)
        {
            case "BorderTop":
                y = -1;
                x = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderBottom":
                y = 1;
                x = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderRight":
                x = -1;
                y = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderLeft":
                x = 1;
                y = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
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
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderBottom":
                y = 1;
                x = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderRight":
                x = -1;
                y = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
            case "BorderLeft":
                x = 1;
                y = Random.Range(-1f, 1f);
                targetDir = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                break;
        }
    }
}
