using Godot;

namespace Digicore.Player;

public partial class DigicorePlayer : CharacterBody3D {
    [ExportCategory("UI")]
    [Export] private Label _healthLabel;
    [Export] private Label _speedLabel;
}
