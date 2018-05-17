using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public bool dialogue;
    public Animator anim;
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;


    public Queue<string> sentences;
    public static DialogueManager instance;

	void Start ()
    {
        sentences = new Queue<string>();
        instance = this;
	}
	

    public void StartDialogue(Dialogue dialogueTrig)
    {
        dialogue = true;
        anim.SetBool("isOpen", true);
        nameText.text = dialogueTrig.name;
        sentences.Clear();

        foreach(string sentence in dialogueTrig.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();

    }

    public void DisplayNextSentences()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        dialogue = false;

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

}
