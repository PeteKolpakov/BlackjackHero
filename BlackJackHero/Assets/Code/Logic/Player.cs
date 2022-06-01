using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BlackJackHero
{
    public class Player : MonoBehaviour
    {
        [SerializeField, Required, HideInPrefabInstances]
        private Hand hand;
        public Hand MyHand { get { return hand; } }

        private int value { get { return hand.GetHandValue(); } }
        public int Value { get { return value; } }
        [SerializeField]
        private int health = 3;
        public int HP => health;

        [SerializeField, HideInPrefabInstances]
        private TextMeshProUGUI healthDisplay, goldDisplay;

        [SerializeField]
        private int gold = 10;
        public int Gold => gold;

        public bool CheckIfBust()
        {
            if (Value >= 0 && Value <= 21)
            {
                return false;
            }
            return true;
        }

        public void PlaceBet()
        {
            if (gold > 0)
            {
                gold--;
                UpdateGoldDisplay();
            }
            else
            {
                health--;
                UpdateHealthDisplay();
            }
        }

        public virtual void Init()
        {
            UpdateHealthDisplay();
            UpdateGoldDisplay();

            if (CheckIfBust())
            {
                print(gameObject.name + " Is Bust");
            }
        }

        private void UpdateHealthDisplay()
        {
            healthDisplay.text = "Lives: " + HP;
        }        
        private void UpdateGoldDisplay()
        {
            goldDisplay.text = "Gold: " + Gold;
        }

        public void FinishTurn()
        {
            if (CheckIfBust())
            {
                health--;
                UpdateHealthDisplay();
            }
            if (health <= 0)
            {
                LoseBattle();
            }
            hand.DiscardAll();
        }

        private void LoseBattle()
        {
            print(gameObject.name + "has lost");
        }
    }
}
