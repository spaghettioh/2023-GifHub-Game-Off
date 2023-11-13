using System;

public struct VaultKey
{
    public static VaultKey Invalid = new(-1, null);

    internal int Id;
    internal Type Owner;

    internal VaultKey(int id, Type owner)
    {
        Id = id;
        Owner = owner;
    }

    public override bool Equals(object obj) =>
        obj is VaultKey x && Id == x.Id && Owner == x.Owner;

    public override int GetHashCode() => Id.GetHashCode() ^ Owner.GetHashCode();

    public static bool operator ==(VaultKey x, VaultKey y) =>
        x.Id == y.Id && x.Owner == y.Owner;

    public static bool operator !=(VaultKey x, VaultKey y) => !(x == y);
}
