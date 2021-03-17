using Godot;
using System;

public class DeadMenu : CenterContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void onAgain() {
        GetTree().ChangeScene("res://Main.tscn");
    }

    public void onExit() {
        GetTree().Quit();
    }
}
