using UnityEngine;

public class Item 
{
    public string name { get; private set; }

    public Sprite sprite { get; private set; }

    public Item(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }

}
