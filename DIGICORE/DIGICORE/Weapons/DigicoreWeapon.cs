using Godot;

namespace Digicore.Player.Weapons;

/// <summary>
/// Inherit from this class to create a new weapon.
/// </summary>
[GlobalClass]
public abstract partial class DigicoreWeapon : Node {
    /// <summary>
    /// Reference to player.
    /// </summary>
    [Export] protected DigicorePlayer Player;
    
    /// <summary>
    /// Equip the weapon, runs when the weapon is selected.
    /// </summary>
    public abstract void Equip();
    
    /// <summary>
    /// Unequip the weapon, runs when the weapon is deselected.
    /// </summary>
    public abstract void Unequip();
}