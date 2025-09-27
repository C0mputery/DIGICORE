using Digicore.Interfaces;
using Digicore.Player.Projectiles;
using Godot;
using Godot.Collections;

namespace Digicore.Player.Weapons;

[GlobalClass]
public partial class SemiAutoHitscanWithProjectileAltFire : SemiAutoHitscan {
    [Export] private PackedScene _projectile;
    [Export] public float AltFireRate = 0.5f;
    private float _timeSinceLastShot = 0f;
    
    public override void _Process(double delta) { 
        base._Process(delta);
        _timeSinceLastShot += (float)delta; 
    }
    
    public void SecondaryAttack() {
        if (_timeSinceLastShot < 1f / AltFireRate) { return; }
        
        Projectile loadedProjectile = _projectile.Instantiate<Projectile>();
        loadedProjectile.Player = Player;
        GetTree().CurrentScene.AddChild(loadedProjectile);
        loadedProjectile.GlobalTransform = Player.Camera.GlobalTransform;
        
        _timeSinceLastShot = 0f;
    }
    
    public override void Equip() {
        base.Equip();
        InputMapHandler.Secondary.Pressed += SecondaryAttack;
    }
    
    public override void Unequip() {
        base.Unequip();
        InputMapHandler.Secondary.Pressed -= SecondaryAttack;
    }
}