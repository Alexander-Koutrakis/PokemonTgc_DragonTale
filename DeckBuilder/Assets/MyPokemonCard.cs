using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using PokemonTcgSdk;
using PokemonTcgSdk.Models;
using System.ComponentModel;
public class MyPokemonCard : MonoBehaviour
{
    SpriteRenderer SR;
    public int HpInt=0;   
    public int RarityInt;
    public int ID;
    public List<string> Types = new List<string>();
    public Vector3 targetLocation=new Vector3(0,0,0);
    public Vector3 targetScale=new Vector3(1,1,1);
    float smoothing = 10f;
    public PokemonCard pokemonCard;
    Shader shader;




    public void GetCardStats(PokemonCard card)
    {
        //get the data of the PokemonCard and store them to fit a Card gameobject

        HpInt = GetHP(card.Hp);
        if (this.HpInt <= 0)
        {
            Destroy(gameObject);
        }

        gameObject.tag = "Card";
        gameObject.name = card.Name.ToString();
        pokemonCard = card;        
        RarityInt = GetRarity(card.Rarity);
        GetTypes(card);
        ID = card.NationalPokedexNumber;
    }

    // change the value rarity from string to int in order to compare the cards
    public int GetRarity(string rarity)
    {
       

        if (rarity.Contains("Common"))
        {
            return 1;
        }else if (rarity.Contains("Uncommon"))
        {
            return 2;
        }
        else if (rarity.Contains("Rare"))
        {
            if (rarity.Contains("Ultra"))
            {
                return 4;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            return 0;
        }
    }
    public int GetHP(string Hp)//change the HP from string to int in order to compare
    {      
        int HPtoInt = 0;
        if(int.TryParse(Hp,out HPtoInt))
        {
            return HPtoInt;
        }
        else
        {
            return HPtoInt;
        }
    }
    public void GetTypes(PokemonCard card)// get the type of the Pokemon in order to compare
    {
        if (card.Types != null)
        {
            this.Types = card.Types;
        }        
    }

    public void GetImage()
    {
        StartCoroutine(GetImage(this.pokemonCard.ImageUrlHiRes));
    }


    public IEnumerator GetImage(string url)//get the image of the card and inforf then system when its ready
    {       
        Renderer Rend = GetComponent<Renderer>();
        WWW wwwLoader =new WWW(url);
        yield return wwwLoader;
        Rend.material.mainTexture = wwwLoader.texture;
        shader = Shader.Find("Sprites/Default");
        Rend.material.shader = shader;
        MasterDeck.imagesLoaded++;
    }

    public void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 90, -90));// change the rotation of the Plane gameobject in order to be visible

    }

    public void Update()
    {        
        if(Vector3.Distance(transform.position, targetLocation) > 0.01f)//move the card to the target location
            transform.position = Vector3.Lerp(transform.position, targetLocation, smoothing * Time.deltaTime);


        if (transform.localScale!=targetScale)// scale the card to the target location
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, smoothing * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }

    }

    public void OnMouseOver()
    {
        targetScale =new Vector3(2,2,2);

    }

    public void OnMouseExit()
    {
        targetScale = new Vector3(1,1,1);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
