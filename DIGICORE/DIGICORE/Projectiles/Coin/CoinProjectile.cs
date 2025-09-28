using Digicore.Interfaces;
using Godot;
using Godot.Collections;

namespace Digicore.Player.Projectiles;

[GlobalClass]
public partial class CoinProjectile : Projectile, IDamageable {
    [Export] private Curve _speedCurve = new Curve();
    [Export] private float _lifetime = 5f;
    private float _age = 0f;

    private Array<Rid> _playerRid;
    public override void _Ready() { _playerRid = [Player.GetRid()]; }
    public override void _PhysicsProcess(double delta) {
        _age += (float)delta;
        if (_age >= _lifetime) { QueueFree(); return; }
        float speed = _speedCurve.SampleBaked(_age);
        if (BetterPhysicsCast.Raycast(GlobalPosition, GlobalTransform.Basis.Z * -1, speed * (float)delta, _playerRid, out RaycastHit hit)) {
            GlobalPosition = hit.Position;
            if (hit.Collider is IDamageable damageable) { damageable.TakeDamage(50f, hit.Normal); }
            QueueFree();
        } else {
            GlobalPosition += GlobalTransform.Basis.Z * -1 * speed * (float)delta;
        }
    }

    public void Shot() {
        
    }

    public void TakeDamage(float damage, Vector3 force)
    {
        throw new System.NotImplementedException();
    }
}