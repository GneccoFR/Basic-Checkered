/*
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Networking;
using Cysharp.Threading.Tasks;
using FoodTruckCity.Scripts.Core.Models.SpatialGrid;
using FoodTruckCity.Scripts.Models;
using FoodTruckCity.Scripts.Networking.Dtos;
using FoodTruckCity.Scripts.Networking.Dtos.CollectTruck;
using FoodTruckCity.Scripts.Networking.Dtos.Friends;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity.GetNextUserCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetInventory;
using FoodTruckCity.Scripts.Networking.Dtos.GetPlayer;
using FoodTruckCity.Scripts.Networking.Dtos.KickTruck;
using FoodTruckCity.Scripts.Networking.Dtos.Login;
using FoodTruckCity.Scripts.Networking.Dtos.PlaceTruck;
using FoodTruckCity.Scripts.Networking.Dtos.UpgradeTruck;
using FoodTruckCity.Scripts.Networking.Dtos.WebSocket;
using FoodTruckCity.Scripts.Networking.HttpClient;
using FoodTruckCity.Scripts.Networking.HttpRequestBuilder;
using UnityEngine;
using UnityEngine.Networking;

namespace FoodTruckCity.Scripts.Networking.GatewayLogic
{
    public class CustomGatewayExample : IGatewayExample
    {
        #region Jsons
        
        private readonly string _jsonLoginResponse =
            "{" +
            "\n    \"success\": true," +
            "\n    \"data\": {" +
            "\n        \"player\": {" +
            "\n            \"coins\": 500," +
            "\n            \"level\": 1," +
            "\n            \"createdAt\": 1747059185215," +
            "\n            \"firstName\": \"Fede\"," +
            "\n            \"photoURL\": \"https://i.pravatar.cc/200\"," +
            "\n            \"gamesPlayed\": 0," +
            "\n            \"goldCoins\": 10," +
            "\n            \"xp\": 0," +
            "\n            \"id\": 1337," +
            "\n            \"updatedAt\": 1747059185215," +
            "\n            \"username\": \"Fede\"" +
            "\n        }," +
            "\n        \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTMzNywiaWF0IjoxNzQ3MDcxNDE3LCJleHAiOjE3NTEwNzQ2MTd9.KTgHiTy6YJQ6N6USW9Y52oYJ9QSOKtDAoShpes9S8QA\"" +
            "\n    }" +
            "\n}";
        
        private readonly string _getCityResponse =
            "{" +
            "\n    \"sucess\": true," +
            "\n    \"data\": {" +
            "\n        \"zones\": [" +
            "\n            {" +
            "\n                \"id\": \"Z#etwkEdvi4a\"," +
            "\n                \"slots\": [" +
            "\n                    {" +
            "\n                        \"layout\": \"Layout A\"," +
            "\n                        \"type\": \"LEGAL\"," +
            "\n                        \"order\": 2," +
            "\n                        \"status\": \"PLACED\"," +
            "\n          \"entityOwner\": {" +
            "\n                            \"player\": {" +
            "\n                                \"id\": \"2\"," +
            "\n                                \"username\": \"Matias\"," +
            "\n                                \"avatar\": \"https://i.pravatar.cc/300\"" +
            "\n                            }," +
            "\n                            \"truck\": {" +
            "\n                                \"name\": \"Argentinian Food\"," +
            "\n                                \"id\": \"et1pHf67Dy\"," +
            "\n                                \"incomeRate\": 1000," +
            "\n                                \"level\": 1," +
            "\n                                \"msIncomeInterval\": 3600000" +
            "\n                            }" +
            "\n                        }," +
            "\n                        \"placedAt\": 1747079245202," +
            "\n              \"id\": \"S#etwkEdwJCC\"" +
            "\n                    }," +
            "\n                    {" +
            "\n                        \"layout\": \"Layout A\"," +
            "\n                        \"type\": \"LEGAL\"," +
            "\n                        \"order\": 4," +
            "\n                        \"status\": \"EMPTY\"," +
            "\n                        \"id\": \"S#etwkEdwWq2\"" +
            "\n                    }," +
            "\n                    {" +
            "\n                        \"layout\": \"Layout A\"," +
            "\n                        \"type\": \"LEGAL\"," +
            "\n                        \"order\": 1," +
            "\n                        \"status\": \"EMPTY\"," +
            "\n                        \"id\": \"S#etwkEdwdC1\"" +
            "\n                    }," +
            "\n                    {" +
            "\n                        \"layout\": \"Layout A\"," +
            "\n                        \"type\": \"ILEGAL\"," +
            "\n                        \"order\": 5," +
            "\n                        \"status\": \"EMPTY\"," +
            "\n                        \"id\": \"S#etwkEdwgge\"" +
            "\n                    }," +
            "\n                    {" +
            "\n                        \"layout\": \"Layout A\"," +
            "\n                        \"type\": \"LEGAL\"," +
            "\n                        \"order\": 3," +
            "\n                        \"status\": \"EMPTY\"," +
            "\n                        \"id\": \"S#etwkEdwwYa\"" +
            "\n                    }" +
            "\n                ]" +
            "\n            }" +
            "\n        ]" +
            "\n    }" +
            "\n}";
        
        private readonly string _getCityResponseV2 =
                    "{" +
                    "\n    \"sucess\": true," +
                    "\n    \"data\": {" +
                    "\n        \"zones\": [" +
                    "\n            {" +
                    "\n                \"id\": \"Z#etwkEdvi4a\"," +
                    "\n                \"slots\": [" +
                    "\n                    {" +
                    "\n                        \"layout\": \"Layout A\"," +
                    "\n                        \"type\": \"LEGAL\"," +
                    "\n                        \"order\": 2," +
                    "\n                        \"status\": \"EMPTY\"," +
                    "\n                        \"id\": \"S#etwkEdwJCC\"" +
                    "\n                    }," +
                    "\n                    {" +
                    "\n                        \"layout\": \"Layout A\"," +
                    "\n                        \"type\": \"LEGAL\"," +
                    "\n                        \"order\": 4," +
                    "\n                        \"status\": \"EMPTY\"," +
                    "\n                        \"id\": \"S#etwkEdwWq2\"" +
                    "\n                    }," +
                    "\n                    {" +
                    "\n                        \"layout\": \"Layout A\"," +
                    "\n                        \"type\": \"LEGAL\"," +
                    "\n                        \"order\": 1," +
                    "\n                        \"status\": \"EMPTY\"," +
                    "\n                        \"id\": \"S#etwkEdwdC1\"" +
                    "\n                    }," +
                    "\n                    {" +
                    "\n                        \"layout\": \"Layout A\"," +
                    "\n                        \"type\": \"ILEGAL\"," +
                    "\n                        \"order\": 5," +
                    "\n                        \"status\": \"EMPTY\"," +
                    "\n                        \"id\": \"S#etwkEdwgge\"" +
                    "\n                    }," +
                    "\n                    {" +
                    "\n                        \"layout\": \"Layout A\"," +
                    "\n                        \"type\": \"LEGAL\"," +
                    "\n                        \"order\": 3," +
                    "\n                        \"status\": \"EMPTY\"," +
                    "\n                        \"id\": \"S#etwkEdwwYa\"" +
                    "\n                    }" +
                    "\n                ]" +
                    "\n            }" +
                    "\n        ]" +
                    "\n    }" +
                    "\n}";

        private string _getInventory =
            "{\n  \"success\": true,\n  \"data\": {\n  \"trucks\":    [\n {\n      \"productionSpeed\": 1.2,\n      \"placed\": false,\n      \"level\": 1,\n      \"msIncomeInterval\": 3600000,\n      \"description\": \"Savor the variety of Chinese food, where each dish bursts with rich, savory flavors. Whether it's crispy spring rolls or stir-fried noodles, it's the ultimate choice for any craving!\",\n      \"stars\": 25,\n      \"incomeRate\": 1000,\n      \"msCollectInterval\": 300000,\n      \"revenue\": 0,\n      \"msInterval\": 10800000,\n      \"price\": 300,\n      \"name\": \"Argentinian Food\",\n      \"status\": \"NOT_AVAILABLE\",\n      \"maxRevenue\": 1337,\n      \"id\": \"T#etv2QmtzPE\",\n      \"rarity\": 1, \n      \"modelId\": \"CHINESE_FOOD\" },\n    {\n      \"productionSpeed\": 1.0,\n      \"placed\": true,\n      \"level\": 2,\n      \"msIncomeInterval\": 1800000,\n      \"description\": \"Fresh Italian pasta and cheesy pizzas served hot! Boosts foot traffic in any area.\",\n      \"stars\": 40,\n      \"incomeRate\": 1500,\n      \"msCollectInterval\": 180000,\n      \"revenue\": 500,\n      \"msInterval\": 7200000,\n      \"price\": 450,\n      \"name\": \"Italian Food\",\n      \"status\": \"PLACED\",\n      \"maxRevenue\": 2000,\n      \"id\": \"T#abc123\", \n      \"rarity\": 1, \n      \"modelId\": \"ITALIAN_FOOD\" }\n ]\n }\n}\n";
      
        #endregion Jsons
        
        private readonly IHttpClient _httpClient;
        private readonly IHttpRequestBuilder _httpRequestBuilder;

        private FriendDto[] friends = new FriendDto[]
        {
                new FriendDto { Id = "1", FirstName = "Santiago", PhotoURL = "https://i.pravatar.cc/200", Username = "SantiDev" },
                new FriendDto { Id = "2", FirstName = "Fernando", PhotoURL = "https://i.pravatar.cc/200", Username = "FerDev" },
                new FriendDto { Id = "3", FirstName = "Matias", PhotoURL = "https://i.pravatar.cc/200", Username = "MatiDev" },
        };

        public CustomGatewayExample()
        {
            for (int i = 0; i < friends.Length; i++)
            {
                friendsCity.Add(friends[i].Id.ToString(), _getCityResponseV2);
            }
        }

        public UniTask InitializeWebSocketConnection()
        { 
            return UniTask.CompletedTask;
        }

        public async UniTask<LoginResponseDto> Login(LoginDto loginDto)
        {
            var loginResponseDto =  JsonHandler.Deserialize<LoginResponseDto>(_jsonLoginResponse);
         
            return loginResponseDto;
        }

        public async UniTask<GetPlayerResponseDto> GetPlayer(string playerId)
        {
            return new GetPlayerResponseDto
            {
                Success = true,
                Data = new PlayerDto
                {
                    Id = playerId,
                    Username = "generic",
                    PhotoUrl = "https://i.pravatar.cc/200"
                }
            };
        }

        Dictionary<string, string> friendsCity = new Dictionary<string, string>();

        public async UniTask<GetCityResponseDto> GetCity(long cityId)
        {
            if (friendsCity.TryGetValue(cityId.ToString(), out var cityJson))
            {
                var dto = JsonHandler.Deserialize<GetCityResponseDto>(cityJson);
                return dto;
            }

            var getCityDto = JsonHandler.Deserialize<GetCityResponseDto>(_getCityResponse);

            return getCityDto;
        }
        
        public async UniTask<PlaceTruckResponseDto> PlaceTruck(PlaceTruckDto placeTruckDto)
        {
            var result = new PlaceTruckResponseDto
            {
                Success = true
            };
            
            return result;
        }

        public async UniTask<CollectTruckResponseDto> CollectTruck(CollectTruckDto collectTruckDto)
        {
            var result = new CollectTruckResponseDto
            {
                Success = true
            };

            return result;
        }

        public async UniTask<GetInventoryResponseDto> GetInventory(string id)
        {
            var getInventoryDto =  JsonHandler.Deserialize<GetInventoryResponseDto>(_getInventory);
         
            return getInventoryDto;
        }

        public UniTask<GetShopInventoryResponseDto> GetShopInventory(string playerId)
        {
            throw new NotImplementedException();
        }

        public UniTask<BuyTruckResponseDto> BuyTruck(TruckDto truckDto)
        {
            throw new NotImplementedException();
        }

        public async UniTask<GetInventoryResponseDto> SendInventory(string playerId, GetInventoryResponseDto inventoryResponseDto)
        {
            string inventory = JsonHandler.Serialize(inventoryResponseDto);
            _getInventory = inventory;
            
            var getInventoryDto =  JsonHandler.Deserialize<GetInventoryResponseDto>(_getInventory);
            return getInventoryDto;
        }

        public UniTask InviteFriend(string playerId)
        {
            throw new NotImplementedException();
        }

        public UniTask CopyInviteLink(string playerId)
        {
            throw new NotImplementedException();
        }


        public async UniTask<Sprite> DownloadImageFromUrl(string url)
        {
            try
            {
                using var webRequest = UnityWebRequestTexture.GetTexture(url);
              
                await webRequest.SendWebRequest().ToUniTask();
            
                if (webRequest.result is not (UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError))
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

                    var rect = new Rect(0, 0, texture.width, texture.height);
                    var pivot = new Vector2(0.5f, 0.5f);
                    var sprite = Sprite.Create(texture, rect, pivot);

                    return sprite;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Exception during sprite download: {ex.Message}");
                return null;
            }
        }

        public void SubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived)
        {
            
        }
        
        public void UnSubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived)
        {
            
        }

        public void NotifyExternalDataReceived(string message)
        {
            
        }

        public UniTask<GetNextUserCityResponseDto> GetNextUserCity(string limit = "1" , string next = "")
        {
            throw new NotImplementedException();
        }

        public UniTask<GetFriendResponseDto> GetFriends()
        {
            var result = new GetFriendResponseDto
            {
                Success = true,
                Data = new FriendDataDto { Friends = friends.ToList() }
            };

            return UniTask.FromResult(result);
        }

        public UniTask<List<GridCellDto>> GetFriendGrid(string friendID)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask<KickOutTruckResponseDto> KickOutTruck(KickOutTruckDto collectTruckDto)
        {
            var result = new KickOutTruckResponseDto
            {
                Success = true
            };

            return result;
        }

        public async UniTask<UpgradeTruckResponseDto> UpgradeTruck(UpgradeTruckDto upgradeTruckDto)
        {
            var result = new UpgradeTruckResponseDto
            {
                Success = true
            };

            return result;
        }

        public async UniTask<GetUpgradeResponseDto> GetNextUpgrade(string truckModelId, int nextLevel)
        {
            var result = new GetUpgradeResponseDto
            {
                Success = true,
                Data = new UpgradeDto
                {
                    Level = nextLevel,
                    IncomeRate = 250,
                    MsInterval = 21600,
                    PriceCoins = 10000,
                    PriceTokens = 0
                }
            };

            return result;
        }
    }
}
*/