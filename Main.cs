using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Main : Node2D {
    private List<WordSprite> wordPool = new List<WordSprite>();
    PackedScene wordLabel = ResourceLoader.Load("res://WordSprite.tscn") as PackedScene;
    PackedScene deadMenu = ResourceLoader.Load("res://DeadMenu.tscn") as PackedScene;

    public override void _Ready() {
        PrintTree();
        GetNode("background/HUD/HealthBox").Connect("Dead", this, nameof(onDead));
        
        for (int i = 0; i < 20; i++) {
            var word = wordLabel.Instance();
            AddChild(word);
            wordPool.Add(word as WordSprite);
        }
    }

    private float speed = 3; // seconds per word launch
    private float timer = 0f;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Process(float delta) {
        timer += delta;
        if (timer > speed) {
            rng.Randomize();
            var idleWords = wordPool.Where(x => x.active == false).ToList();
            if (idleWords.Count > 0) {
                idleWords[rng.RandiRange(0, idleWords.Count - 1)].Activate();
            }

            timer = 0f;
        }
    }

    public void onDead() {
        AddChild(deadMenu.Instance());
    }
}