using UnityEngine;
using UnityEngine.UI;

//A Dialogue Line, holds all the data for a single Dialogue Line
//Used in conjuction with the Dialogue Holder
namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header("Text Options")]
        [TextArea(3,10)]
        [SerializeField] private string input = null;
        [SerializeField] private Color textColor = Color.black;
        [SerializeField] private Font textFont = null;

        [Header("Time Parameters")]
        [SerializeField] private float delay = 0;

        [Header("Sound")]
        [SerializeField] private AudioClip sound = null;

        [Header("Character")]
        [SerializeField] private Unit character = null;

        [Header("Image Holder")]
        [SerializeField] private SpriteRenderer imageHolder = null;

        [Header("Nameplates")]
        public Text namePlate1;
        public Text namePlate2;
        
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