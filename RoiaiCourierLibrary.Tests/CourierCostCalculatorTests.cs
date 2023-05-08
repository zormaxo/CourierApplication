using RoiaiCourierLibrary.Models;

namespace RoiaiCourierLibrary.Tests;

[TestFixture]
public class CourierCostCalculatorTests
{
    [TestCase(5, 5, 5, 1, false, 3)]
    [TestCase(5, 5, 30, 3, false, 8)]
    [TestCase(5, 5, 60, 6, false, 15)]
    [TestCase(5, 5, 100, 10, false, 25)]
    [TestCase(70, 70, 70, 6, false, 15)]
    [TestCase(120, 120, 10, 120, false, 25)]
    [TestCase(100, 100, 10, 100, false, 25)]
    [TestCase(5, 5, 5, 1, true, 6)]
    [TestCase(5, 5, 30, 3, true, 16)]
    [TestCase(5, 5, 60, 6, true, 30)]
    [TestCase(5, 5, 100, 10, true, 50)]
    [TestCase(70, 70, 70, 6, true, 30)]
    [TestCase(120, 120, 120, 10, true, 50)]
    [TestCase(100, 100, 100, 10, true, 50)]
    public void CalculateTotalCost_WithinWeightLimit_ReturnsExpectedCostAndParcels(
        int height,
        int width,
        int length,
        int weight,
        bool speedyShipping,
        int expectedCost)
    {
        var calculator = new CourierCostCalculator();
        var parcel = Parcel.Create(height, width, length, weight);
        var parcels = new List<Parcel> { parcel };

        var response = calculator.CalculateTotalCost(parcels, speedyShipping);

        Assert.That(response.Parcels, Is.EqualTo(parcels));
        Assert.That(response.TotalCost, Is.EqualTo(expectedCost));
        if(speedyShipping)
        {
            Assert.That(response.SpeedyCost, Is.EqualTo(expectedCost / 2));
        }
        else
        {
            Assert.That(response.SpeedyCost, Is.EqualTo(0));
        }
    }

    public void CalculateTotalCost_ReturnsParcelsAndTotalCostAndSpeedyCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel = Parcel.Create(70, 70, 70, 3);
        var parcels = new List<Parcel> { parcel };

        var response = calculator.CalculateTotalCost(parcels, true);
        Assert.Multiple(
            () =>
            {
                Assert.That(response.Parcels, Is.EqualTo(parcels));
                Assert.That(response.TotalCost, Is.EqualTo(30));
                Assert.That(response.SpeedyCost, Is.EqualTo(15 / 2));
            });
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
    public void CalculateTotalCost_WithSpeedyShipping_ReturnsSpeedyCostEqualToHalfTotalCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(70, 70, 70, 5);
        var parcel2 = Parcel.Create(120, 120, 120, 10);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels, speedyShipping: true);

        var expectedSpeedyCost = response.TotalCost / 2;
        Assert.That(response.SpeedyCost, Is.EqualTo(expectedSpeedyCost));
    }

    [Test]
    public void CalculateTotalCost_TwoMediumParcels_ReturnsTotalMediumParcelsCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(30, 30, 30, 3);
        var parcel2 = Parcel.Create(40, 40, 40, 3);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(16));
    }

    [Test]
    public void CalculateTotalCost_TwoMediumParcels_WithSpeedyShipping_ReturnsTotalMediumParcelsCostPlusSpeedyCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(30, 30, 30, 3);
        var parcel2 = Parcel.Create(40, 40, 40, 3);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels, true);

        Assert.That(response.TotalCost, Is.EqualTo(32));
        Assert.That(response.SpeedyCost, Is.EqualTo(16));
    }

    [Test]
    public void CalculateTotalCost_OneLargeAndOneXLargeParcels_ReturnsTotalLargeAndXLargeParcelsCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(70, 70, 70, 6);
        var parcel2 = Parcel.Create(120, 120, 120, 10);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels);

        Assert.That(response.TotalCost, Is.EqualTo(40));
    }

    [Test]
    public void CalculateTotalCost_OneLargeAndOneXLargeParcels_WithSpeedyShipping_ReturnsTotalLargeAndXLargeParcelsCostPlusSpeedyCost()
    {
        var calculator = new CourierCostCalculator();
        var parcel1 = Parcel.Create(70, 70, 70, 6);
        var parcel2 = Parcel.Create(120, 120, 120, 10);
        var parcels = new List<Parcel> { parcel1, parcel2 };

        var response = calculator.CalculateTotalCost(parcels, speedyShipping: true);

        Assert.That(response.TotalCost, Is.EqualTo(80));
        Assert.That(response.SpeedyCost, Is.EqualTo(40));
    }
}
