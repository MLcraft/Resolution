using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndViewPresenter : ViewPresenter {
    public ImageViewPresenter Background;
    public ImageViewPresenter GameEndImage;
    public ButtonViewPresenter MainMenuButton;

    public override void Show(bool show)
    {
        base.Show(show);

        Background.Show(show);
        GameEndImage.Show(show);
        MainMenuButton.Show(show);
    }
}
