using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{

    public GameObject player;
    public float smoothTime;


    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float posY = Mathf.Lerp(transform.position.y, player.transform.position.y, smoothTime);
        float posX = Mathf.Lerp(transform.position.x, player.transform.position.x, smoothTime);

        transform.position = new Vector3(posX, posY + 1.5f, transform.position.z);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }
}
