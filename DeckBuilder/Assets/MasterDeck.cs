using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using PokemonTcgSdk.Models;
using PokemonTcgSdk;
using System.Linq;
using System;


public class MasterDeck:Deck 
{ 
    public static int imagesLoaded=0;
    public static bool ready;


    //Get the card data from Pokemontgc.io
    //Create a list of gameobjects and add them the data
    //in the form of MyPokemonCard
    public void GetCards()
    {
         var pokemonCards = Card.Get<Pokemon>()as Pokemon;
       


        for (int i = 0; i < pokemonCards.Cards.Count; i++)
       {
               this.myDeck.Add(GameObject.CreatePrimitive(PrimitiveType.Plane));
                myDeck[i].transform.parent = this.transform;
               this.myDeck[i].AddComponent<MyPokemonCard>().GetCardStats(pokemonCards.Cards[i]);                     
        }

    }
    

    //All cards that are not Pokemon are Self destructed
    //So we clear the null gameobjects from the list
    //we also sort the list to 50 cards
    public void ClearMasterDeck()
    {
        this.myDeck.RemoveAll(GameObject => GameObject == null);
       
          
        for (int j = myDeck.Count - 1; j > size-1; j--)
        {
          Destroy(myDeck[j].gameObject);
          myDeck.RemoveAt(j);
        }
        //place the cards to the grid
        this.GetComponent<CardGrid>().RefreshCards(myDeck);

    }

    //download the image from the net
    public void ReadyDeck()
    {
        for (int i = 0; i < myDeck.Count; i++)
        {
            myDeck[i].GetComponent<MyPokemonCard>().GetImage();
        }
    }


    private void Start()
    {
        startMasterDeck();

    }

    
    public void SortByHP()
    {
        myDeck = myDeck.OrderBy(n => n.GetComponent<MyPokemonCard>().HpInt).ToList();
        grid.RefreshCards(myDeck);
    }
    public void SortByRarity()
    {
        myDeck = myDeck.OrderBy(n => n.GetComponent<MyPokemonCard>().RarityInt).ToList();
        grid.RefreshCards(myDeck);
    }
    public void SortByType()
    {
        myDeck = myDeck.OrderBy(n => n.GetComponent<MyPokemonCard>().Types[0]).ToList();
        grid.RefreshCards(myDeck);
    }

    // get cards from the net each time we start or load a game
    public void startMasterDeck()
    {
        GetCards();
        Invoke("ClearMasterDeck", 1f);
        Invoke("ReadyDeck", 1.2f);
    }
   
   
}