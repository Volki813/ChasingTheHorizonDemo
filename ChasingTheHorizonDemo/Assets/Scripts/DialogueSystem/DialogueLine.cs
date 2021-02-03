using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Parameters")]
        [SerializeField] private float delay;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Portrait")]
        [SerializeField] private Sprite portraitSprite;
        [SerializeField] private Image imageHolder;

        [Header("Textbox")]
        public bool textBox1;
        public bool textBox2;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = portraitSprite;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound));
        }
    }
}