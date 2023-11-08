// Comment to hush
#define VERBOSE
using Nerdscape.Events.Logging;

namespace Nerdscape.Events
{
    public static class VaultKeyEventExtensions
    {
        public static VaultKey CheckKeySubscriptions<T1>(
            this VaultKeyAction<T1> action, T1 arg, string msg
        )
        {
            if (action == null)
            {
                msg.LogWarning();
                return VaultKey.Invalid;
            }
            msg.Log();
            return action.Invoke(arg);
        }

        public static VaultKey CheckKeySubscriptions<T1, T2>(
            this VaultKeyAction<T1, T2> action, T1 arg1, T2 arg2, string msg
        )
        {
            if (action == null)
            {
                msg.LogWarning();
                return VaultKey.Invalid;
            }
            msg.Log();
            return action.Invoke(arg1, arg2);
        }
    }
}
