using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameMaster : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    GameObject card;
    public Deck selectedDeck;
    public MasterDeck masterDeck;
    public Deck deck1;
    public Deck deck2;
    public Deck deck3;
    public LayerMask WhatIsCard;

    public delegate void ClickAction();
    public static event ClickAction OnCardClick;

    public void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {


            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                // if we click a deckIcon we set it as our active/selected deck
                if (hitInfo.transform.tag == "savedDeck")
                {
                    if (selectedDeck != null)
                    {
                        selectedDeck.selectedDeck=false;
                    }

                    selectedDeck = SelectDeck();
                    selectedDeck.selectedDeck = true;

                    if (OnCardClick != null)
                    {// all decks refresh their card position accordingly
                        OnCardClick();
                    }

                }
                else if (hitInfo.transform.parent.name == "MasterDeck")
                {// if we select a card from the master deck while we have an active/selected deck , the card is removed and added to our deck
                    if (selectedDeck != null)
                    {
                        if (selectedDeck.myDeck.Count < selectedDeck.size )
                        {
                            masterDeck.RemoveMoveCard(hitInfo.transform.gameObject,  selectedDeck);
                            if (OnCardClick != null)
                            {// all decks refresh their card position accordingly
                                OnCardClick();
                            }
                        }
                    }
                }
                else {
                    if(hitInfo.transform.parent.tag == "savedDeck")
                    {// if we select a card from our active deck , the card is returned to master deck
                        selectedDeck.RemoveMoveCard(hitInfo.transform.gameObject, masterDeck);
                        if (OnCardClick != null)
                        {
                            OnCardClick();
                        }
                    }
                }
               
            }           
        }


       

    }


    private Deck SelectDeck()
    {
        Deck sDeck;
        if (hitInfo.transform.GetComponent<Deck>() != null)
        {
            sDeck = hitInfo.transform.GetComponent<Deck>();

        }
        else
        {
            sDeck = selectedDeck;
        }
        return sDeck;
    }


   
 


    private void LoadData(DeckData data)
    {
        //Before loading the data we reset all decks
        MasterDeck.imagesLoaded = 0;
        masterDeck.ClearDeck();
        deck1.ClearDeck();
        deck2.ClearDeck();
        deck3.ClearDeck();


        int j=0, k=0, l = 0;
        masterDeck.IDs = new int[data.decksize[0]];
        deck1.IDs = new int[data.decksize[1]];
        deck2.IDs = new int[data.decksize[2]];
        deck3.IDs = new int[data.decksize[3]];

        // we add pokedexNumbers to the according decks 
        for (int i = 0;i<data.cardID.Length; i++)
        {
                     
           
            if (i < data.decksize[0]) //the first data.cardID indexes represent the first deck up to the  data.decksize[0]
            {
                masterDeck.IDs[i] = data.cardID[i];
            }
            else if (i < data.decksize[1]+ data.decksize[0]) //the cards from data.decksize[0] up to data.decksize[1] represent the first deck
            {
                deck1.IDs[j] = data.cardID[i];
                j++;

            }
            else if (i < data.decksize[2]+ data.decksize[0]+ data.decksize[1]) //the cards from data.decksize[1] up to data.decksize[2] represent the second deck
            {
                deck2.IDs[k] = data.cardID[i];
                k++;
            }
            else if (i < data.decksize[3]+data.decksize[0]+ data.decksize[1]+ data.decksize[2])// //the cards from data.decksize[2] up to data.decksize[3] represent the third deck
            {
                deck3.IDs[l] = data.cardID[i];
                l++;

            }
        }

        // when restart the master deck
        masterDeck.startMasterDeck();



       Invoke("LoadDecks",2f);

    }

    private void LoadDecks()
    {
        //we colect an array of cards and match them to each deck according their pokedex IDs

        Transform[] cards = masterDeck.GetComponentsInChildren<Transform>();

        foreach (Transform card in cards)
        {

            MyPokemonCard Pcard = card.GetComponent<MyPokemonCard>();
            Debug.Log(card.name);

            if (Pcard != null) { 
            for (int i = 0; i < deck1.IDs.Length; i++)
            {
                if (Pcard.ID == deck1.IDs[i])
                {
                    masterDeck.RemoveMoveCard(Pcard.gameObject, deck1);

                }
            }
            for (int i = 0; i < deck2.IDs.Length; i++)
            {
                if (Pcard.ID == deck2.IDs[i])
                {

                    masterDeck.RemoveMoveCard(Pcard.gameObject, deck2);

                }
            }
            for (int i = 0; i < deck3.IDs.Length; i++)
            {
                if (Pcard.ID == deck3.IDs[i])
                {
                    masterDeck.RemoveMoveCard(Pcard.gameObject, deck3);

                }
            }
            }
        }

        if (OnCardClick != null)
        {
            OnCardClick();
        }

    }

    public void LoadGame()
    {
        DeckData data = SaveSystem.LoadDeck();
        LoadData(data);
    }
    public void SaveDecks()
    {
        if (OnCardClick != null)
        {
            OnCardClick();
        }


        SaveSystem.SaveDeck(masterDeck, deck1, deck2, deck3);
    }
}
