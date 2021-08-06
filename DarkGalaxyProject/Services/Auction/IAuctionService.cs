﻿using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.Auction
{
    public interface IAuctionService
    {
        public IEnumerable<DealServiceModel> All();

        public IEnumerable<Ship> ShipsForSale(string playerId, string shipType, int shipCount);//Shouldn't this be ShipServiceModel?!?

        public string CreateDeal(int price, string sellerId, int quantity, string shipType);

        public bool DeleteDeal(string dealId); 

        public bool Buy(string dealId, string buyerId); 
    }
}