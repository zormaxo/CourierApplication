using RoiaiCourierLibrary.Models;

namespace RoiaiCourierLibrary.Common;

public record CourierResponse(List<Parcel> Parcels, int TotalCost);

