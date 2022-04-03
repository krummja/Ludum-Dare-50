using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBoxIcon : MonoBehaviour
{
    public int icon;
    public Sprite[] Items;
    public SpriteRenderer picture;
    public TextMeshProUGUI messageTextMesh;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (icon > 18) icon = 18;
            picture.sprite = Items[icon];
            
    }
}
