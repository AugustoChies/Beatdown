using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
   public static DialogueManager Instance;
   [SerializeField] private GameObject dialoguePanel;
   [SerializeField] private Image dialogueUpperImage;
   [SerializeField] private Image playerRightImage; 
   [SerializeField] private Image playerLeftImage;

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
      //PlayDialogue(dialogueToPlay);
   }

   private void Update()
   {
      if (isPlayingDialogue) CheckForInputs();
   }

   public void PlayDialogue(Dialogue newDialogue)
   {
      dialoguePanel.SetActive(true);
      dialogueToPlay = newDialogue;
      dialogueBoxLeft.SetActive(false);
      dialogueBoxRight.SetActive(false);
      dialogueText.text = "";
      isPlayingDialogue = true;
      dialogueUpperImage.sprite = dialogueToPlay.dialogueUpperSprite;

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
         //dialoguePanel.SetActive(false);
         isPlayingDialogue = false;
         SceneManager.LoadScene("BattleScene");
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
      
      if (dialogueToPlay.dialogues[currentIndex].newSpriteRight)
         playerRightImage.sprite = dialogueToPlay.dialogues[currentIndex].newSpriteRight;
      
      if (dialogueToPlay.dialogues[currentIndex].newSpriteLeftBoy && Inventory.Instance.IsMale)
         playerLeftImage.sprite = dialogueToPlay.dialogues[currentIndex].newSpriteLeftBoy;
      
      if (dialogueToPlay.dialogues[currentIndex].newSpriteLeftGirl && !Inventory.Instance.IsMale)
         playerLeftImage.sprite = dialogueToPlay.dialogues[currentIndex].newSpriteLeftGirl;
      
      currentIndex++;
   }
}
