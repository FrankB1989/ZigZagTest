using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;

    [SerializeField]
    AudioClip _playerMoveSound;

    Vector3 direction;
    float amountToMove;
    bool isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow)) && !isDead)
        {
            AudioSource.PlayClipAtPoint(_playerMoveSound, transform.position);
            if (direction == Vector3.forward)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
        }

        amountToMove = speed * Time.deltaTime;
        transform.Translate(direction * amountToMove);
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            RaycastHit hitInfo;

            Ray ray = new Ray(transform.position, Vector3.down);

            if(!Physics.Raycast(ray, out hitInfo))
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
        }
    }

}
