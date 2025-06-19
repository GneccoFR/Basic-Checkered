using Core.EventBus;
using Core.Service_Locator;

namespace Core.Installers
{
    public class EventBusInstaller : Installer
    {
        public override void Install(ServiceLocator serviceLocator)
        {
            serviceLocator.RegisterService<IEventBus>(new EventBus.EventBus());
        }
    }
}