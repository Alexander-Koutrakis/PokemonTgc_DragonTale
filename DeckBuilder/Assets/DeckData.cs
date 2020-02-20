using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DeckData 
{
    public int[] cardID;// an array of PokedexNumbers stored in line
    public int[] decksize;// the first index represents how many indexes of the cardID Array belong the masterdeck.we work simillarly with the other decks

    public DeckData (Deck masterDeckData,Deck Deck1Data,Deck Deck2Data,Deck Deck3Data)
    {

        cardID = masterDeckData.IDs.Concat(Deck1Data.IDs).ToArray();
        cardID = cardID.Concat(Deck2Data.IDs).ToArray();
        cardID = cardID.Concat(Deck3Data.IDs).ToArray();

        decksize =new int[]{ masterDeckData.IDs.Length,Deck1Data.IDs.Length,Deck2Data.IDs.Length,Deck3Data.IDs.Length};

    }


}
