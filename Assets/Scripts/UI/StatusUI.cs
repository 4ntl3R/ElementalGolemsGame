using System;
using Base;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh; 
        private void Awake()
        {
            SetStatusText();
        }

        private void SetStatusText()
        {
            switch (GameStatus.GameState)
            {
                case (EndingOptions.Draw):
                    textMesh.text = "Draw";
                    break;
                case (EndingOptions.Win):
                    textMesh.text = "You won!";
                    break;
                case (EndingOptions.Lose):
                    textMesh.text = "You lost";
                    break;
                default:
                    textMesh.text = "Try again";
                    break;
            }
        }
    }
}
