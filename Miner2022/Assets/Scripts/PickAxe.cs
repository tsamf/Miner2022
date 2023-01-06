using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour
{
    [SerializeField] AudioClip throwSFX;

    void Start()
    {
        AudioSource.PlayClipAtPoint(throwSFX, Camera.main.transform.position);
    }
}
