/*
using System;
using Core.Networking;
using Cysharp.Threading.Tasks;
using FoodTruckCity.Scripts.Networking.HttpClient;
using FoodTruckCity.Scripts.Networking.HttpRequestBuilder;
using FoodTruckCity.Scripts.Networking.Dtos;
using FoodTruckCity.Scripts.Networking.Dtos.CollectTruck;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity.GetNextUserCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetInventory;
using FoodTruckCity.Scripts.Networking.Dtos.Login;
using FoodTruckCity.Scripts.Networking.Dtos.PlaceTruck;
using UnityEngine;
using FoodTruckCity.Scripts.Networking.Dtos.UpgradeTruck;
using JetBrains.Annotations;
using FoodTruckCity.Scripts.Networking.Dtos.Friends;
using FoodTruckCity.Scripts.Networking.Dtos.GetPlayer;
using FoodTruckCity.Scripts.Networking.Dtos.KickTruck;
using FoodTruckCity.Scripts.Networking.Dtos.WebSocket;

namespace FoodTruckCity.Scripts.Networking.GatewayLogic
{
    public class GatewayExample : IGatewayExample
    {
        private readonly IHttpClient _httpClient;
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        private readonly IWebSocketClient _webSocketClient;
        private const string SERVER_URL = "https://1337tests.run.place:5000/api/v1";
        private const string WEB_SOCKET_URL = "wss://1337tests.run.place:5000";
        private const string TELEGRAM_BOT_NAME = "Ftcgtb_bot";
        private string _bearerToken;
        private event Action<WebSocketResponseDto> OnExternalDataReceived;
        
        public GatewayExample(IHttpClient httpClient , IHttpRequestBuilder httpRequestBuilder , IWebSocketClient webSocketClient)
        {
            _httpClient = httpClient;
            _httpRequestBuilder = httpRequestBuilder;
            _webSocketClient = webSocketClient;
        }

        public async UniTask InitializeWebSocketConnection()
        {
            await _webSocketClient.Initialize(this, UrlBuilder(WEB_SOCKET_URL,"/?token=",$"{_bearerToken}"));
        }

        public async UniTask<LoginResponseDto> Login(LoginDto loginDto)
        {
            var json = JsonHandler.Serialize(loginDto);
            var result =   await _httpClient.SendHttpPostRequest(UrlBuilder(SERVER_URL,
                "/login",""),json, _bearerToken , "Login");
            
            var loginResponseDto = JsonHandler.Deserialize<LoginResponseDto>(result);
            _bearerToken = loginResponseDto.Data.Token;
            
            return loginResponseDto;
        }
        
        public async UniTask<GetPlayerResponseDto> GetPlayer(string playerId)
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL,"/player/",playerId),
                _bearerToken ,$"GetPlayer player id: {playerId}");

            var playerDto = JsonHandler.Deserialize<GetPlayerResponseDto>(result);
            
            return playerDto;
        }

        public async UniTask<GetCityResponseDto> GetCity(long playerId)
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL,"/player/",playerId.ToString(),"/zones/"),
               _bearerToken ,$"GetCity player id: {playerId}");
            
            var getCityResponseDto = JsonHandler.Deserialize<GetCityResponseDto>(result);
            
            return getCityResponseDto;
        }
        
        public async UniTask<GetInventoryResponseDto> GetInventory(string playerId)
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL,"/player/inventory"),
                _bearerToken,$"GetInventory player id: {playerId}");
            Debug.Log(result);
            var getInventoryResponseDto = JsonHandler.Deserialize<GetInventoryResponseDto>(result);
            
            return getInventoryResponseDto;
        }

        public async UniTask<GetShopInventoryResponseDto> GetShopInventory(string playerId)
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL,"/marketplace/trucks"),
                _bearerToken,$"GetShopInventory player id: {playerId}");

            var getShopInventoryDto = JsonHandler.Deserialize<GetShopInventoryResponseDto>(result);
            
            return getShopInventoryDto;
        }
        
        public async UniTask<BuyTruckResponseDto> BuyTruck(TruckDto truckDto)
        {
            var json = JsonHandler.Serialize(truckDto);
            var result =   await _httpClient.SendHttpPostRequest(UrlBuilder(SERVER_URL,
                "/marketplace/trucks/",truckDto.ModelId,"/buy"),json, _bearerToken , "BuyTruck");
            
            var buyTruckResponseDto = JsonHandler.Deserialize<BuyTruckResponseDto>(result);
            
            return buyTruckResponseDto;
        }

        public async UniTask<PlaceTruckResponseDto> PlaceTruck(PlaceTruckDto placeTruckDto)
        {
            var json = JsonHandler.Serialize(placeTruckDto);
            var result =   await _httpClient.SendHttpPostRequest(UrlBuilder(SERVER_URL,
                "/player/truck/place/"),json, _bearerToken , "PlaceTruck");
            
            var placeTruckResponseDto = JsonHandler.Deserialize<PlaceTruckResponseDto>(result);
            
            return placeTruckResponseDto;
        }

        public async UniTask<CollectTruckResponseDto> CollectTruck(CollectTruckDto collectTruckDto)
        {
            var json = JsonHandler.Serialize(collectTruckDto);
            var result =   await _httpClient.SendHttpPostRequest(UrlBuilder(SERVER_URL,
                "/player/truck/collect/"),json, _bearerToken , "CollectTruck");
            
            var collectTruckResponseDto = JsonHandler.Deserialize<CollectTruckResponseDto>(result);
            
            return collectTruckResponseDto;
        }
        
        public async UniTask<KickOutTruckResponseDto> KickOutTruck(KickOutTruckDto kickOutTruckDto)
        {
            var json = JsonHandler.Serialize(kickOutTruckDto);
            var result =   await _httpClient.SendHttpPostRequest(UrlBuilder(SERVER_URL,
                "/player/truck/collect/"),json, _bearerToken , "KickOutTruck");
            
            var kickOutTruckResponseDto = JsonHandler.Deserialize<KickOutTruckResponseDto>(result);
            
            return kickOutTruckResponseDto;
        }
        
        public async UniTask<GetNextUserCityResponseDto> GetNextUserCity(string limit = "1" ,[CanBeNull] string next = "")
        {
            var queryParams = $"limit={limit}";

            if (!string.IsNullOrEmpty(next))
                queryParams += $"&next={next}";

            var url = UrlBuilder(SERVER_URL, $"/zones/available?{queryParams}");

            var result = await _httpClient.SendHttpGetRequest(url, _bearerToken, "Get next user city");

            return JsonHandler.Deserialize<GetNextUserCityResponseDto>(result);
        }

        public UniTask InviteFriend(string playerId)
        {
            Application.OpenURL(GetInviteFriendLink(playerId));
            return UniTask.CompletedTask;
        }

        private string GetInviteFriendLink(string playerId)
        {
            var link = $"https://t.me/{TELEGRAM_BOT_NAME}?start=ref_{playerId}";
            var message = Uri.EscapeDataString("¡Come join me in Food Truck City! 🚚🍔");
            return $"https://t.me/share/url?url={link}&text={message}";
        }

        public UniTask CopyInviteLink(string playerId)
        {
            GUIUtility.systemCopyBuffer = GetInviteFriendLink(playerId);
            return UniTask.CompletedTask;
        }
        
        public async UniTask<GetInventoryResponseDto> SendInventory(string playerId, GetInventoryResponseDto inventoryResponseDto)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<Sprite> DownloadImageFromUrl(string url)
        {
            //Todo: is only for test purposes
           // var result = await _httpClient.DownloadSprite(url,"", "DownloadImage");
            var result = await _httpClient.DownloadSpriteFromStreamingAssets("user.png", "DownloadImage");
            return result;
        }
        
        public async UniTask<GetFriendResponseDto> GetFriends()
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL, "/player/friends"),
                _bearerToken, $"Get Friends");

            var getFriendResponseDto = JsonHandler.Deserialize<GetFriendResponseDto>(result);

            return getFriendResponseDto;
        }
        
        public async UniTask<UpgradeTruckResponseDto> UpgradeTruck(UpgradeTruckDto upgradeTruckDto)
        {
            var json = JsonHandler.Serialize(upgradeTruckDto);
            var result =   await _httpClient.SendHttpPutRequest(UrlBuilder(SERVER_URL,
                "/trucks/",upgradeTruckDto.TruckId,"/upgrade"),json, _bearerToken , "UpgradeTruck", "PATCH");
            
            var upgradeTruckResponseDto = JsonHandler.Deserialize<UpgradeTruckResponseDto>(result);
            
            return upgradeTruckResponseDto;
        }

        public async UniTask<GetUpgradeResponseDto> GetNextUpgrade(string truckModelId, int nextUpgrade)
        {
            var result = await _httpClient.SendHttpGetRequest(UrlBuilder(SERVER_URL,
                "/trucks/info/", truckModelId, "/" + nextUpgrade.ToString()), _bearerToken, "Get next upgrade");

            var upgradeTruckResponseDto = JsonHandler.Deserialize<GetUpgradeResponseDto>(result);

            return upgradeTruckResponseDto;
        }

        public void SubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived)
        {
            OnExternalDataReceived += onExternalDataReceived;
        }
        
        public void UnSubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived)
        {
            OnExternalDataReceived -= onExternalDataReceived;
        }

        public void NotifyExternalDataReceived(string message)
        { 
            var webSocketResponseDto = JsonHandler.Deserialize<WebSocketResponseDto>(message);
            
            OnExternalDataReceived?.Invoke(webSocketResponseDto);
        }

        private string UrlBuilder(string baseUrl, string path = "", string id = "", string additionalPath = "")
        {
            _httpRequestBuilder.Reset();
            _httpRequestBuilder.SetBaseUrl(baseUrl);
            _httpRequestBuilder.SetBasePath(path);
            _httpRequestBuilder.SetId(id);
            _httpRequestBuilder.SetAdditionalPath(additionalPath);

            return _httpRequestBuilder.GetHttpRequest();
        }
    }
}
*/
