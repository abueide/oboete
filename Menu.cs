using Godot;
using System;

public class Menu : Control {
    public override void _Ready() {
        
    }

    public void onPlay() {
        GetTree().ChangeScene("res://Main.tscn");
    }

    public void onExit() {
        GetTree().Quit();
    }
}