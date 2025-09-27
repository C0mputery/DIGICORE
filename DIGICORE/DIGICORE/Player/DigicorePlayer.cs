using Digicore.Interfaces;
using Godot;

namespace Digicore.Player;

[GlobalClass]
public partial class DigicorePlayer : CharacterBody3D {
	public override void _Ready() {
		Input.MouseMode = Input.MouseModeEnum.Captured;

		SetUpWeapons();
	}
	
	public override void _Process(double delta) {
		HandleControllerLook((float)delta);
		
		#if DEBUG
		// TODO: add a real pause menu
		if (InputMapHandler.Pause.IsJustPressed) {
			Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
		}
		#endif
	}
	
	public override void _PhysicsProcess(double delta) {
		HandleMovement((float)delta);
	}

	public override void _EnterTree() {
		SubscribeToCameraInputs();
		SubscribeToWeaponInputs();
	}

	public override void _ExitTree() {
		UnsubscribeFromCameraInputs();
		UnsubscribeFromWeaponInputs();
	}
}
