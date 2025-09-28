using Godot;

namespace Digicore.Player;

public partial class DigicorePlayer : CharacterBody3D {
	[ExportCategory("Health")]
	[Export] private float _maxHealth = 100f;
	private float _currentHealth = 100;
	
	public void TakeDamage(float damage, Vector3 force) {
		GD.Print($"Player took {damage} damage with force {force}");
		_currentHealth -= damage;
		if (_currentHealth <= 0) {
			_currentHealth = 0;
			GD.Print("Player died");
		}

		_healthLabel.Text = _currentHealth.ToString("F");
		
		Velocity += force;
	}
}
