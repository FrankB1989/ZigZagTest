using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingText : MonoBehaviour
{
    // Start is called before the first frame update

    private int secondBeforeDestroy = 2;
    private int slideUpSpeed = 5;
    private Vector3 offset = new Vector3(0, 3, 0);
    void Start()
    {
        transform.position += offset;
        Destroy(gameObject, secondBeforeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * slideUpSpeed * Time.deltaTime);
    }
}
