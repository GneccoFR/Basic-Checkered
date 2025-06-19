using Core.Service_Locator;

namespace Core.Installers
{
    public class GatewayInstaller : Installer
    {
        public override void Install(ServiceLocator serviceLocator)
        {
            serviceLocator.RegisterService<BasicCheckeredBE.Networking.IGateway>(new BasicCheckeredBE.Networking.ServerGateway());
            var gateway = serviceLocator.GetService<BasicCheckeredBE.Networking.IGateway>();
            gateway.Initialize();
        }
    }
}