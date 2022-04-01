namespace MusaUtils.UpgradeSystem
{
    public interface IUpgradeable<T>
    {
        public void OnUpgradeEvents(T data);
        public void UpgradeVisuals();
    }
}
