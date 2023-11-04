using System.ComponentModel.DataAnnotations;

namespace MyWatchListWebApp.Models
{
    public class Watch
    {
        [Key]
        public required string ReferenceNumber { get; set; }

        public string Brand { get; set; }
        public string? Model { get; set; }
        public string? Movement { get; set; }
        public string? CaseMaterial { get; set; }
        public string? BandMaterial { get; set; }
        public string? DialColor { get; set; }
        public string? BraceletColor { get; set; }
        public string? ImagePath { get; set; }

        public double? PowerReserve { get; set; }
        public double? CaseDiameter { get; set; }
        public double? LugToLugWidth { get; set; }
        public double? Thickness { get; set; }

        public Watch()
        {
            ReferenceNumber = string.Empty;
        }

        public Watch(string refNum, string brand, string? model, string? movement, string? caseMaterial, string? bandMaterial, string? dialColor, string? braceletColor, string? imagePath, double? powerReserve, double? caseDiameter, double? lugToLugWidth, double? thickness)
        {
            /*
            this.ReferenceNumber = refNum;
            this.Brand = brand;
            this.Model = model;
            this.Movement = movement;
            this.CaseMaterial = caseMaterial;
            this.BandMaterial = bandMaterial;
            this.DialColor = dialColor;
            this.BraceletColor = braceletColor;
            this.ImagePath = imagePath;
            this.PowerReserve = powerReserve;
            this.CaseDiameter = caseDiameter;
            this.LugToLugWidth = lugToLugWidth;
            this.Thickness = thickness;
            */
        }
    }
}
