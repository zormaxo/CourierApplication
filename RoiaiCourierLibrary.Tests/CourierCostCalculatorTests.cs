using RoiaiCourierLibrary.Models;

namespace RoiaiCourierLibrary.Tests;

[TestFixture]
public class CourierCostCalculatorTests
{
    [TestCase(5, 5, 5, 3)]
    [TestCase(5, 5, 30, 8)]
    [TestCase(5, 5, 60, 15)]
    [TestCase(5, 5, 100, 25)]
    [TestCase(70, 70, 70, 15)]
    [TestCase(120, 120, 120, 25)]
    [TestCase(100, 100, 100, 25)]
    public void CalculateTotalCost_ReturnsExpectedCost(int height, int width, int length, int expectedCost)
    {
        var calculator = new CourierCostCalculator();
        var parcel = Parcel.Create(height, width, length);
        var parcels = new List<Parcel> { parcel };

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(expectedCost));
    }

    [Test]
    public void CalculateTotalCost_EmptyList_ReturnsZeroCost()
    {
        var calculator = new CourierCostCalculator();
        var parcels = new List<Parcel>();

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(0));
    }

    [Test]
    public void CalculateTotalCost_TwoMediumParcels_ReturnsTotalMediumParcelsCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(30, 30, 30);
        var parcel2 = Parcel.Create(40, 40, 40);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(16));
    }

    [Test]
    public void CalculateTotalCost_OneLargeAndOneXLargeParcels_ReturnsTotalLargeAndXLargeParcelsCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(70, 70, 70);
        var parcel2 = Parcel.Create(120, 120, 120);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(40));
    }
}
