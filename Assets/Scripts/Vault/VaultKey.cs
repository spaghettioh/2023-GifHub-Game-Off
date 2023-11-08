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

    public override bool Equals(Object obj)
    {
        return obj is VaultKey x && Id == x.Id && Owner == x.Owner;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() ^ Owner.GetHashCode();
    }

    public static bool operator ==(VaultKey x, VaultKey y)
    {
        return x.Id == y.Id && x.Owner == y.Owner;
    }

    public static bool operator !=(VaultKey x, VaultKey y)
    {
        return !(x == y);
    }
}
