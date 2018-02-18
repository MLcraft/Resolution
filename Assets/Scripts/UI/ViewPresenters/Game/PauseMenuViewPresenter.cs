using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuViewPresenter : ViewPresenter {
    public ImageViewPresenter Blocker;
    public ImageViewPresenter Background;
    public ButtonViewPresenter ResumeButton;
    public ButtonViewPresenter RestartButton;
    public ButtonViewPresenter MainMenuButton;

    public override void Show(bool show)
    {
        base.Show(show);

        Blocker.Show(show);
        Background.Show(show);
        ResumeButton.Show(show);
        RestartButton.Show(show);
        MainMenuButton.Show(show);
    }
}
