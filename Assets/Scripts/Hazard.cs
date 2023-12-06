using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float levelSpeed;
    public float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveSelf();
    }

    private void moveSelf()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <=0)
        {
            Destroy(gameObject);
        }

        // TODO vector left
        transform.position = transform.position + Vector3.left * Time.deltaTime * levelSpeed;
    }

}
