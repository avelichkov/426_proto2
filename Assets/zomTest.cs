using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zomTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Collider2D coll1 = GameObject.Find("Zom1").GetComponent<Collider2D>();
        Debug.Log(GetComponent<Collider2D>().IsTouching(coll1));
    }
}
