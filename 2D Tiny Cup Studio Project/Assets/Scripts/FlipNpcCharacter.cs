using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNpcCharacter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void FlipX(int state)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (state != 0);
        }
    }
}
