using Godot;
using System;

public class HUD : HBoxContainer {
    public int health = 3;
    public int score = 0;

    private Texture broken;
    public Label scoreLabel;

    [Signal]
    public delegate void Dead();

    public override void _Ready() {
        broken = ResourceLoader.Load("res://art/broken-heart.png") as Texture;
        scoreLabel = GetNode("Score") as Label;
    }

    public void OnSpriteHit() {
        var heart = GetChild(health) as TextureRect;
        heart.Texture = broken;
        health -= 1;
        if (health < 1) {
            EmitSignal(nameof(Dead));
        }
    }

    public void OnScore() {
        score++;
        scoreLabel.Text = score.ToString();
    }
}