using BlackJackHero.Assets.Code.Cards;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace BlackJackHero
{
    class Utils
    {
        private static Random rand = new Random();

        public static List<T> riffleShuffle<T>(ICollection<T> list, int flips)
        {
            List<T> newList = new List<T>(list);

            for (int n = 0; n < flips; n++)
            {
                //cut the deck at the middle +/- 10%, remove the second line of the formula for perfect cutting
                int cutPoint = newList.Count / 2
                    + (rand.Next(0, 2) == 0 ? -1 : 1) * rand.Next((int)(newList.Count * 0.1));

                //split the deck
                List<T> left = new List<T>(newList.Take(cutPoint));
                List<T> right = new List<T>(newList.Skip(cutPoint));

                newList.Clear();

                while (left.Count > 0 && right.Count > 0)
                {
                    //allow for imperfect riffling so that more than one card can come form the same side in a row
                    //biased towards the side with more cards
                    //remove the if and else and brackets for perfect riffling
                    if (rand.NextDouble() >= ((double)left.Count / right.Count) / 2)
                    {
                        newList.Add(right.First());
                        right.RemoveAt(0);
                    }
                    else
                    {
                        newList.Add(left.First());
                        left.RemoveAt(0);
                    }
                }

                //if either hand is out of cards then flip all of the other hand to the shuffled deck
                if (left.Count > 0) newList.AddRange(left);
                if (right.Count > 0) newList.AddRange(right);
            }

            return newList;
        }

        public static List<T> overhandShuffle<T>(List<T> list, int passes)
        {
            List<T> mainHand = new List<T>(list);

            for (int n = 0; n < passes; n++)
            {
                List<T> otherHand = new List<T>();

                while (mainHand.Count > 0)
                {
                    //cut at up to 20% of the way through the deck
                    int cutSize = rand.Next((int)(list.Count * 0.2)) + 1;

                    List<T> temp = new List<T>();

                    //grab the next cut up to the end of the cards left in the main hand
                    for (int i = 0; i < cutSize && mainHand.Count > 0; i++)
                    {
                        temp.Add(mainHand.First());
                        mainHand.RemoveAt(0);
                    }

                    //add them to the cards in the other hand, sometimes to the front sometimes to the back
                    if (rand.NextDouble() >= 0.1)
                    {
                        //front most of the time
                        temp.AddRange(otherHand);
                        otherHand = temp;
                    }
                    else
                    {
                        //end sometimes
                        otherHand.AddRange(temp);
                    }
                }

                //move the cards back to the main hand
                mainHand = otherHand;
            }

            return mainHand;
        }

        public static int GetTrueValueStandard(CardVal val, int handTotal) 
        {
            switch (val)
            {
                case CardVal.Ace:
                    if (handTotal + 11 >= 21)
                        return 1;
                    else
                        return 11;
                case CardVal.Jack:
                    return 10;
                case CardVal.Queen:
                    return 10;
                case CardVal.King:
                    return 10;
                default:
                    return (int)val;
            }
        }
    }
}

