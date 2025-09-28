using System.Collections.Generic;
using Godot;

namespace Digicore.Player.Projectiles;

[GlobalClass]
public partial class CoinTarget : Node3D {
    public static readonly Dictionary<int, List<CoinTarget>> CoinTargets = new();
    public static readonly List<int> SortedPriorities = [];
    public void _AddToPriorityList() {
        if (!CoinTargets.TryGetValue(Priority, out List<CoinTarget> coinTargets)) {
            coinTargets = [];
            CoinTargets[Priority] = coinTargets;
            SortedPriorities.Add(Priority);
            SortedPriorities.Sort();
            SortedPriorities.Reverse();
        }

        coinTargets.Add(this);
    }
    public void _RemoveFromPriorityList() {
        if (!CoinTargets.TryGetValue(Priority, out List<CoinTarget> coinTargets)) { return; }
        
        coinTargets.Remove(this);
        if (CoinTargets[Priority].Count != 0) { return; }
        
        CoinTargets.Remove(Priority);
        SortedPriorities.Remove(Priority);
    }
    
    [Export] public int Priority = 0;
}