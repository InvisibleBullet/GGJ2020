using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxS : MonoBehaviour
{
    public Transform cam;
    public float relativeMove = .3f;
    public float ix;
    public float iy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(cam.position.x * relativeMove+ ix, cam.position.y * relativeMove +iy);
    }
}
