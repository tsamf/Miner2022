using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Guide : MonoBehaviour
{
    [SerializeField] DialogueSO dialogue;

    [Header("E values")]
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] float distanceToStartFadeIn = 2f;
    [SerializeField] float distanceToFullyFadeIn = 1f;
    [SerializeField] float startingFadeInAlpha = .4f;
    [SerializeField] float endingAlpha = 1f;

    private PlayerMovement playerMovement;
    private Color textColor;


    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start() 
    {
        textColor = interactText.color;    
    }

    void Update()
    {
        Flip();
        InteractableTransparency();
    }

    private void InteractableTransparency()
    {
        //Get the distance between the player and the interactable
        float distance = Vector3.Distance(playerMovement.transform.position, transform.position);
        
        float mapped =  map(distance, distanceToStartFadeIn, distanceToFullyFadeIn, startingFadeInAlpha, endingAlpha); 
        
        interactText.color = new Color(textColor.r, textColor.g, textColor.b, mapped);
    }

    float map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        float mapped = toLow + (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow);
        return Mathf.Clamp(mapped,toLow, toHigh);
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

    public void StartDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
}
