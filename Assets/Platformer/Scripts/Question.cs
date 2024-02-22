using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    // Start is called before the first frame update
    private float rate = -.2f;
    private int check = 0;
    void Start()
    {
        // StartCoroutine(Block());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(check == 0){
            StartCoroutine(Block(0.75f));
        }
    }

    IEnumerator Block(float duration){
        float timeElapsed = 0f;
        check++;

        while(timeElapsed < duration){
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material.mainTextureOffset = new Vector2(0f, rate);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        check--;
        rate -= 0.2f;
    }
}
