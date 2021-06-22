using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header("Text Options")]
        [TextArea(3,10)]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Parameters")]
        [SerializeField] private float delay;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Character")]
        [SerializeField] private Unit character;

        [Header("Image Holder")]
        [SerializeField] private Image imageHolder;

        [Header("Nameplates")]
        [SerializeField] private Text namePlate1;
        [SerializeField] private Text namePlate2;
        
        private Sprite portraitSprite;

        [Header("Textbox")]
        public bool textBox1;
        public bool textBox2;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            portraitSprite = character.portrait;
        }

        private void Start()
        {            
            imageHolder.sprite = portraitSprite;
            if (textBox1)
            {
                namePlate1.text = character.unitName;
            }
            else
            {
                namePlate2.text = character.unitName;
            }
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound));
        }
    }
}