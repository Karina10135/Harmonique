using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator anim;
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;


    public Queue<string> sentences;
    public static DialogueManager instance;

	void Start () {
        sentences = new Queue<string>();
        instance = this;
	}
	
	void Update () {
		
	}

    public void StartDialogue(Dialogue dialogue)
    {
        
        anim.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
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
