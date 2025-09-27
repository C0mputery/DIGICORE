using Digicore.Interfaces;
using Godot;

namespace Digicore.Enemies;

public partial class Dummy : StaticBody3D, IDamageable {
    public void TakeDamage(float damage, Vector3 force) {
        GD.Print($"Dummy took {damage} damage with force {force}");
    }
}