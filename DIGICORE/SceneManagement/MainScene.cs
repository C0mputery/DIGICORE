using Godot;

// Assume this is pointless and dumb and I just don't know something about godot yet.
public partial class MainScene : Node3D {
	public static MainScene Instance;
	public override void _Ready() { Instance = this; }
}
