using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int Neighbours;
    public bool isalive;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        isalive = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleCell()
    {
        if(isalive)
        {
            isalive = false;
            sprite.enabled = false;
        }
        else
        {
            isalive = true;
            sprite.enabled = false;
        }
    }
}
