/*
using System.Net.Http;
using FoodTruckCity.Scripts.Shared.ServiceLocator;
using FoodTruckCity.Scripts.Shared.Utils;

namespace FoodTruckCity.Scripts.Networking.GatewayLogic.Installer
{
    public class GatewayInstallerExample : Shared.ServiceLocator.Installer
    {
        private IGatewayExample _gatewayExample;
        public override void Install(IServiceLocator serviceLocator)
        {
            if (GlobalConfiguration.DataOrigin == DataOrigin.Local)
                _gatewayExample = new CustomGatewayExample();
            else
                _gatewayExample = new GatewayExample(new HttpClient.HttpClient(), new HttpRequestBuilder.HttpRequestBuilder(),
                    new WebSocketClient());
            
            serviceLocator.RegisterService(_gatewayExample);
        }
    }
}
*/