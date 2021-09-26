using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField]
    GameObject DestroyedParticle;
    [SerializeField]
    GameObject FloatingScoreTextPrefab;
    [SerializeField]
    AudioClip _pickupSound;
    private int scoreAmount = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(_pickupSound, transform.position);
            var particleTmp = Instantiate(DestroyedParticle, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateScore(scoreAmount);
            var floatingTextTmp = Instantiate(FloatingScoreTextPrefab, transform.position, Quaternion.identity); ;
            floatingTextTmp.GetComponent<TextMesh>().text = "+" + scoreAmount.ToString();
            Destroy(particleTmp, 3.0f);
            Destroy(this.gameObject);
        }
    }
}
