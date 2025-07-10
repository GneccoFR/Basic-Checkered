using System;
using System.Collections.Generic;
using System.Linq;
using BasicCheckeredBE.Networking;
using Core.Shared;
using UnityEngine;


namespace Core.Networking
{
    public static class LinqMapper
    {
        /// <summary>
        /// Maps a single object from one type to another using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <typeparam name="TU">The source type.</typeparam>
        /// <param name="source">The object to be mapped.</param>
        /// <param name="mappingFunc">The function that defines how to map from TU to T.</param>
        /// <returns>The mapped object of type T.</returns>
        public static T MapTo<T, TU>(this TU source, Func<TU, T> mappingFunc)
        {
            return mappingFunc(source);
        }

        /// <summary>
        /// Maps a collection of objects from one type to another using the provided mapping function.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <typeparam name="TU">The source type.</typeparam>
        /// <param name="source">The collection of objects to be mapped.</param>
        /// <param name="mappingFunc">The function that defines how to map from TU to T.</param>
        /// <returns>A list of mapped objects of type T.</returns>
        public static List<T> MapToList<T, TU>(this IEnumerable<TU> source, Func<TU, T> mappingFunc)
        {
            return source.Select(mappingFunc).ToList();
        }

        public static List<BoardSquare> MapToList(this IEnumerable<BasicCheckeredBE.Networking.DTOs.SquareDTO> squaresDto)
        {
            return squaresDto.MapToList(squareDto =>
            {
                var player = new Player((PlayerType)squareDto.Piece.Owner.OwnerType, squareDto.Piece.Owner.PlayerId);
                var piece = new Piece((PieceType)squareDto.Piece.PieceType, player);
                return new BoardSquare(piece, new Vector2(squareDto.X, squareDto.Y));
            });
        }
        
        public static GameBoard ToModel(this BasicCheckeredBE.Networking.DTOs.BoardDTO boardDto) => boardDto.MapTo(d =>
        {
            var squares = d.Board;
            var boardSquares = new BoardSquare[squares.GetLength(0), squares.GetLength(1)];
            for (int y = 0; y < squares.GetLength(1); y++)
            {
                for (int x = 0; x < squares.GetLength(0); x++)
                {
                    var squareDto = squares[x, y];
                    var player = new Player((PlayerType)squareDto.Piece.Owner.OwnerType, squareDto.Piece.Owner.PlayerId);
                    var piece = new Piece((PieceType)squareDto.Piece.PieceType, player);
                    boardSquares[x, y] = new BoardSquare(piece, new Vector2(x, y));
                }
            }
            return new GameBoard(boardSquares);
        });
            
        public static Player MapToPlayerType(this BasicCheckeredBE.Core.Domain.Player player)
        {
            return new Player((PlayerType)player.OwnerType, player.PlayerId);
        }
        public static BasicCheckeredBE.Core.Domain.Player MapToPlayerBEType(this Player player)
        {
            return new BasicCheckeredBE.Core.Domain.Player((GlobalFields.PlayerType)player.OwnerType, player.PlayerId);
        }
        
        //public static  
        
        /*
        
        List<Card> handCards = handCardsDto.MapToList(c => c.ToModel());

        public static GetInventoryResponseDto ToDto(this Inventory inventory)
            => inventory.MapTo(inv => new GetInventoryResponseDto
            {
                Data = new InventoryData
                {
                    Trucks = inv.Trucks.MapToList(truck => truck.ToDto())
                }
            });

        public static TruckDto ToDto(this Truck truck)
            => truck.MapTo(t => new TruckDto
            {
                Id = t.ID,
                Name = t.Name,
                Description = t.Description,
                PriceCoins = t.Cost,
                Status = !string.IsNullOrEmpty(t.PlacedSlotID) ? "PLACED" : t.Purchased ? "AVAILABLE" : "NOT_AVAILABLE",
                MsInterval = t.RevenueTotalTime,
                MaxRevenue = t.RevenueAmount,
                Rarity = t.Rarity.ToString(),
                ModelId = t.Model_ID,
                SlotID = t.PlacedSlotID,
                ZoneID = t.PlacedZoneID,
                OwnerZoneID = t.OwnerZoneID
            });

        public static Grid ToModel(this CityDataDto cityDto)
         => cityDto.MapTo(d => new Grid(cityDto.Zones[0].Slots.Select(x => x.ToDto()).ToArray()));
       
        */
    }
}
