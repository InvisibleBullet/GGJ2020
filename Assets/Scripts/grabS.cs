using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabS : MonoBehaviour
{
    public GameObject grabObject;
    public RaycastHit2D hit;
    public FixedJoint2D joint;
    public LayerMask mask;
    public GameObject parent;
    public float radius;
    public bool i = false;
    // Start is called before the first frame update
    void Start()
    {
        joint = parent.GetComponent<FixedJoint2D>();
        joint.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {


        if (i == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics2D.CircleCast(transform.position, radius, new Vector2(0f, 0f), 0f, mask))
            {
                hit = Physics2D.CircleCast(transform.position, radius, new Vector2(0f, 0f), 0f, mask);
                Debug.Log(hit.transform.name);
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.enabled = true;
                i = true;
                parent.GetComponent<Animator>().SetBool("i", true);

            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                parent.GetComponent<Animator>().SetTrigger("trigger");
            }
        }
        else if (i == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            joint.enabled = false;
            i = false;
            parent.GetComponent<Animator>().SetBool("i", false);
        }







        /*if (grabObject != null)
        {
            grabObject.transform.position = transform.position;
            if (Input.GetKeyDown(KeyCode.G))
            {
                grabObject = null;
                //col.transform.SetParent(null);

            }
        }*/
    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (col.CompareTag("GrabObject"))
            {
                grabObject = col.gameObject;
                //col.transform.SetParent(transform);
            }

        }
        

    }*/
}


