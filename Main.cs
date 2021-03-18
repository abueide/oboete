using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Main : Node2D {
    private List<WordSprite> wordPool = new List<WordSprite>();
    private PackedScene wordLabel = ResourceLoader.Load("res://WordSprite.tscn") as PackedScene;
    private HUD hud;


    public override void _Ready() {
        hud = GetNode("background/HUD/HealthBox") as HUD;
        hud.Connect("Dead", this, nameof(onDead));

        for (int i = 0; i < 20; i++) {
            var word = wordLabel.Instance();
            AddChild(word);
            wordPool.Add(word as WordSprite);
            word.Connect("SpriteHit", hud, nameof(hud.OnSpriteHit));
        }

        activateRandom();
    }

    private float speed = 3; // seconds per word launch
    private float timer = 0f;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    private WordSprite current;
    private WordSprite first;

    public override void _Process(float delta) {
        timer += delta;
        if (timer > speed) {
            activateRandom();
            timer = 0f;
        }

        first.Debug();
    }

    public void onDead() {
        GetTree().ChangeScene("res://DeadMenu.tscn");
    }

    public void activateRandom() {
        rng.Randomize();
        var idleWords = wordPool.Where(x => x.active == false).ToList();
        current = wordPool.OrderByDescending(x => x.Position.x).First();
        if (idleWords.Count > 0) {
            if (first == null) {
                first = idleWords[rng.RandiRange(0, idleWords.Count - 1)];
                first.Activate();
            } else {
                idleWords[rng.RandiRange(0, idleWords.Count - 1)].Activate();
            }
        }
    }
}