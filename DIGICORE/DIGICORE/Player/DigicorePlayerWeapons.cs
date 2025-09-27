using Godot;
using Digicore.Player.Weapons;

namespace Digicore.Player;

public partial class DigicorePlayer : CharacterBody3D {
    [ExportCategory("Weapons")]
    // Godot does not support exporting multi-dimensional arrays this makes me sad.
    [Export] public WeaponTypeContainer[] WeaponTypeContainer = [];
    [Export] private DigicoreWeapon _defaultWeapon;
    private DigicoreWeapon _currentWeapon;
    private int _currentWeaponTypeIndex = -1;
    
    /// <summary>
    /// Sets up the objet and equips the default weapon.
    /// </summary>
    private void SetUpWeapons() {
        EquipOrCycleWeaponType(-1);
    }
    
    /// <summary>
    /// Cycles through the weapons of a given type, or equips a new type.
    /// If the typeIndex is out of range, it equips the default weapon.
    /// If the typeIndex is the same as the current weapon type, it cycles to the next weapon of that type.
    /// </summary>
    /// <param name="typeIndex"></param>
    private void EquipOrCycleWeaponType(int typeIndex) {
        if (typeIndex < 0 || typeIndex >= WeaponTypeContainer.Length) {
            _currentWeapon?.Unequip();
            _currentWeapon = _defaultWeapon;
            
            _currentWeaponTypeIndex = -1;
        } else if (typeIndex != _currentWeaponTypeIndex) {
            _currentWeapon?.Unequip();
            
            _currentWeaponTypeIndex = typeIndex;
            WeaponTypeContainer weaponType = WeaponTypeContainer[_currentWeaponTypeIndex];
            _currentWeapon = weaponType.Weapons[weaponType.CurrentIndex];
            
            _weaponLabel.Text = _currentWeapon?.Name ?? "None";
            _currentWeapon?.Equip();
        } else {
            WeaponTypeContainer weaponType = WeaponTypeContainer[_currentWeaponTypeIndex];
            
            int newIndex = (weaponType.CurrentIndex + 1) % weaponType.Weapons.Length;
            if (newIndex != weaponType.CurrentIndex) {
                _currentWeapon?.Unequip();
                
                weaponType.CurrentIndex = newIndex;
                _currentWeapon = weaponType.Weapons[weaponType.CurrentIndex];
                
                _weaponLabel.Text = _currentWeapon?.Name ?? "None";
                _currentWeapon?.Equip();
            }
        }
    }
    
    /// <summary>
    /// Subscribes to input events related to weapon actions and switching.
    /// </summary>
    private void SubscribeToWeaponInputs() {
        InputMapHandler.NextWeapon.Pressed += OnNextWeaponType;
        InputMapHandler.PreviousWeapon.Pressed += OnPreviousWeaponType;
        InputMapHandler.NumbersEvent += EquipOrCycleWeaponType;
    }
    
    /// <summary>
    /// Unsubscribes from input events related to weapon actions and switching.
    /// </summary>
    private void UnsubscribeFromWeaponInputs() {
        InputMapHandler.NextWeapon.Pressed -= OnNextWeaponType;
        InputMapHandler.PreviousWeapon.Pressed -= OnPreviousWeaponType;
        InputMapHandler.NumbersEvent -= EquipOrCycleWeaponType;
    }
    
    /// <summary>
    /// Cycles to the next weapon type
    /// </summary>
    private void OnNextWeaponType() {
        int nextIndex = _currentWeaponTypeIndex + 1;
        if (nextIndex >= WeaponTypeContainer.Length) { nextIndex = 0; }
        EquipOrCycleWeaponType(nextIndex);
    }
    
    /// <summary>
    /// Cycles to the previous weapon type
    /// </summary>
    private void OnPreviousWeaponType() {
        int previousIndex = _currentWeaponTypeIndex - 1;
        if (previousIndex < 0) { previousIndex = WeaponTypeContainer.Length - 1; }
        EquipOrCycleWeaponType(previousIndex);
    }
}
