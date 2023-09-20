using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{

    private SpriteRenderer mSprite;
    // Start is called before the first frame update
    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();

        if (Camera.main != null)
        {
            float worldScreenHeight = Camera.main.orthographicSize * 2;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var sprite = mSprite.sprite;
            
            transform.localScale = new Vector3(
                worldScreenHeight / sprite.bounds.size.x
                , worldScreenWidth / sprite.bounds.size.y, 1);
        }
    }

}
