using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Main_Menu : MonoBehaviour
{
    public MasterDeck masterDeck;
    public GameObject image;
    public GameObject DeckMenu;
    public GameObject loadingball;
        private void Update()
    {// wait until all the card images are loaded and hide the loading screen
        if (MasterDeck.imagesLoaded == 50)
        {
            image.GetComponentInParent<Image>().CrossFadeAlpha(0f, 1.0f, false); 
            DeckMenu.SetActive(true);
            loadingball.SetActive(false);
        }
        else
        {
            DeckMenu.SetActive(false);
            image.GetComponentInParent<Image>().CrossFadeAlpha(1f, 1.0f, false);
           
        }
    }

   


    public void QuitGame()
    {
        Application.Quit();
    }
}
