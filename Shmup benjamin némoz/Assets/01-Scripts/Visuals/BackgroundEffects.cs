using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffects : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite purpleSprite;
    [SerializeField] Sprite softPurpleSprite;

    public static BackgroundEffects instance;

    private void Start()
    {
        instance = this;
    }

    public enum Type
    {
        normal,
        purple,
        softPurple
    }

    Sprite ConvertTypeToSprite(Type type)
    {
        switch(type)
        {
            case Type.normal:
                return normalSprite;
            case Type.purple:
                return purpleSprite;
            case Type.softPurple:
                return softPurpleSprite;
            default:
                Debug.Log("Texture type not implemented");
                return normalSprite;
        }
    }
    
    void SetSprites(Sprite sprite)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public void ChangeBackground(Type type, float time)
    {
        Sprite sprite = ConvertTypeToSprite(type);

        SetSprites(sprite);

        if (time != 0.0f)
            StartCoroutine(Revert(time));
    }

    //Change back to the normal background after a while
    IEnumerator Revert(float time)
    {
        yield return new WaitForSeconds(time);

        SetSprites(normalSprite);
    }
}
