using Godot;
using System;

public partial class OurCar : CharacterBody2D
{
	private float Speed = 400/2;
	private float AngularSpeed = Mathf.Pi * 1.6f;
	private Label LoopLabel;
	private Area2D FinCheck, SecCheck;
	private CollisionShape2D FinCol, SecCol;
	private int LoopCounter = 0;
	public bool FinTouched = false;
	private Vector2 Acceleration;

	public override void _Draw(){
		Vector2 to = Velocity;
		Vector2 vect = Velocity.Rotated(Rotation*-1); //Negate the Car's rotation
		vect = vect/4f; // Make it smaller so it fits on the screen
		DrawLine(Vector2.Zero, vect, Colors.Brown, 4.0f);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoopLabel = GetNode<Label>("../StaticCam/NumCircles");
		FinCheck = GetNode<Area2D>("../FinalCheck");
		SecCheck = GetNode<Area2D>("../SecondCheck");
		FinCol = GetNode<CollisionShape2D>("../FinalCheck/FinCol");
		SecCol = GetNode<CollisionShape2D>("../SecondCheck/SecCol");

		FinCheck.DisableMode = DisableModeEnum.Remove;
		SecCheck.DisableMode = DisableModeEnum.Remove;
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
			// Velocity = Vector2.Up.Rotated(Rotation) * -1 * (Speed/2);
			Acceleration = Vector2.Up.Rotated(Rotation) * -1 * Speed/7/2;
		}
		Velocity += Acceleration;
		Acceleration = Vector2.Zero;
		var motion = Velocity * delta;
		// MoveAndCollide(motion);
		MoveAndSlide();

		QueueRedraw();
	}

	private Node.ProcessModeEnum disable = Node.ProcessModeEnum.Disabled;
	private Node.ProcessModeEnum enable = Node.ProcessModeEnum.Always;
	private void _on_final_chek_body_entered(Node2D body) {
		if (!FinTouched){
			string count = LoopCounter++.ToString();
			LoopLabel.Text = count;
			FinTouched = !FinTouched; //sets to true
		}
	}

	private void _on_second_check_body_entered(Node2D body) {
		if (FinTouched){
			FinTouched = !FinTouched; //sets to false
		}
	}


}


