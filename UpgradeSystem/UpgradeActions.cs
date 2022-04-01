using System;

namespace MusaUtils.UpgradeSystem
{
    public static class UpgradeActions<T>
    {
        public static event Action<T> onUpgrade;
        public static void Upgrade(T data) => onUpgrade?.Invoke(data);
    }
}
