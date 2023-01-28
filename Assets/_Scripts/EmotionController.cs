using System;
using UnityEngine;


    public class EmotionController : MonoBehaviour
    {
        [SerializeField] private Sprite smile, cry;
        [SerializeField] private SpriteRenderer renderer;

        private void Update()
        {
            AdjustEmotion();
        }

        private void AdjustEmotion()
        {
            if (GameController.isLost)
            {
                renderer.sprite = cry;
                return;
            }

            renderer.sprite = smile;
        }
    }
