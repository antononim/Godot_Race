using Godot;
using System;

public partial class OurCar : CharacterBody2D
{
	private float Speed = 200;
	private float AngularSpeed = Mathf.Pi * 1.6f;
	private float Nitro = 100f;
	private Label LoopLabel;
	private Area2D FinCheck, SecCheck;
	private CollisionShape2D FinCol, SecCol;
	private int LoopCounter = 0;
	public bool FinTouched = false;
	private Vector2 Acceleration;

	public override void _Draw(){
		Vector2 to = Velocity;
		Vector2 vect = Velocity.Rotated(-Rotation); //Negate the Car's rotation
		vect = vect/4f; // Make it smaller so it fits on the screen
		DrawLine(Vector2.Zero, vect, Colors.Brown, 4.0f);
	}

	public override void _Ready()
	{
		LoopLabel = GetNode<Label>("../StaticCam/Loops/NumCircles");
		FinCheck = GetNode<Area2D>("../FinalCheck");
		SecCheck = GetNode<Area2D>("../SecondCheck");
		FinCol = GetNode<CollisionShape2D>("../FinalCheck/FinCol");
		SecCol = GetNode<CollisionShape2D>("../SecondCheck/SecCol");

		FinCheck.DisableMode = DisableModeEnum.Remove;
		SecCheck.DisableMode = DisableModeEnum.Remove;
	}

	public override void _Process(double Delta)
	{
		float delta = (float) Delta;
		
		if (Input.IsActionPressed("ui_right"))
			Rotation += AngularSpeed * delta;
		if (Input.IsActionPressed("ui_left"))
			Rotation -= AngularSpeed * delta;

		Velocity *= 0.85f;
		Acceleration = Vector2.Zero;

		if (Input.IsActionPressed("ui_up")){
			Acceleration = Vector2.Up.Rotated(Rotation); // Speed 
			Acceleration *= Speed * 0.28f; // Set speed
		}
		
		if (Input.IsActionPressed("ui_down")){
			Acceleration = Vector2.Up.Rotated(Rotation); // The "Forward" vector
			Acceleration *= Speed * 0.28f; // Set the car's speed
			Acceleration *= -1 * 0.5f; // Move slower (0.5) and backwards (-1)
		}

		// Appling our acceleration
		Velocity += Acceleration;
		var motion = Velocity * delta;
		MoveAndSlide();

		QueueRedraw();
	}

	public override void _UnhandledInput(InputEvent ev){
		if (Input.IsKeyPressed(Key.Escape)){
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}

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


