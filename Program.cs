using System;
using System.Collections.Generic;
namespace BlackJack
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to Blackjack! I am shuffling cards.");
      // Group the suits and ranks
      var suits = new List<string> { "of Clubs", "of Spades", "of Hearts", "of Diamonds" };
      var ranks = new List<string> { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
      var playDeck = new List<Card>();
      var keepPlaying = true;
      for (int i = 0; i < suits.Count; i++)
      {
        for (int j = 0; j < ranks.Count; j++)
        {
          var card = new Card();
          card.Rank = ranks[j];
          card.Suit = suits[i];
          if (card.Suit == "Diamonds" || card.Suit == "Hearts")
          {
            card.ColorOfTheCard = "red";
          }
          else
          {
            card.ColorOfTheCard = "black";
          }
          playDeck.Add(card);
        }
      }
      // shuffle
      while (keepPlaying == true)
      {
        Console.WriteLine("Thhhhhwap");
        Random rnd = new Random();
        for (int i = playDeck.Count - 1; i >= 1; i--)
        {
          var j = new Random().Next(i);
          var temp = playDeck[j];
          playDeck[j] = playDeck[i];
          playDeck[i] = temp;
        }
        // Defining "hand" & "sum"
        var playerHand = new List<Card> { };
        var playerSum = 0;
        var houseHand = new List<Card> { };
        var playerSplit = new List<Card> { };
        var houseSum = 0;
        int k = 0;
        int h = 0;
        var playerBusted = false;
        var houseBusted = false;
        var noSplitting = true;
        var splitSum = 0;
        var splitBusted = false;

        {
          // Spliting checker
          // Initial playing cards
          while (k < 2)
          {
            Console.WriteLine($"The dealt card is the {playDeck[0].Rank} {playDeck[0].Suit}");
            playerHand.Add(playDeck[0]);
            // Ace choice
            if (playDeck[0].Rank == "Ace")
            {
              Console.WriteLine("You drew an ace! Would you like the ace to be worth 11 or 1?");
              var playerAceValue = Console.ReadLine();
              if (playerAceValue == "1")
              {
                playDeck[0].Rank = "1";
              }
            }
            Console.WriteLine($"Added the following to player hand: {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"This card has a value of {playDeck[0].GetCardValue()}");
            playerSum = playerSum + playDeck[0].GetCardValue();
            playDeck.RemoveAt(0);
            Console.WriteLine($"The current sum for the player is {playerSum}");

            // if the player gets 21
            if (playerSum == 21)
            {
              Console.WriteLine("The player has 21! The house gets a chance to match");
            }
            k++;
            if (k == 2)
            {
              if (playerHand[0].Rank == playerHand[1].Rank)
              {
                Console.WriteLine($"Drawing {playerHand[0].Rank} {playerHand[0].Suit} and {playerHand[1].Rank} {playerHand[1].Suit} allows you to go for a split!");
                Console.WriteLine("Would you like to split this hand? Yes or no.");
                var playerSplitting = Console.ReadLine().ToLower();
                if (playerSplitting == "yes")
                {
                  noSplitting = false;
                  Console.WriteLine("The player has chosen to split the hand!");
                  Console.WriteLine($"Kept the following in the original player hand: {playerHand[0].Rank} {playerHand[0].Suit}");
                  playerSplit.Add(playerHand[1]);
                  splitSum = splitSum + playerHand[0].GetCardValue();
                  Console.WriteLine($"The split hand receives {playerHand[1].Rank} {playerHand[1].Suit}");
                  playerSum = playerSum - playerHand[1].GetCardValue();
                  playerHand.RemoveAt(1);
                  playerHand.Add(playDeck[0]);
                  playerSum = playerSum + playDeck[0].GetCardValue();
                  Console.WriteLine($"Added the following card to the original player hand: {playDeck[0].Rank} {playDeck[0].Suit}");
                  playDeck.RemoveAt(0);
                  playerSplit.Add(playDeck[0]);
                  splitSum = splitSum + playDeck[0].GetCardValue();
                  Console.WriteLine($"Added the following card to the split hand: {playDeck[0].Rank} {playDeck[0].Suit}");
                  playDeck.RemoveAt(0);
                  Console.WriteLine($"The total for the original hand is {playerSum}. The total for the split is {splitSum}");
                }
              }
            }
          }
        }
        // Give dealer two cards
        while (h < 2)
        {
          if (h == 0)
          {
            houseHand.Add(playDeck[0]);
            Console.WriteLine($"The first card dealt to the house was revealed as the {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"Added card to house hand: {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"This card has a value of {playDeck[0].GetCardValue()}");
            houseSum = houseSum + playDeck[0].GetCardValue();
            playDeck.RemoveAt(0);
          }
          else
          {
            houseHand.Add(playDeck[0]);
            Console.WriteLine("The house takes another card, but leaves this one face down. The dealer shows a stern pokerface.");
            houseSum = houseSum + playDeck[0].GetCardValue();
            playDeck.RemoveAt(0);
          }
          h++;
        }
        // input time
        int n = 0;
        while (n == 0 && playerBusted == false)
        {
          Console.WriteLine($"Your current total is {playerSum}. Hit or stay?");
          var hitMe = Console.ReadLine().ToLower();
          if (hitMe == "hit")
          {
            Console.WriteLine($"The dealt card is the {playDeck[0].Rank} {playDeck[0].Suit}");
            if (playDeck[0].Rank == "Ace")
            {
              Console.WriteLine("You drew an ace! Would you like the ace to be worth 11 or 1?");
              var playerAceValue = Console.ReadLine();
              if (playerAceValue == "1")
              {
                playDeck[0].Rank = "1";
              }
            }
            Console.WriteLine($"Added the following to player hand: {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"This card has a value of {playDeck[0].GetCardValue()}");
            playerSum = playerSum + playDeck[0].GetCardValue();
            playDeck.RemoveAt(0);
            Console.WriteLine($"The current sum is {playerSum}");
            if (playerSum > 21)
            {
              Console.WriteLine($"BUST! The player has a total of {playerSum}, which is over 21.");
              playerBusted = true;
            }
            else if (playerSum == 21)
            {
              Console.WriteLine("THE PLAYER HAS 21! The house has a chance to respond.");
              n++;
            }
          }
          else
          {
            Console.WriteLine($"Player has chosen to stay with {playerSum}");
            n++;
          }
        }
        if (noSplitting == false)
        {

          // Splitting section
          int s = 0;
          while (s == 0 && splitBusted == false)
          {
            Console.WriteLine($"Your current total for the split is {splitSum}. Hit or stay?");
            var hitMe = Console.ReadLine().ToLower();
            if (hitMe == "hit")
            {
              Console.WriteLine($"The dealt card is the {playDeck[0].Rank} {playDeck[0].Suit}");
              if (playDeck[0].Rank == "Ace")
              {
                Console.WriteLine("You drew an ace! Would you like the ace to be worth 11 or 1?");
                var getAceValue = Console.ReadLine();
                if (getAceValue == "1")
                {
                  playDeck[0].Rank = "1";
                }
              }
              Console.WriteLine($"Added the following to the split hand: {playDeck[0].Rank} {playDeck[0].Suit}");
              Console.WriteLine($"This card has a value of {playDeck[0].GetCardValue()}");
              splitSum = splitSum + playDeck[0].GetCardValue();
              playDeck.RemoveAt(0);
              Console.WriteLine($"The current sum of the split is {splitSum}");
              if (splitSum > 21)
              {
                Console.WriteLine($"THE SPLIT HAS BUSTED! The split has a total of {splitSum}, which is over 21.");
                splitBusted = true;
                noSplitting = true;
              }
              else if (splitSum == 21)
              {
                Console.WriteLine("THE SPLIT HAS REACHED 21!");
                s++;
              }
            }
            else
            {
              Console.WriteLine($"The split is staying with {splitSum}");
              s++;
            }
          }
        }
        // House response and adds cards
        if (playerBusted == false || (splitBusted == false && noSplitting == false))
        {
          Console.WriteLine($"The house is drawing cards. The house currently has {houseSum}");
          // revealing house hand
          Console.WriteLine($"The house currently has the following cards: {houseHand[0].Rank} {houseHand[0].Suit} and {houseHand[1].Rank} {houseHand[1].Suit}");
          while (houseSum < 17 && houseBusted == false && (playerBusted == false || (splitBusted == false && noSplitting == false)))
          {
            houseHand.Add(playDeck[0]);
            if (playDeck[0].Rank == "Ace" && houseSum > 11)
            {
              Console.WriteLine("The house drew an ace, but it will play it as a value of 1.");
              playDeck[0].Rank = "1";
            }
            Console.WriteLine($"The next dealt card to the house is {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"Added card to house hand: {playDeck[0].Rank} {playDeck[0].Suit}");
            Console.WriteLine($"This card has a value of {playDeck[0].GetCardValue()}");
            houseSum = houseSum + playDeck[0].GetCardValue();
            playDeck.RemoveAt(0);
            Console.WriteLine($"The house has {houseSum}");
            // dealer looses by busting
            if (houseSum > 21)
            {
              Console.WriteLine($"The house has BUSTED!");
              houseBusted = true;
            }
            else if ((houseSum == 21) && (playerSum == 21))
            {
              {
                Console.WriteLine($"The result is a push! The player has {playerSum}, and the house has {houseSum}");
              }
            }
            else if (houseSum == 21)
            {
              Console.WriteLine("The house has 21!");
            }
          }
        }

        // Comparing scores
        var houseScore = (21 - houseSum);
        var playerScore = (21 - playerSum);
        // adding split score
        var splitScore = (21 - splitSum);
        {

          if (playerBusted == false && houseBusted == false)
          {
            Console.WriteLine($"Run comparision: The house has a total of {houseSum}");
            if (houseScore > playerScore)
            {
              Console.WriteLine($"The player wins! The player has a total of {playerSum}, which is a difference of {playerScore}. The house has a total of {houseSum}, which is a difference of {houseScore}");
            }
            else if (houseScore == playerScore)
            {
              Console.WriteLine($"The result is a PUSH! The player has a total of {playerSum}. The house has a total of {houseSum}.");
            }
            else
            {
              Console.WriteLine($"The house wins! The player has a total of {playerSum}, which is a difference of {playerScore}. The house has a total of {houseSum}, which is a difference of {houseScore}");
            }
          }
          else if (houseBusted == true && playerBusted == false)
          {
            Console.WriteLine("The player has won due to the house busting");
          }
        }
        if (noSplitting == false)
        {
          if (splitBusted == false && noSplitting == false && houseBusted == false)
          {

            Console.WriteLine($"The house vs. the split: The house has a total of {houseSum}, and the split has a total of {splitSum}.");
            if (houseScore > splitScore)
            {
              Console.WriteLine($"The split wins! The split has a total of {splitSum}, which is a difference of {splitScore}. The house has a total of {houseSum}, which is a difference of {houseScore}");
            }
            else if (houseScore == splitScore)
            {
              Console.WriteLine($"The result is a PUSH! The split has a total of {splitSum}. The house has a total of {houseSum}.");
            }
            else
            {
              Console.WriteLine($"The house wins! The split has a total of {splitSum}, which is a difference of {splitScore}. The house has a total of {houseSum}, which is a difference of {houseScore}");
            }
          }
          else if (houseBusted == true && playerBusted == false && splitBusted == false)
          {
            Console.WriteLine("The house has been defeated by both the player and the split");
          }
          else if (houseBusted == true && splitBusted == true)
          {
            Console.WriteLine("The split has won, and the house has lost");
          }
        }
        Console.WriteLine("Would you like to play again? Yes or no.");
        var input = Console.ReadLine().ToLower();
        if (input == "no")
        {
          keepPlaying = false;
          Console.WriteLine("Thanks for playing!");
          Console.ReadLine();
        }
      }
    }
  }
}