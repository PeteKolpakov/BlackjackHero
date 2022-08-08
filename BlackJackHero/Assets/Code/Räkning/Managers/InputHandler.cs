using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackJackHero
{
    public class InputHandler : MonoBehaviour
    {
        private GameLogicHandler GM;

        private void Awake()
        {
            GM = GetComponent<GameLogicHandler>();
        }
        public void OnHoldButtonPressed(bool isLeftCard)
        {
            GM.HoldPosition(isLeftCard);
        }

        public void OnHoldCardPressed(bool isleftButton)
        {
                GM.PlayHoldCard(isleftButton);
        }

        public void OnBurnButtonPressed(bool isLeftCard)
        {
            GM.BurnPosition(isLeftCard);
        }

        public void OnTurnButtonPressed()
        {
            GM.EndPlayerTurn();
        }

        public void OnMuteButtonPressed()
        {

        }

        public void OnStartButtonPressed()
        {
            GM.StartGame();
        }

        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }
    }
}
