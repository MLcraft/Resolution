using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewPresenter : ViewPresenter
{
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();

    }

    public override void Show(bool show)
    {
        base.Show(show);

        _image.enabled = show;
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public Color GetColor()
    {
        return _image.color;
    }

    public void SetSpriteImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetNativeSize()
    {
        _image.SetNativeSize();
    }

}