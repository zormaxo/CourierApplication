using RoiaiCourierLibrary.Common;

namespace RoiaiCourierLibrary.Models;

public class Parcel
{
    private Parcel(int height, int width, int length)
    {
        Height = height;
        Width = width;
        Length = length;
    }

    public int Height { get; }

    public int Width { get; }

    public int Length { get; }

    public ParcelType ParcelType => SetParcelType();

    public int Cost { get; set; }

    public static Parcel Create(int height, int width, int length)
    {
        return new Parcel(height, width, length);
    }

    private ParcelType SetParcelType()
    {
        if(Height < 10 && Length < 10 && Width < 10)
        {
            return ParcelType.Small;
        }
        else if(Height < 50 && Length < 50 && Width < 50)
        {
            return ParcelType.Medium;
        }
        else if(Height < 100 && Length < 100 && Width < 100)
        {
            return ParcelType.Large;
        }
        else
        {
            return ParcelType.XLarge;
        }
    }
}
