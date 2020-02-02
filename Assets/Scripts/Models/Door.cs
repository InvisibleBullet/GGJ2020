using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float throughput = 1f;

    public Room RoomA;
    public Room RoomB;

    public float RoomAHeight = 0;
    public float RoomBHeight = 0;

    public bool isHorizontal;

    void Awake() {
        BoxCollider2D doorBox = gameObject.GetComponent<BoxCollider2D>();
        // Setting door values from its BoxCollider
        if (isHorizontal) {
            throughput = doorBox.bounds.size.x;
        }
        else {
            throughput = doorBox.bounds.size.y;
        }

        // Getting rooms connected with that door
        Collider2D[] doorColliders = new Collider2D[2];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("RoomLogic"));
        doorBox.OverlapCollider(contactFilter, doorColliders);
        
        // Setting door height in each room respective to its floor
        Collider2D roomACol = doorColliders[0];
        Collider2D roomBCol = doorColliders[1];
        RoomAHeight = SetRoomHeight(roomACol, doorBox);
        RoomBHeight = SetRoomHeight(roomBCol, doorBox);
        RoomA = roomACol.gameObject.GetComponent<Room>();
        RoomB = roomBCol.gameObject.GetComponent<Room>();

        // Adding this door to each of the rooms as a reference
        RoomA.doors.Add(this);
        RoomB.doors.Add(this);
    }

    float SetRoomHeight(Collider2D roomCol, Collider2D doorBox) {
        float roomCenter = roomCol.bounds.center.y;
        float roomLowPoint = roomCol.bounds.center.y - roomCol.bounds.size.y / 2;
        float roomHeight = roomCol.bounds.size.y;
        roomCol.GetComponent<Room>().roomBottomLine = roomLowPoint;
        if (isHorizontal) {
            if (doorBox.bounds.center.y < roomCenter) {
                return 0;
            }
            else {
                return roomHeight;
            }
        }
        else {
            float doorLowPoint = doorBox.bounds.center.y - doorBox.bounds.size.y / 2;
            return doorLowPoint - roomLowPoint;
        }
 
    }

    public float GetDiff(Room roomFrom) {
        float height = roomFrom == RoomA ? RoomAHeight : RoomBHeight;
        float diff = (float)System.Math.Round(roomFrom.waterHeight - height, 2);
        return diff;
    }

    public void MoveWater(float water, Room roomFrom) {
        Room fromTemp = roomFrom == RoomA ? RoomA : RoomB;
        Room toTemp = roomFrom == RoomA ? RoomB : RoomA;
        if (!isHorizontal) {
            if (fromTemp.roomBottomLine + fromTemp.waterHeight <= toTemp.roomBottomLine + toTemp.waterHeight) {
                return;
            }
        }

        fromTemp.UpdateWater(-water);
        toTemp.UpdateWater(water);
    }
}
