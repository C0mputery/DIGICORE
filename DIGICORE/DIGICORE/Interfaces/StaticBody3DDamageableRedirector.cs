using Godot;

namespace Digicore.Interfaces;

/// <summary>
/// This is a thing because I hate attaching scripts to different node types than is defined by the class.
/// </summary>
public partial class StaticBody3DDamageableRedirector : StaticBody3D, IDamageable {
    [Export] public Node TargetPath;
    private IDamageable _target;

    public override void _Ready() { _target = (IDamageable)TargetPath; }
    
    public void TakeDamage(float damage, Vector3 force) {
        _target?.TakeDamage(damage, force);
    }
}