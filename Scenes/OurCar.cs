using Godot;
using System;

public partial class OurCar : CharacterBody2D
{
	private float Speed = 400/2;
	private float AngularSpeed = (Mathf.Pi * 1.2f)/2;
	private Label LoopLabel;
	private Area2D FinCheck, SecCheck;
	private int LoopCounter = 0;
	public bool OnTouchFirst = false;
	public bool OnTouchSecond = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoopLabel = GetNode<Label>("../StaticCam/NumCircles");
		FinCheck = GetNode<Area2D>("/Core/FinalCheck");
		SecCheck = GetNode<Area2D>("/Core/SecondCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double Delta)
	{
		float delta = (float) Delta;

		if (Input.IsActionPressed("ui_right"))
			Rotation += AngularSpeed * delta;
		if (Input.IsActionPressed("ui_left"))
			Rotation -= AngularSpeed * delta;

		Velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_up")){
			Velocity = Vector2.Up.Rotated(Rotation) * Speed;
		}
		if (Input.IsActionPressed("ui_down")){
			Velocity = Vector2.Up.Rotated(Rotation) * (-1*(Speed/2));
		}

		var motion = Velocity * delta;
		MoveAndCollide(motion);

	}

	private void _on_final_chek_body_entered(Node2D body) {
		string count = LoopCounter++.ToString();
		LoopLabel.Text = count;

		FinCheck.ProcessMode = Node.ProcessModeEnum.Disabled;
		SecCheck.ProcessMode = Node.ProcessModeEnum.Always;
	}

	private void _on_second_check_body_entered(Node2D body) {
		
		FinCheck.ProcessMode = Node.ProcessModeEnum.Always;
		SecCheck.ProcessMode = Node.ProcessModeEnum.Disabled;
	}


}


