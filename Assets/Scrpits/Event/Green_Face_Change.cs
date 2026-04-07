using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Face_Change : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Blank_Face()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public void Smile_Face()
    {
        spriteRenderer.sprite = sprites[1];
    }

    public void Laugh_Face()
    {
        spriteRenderer.sprite = sprites[2];
    }

    public void Angry_Face()
    {
        spriteRenderer.sprite = sprites[3];
    }
}
