﻿namespace Shengyi_WebAPI.Models.In
{
    public class SearchInventoryInfo
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }

        public string Length { get; set; }
        public string Width{ get; set; }
        public string Height { get; set; }
    }
}
