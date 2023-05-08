using RoiaiCourierLibrary.Common;
using RoiaiCourierLibrary.Models;

namespace RoiaiCourierLibrary;

public interface ICourierCostCalculator
{
    CourierResponse CalculateTotalCost(List<Parcel> parcels);
}
