using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
   public static DialogueManager Instance;
   [SerializeField] private GameObject dialoguePanel;
   [SerializeField] private GameObject dialogueBoxRight;
   [SerializeField] private GameObject dialogueBoxLeft;
   [SerializeField] private TextMeshProUGUI dialogueText;

   [SerializeField] private Dialogue dialogueToPlay;

   [SerializeField]private int currentIndex;

   private bool isPlayingDialogue;

  
   private void Awake()
   {
      Instance = this;
      dialoguePanel.SetActive(false);
      
      //debug
     // PlayDialogue(dialogueToPlay);
   }

   private void Update()
   {
      if (isPlayingDialogue) CheckForInputs();
   }

   public void PlayDialogue(Dialogue newDialogue)
   {
      dialogueToPlay = newDialogue;
      dialogueBoxLeft.SetActive(false);
      dialogueBoxRight.SetActive(false);
      dialogueText.text = "";
      isPlayingDialogue = true;

      NextDialogueLine();
   }

   public void CheckForInputs()
   {
      if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
      {
         NextDialogueLine();
      }
   }

   public void NextDialogueLine()
   {
      if (currentIndex >= dialogueToPlay.dialogues.Length)
      {
         dialoguePanel.SetActive(false);
         isPlayingDialogue = false;
         return;
      }

      if (dialogueToPlay.dialogues[currentIndex].isRightSpeaker)
      {
         dialogueBoxRight.SetActive(true);
         dialogueBoxLeft.SetActive(false);
      }
      else
      {
         dialogueBoxLeft.SetActive(true);
         dialogueBoxRight.SetActive(false);
      }

      dialogueText.text = dialogueToPlay.dialogues[currentIndex].dialogueText;
      currentIndex++;
   }
}
