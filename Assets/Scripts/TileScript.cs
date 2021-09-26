using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TileSpawner.Instance.SpawnTiles();
            rigidbody.isKinematic = false;
            StartCoroutine(StackTiles());
        }
    }

    IEnumerator StackTiles()
    {
        yield return new WaitForSeconds(2.0f);
        rigidbody.isKinematic = true;
        TileSpawner.Instance.ReusedTiles.Push(this.gameObject);
    }
}
