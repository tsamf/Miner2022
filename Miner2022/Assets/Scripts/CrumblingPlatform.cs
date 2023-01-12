using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] float crumbleTimer = 1f;
    [SerializeField] float respawnTimer = 2f;

    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    SoundManager soundManager;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           
            StartCoroutine(Crumble());
        }
    }

    private IEnumerator Crumble()
    {
        myAnimator.SetBool("isCrumbling", true);
        soundManager.PlayPlatformCrumbleSFX();
        yield return new  WaitForSeconds(crumbleTimer);
        mySpriteRenderer.enabled = false;
        myBoxCollider2D.enabled = false;
        yield return new  WaitForSeconds(respawnTimer);
        myAnimator.SetBool("isCrumbling", false);
        mySpriteRenderer.enabled = true;
        myBoxCollider2D.enabled = true;
    }
}
