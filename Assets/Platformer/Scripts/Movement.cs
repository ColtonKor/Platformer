using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Camera");
        Vector3 newPosition = transform.position + new Vector3(direction, 0, 0) * speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
