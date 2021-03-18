using Godot;
using System;

public class HUD : HBoxContainer {
    public int health = 3;

    private Texture broken;

    [Signal]
    public delegate void Dead();

    public override void _Ready() {
        broken = ResourceLoader.Load("res://art/broken-heart.png") as Texture;
    }

    public void OnSpriteHit() {
        var heart = GetChild(health - 1) as TextureRect;
        heart.Texture = broken;
        health -= 1;
        if (health < 1) {
            EmitSignal(nameof(Dead));
        }
    }
}