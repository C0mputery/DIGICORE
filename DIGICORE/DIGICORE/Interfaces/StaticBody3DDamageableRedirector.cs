using Godot;

namespace Digicore.Interfaces;

/// <summary>
/// This is a thing because I hate attaching scripts to different node types than is defined by the class.
/// </summary>
[GlobalClass]
public partial class StaticBody3DDamageableRedirector : StaticBody3D, IDamageable {
    [Export] public Node Target;
    private IDamageable _target;

    public override void _Ready() { _target = (IDamageable)Target; }
    
    public void TakeDamage(float damage, Vector3 force) {
        _target?.TakeDamage(damage, force);
    }
}