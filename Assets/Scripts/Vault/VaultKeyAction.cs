public delegate VaultKey VaultKeyAction<T1>(T1 keyOwner);
public delegate VaultKey VaultKeyAction<T1, T2>(T1 keyOwner, T2 passedObject);
