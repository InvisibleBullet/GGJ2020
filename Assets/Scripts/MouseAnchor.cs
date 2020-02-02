using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAnchor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get main camera screen to world point, move anchor there
        Vector3 zFixedPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(zFixedPos);
        transform.position = worldPos;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
