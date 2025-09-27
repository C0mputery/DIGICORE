using Godot;

namespace Digicore.Interfaces;

/// <summary>
/// Interface for anything that can take damage.
/// </summary>
public interface IDamageable {
    /// <summary>
    /// Apply damage to the object.
    /// </summary>
    /// <param name="damage"> Amount of damage to apply. </param>
    /// <param name="force"> Force to apply to the object. Different objects may use this differently. </param>
    public void TakeDamage(float damage, Vector3 force);
}