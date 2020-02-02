using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public float totalArea;

    PolygonCollider2D col;

    void Awake() {
        col = gameObject.GetComponent<PolygonCollider2D>();
        Mesh mesh = null;
        totalArea = SuperficieIrregularPolygon(col);
        Debug.Log(string.Format("GO: {0}; Collider area: {1}", gameObject.name, totalArea));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ModifyCollider() {
        /*
            Run this when duct tape is placed AND if it intersects with this collider.
            On this step, we should somehow modify mesh collider in place
            Then we'll check if emitter transform is still within the newly generated collider.
            If it's not, turn off water emission.
            ???
            PROFIT
        */
    }

    float SuperficieIrregularPolygon(PolygonCollider2D col) {
        List<Vector2> list = new List<Vector2>(col.points);
        float temp = 0;
        for (int i=0; i < list.Count ; i++) {
            if (i != list.Count - 1) {
                float mulA = list[i].x * list[i+1].y;
                float mulB = list[i+1].x * list[i].y;
                temp = temp + ( mulA - mulB );
            } else {
                float mulA = list[i].x * list[0].y;
                float mulB = list[0].x * list[i].y;
                temp = temp + ( mulA - mulB );
            }
        }
        temp *= 0.5f;
        return Mathf.Abs(temp);
    }
}
