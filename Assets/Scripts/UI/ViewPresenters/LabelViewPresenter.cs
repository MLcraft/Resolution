using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelViewPresenter : ViewPresenter
{
    private Text _text;

    void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }

    public override void Show(bool show)
    {
        base.Show(show);

        _text.enabled = show;
    }

    public string GetText()
    {
        return _text.text;
    }

    public void SetText(string val)
    {
        _text.text = val;
    }

    public void SetColor(Color color)
    {
        _text.color = color;
    }

}