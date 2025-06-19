/*
using System;
using Cysharp.Threading.Tasks;
using FoodTruckCity.Scripts.Networking.Dtos.CollectTruck;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetCity.GetNextUserCity;
using FoodTruckCity.Scripts.Networking.Dtos.GetInventory;
using FoodTruckCity.Scripts.Networking.Dtos.Login;
using FoodTruckCity.Scripts.Networking.Dtos.PlaceTruck;
using UnityEngine;
using FoodTruckCity.Scripts.Networking.Dtos.UpgradeTruck;
using FoodTruckCity.Scripts.Networking.Dtos.Friends;
using FoodTruckCity.Scripts.Networking.Dtos.GetPlayer;
using FoodTruckCity.Scripts.Networking.Dtos.KickTruck;
using FoodTruckCity.Scripts.Networking.Dtos.WebSocket;

namespace FoodTruckCity.Scripts.Networking.GatewayLogic
{
    public interface IGatewayExample
    {
        UniTask InitializeWebSocketConnection();
        UniTask <LoginResponseDto> Login(LoginDto loginDto);
        UniTask<GetPlayerResponseDto> GetPlayer(string playerId);
        UniTask <GetCityResponseDto> GetCity(long playerId);
        UniTask<GetFriendResponseDto> GetFriends();
        UniTask<PlaceTruckResponseDto> PlaceTruck(PlaceTruckDto placeTruckDto);
        UniTask<CollectTruckResponseDto> CollectTruck(CollectTruckDto collectTruckDto);
        UniTask<KickOutTruckResponseDto> KickOutTruck(KickOutTruckDto collectTruckDto);
        UniTask<GetInventoryResponseDto> GetInventory(string playerId);
        UniTask<UpgradeTruckResponseDto> UpgradeTruck(UpgradeTruckDto upgradeTruckDto);
        UniTask<GetUpgradeResponseDto> GetNextUpgrade(string truckModelId, int nextLevel);
        UniTask<GetShopInventoryResponseDto> GetShopInventory(string playerId);
        UniTask<BuyTruckResponseDto> BuyTruck(TruckDto truckDto);
        UniTask<GetInventoryResponseDto> SendInventory(string playerId, GetInventoryResponseDto inventoryResponseDto);
        UniTask InviteFriend(string playerId);
        UniTask CopyInviteLink(string playerId);
        UniTask<Sprite> DownloadImageFromUrl(string url);
        void SubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived);
        void UnSubscribeToReceiveExternalData(Action<WebSocketResponseDto> onExternalDataReceived);
        void NotifyExternalDataReceived(string message);
        UniTask<GetNextUserCityResponseDto> GetNextUserCity(string limit = "1", string next = "");
       
    }
}
*/