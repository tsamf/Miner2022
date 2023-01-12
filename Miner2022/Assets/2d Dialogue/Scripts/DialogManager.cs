using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI dialogueTXT;
    [SerializeField] TextMeshProUGUI nameTXT;
    [SerializeField] Animator animator;

    Queue<string> sentences;
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        dialogueCanvas.gameObject.SetActive(true);
        animator.SetBool("IsOpen", true);
        sentences.Clear();

        nameTXT.text = dialogue.getNPCName();
        foreach (string sentence in dialogue.getSentences())
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTXT.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTXT.text += letter;
            yield return null; // Yields for a single frame before typing another letter;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        playerMovement.UnPause();
    }
}
