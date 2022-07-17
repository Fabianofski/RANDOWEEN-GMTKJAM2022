
namespace Building
{
    public interface Building
    {
        public bool IsUpgradeable();
        public void Upgrade();
        public bool IsDowngradeable();

        public void Downgrade();
        public void Enable();
        public void Disable();
    }
}