using Digicore.Interfaces;
using Godot;

namespace Digicore.Player.Projectiles;

[GlobalClass]
public partial class CoinDamageable : StaticBody3D, IDamageable {
    public void TakeDamage(float damage, Vector3 force) {
        GD.Print("SHOT COINT");
    }
}