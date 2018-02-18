using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenViewPresenter : ViewPresenter
{
    public ImageViewPresenter Background;
    public ImageViewPresenter[] Stars;

    public override void Show(bool show)
    {
        base.Show(show);

        Background.Show(show);

        if(Stars != null)
        {
            for(int i = 0; i < Stars.Length; i++)
            {
                Stars[i].Show(show);
            }
        }

    }

}
