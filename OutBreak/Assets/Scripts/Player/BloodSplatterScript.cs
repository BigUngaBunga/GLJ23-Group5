using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterScript : MonoBehaviour
{
    float delay=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }

    // Update is called once per frame
   
}
