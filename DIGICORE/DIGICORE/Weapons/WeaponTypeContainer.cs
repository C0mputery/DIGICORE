using Godot;

namespace Digicore.Player.Weapons;

/// <summary>
/// Stupid container shit cuz godot has poor sereirzlaion stuff
/// </summary>
[GlobalClass]
public partial class WeaponTypeContainer : Node {
    [Export] public DigicoreWeapon[] Weapons = [];
    public int CurrentIndex = 0;
}
