using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewPresenter : ViewPresenter {
    public ImageViewPresenter Background;
    public ImageViewPresenter Title;
    public ImageViewPresenter[] Stars;
    public ButtonViewPresenter StartButton;
    public ButtonViewPresenter ExitButton;
    public ButtonViewPresenter KeysButton;

    public override void Show(bool show)
    {
        base.Show(show);

        Background.Show(show);
        Title.Show(show);
        StartButton.Show(show);
        ExitButton.Show(show);
        KeysButton.Show(show);

        if(Stars != null)
        {
            for(int i = 0; i < Stars.Length; i++)
            {
                Stars[i].Show(show);
            }
        }

    }

}
