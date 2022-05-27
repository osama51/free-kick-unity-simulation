using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassTrigger : MonoBehaviour
{
    public Collider Grass;
    public AudioSource audioSource2;
    public AudioClip grass;
    public float volume2 = 1.0f;

    void OnCollisionEnter (Collision collision) {
        audioSource2.PlayOneShot(grass, volume2);
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource2.PlayOneShot(grass, volume2);
    }

    void Update() {
        //Debug.Log("Trigger On : " + myObject.isTrigger);
    }
} 
