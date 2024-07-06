using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    
    //Holds all of the possible sprites that the player ball can look like 
    [SerializeField] private Sprite[] sprites;
    //The render that will have that sprite attached 
    private SpriteRenderer _spriteRenderer;

    //==================================================================================================================
    // Base Method   
    //==================================================================================================================
    
    //Picks a random sprite and attached it to the renderer 
    private void Start()
    {
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }
}
