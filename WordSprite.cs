
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;
using oboete;

public class WordSprite : Sprite {
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private static List<JapaneseWord> words = new List<JapaneseWord>() {
        new JapaneseWord("oboe", "覚え", "memorize"),
        new JapaneseWord("hikikaeken", "引き換え券", "receipt"),
        new JapaneseWord("magaru", "曲がる", "turn"),
        new JapaneseWord("kariru", "借りる", "borrow"),
        new JapaneseWord("mottekuru", "持ってくる", "bring"),
        new JapaneseWord("motsu", "持つ", "carry"),
        new JapaneseWord("undousuru", "運動する", "exercise"),
        new JapaneseWord("genzou", "現像", "development"),
    };

    private Label questionLabel;
    private Label answerLabel;

    private Vector2 start = new Vector2(-200, 0f);
    public Boolean active = false;
    private int speed = 150; // pixels per second

    private JapaneseWord randomWord;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready() {
        questionLabel = GetChild(0) as Label;
        answerLabel = GetChild(1) as Label;
        reset();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
        if (GetPosition().x > 1000) {
            GD.Print("Reset!");
            reset();
        }

        if (active) {
            SetPosition(new Vector2(GetPosition().x + speed * delta, GetPosition().y));
        }
    }

    public void reset() {
        rng.Randomize();
        active = false;
        start.y = rng.RandfRange(0f, 220f);
        SetPosition(start);
        randomWord = words[rng.RandiRange(0, words.Count - 1)];
        questionLabel.Text = randomWord.kanji;
    }

    public void Activate() {
        active = true;
    }
}