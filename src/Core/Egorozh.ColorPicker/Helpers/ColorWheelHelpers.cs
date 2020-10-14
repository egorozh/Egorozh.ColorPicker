using System;
using System.Collections.Generic;

namespace Egorozh.ColorPicker
{
    public static class ColorWheelHelpers
    {
        public static List<byte> GetHsvData(double width, double height)
        {
            var data = new List<byte>();

            var pixelCount = (int)(Math.Round(width) * Math.Round(height));

            var pixelDataSize = pixelCount * 4;
            data.Capacity = pixelDataSize;

            for (var y = (int)Math.Round(height) - 1; y >= 0; --y)
            for (var x = 0; x < Math.Round(width); ++x)
                FillPixelForRing(x, y, Math.Round(width) / 2.0, data);

            return data;
        }

        private static void FillPixelForRing(int x, int y,
            double radius,
            List<byte> bgraMaxPixelData)
        {
            var distanceFromRadius = Math.Sqrt(Math.Pow(x - radius, 2) + Math.Pow(y - radius, 2));

            double xToUse = x;
            double yToUse = y;

            if (distanceFromRadius > radius)
            {
                xToUse = (radius / distanceFromRadius * (x - radius)) + radius;
                yToUse = (radius / distanceFromRadius * (y - radius)) + radius;
                distanceFromRadius = radius;
            }

            var r = 1 - (distanceFromRadius / radius);

            var theta = Math.Atan2(radius - yToUse, radius - xToUse) * 180.0 / Math.PI;
            theta += 180.0;
            theta = Math.Floor(theta);

            while (theta > 360)
                theta -= 360;

            var thetaPercent = theta / 360;

            var hsvMax = new HsvColor(thetaPercent * 359, 1 - r, 1);

            var rgbMax = hsvMax.ToRgbColor();

            bgraMaxPixelData.Add(rgbMax.B); // b
            bgraMaxPixelData.Add(rgbMax.G); // g
            bgraMaxPixelData.Add(rgbMax.R); // r
            bgraMaxPixelData.Add(255);
        }

    }
}
