using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed=1;

    List<Transform> targets;

    Vector3 direction;
    Vector3 targetPos;
    float distance =10000;

    int health = 1;

    // Start is called before the first frame update
    void Start()
    {


    }


    public void Instanciate(Vector3 startPos, List<Transform> targets)
    {
        transform.position = startPos;
        this.targets = targets;

        health = 1;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        distance = 100000;

        //fins the closest player
        for(int i = 0; i < targets.Count; i++)
        {
            if(Vector3.Distance(targets[i].position,transform.position)<distance)
            {
                distance = Vector3.Distance(targets[i].position, transform.position);
                targetPos = targets[i].position;
            }
        }       

        direction = (targetPos - transform.position).normalized;
        distance = Vector3.Distance(targetPos, transform.position);
        transform.up = direction;

        if (distance > 0.1f)
        {
            transform.position += direction * speed / 20;
        }

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}