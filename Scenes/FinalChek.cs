using Godot;
using System;

public partial class FinalChek : Area2D
{

	private CharacterBody2D player;

	public override void _Ready()
	{
		player = (CharacterBody2D)GetNode("/root/Core/OurCar");
	}

	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
