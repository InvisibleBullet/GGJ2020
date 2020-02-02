﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public Room room;

    public float rate = 0.5f;

    public bool isClosed = false;

    void Awake()
    {
        room = GetRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (room && !isClosed) {
            room.UpdateWater(rate * Time.deltaTime * 3);
        }
    }

    private Room GetRoom()
    {
        BoxCollider2D col2d = gameObject.GetComponent<BoxCollider2D>();
        Collider2D[] rooms = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("RoomLogic"));
        col2d.OverlapCollider(contactFilter, rooms);

        Collider2D roomCollider = rooms[0];

        return roomCollider.gameObject.GetComponent<Room>();
    }
}