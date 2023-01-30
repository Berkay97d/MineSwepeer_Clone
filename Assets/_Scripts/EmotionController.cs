using System;
using UnityEngine;
using UnityEngine.UI;


public class EmotionController : MonoBehaviour
    {
        [SerializeField] private Sprite smile, cry, win;
        [SerializeField] private SpriteRenderer renderer;


        private void Update()
        {
            AdjustEmotion();
        }

        private void AdjustEmotion()
        {
            if (GameController.GameState == GameState.Lost)
            {
                renderer.sprite = cry;
                return;
            }

            if (GameController.GameState == GameState.Win)
            {
                renderer.sprite = win;
                return;
            }

            renderer.sprite = smile;
        }

        private void OnMouseOver()
        {
            Debug.Log("sa");
            if (Input.GetMouseButtonUp(0))
            {
                GameController.RestartGame();
            }
        }
    }
