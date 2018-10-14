﻿

namespace Akhmerov_HomeWork_1.Objects.Active
{
    using System;
    using System.Drawing;
    using GameForm;

    class Battery : BaseObject
    {
        public const string BatteryImagePath = @"res\Ship\Battery.png";
        public static Size BatteryImageSize = Image.FromFile(BatteryImagePath).Size;

        public Battery(Point pos, Point dir, Size size, string imageName) : base(pos, dir, size, imageName)
        {
        }

        public override void Update()
        {
            Pos.X -= Dir.X;

            if (Pos.X < 0 - Size.Width)
            {
                DeleteFlag = true;
            }
        }
    }
}
