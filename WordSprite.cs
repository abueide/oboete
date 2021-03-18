using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot.Collections;
using oboete;

public class WordSprite : Sprite {
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    // Called when the node enters the scene tree for the first time.
    private static List<JapaneseWord> words = new List<JapaneseWord>() {
        new JapaneseWord("oboe", "覚え", "memorize"),
        new JapaneseWord("mizu", "水着", "water"),
        new JapaneseWord("hon", "本", "book"),
        new JapaneseWord("ringo", "りんご", "apple"),
        new JapaneseWord("haku", "はく", "pants"),
        new JapaneseWord("kakeru", "かける", "glasses"),
        new JapaneseWord("kiru", "きる", "shirt"),
        new JapaneseWord("hashi", "はし", "chopsticks"),
        new JapaneseWord("shigoto", "仕事", "job"),
        new JapaneseWord("kaishain", "会社員", "worker"),
        new JapaneseWord("natsu", "なつ", "summer"),
        new JapaneseWord("soujisuru", "そうじする", "clean"),
        new JapaneseWord("sentakusuru", "せんたくする", "laundry"),
        // new JapaneseWord("", "", ""),
        // new JapaneseWord("hikikaeken", "引き換え券", "receipt"),
        // new JapaneseWord("magaru", "曲がる", "turn"),
        // new JapaneseWord("kariru", "借りる", "borrow"),
        // new JapaneseWord("mottekuru", "持ってくる", "bring"),
        // new JapaneseWord("motsu", "持つ", "carry"),
        // new JapaneseWord("undousuru", "運動する", "exercise"),
        new JapaneseWord("genzou", "現像", "development"),
    };

    [Signal]
    public delegate void SpriteHit();
    [Signal]
    public delegate void Score();

    private Label questionLabel;
    private Label answerLabel;

    private Vector2 start = new Vector2(-200, 0f);
    public Boolean active = false;
    private int speed = 100; // pixels per second

    private JapaneseWord randomWord;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready() {
        questionLabel = GetNode("VBox/QuestionLabel") as Label;
        answerLabel = GetNode("VBox/AnswerLabel") as Label;
        reset();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
        if (GetPosition().x > 500) {
            GD.Print("Reset!");
            EmitSignal(nameof(SpriteHit));
            reset();
        }

        if (active) {
            SetPosition(new Vector2(GetPosition().x + speed * delta, GetPosition().y));
        }
    }

    public void handleKey(Char c) {
        int index = answerLabel.Text.IndexOf('_');
        if (index != -1 && randomWord.meaning[index].ToString().ToLower().Equals(c.ToString().ToLower())) {
            var builder = new StringBuilder(answerLabel.Text);
            builder[index] = c;
            answerLabel.Text = builder.ToString().ToLower();
        }
        
        index = answerLabel.Text.IndexOf('_');
        if (index == -1) {
            reset();
            EmitSignal(nameof(Score));
        }
    }


    public void reset() {
        rng.Randomize();
        questionLabel.Text = "";
        answerLabel.Text = "";
        randomWord = words[rng.RandiRange(0, words.Count - 1)];
        active = false;
        start.y = rng.RandfRange(20f, 220f);
        SetPosition(start);

        questionLabel.Text = randomWord.kanji;
        for (int i = 0; i < randomWord.meaning.Length; i++) {
            answerLabel.Text += "_";
        }
    }

    public void Activate() {
        active = true;
    }

    public void Debug() {
        GD.Print(active + " " + Position + " " + randomWord.meaning);
    }
}