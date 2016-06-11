﻿namespace MegaMine.Modules.Quarry.Models
{
    public class ProductSummaryModel
    {
        public string Id { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int QuarryId { get; set; }
        public string QuarryName { get; set; }
        public int MaterialColourId { get; set; }
        public string ColourName { get; set; }
        public int MaterialCount { get; set; }
    }
}
