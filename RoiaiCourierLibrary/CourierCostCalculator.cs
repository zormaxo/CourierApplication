using RoiaiCourierLibrary.Common;
using RoiaiCourierLibrary.Models;

namespace RoiaiCourierLibrary;

public class CourierCostCalculator : ICourierCostCalculator
{
    public CourierResponse CalculateTotalCost(List<Parcel> parcels, bool speedyShipping = false)
    {
        var totalCost = 0;
        var speedyCost = 0;
        foreach(var item in parcels)
        {
            var cost = item.ParcelType switch
            {
                ParcelType.Small => 3,
                ParcelType.Medium => 8,
                ParcelType.Large => 15,
                ParcelType.XLarge => 25,
                _ => 25,
            };

            totalCost += cost;
        }

        if(speedyShipping)
        {
            speedyCost = totalCost;
            totalCost += speedyCost;
        }
        return new CourierResponse(parcels, totalCost, speedyCost);
    }
}
