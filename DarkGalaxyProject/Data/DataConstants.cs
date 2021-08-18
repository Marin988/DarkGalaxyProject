using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data
{
    public class DataConstants
    {
        public class ChatMessage
        {
            public const int MinLength = 1;
            public const int MaxLength = 40;
        }

        public class Message
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 36;
            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 240;
        }

        public class Builder
        {
            public const int MinCount = 1;
            public const int MaxCount = 1000;
        }

        public class Planet
        {
            public const int TerraformPriceConst = 120000;
            public const int BuildingCapConst = 15000;
        }

        public class Alliance
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
            public const int DescriptionMinLength = 6;
            public const int DescriptionMaxLength = 120;
        }

        public class AuctionDeal
        {
            public const int MinQuantity = 1;
            public const int MaxQuantity = 1000;
            public const int MinPrice = 100;
            public const int MaxPrice = 10000000;
        }

        public class System
        {
            public const int StartingFuelQuantity = 2000;
            public const int StartingMilkyCoinQuantity = 5000;
            public const int StartingPaperQuantity = 0;
            public const int StartingEnergyQuantity = 300;

            public const int MinPosition = 1;
            public const int MaxPosition = 100;
        }
    }
}
