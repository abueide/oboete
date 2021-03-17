using Godot;
using System;

public class HealthHBox : HBoxContainer {
    
    public int health = 3;


    private Texture full;
    private Texture broken;

    [Signal]
    public delegate void Dead();
    
    public override void _Ready()
    {
        full = ResourceLoader.Load("res://art/pink-heart.png") as Texture;
        broken = ResourceLoader.Load("res://art/broken-heart.png") as Texture;
    }

    public void onHit() {
        var heart = GetChild(health - 1) as TextureRect;
        heart.Texture = broken;
        health -= 1;
        if (health < 1) {
            EmitSignal(nameof(Dead));
        }
    }
}
