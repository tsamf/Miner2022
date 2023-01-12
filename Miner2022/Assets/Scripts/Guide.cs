using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide : MonoBehaviour
{

    [SerializeField] DialogueSO dialogue;
    [SerializeField] private TextMeshProUGUI interactText;
    private PlayerMovement playerMovement;
    

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (playerMovement.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            interactText.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            interactText.transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == ("Player"))
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == ("Player"))
        {
            interactText.gameObject.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue); 
    }
}
