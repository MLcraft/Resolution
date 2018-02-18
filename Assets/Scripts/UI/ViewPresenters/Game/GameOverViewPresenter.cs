using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverViewPresenter : ViewPresenter
{
    public ImageViewPresenter Background;
    public ImageViewPresenter GameOverImage;
    public ButtonViewPresenter AgainButton;
    public ButtonViewPresenter MainMenuButton;

    public override void Show(bool show)
    {
        base.Show(show);

        Background.Show(show);
        GameOverImage.Show(show);
        AgainButton.Show(show);
        MainMenuButton.Show(show);
    }

}
