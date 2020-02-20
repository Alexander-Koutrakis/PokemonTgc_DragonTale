using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPokeball : MonoBehaviour
{
    RectTransform pokebalRectTransform;
    float z=2;
    // Start is called before the first frame update
    void Awake()
    {
        pokebalRectTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        pokebalRectTransform.Rotate(new Vector3(0, 0, z+Time.deltaTime));
    }
}
