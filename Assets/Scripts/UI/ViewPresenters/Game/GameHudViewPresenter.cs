using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHudViewPresenter : ViewPresenter {
    public ImageViewPresenter TopBorder;
    public ImageViewPresenter BottomBorder;
    public ImageViewPresenter PlayerIcon;
    public ImageViewPresenter IconBorder;
    public ImageViewPresenter HealthOne;
    public ImageViewPresenter HealthTwo;
    public ImageViewPresenter HealthThree;
    public ButtonViewPresenter PauseButton;

    public Sprite FullHeart;
    public Sprite HalfHeart;

    public override void Show(bool show)
    {
        base.Show(show);

        TopBorder.Show(show);
        BottomBorder.Show(show);
        PlayerIcon.Show(show);
        IconBorder.Show(show);
        HealthOne.Show(show);
        HealthTwo.Show(show);
        HealthThree.Show(show);
        PauseButton.Show(show);
    }

    public void ShowHealth(int health)
    {
        if(health == 6)
        {
            HealthOne.SetSpriteImage(FullHeart);
            HealthTwo.SetSpriteImage(FullHeart);
            HealthThree.SetSpriteImage(FullHeart);
        } else if (health == 5)
        {
            HealthOne.SetSpriteImage(FullHeart);
            HealthTwo.SetSpriteImage(FullHeart);
            HealthThree.SetSpriteImage(HalfHeart);
        } else if (health == 4)
        {
            HealthOne.SetSpriteImage(FullHeart);
            HealthTwo.SetSpriteImage(FullHeart);
            HealthThree.Show(false);

        } else if (health == 3)
        {
            HealthOne.SetSpriteImage(FullHeart);
            HealthTwo.SetSpriteImage(HalfHeart);
            HealthThree.Show(false);
        } else if (health == 2)
        {
            HealthOne.SetSpriteImage(FullHeart);
            HealthTwo.Show(false);
            HealthThree.Show(false);

        } else if (health == 1)
        {
            HealthOne.SetSpriteImage(HalfHeart);
            HealthTwo.Show(false);
            HealthThree.Show(false);

        } else
        {
            HealthOne.Show(false);
            HealthTwo.Show(false);
            HealthThree.Show(false);
        }

    }

}
