using Digicore.Interfaces;
using Godot;
using Godot.Collections;

namespace Digicore.Player.Weapons;

[GlobalClass]
public partial class SemiAutoHitscan : DigicoreWeapon {
    [Export] public float Damage = 10f;
    [Export] public float Range = 100f;
    [Export] public float FireRate = 0.5f; // Shots per second
    private float _timeSinceLastShot = 0f;
    
    public override void _Process(double delta) { _timeSinceLastShot += (float)delta; }

    private Array<Rid> _playerRid;
    public override void _Ready() { _playerRid = [Player.GetRid()]; }

    public void PrimaryAction() {
        if (_timeSinceLastShot < 1f / FireRate) { return; }
        
        if (BetterPhysicsCast.Raycast(Player.Camera.GlobalPosition, Player.Camera.GlobalTransform.Basis.Z * -1, Range, _playerRid, out RaycastHit hit)) {
            if (hit.Collider is IDamageable damageable) { damageable.TakeDamage(Damage, hit.Normal); }
            GD.Print(hit.Collider);
        }
        
        _timeSinceLastShot = 0f;
    }

    public override void Equip() {
        InputMapHandler.Primary.Pressed += PrimaryAction;
    }

    public override void Unequip() {
        InputMapHandler.Primary.Pressed -= PrimaryAction;
    }
}