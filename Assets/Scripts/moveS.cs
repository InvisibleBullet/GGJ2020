﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveS : MonoBehaviour
{
    public Vector3 difference;
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    public Vector2 direction;
    public float moveSpeed;
    public Camera camera;
    public DistanceJoint2D joint;
    public float step;
    public LineRenderer line;
    public GameObject graber;

    public Vector3 diff;
    public int HP;
    public GameObject ripMenuUI;

    public Vector3 playerScale;

    public float waterDamageCooldown = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
    }

    void Update()
    {
        if (HP <= 0)
            ripMenuUI.SetActive(true);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, graber.transform.position);
        line.startWidth = 0.1f;
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (joint.distance < 5)
                joint.distance += step;
            else
                joint.distance = 5;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (joint.distance > 1)
                joint.distance -= step;
            else
                joint.distance = 1;
        }

        difference = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;

        rb.velocity = new Vector2(difference.x * moveSpeed, difference.y * moveSpeed);

        playerScale = transform.localScale;
        if (rb.velocity.x >= 0)
        {
            playerScale.x = 1;
            transform.localScale = playerScale;
        }
        else if (rb.velocity.x < 0)
        {
            playerScale.x = -1;
            transform.localScale = playerScale;
        }

        if (difference.x >= 2 || difference.x <= -2)
            gameObject.GetComponent<Animator>().SetFloat("run", difference.x);
        else if (difference.y >= 2 || difference.y <= -2)
            gameObject.GetComponent<Animator>().SetFloat("run", difference.y);
        else
            gameObject.GetComponent<Animator>().SetFloat("run", difference.x);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            diff = difference;

            if (diff.x > 20 || diff.x < -20 || diff.y > 20 || diff.y < -20)
                Damage(1);

        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Water") {
            Debug.Log("colliding with water");
            if (waterDamageCooldown == 0) {
                Damage(1);
            } 
            waterDamageCooldown += Time.deltaTime;
            if (waterDamageCooldown >= 1) {
                waterDamageCooldown = 0;
            }
        }
    }
    
    void Damage (int damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }
}
