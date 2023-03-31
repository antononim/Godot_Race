using Godot;
using System;

public partial class OurCar : CharacterBody2D
{
	private float Speed = 400;
	private float AngularSpeed = Mathf.Pi * 1.6f;
	private Label LoopLabel;
	private Area2D FinCheck, SecCheck;
	private int LoopCounter = 0;
	public bool OnTouchFirst = false;
	public bool OnTouchSecond = false;
	private Vector2 Acceleration;

	public override void _Draw(){
		Vector2 to = Velocity;
		
		DrawLine(Vector2.Zero, Velocity, Colors.Green, 2.0f);
	}

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
		//comment
		if (Input.IsActionPressed("ui_right"))
			Rotation += AngularSpeed * delta;
		if (Input.IsActionPressed("ui_left"))
			Rotation -= AngularSpeed * delta;

		//Velocity = Vector2.Zero;
		Velocity *= 0.95f;

		if (Input.IsActionPressed("ui_up")){
			// So SPEED is maximum velocity. Kinda. For now.
			// So I will take max, subtract... something.
			// Max = Old+New. Old is Velocity.
			// Acceleration = Max - CurrentVelocity. IG...

			Acceleration = Vector2.Up.Rotated(Rotation) * Speed/7; //New Velocity

			// Velocity += Vector2.Up.Rotated(Rotation) * Speed;
			// Velocity = Vector2.Up.Rotated(Rotation) * Speed;
		}
		if (Input.IsActionPressed("ui_down")){
			// Velocity += Vector2.Up.Rotated(Rotation) * -1 * Speed/2;
			Velocity = Vector2.Up.Rotated(Rotation) * -1 * (Speed/2);
		}
		Velocity += Acceleration;
		Acceleration = Vector2.Zero;
		var motion = Velocity * delta;
		MoveAndCollide(motion);

		QueueRedraw();
	}

	private void _on_final_chek_body_entered(Node2D body) {
		string count = LoopCounter++.ToString();
		LoopLabel.Text = count;
		FinCheck.ProcessMode = Node.ProcessModeEnum.Disabled;
		SecCheck.ProcessMode = Node.ProcessModeEnum.Always;
		//uhh
	}

	private void _on_second_check_body_entered(Node2D body) {
		
		FinCheck.ProcessMode = Node.ProcessModeEnum.Always;
		SecCheck.ProcessMode = Node.ProcessModeEnum.Disabled;
	}


}


