using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGrid :MonoBehaviour
{
    public  List<Vector3> grid = new List<Vector3>();
    public int startX = 0;
    public int startY = 0;
    public  int limitX=108;
    public int limitY=-62;
    public int step=12;
    Vector3 hidecards=new Vector3(-20,-60,0);


    private void Awake()
    {
        CreateGrid();
    }


    // create a grid with the use of vectors
    public void CreateGrid()
    {
        for(int i= startX; i<limitX; i += step)
        {
            for(int j = startY; j > limitY; j -= step)
            {
                grid.Add(new Vector3(i, j, 0));
            }
        }
    }


    // set the target location of each card
    // its called after each mouse click
    public void RefreshCards(List<GameObject> GOcards)
    {
        if (gameObject.GetComponent<Deck>().selectedDeck)
        {
            for (int i = 0; i < GOcards.Count; i++)
            {

                GOcards[i].GetComponent<MyPokemonCard>().targetLocation = grid[i];
            }
        }
        else
        {
            for (int i = 0; i < GOcards.Count; i++)
            {
               //when we select another deck , the cards of the other decks hide to the side
                GOcards[i].GetComponent<MyPokemonCard>().targetLocation = hidecards;

            }
        }
    }

   

}
