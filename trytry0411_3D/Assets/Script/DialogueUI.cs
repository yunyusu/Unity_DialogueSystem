using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    // Start is called before the first frame update
    private TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject){
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    // Update is called once per frame
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject){
        //yield return new WaitForSeconds(2);
        for(int i=0; i< dialogueObject.Dialogue.Length; i++){
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);
            
            if(i== dialogueObject.Dialogue.Length -1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if(dialogueObject.HasResponses){
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else{
            CloseDialogueBox();
        }
    }

    private void CloseDialogueBox(){
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
