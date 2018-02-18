using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonViewPresenter : ViewPresenter
{
    private Button _button;
    private Image _image;
    private Text _text;

    public event EventHandler Clicked;

    void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
    }

    public override void Show(bool show)
    {
        base.Show(show);

        _button.enabled = show;
        _image.enabled = show;
        _text.enabled = show;
    }

    public void OnButtonClicked()
    {
        if (Clicked != null)
        {
            Clicked(this, System.EventArgs.Empty);
        }
    }

    public void SetSpriteImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public Color GetColor()
    {
        return _image.color;
    }

    public void SetNativeSize()
    {
        _image.SetNativeSize();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

}