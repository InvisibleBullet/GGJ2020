using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Door> doors = new List<Door>();

    public float volume;
    public float width;

    public float waterVolume = 0;

    public float waterHeight = 0;

    private GameObject waterPillar;

    public float roomBottomLine;

    public void UpdateWater(float amount) {
        waterVolume = Mathf.Min(waterVolume + amount, volume);
        waterHeight = (float)System.Math.Round(waterVolume / width, 2);
        UpdateWaterView();
    }

    void UpdateWaterView() {
        Vector3 lTemp = waterPillar.transform.localScale;
        lTemp.y = waterVolume / volume;
        waterPillar.transform.localScale = lTemp;
        BoxCollider2D pillarBox = waterPillar.GetComponent<BoxCollider2D>();
        BoxCollider2D roomBox = gameObject.GetComponent<BoxCollider2D>();
        float roomLowPoint = roomBox.bounds.center.y - roomBox.bounds.size.y / 2;

        Vector3 newPos = new Vector3(waterPillar.transform.position.x, roomLowPoint + pillarBox.bounds.size.y / 2, waterPillar.transform.position.z);
        waterPillar.transform.position = newPos;
    }

    void Awake()
    {
        waterPillar = gameObject.transform.Find("Water").gameObject;
        BoxCollider2D roomBox = gameObject.GetComponent<BoxCollider2D>();
        volume = roomBox.bounds.size.x * roomBox.bounds.size.y;
        width = roomBox.bounds.size.x;
        UpdateWater(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Managing water
        List<Door> doorsToManage = new List<Door>();
        foreach (Door door in doors) {
            float diff = door.GetDiff(this);
            if (diff > 0) {
                doorsToManage.Add(door);
            }
        }

        foreach (Door door in doorsToManage) {
            float diff = door.GetDiff(this);
            if (diff > 0) {
                float waterToRemove = (float)System.Math.Round(Mathf.Clamp(diff / door.throughput, 0, 1), 2);
                door.MoveWater(waterToRemove * Time.deltaTime * 3, this);
            }
        }
    }
}
