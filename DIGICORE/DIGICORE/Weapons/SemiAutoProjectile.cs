using Digicore.Interfaces;
using Digicore.Player.Projectiles;
using Godot;
using Godot.Collections;

namespace Digicore.Player.Weapons;

[GlobalClass]
public partial class SemiAutoProjectile : DigicoreWeapon {
    [Export] private PackedScene _projectile;
    [Export] public float FireRate = 0.5f; // Shots per second
    private float _timeSinceLastShot = 0f;
    
    public override void _Process(double delta) { _timeSinceLastShot += (float)delta; }
    
    public void PrimaryAction() {
        if (_timeSinceLastShot < 1f / FireRate) { return; }
        
        Projectile loadedProjectile = _projectile.Instantiate<Projectile>();
        loadedProjectile.Player = Player;
        MainScene.Instance.AddChild(loadedProjectile);
        loadedProjectile.GlobalTransform = Player.Camera.GlobalTransform;
        
        _timeSinceLastShot = 0f;
    }

    public override void Equip() {
        InputMapHandler.Primary.Pressed += PrimaryAction;
        GD.Print($"Equipped {Name}");
    }

    public override void Unequip() {
        InputMapHandler.Primary.Pressed -= PrimaryAction;
    }
}