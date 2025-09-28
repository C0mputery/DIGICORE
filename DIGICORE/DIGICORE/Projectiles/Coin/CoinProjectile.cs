using System.Collections.Generic;
using Digicore.Interfaces;
using Godot;

namespace Digicore.Player.Projectiles;

[GlobalClass]
public partial class CoinProjectile : Projectile, IDamageable {
    [Export] private Curve _speedCurve = new Curve();
    [Export] private float _lifetime = 5f;
    [Export] private float _ricoshotRange = 1000f;
    [Export] private float _ricoshotMultiplier = 1.5f;
    public bool beenShot = false;
    private float _age = 0f;

    private Godot.Collections.Array<Rid> _playerRid;
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

    public void TakeDamage(float damage, Vector3 force) {
        List<int> sortedPriorities = CoinTarget.SortedPriorities;
        foreach (int priority in sortedPriorities) {
            CoinTarget closestTarget = null;
            float closestDistance = float.MaxValue;
            
            foreach (CoinTarget coinTarget in CoinTarget.CoinTargets[priority]) {
                if (!BetterPhysicsCast.Raycast(GlobalPosition, (coinTarget.GlobalPosition - GlobalPosition).Normalized(), _ricoshotRange, _playerRid, out RaycastHit hit)) { continue; }
                if (hit.Collider != coinTarget.CollisionObject) { continue; }
                
                float distanceToTarget = GlobalPosition.DistanceTo(coinTarget.GlobalPosition);
                
                if (distanceToTarget < closestDistance) {
                    closestDistance = distanceToTarget;
                    closestTarget = coinTarget;
                }
            }
            
            if (closestTarget != null) {
                GD.Print("hit target");
                if (closestTarget.CollisionObject is IDamageable damageable) { damageable.TakeDamage(damage * _ricoshotMultiplier, force); }
            }
        }
    }
}