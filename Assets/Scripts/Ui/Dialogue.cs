using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct DialogueStruct
    {
        //public string speakerName;
        public string dialogueText;
        public bool isRightSpeaker;
        public Sprite newSpriteRight;
        public Sprite newSpriteLeftBoy;
        public Sprite newSpriteLeftGirl;
    }

    public Sprite dialogueUpperSprite;

    public DialogueStruct[] dialogues;
}
