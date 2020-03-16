using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEnemy_movement : MonoBehaviour
{

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKicked(float dir)
    {
        rb.AddForce((new Vector3(dir * 2.0f, 2.0f, 0.0f)), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
