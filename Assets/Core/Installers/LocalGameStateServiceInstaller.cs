using Core.EventBus;
using Core.Service_Locator;

namespace Core.Installers
{
    public class LocalGameStateServiceInstaller : Installer
    {
        public override void Install(ServiceLocator serviceLocator)
        {
            serviceLocator.RegisterService(new LocalGameStateService());
            var localGameStateService = serviceLocator.GetService<LocalGameStateService>();
            localGameStateService.Initialize();
        }
    }
}