using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int Neighbours;
    public bool isalive;
    public Color TurnOff_Color,TurnOn_Color;
    public int index_i, index_j;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = TurnOff_Color;
        isalive = false;
    }
   

    public void setFalse()
    {
        isalive = false;
        sprite.color = TurnOff_Color;
    }
    public void ToggleCell()
    {
        if(isalive)
        {
            isalive = false;
            sprite.color = TurnOff_Color;
            //sprite.enabled = false;
        }
        else
        {
            isalive = true;
            sprite.color = TurnOn_Color;
            //sprite.enabled = true;
        }
    }
}
