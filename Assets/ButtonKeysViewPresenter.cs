using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKeysViewPresenter : ViewPresenter {
    public ImageViewPresenter Keys;
    public ButtonViewPresenter CloseButton;

    public override void Show(bool show)
    {
        base.Show(show);

        Keys.Show(show);
        CloseButton.Show(show);

    }

}
