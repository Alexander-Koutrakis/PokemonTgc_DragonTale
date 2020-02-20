using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public CardGrid grid;
    public int size;
    public List<GameObject> myDeck = new List<GameObject>();
    public bool selectedDeck;
    public MyPokemonCard[] card;
    public int[] IDs;   
    public void RemoveMoveCard(GameObject card , Deck targetDeck)
    {    
        // change the parent of the card
        // and move it the target position
        card.transform.parent = targetDeck.transform;
        targetDeck.grid.RefreshCards(targetDeck.myDeck);

    }

    public void RefreshDeck()
    {
        card = null;
        IDs = null;
        myDeck.Clear();


        // fill the card deck according to the gameobject children
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach(Transform transform in transforms)
        {
            if(transform.tag=="Card")
            myDeck.Add(transform.gameObject);
        }
        card =gameObject.GetComponentsInChildren<MyPokemonCard>();
        IDs = new int[card.Length];
     
        for (int i = 0; i < card.Length; i++)
        {     
            IDs[i] = card[i].ID;
        }
        grid.RefreshCards(this.myDeck);

    }

    public void ClearDeck()
    {
        // destroy all children cards and clear the list
        myDeck.Clear();
        Transform[] cards = GetComponentsInChildren<Transform>();
        foreach(Transform card in cards)
        {
            if(card.tag!="savedDeck")
            Destroy(card.gameObject);
        }
    }

    public void OnEnable()
    {// calculate the lists on each mouse click
        GameMaster.OnCardClick += RefreshDeck;
    }

    public void OnDisable()
    {
        GameMaster.OnCardClick -= RefreshDeck;
    }
}
