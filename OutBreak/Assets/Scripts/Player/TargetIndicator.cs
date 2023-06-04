using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    GameObject target;
    [SerializeField] private GameObject indicator;
    public float offScreenThreshold = 10.5f;
    public Camera cam;
    private bool isIndicatorActive = true;
    [SerializeField] float distanceToTarget;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag=="Player1")
        {
            target = GameObject.FindGameObjectWithTag("Player2");

        }
        else if (gameObject.tag == "Player2")
        {
            target = GameObject.FindGameObjectWithTag("Player1");
        }
        if(target==null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
        if (isIndicatorActive)
        {
            Vector3 targetDirect = target.transform.position - transform.position;
            distanceToTarget = targetDirect.magnitude;

            if(distanceToTarget < offScreenThreshold)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled=false;              
            }
            else
            {
                Vector3 targetViewportPosition = cam.WorldToViewportPoint(target.transform.position);
                if(targetViewportPosition.z > 0 && targetViewportPosition.x >0 && targetViewportPosition.x <1 && targetViewportPosition.y > 0 && targetViewportPosition.y <1)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false; 
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true; ;
                    Vector3 screenEdge = cam.ViewportToWorldPoint(new Vector3(Mathf.Clamp(targetViewportPosition.x, 0.1f, 0.9f), Mathf.Clamp(targetViewportPosition.y, 0.1f, 0.9f), cam.nearClipPlane));
                    transform.position = new Vector3(screenEdge.x, screenEdge.y, 0);
                    Vector3 direction = target.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0,0,angle+180);
                }
            }

        }
       
    }
}
