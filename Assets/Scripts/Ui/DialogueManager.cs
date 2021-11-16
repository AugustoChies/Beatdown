using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
   public static DialogueManager Instance;
   [SerializeField] private GameObject dialoguePanel;
   [SerializeField] private Dialogue dialogueToPlay;

   private bool isPlayingDialogue;
   private void Awake()
   {
      Instance = this;
      dialoguePanel.SetActive(false);
   }

   private void Update()
   {
      if (isPlayingDialogue) CheckForInputs();
   }

   public void PlayDialogue(Dialogue newDialogue)
   {
      dialogueToPlay = newDialogue;
      //play dialogue here
   }

   public void CheckForInputs()
   {
      //check for button presses
   }

   public void NextDialogueLine()
   {
      //go to next dialogue line, if finished, end dialogue
   }
}
