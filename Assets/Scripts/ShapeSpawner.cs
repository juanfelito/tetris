using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour {
    public GameObject[] shapes;

    public GameObject[] nextShapes;

    GameObject upNextShape = null;
    int nextIndex = 0;

    public void SpawnShape() {
        int shapeIndex = nextIndex;

        Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);

        nextIndex = Random.Range(0, 6);

        if (upNextShape != null) {
            Destroy(upNextShape);
        }
        upNextShape = Instantiate(nextShapes[nextIndex], transform.position + new Vector3(11, -2, 0), Quaternion.identity);
    }

    void Start() {
        SpawnShape();
    }
}
