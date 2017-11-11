using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public Vector3 RotationVectorOne;
    public Vector3 RotationVectorTwo;
    public float speed;

    // Update is called once per frame
    void Update () {
        transform.Rotate(RotationVectorOne, Time.deltaTime * speed);
        transform.Rotate(RotationVectorTwo, Time.deltaTime * speed, Space.World);
    }
}
