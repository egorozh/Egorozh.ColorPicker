﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    public static class ColorPalettes
    {
        #region Static Properties

        public static ColorCollection HexagonPalette
        {
            get
            {
                return new ColorCollection(new[]
                {
                    Color.FromRgb(0, 48, 96),
                    Color.FromRgb(47, 96, 144),
                    Color.FromRgb(47, 96, 192),
                    Color.FromRgb(0, 48, 144),
                    Color.FromRgb(0, 0, 144),
                    Color.FromRgb(0, 0, 207),
                    Color.FromRgb(0, 0, 96),
                    Color.FromRgb(0, 96, 96),
                    Color.FromRgb(0, 96, 144),
                    Color.FromRgb(0, 151, 192),
                    Color.FromRgb(0, 103, 207),
                    Color.FromRgb(0, 48, 207),
                    Color.FromRgb(0, 0, 255),
                    Color.FromRgb(48, 48, 255),
                    Color.FromRgb(48, 48, 144),
                    Color.FromRgb(96, 152, 144),
                    Color.FromRgb(0, 152, 159),
                    Color.FromRgb(48, 200, 207),
                    Color.FromRgb(0, 200, 255),
                    Color.FromRgb(0, 152, 255),
                    Color.FromRgb(0, 103, 255),
                    Color.FromRgb(48, 103, 255),
                    Color.FromRgb(48, 48, 192),
                    Color.FromRgb(96, 103, 144),
                    Color.FromRgb(48, 152, 96),
                    Color.FromRgb(0, 200, 144),
                    Color.FromRgb(0, 200, 192),
                    Color.FromRgb(0, 255, 255),
                    Color.FromRgb(48, 200, 255),
                    Color.FromRgb(48, 151, 255),
                    Color.FromRgb(96, 151, 255),
                    Color.FromRgb(96, 96, 255),
                    Color.FromRgb(95, 0, 255),
                    Color.FromRgb(96, 0, 192),
                    Color.FromRgb(48, 151, 48),
                    Color.FromRgb(0, 200, 96),
                    Color.FromRgb(0, 255, 144),
                    Color.FromRgb(96, 255, 207),
                    Color.FromRgb(96, 255, 255),
                    Color.FromRgb(96, 200, 255),
                    Color.FromRgb(144, 200, 255),
                    Color.FromRgb(144, 151, 255),
                    Color.FromRgb(144, 103, 255),
                    Color.FromRgb(144, 48, 255),
                    Color.FromRgb(144, 0, 255),
                    Color.FromRgb(0, 96, 0),
                    Color.FromRgb(0, 200, 0),
                    Color.FromRgb(0, 255, 0),
                    Color.FromRgb(96, 255, 144),
                    Color.FromRgb(144, 255, 192),
                    Color.FromRgb(207, 255, 255),
                    Color.FromRgb(192, 200, 255),
                    Color.FromRgb(192, 151, 255),
                    Color.FromRgb(192, 96, 255),
                    Color.FromRgb(192, 48, 255),
                    Color.FromRgb(192, 0, 255),
                    Color.FromRgb(144, 0, 192),
                    Color.FromRgb(0, 48, 0),
                    Color.FromRgb(0, 152, 48),
                    Color.FromRgb(47, 200, 47),
                    Color.FromRgb(96, 255, 96),
                    Color.FromRgb(144, 255, 144),
                    Color.FromRgb(207, 255, 207),
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(255, 200, 255),
                    Color.FromRgb(255, 151, 255),
                    Color.FromRgb(255, 104, 255),
                    Color.FromRgb(255, 0, 255),
                    Color.FromRgb(207, 0, 207),
                    Color.FromRgb(96, 0, 96),
                    Color.FromRgb(47, 96, 0),
                    Color.FromRgb(0, 152, 0),
                    Color.FromRgb(96, 255, 48),
                    Color.FromRgb(144, 255, 96),
                    Color.FromRgb(192, 255, 144),
                    Color.FromRgb(255, 255, 207),
                    Color.FromRgb(255, 200, 207),
                    Color.FromRgb(255, 151, 207),
                    Color.FromRgb(255, 96, 192),
                    Color.FromRgb(255, 48, 207),
                    Color.FromRgb(207, 0, 144),
                    Color.FromRgb(144, 47, 144),
                    Color.FromRgb(48, 48, 0),
                    Color.FromRgb(96, 151, 0),
                    Color.FromRgb(144, 255, 47),
                    Color.FromRgb(207, 255, 96),
                    Color.FromRgb(255, 255, 144),
                    Color.FromRgb(255, 200, 144),
                    Color.FromRgb(255, 152, 96),
                    Color.FromRgb(255, 96, 144),
                    Color.FromRgb(255, 48, 144),
                    Color.FromRgb(207, 48, 144),
                    Color.FromRgb(144, 0, 144),
                    Color.FromRgb(96, 96, 47),
                    Color.FromRgb(144, 200, 0),
                    Color.FromRgb(192, 255, 57),
                    Color.FromRgb(255, 255, 96),
                    Color.FromRgb(255, 200, 96),
                    Color.FromRgb(255, 152, 96),
                    Color.FromRgb(255, 96, 96),
                    Color.FromRgb(255, 0, 96),
                    Color.FromRgb(207, 103, 144),
                    Color.FromRgb(144, 48, 96),
                    Color.FromRgb(144, 151, 96),
                    Color.FromRgb(192, 200, 0),
                    Color.FromRgb(255, 255, 0),
                    Color.FromRgb(255, 200, 0),
                    Color.FromRgb(255, 151, 47),
                    Color.FromRgb(255, 103, 0),
                    Color.FromRgb(255, 96, 96),
                    Color.FromRgb(192, 0, 95),
                    Color.FromRgb(96, 0, 47),
                    Color.FromRgb(159, 103, 48),
                    Color.FromRgb(207, 151, 0),
                    Color.FromRgb(255, 151, 0),
                    Color.FromRgb(192, 96, 0),
                    Color.FromRgb(255, 48, 0),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(192, 0, 0),
                    Color.FromRgb(144, 0, 47),
                    Color.FromRgb(96, 48, 0),
                    Color.FromRgb(144, 96, 0),
                    Color.FromRgb(192, 48, 0),
                    Color.FromRgb(144, 48, 0),
                    Color.FromRgb(144, 0, 0),
                    Color.FromRgb(127, 0, 0),
                    Color.FromRgb(144, 48, 48),
                    Colors.White,
                    Colors.Black,
                    Color.FromRgb(207, 200, 207),
                    Color.FromRgb(144, 151, 144),
                    Color.FromRgb(96, 103, 96),
                    Color.FromRgb(192, 192, 192),
                    Color.FromRgb(127, 127, 127),
                    Color.FromRgb(48, 48, 48)
                });
            }
        }

        public static ColorCollection NamedColors
        {
            get
            {
                var results = new List<Color>();

                foreach (PropertyInfo property in typeof(Colors)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(property => property.PropertyType == typeof(Color)))
                {
                    Color color;

                    color = (Color) property.GetValue(typeof(Color), null);
                    
                    results.Add(color);
                }

                results.Sort(ColorComparer.Brightness);

                return new ColorCollection(results);
            }
        }

        public static ColorCollection Office2010Standard
        {
            get
            {
                return ScaledPalette(new[]
                {
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(238, 236, 255),
                    Color.FromRgb(31, 73, 125),
                    Color.FromRgb(79, 129, 189),
                    Color.FromRgb(192, 80, 77),
                    Color.FromRgb(155, 187, 89),
                    Color.FromRgb(128, 100, 162),
                    Color.FromRgb(75, 172, 198),
                    Color.FromRgb(247, 150, 70)
                });
            }
        }

        public static ColorCollection PaintPalette
        {
            get
            {
                return new ColorCollection(new[]
                {
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(64, 64, 64),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(255, 106, 0),
                    Color.FromRgb(255, 216, 0),
                    Color.FromRgb(182, 255, 0),
                    Color.FromRgb(76, 255, 0),
                    Color.FromRgb(0, 255, 33),
                    Color.FromRgb(0, 255, 144),
                    Color.FromRgb(0, 255, 255),
                    Color.FromRgb(0, 148, 255),
                    Color.FromRgb(0, 38, 255),
                    Color.FromRgb(72, 0, 255),
                    Color.FromRgb(178, 0, 255),
                    Color.FromRgb(255, 0, 220),
                    Color.FromRgb(255, 0, 110),
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(128, 128, 128),
                    Color.FromRgb(127, 0, 0),
                    Color.FromRgb(127, 51, 0),
                    Color.FromRgb(127, 106, 0),
                    Color.FromRgb(91, 127, 0),
                    Color.FromRgb(38, 127, 0),
                    Color.FromRgb(0, 127, 14),
                    Color.FromRgb(0, 127, 70),
                    Color.FromRgb(0, 127, 127),
                    Color.FromRgb(0, 74, 127),
                    Color.FromRgb(0, 19, 127),
                    Color.FromRgb(33, 0, 127),
                    Color.FromRgb(87, 0, 127),
                    Color.FromRgb(127, 0, 110),
                    Color.FromRgb(127, 0, 55),
                    Color.FromRgb(160, 160, 160),
                    Color.FromRgb(48, 48, 48),
                    Color.FromRgb(255, 127, 127),
                    Color.FromRgb(255, 178, 127),
                    Color.FromRgb(255, 233, 127),
                    Color.FromRgb(218, 255, 127),
                    Color.FromRgb(165, 255, 127),
                    Color.FromRgb(127, 255, 142),
                    Color.FromRgb(127, 255, 197),
                    Color.FromRgb(127, 255, 255),
                    Color.FromRgb(127, 201, 255),
                    Color.FromRgb(127, 146, 255),
                    Color.FromRgb(161, 127, 255),
                    Color.FromRgb(214, 127, 255),
                    Color.FromRgb(255, 127, 237),
                    Color.FromRgb(255, 127, 182),
                    Color.FromRgb(192, 192, 192),
                    Color.FromRgb(96, 96, 96),
                    Color.FromRgb(127, 63, 63),
                    Color.FromRgb(127, 89, 63),
                    Color.FromRgb(127, 116, 63),
                    Color.FromRgb(109, 127, 63),
                    Color.FromRgb(82, 127, 63),
                    Color.FromRgb(63, 127, 71),
                    Color.FromRgb(63, 127, 98),
                    Color.FromRgb(63, 127, 127),
                    Color.FromRgb(63, 100, 127),
                    Color.FromRgb(63, 73, 127),
                    Color.FromRgb(80, 63, 127),
                    Color.FromRgb(107, 63, 127),
                    Color.FromRgb(127, 63, 118),
                    Color.FromRgb(127, 63, 91),
                    Color.FromArgb(128, 0, 0, 0),
                    Color.FromArgb(128, 64, 64, 64),
                    Color.FromArgb(128, 255, 0, 0),
                    Color.FromArgb(128, 255, 106, 0),
                    Color.FromArgb(128, 255, 216, 0),
                    Color.FromArgb(128, 182, 255, 0),
                    Color.FromArgb(128, 76, 255, 0),
                    Color.FromArgb(128, 0, 255, 33),
                    Color.FromArgb(128, 0, 255, 144),
                    Color.FromArgb(128, 0, 255, 255),
                    Color.FromArgb(128, 0, 148, 255),
                    Color.FromArgb(128, 0, 38, 255),
                    Color.FromArgb(128, 72, 0, 255),
                    Color.FromArgb(128, 178, 0, 255),
                    Color.FromArgb(128, 255, 0, 220),
                    Color.FromArgb(128, 255, 0, 110),
                    Color.FromArgb(128, 255, 255, 255),
                    Color.FromArgb(128, 128, 128, 128),
                    Color.FromArgb(128, 127, 0, 0),
                    Color.FromArgb(128, 127, 51, 0),
                    Color.FromArgb(128, 127, 106, 0),
                    Color.FromArgb(128, 91, 127, 0),
                    Color.FromArgb(128, 38, 127, 0),
                    Color.FromArgb(128, 0, 127, 14),
                    Color.FromArgb(128, 0, 127, 70),
                    Color.FromArgb(128, 0, 127, 127),
                    Color.FromArgb(128, 0, 74, 127),
                    Color.FromArgb(128, 0, 19, 127),
                    Color.FromArgb(128, 33, 0, 127),
                    Color.FromArgb(128, 87, 0, 127),
                    Color.FromArgb(128, 127, 0, 110),
                    Color.FromArgb(128, 127, 0, 55)
                });
            }
        }

        public static ColorCollection QbColors
        {
            get
            {
                return new ColorCollection(new[]
                {
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(128, 0, 0),
                    Color.FromRgb(0, 128, 0),
                    Color.FromRgb(128, 128, 0),
                    Color.FromRgb(0, 0, 128),
                    Color.FromRgb(128, 0, 128),
                    Color.FromRgb(0, 128, 128),
                    Color.FromRgb(192, 192, 192),
                    Color.FromRgb(128, 128, 128),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(0, 255, 0),
                    Color.FromRgb(255, 255, 0),
                    Color.FromRgb(0, 0, 255),
                    Color.FromRgb(255, 0, 255),
                    Color.FromRgb(0, 255, 255),
                    Color.FromRgb(255, 255, 255)
                });
            }
        }

        public static ColorCollection StandardPalette
        {
            get
            {
                return new ColorCollection(new[]
                {
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(128, 0, 0),
                    Color.FromRgb(0, 128, 0),
                    Color.FromRgb(128, 128, 0),
                    Color.FromRgb(0, 0, 128),
                    Color.FromRgb(128, 0, 128),
                    Color.FromRgb(0, 128, 128),
                    Color.FromRgb(192, 192, 192),
                    Color.FromRgb(128, 128, 128),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(0, 255, 0),
                    Color.FromRgb(255, 255, 0),
                    Color.FromRgb(0, 0, 255),
                    Color.FromRgb(255, 0, 255),
                    Color.FromRgb(0, 255, 255),
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(25, 25, 25),
                    Color.FromRgb(51, 51, 51),
                    Color.FromRgb(76, 76, 76),
                    Color.FromRgb(90, 90, 90),
                    Color.FromRgb(102, 102, 102),
                    Color.FromRgb(115, 115, 115),
                    Color.FromRgb(128, 128, 128),
                    Color.FromRgb(141, 141, 141),
                    Color.FromRgb(153, 153, 153),
                    Color.FromRgb(166, 166, 166),
                    Color.FromRgb(178, 178, 178),
                    Color.FromRgb(192, 192, 192),
                    Color.FromRgb(204, 204, 204),
                    Color.FromRgb(218, 218, 218),
                    Color.FromRgb(230, 230, 230),
                    Color.FromRgb(243, 243, 243),
                    Color.FromRgb(63, 0, 0),
                    Color.FromRgb(92, 0, 0),
                    Color.FromRgb(120, 0, 0),
                    Color.FromRgb(148, 0, 0),
                    Color.FromRgb(177, 0, 0),
                    Color.FromRgb(205, 0, 0),
                    Color.FromRgb(233, 0, 0),
                    Color.FromRgb(254, 7, 7),
                    Color.FromRgb(255, 35, 35),
                    Color.FromRgb(255, 63, 63),
                    Color.FromRgb(255, 92, 92),
                    Color.FromRgb(255, 120, 120),
                    Color.FromRgb(254, 148, 148),
                    Color.FromRgb(255, 177, 177),
                    Color.FromRgb(254, 205, 205),
                    Color.FromRgb(255, 233, 233),
                    Color.FromRgb(63, 23, 0),
                    Color.FromRgb(92, 34, 0),
                    Color.FromRgb(120, 45, 0),
                    Color.FromRgb(148, 55, 0),
                    Color.FromRgb(177, 66, 0),
                    Color.FromRgb(205, 77, 0),
                    Color.FromRgb(233, 87, 0),
                    Color.FromRgb(254, 100, 7),
                    Color.FromRgb(255, 117, 35),
                    Color.FromRgb(255, 135, 63),
                    Color.FromRgb(255, 153, 92),
                    Color.FromRgb(255, 170, 120),
                    Color.FromRgb(254, 188, 148),
                    Color.FromRgb(255, 206, 177),
                    Color.FromRgb(254, 224, 205),
                    Color.FromRgb(255, 241, 233),
                    Color.FromRgb(63, 47, 0),
                    Color.FromRgb(92, 69, 0),
                    Color.FromRgb(120, 90, 0),
                    Color.FromRgb(148, 111, 0),
                    Color.FromRgb(177, 132, 0),
                    Color.FromRgb(205, 154, 0),
                    Color.FromRgb(233, 175, 0),
                    Color.FromRgb(254, 193, 7),
                    Color.FromRgb(255, 200, 35),
                    Color.FromRgb(255, 207, 63),
                    Color.FromRgb(255, 214, 92),
                    Color.FromRgb(255, 221, 120),
                    Color.FromRgb(254, 228, 148),
                    Color.FromRgb(255, 235, 177),
                    Color.FromRgb(254, 242, 205),
                    Color.FromRgb(255, 249, 233),
                    Color.FromRgb(55, 63, 0),
                    Color.FromRgb(80, 92, 0),
                    Color.FromRgb(105, 120, 0),
                    Color.FromRgb(130, 148, 0),
                    Color.FromRgb(154, 177, 0),
                    Color.FromRgb(179, 205, 0),
                    Color.FromRgb(204, 233, 0),
                    Color.FromRgb(224, 254, 7),
                    Color.FromRgb(227, 255, 35),
                    Color.FromRgb(231, 255, 63),
                    Color.FromRgb(234, 255, 92),
                    Color.FromRgb(238, 255, 120),
                    Color.FromRgb(241, 254, 148),
                    Color.FromRgb(245, 255, 177),
                    Color.FromRgb(248, 254, 205),
                    Color.FromRgb(252, 255, 233),
                    Color.FromRgb(31, 63, 0),
                    Color.FromRgb(46, 92, 0),
                    Color.FromRgb(60, 120, 0),
                    Color.FromRgb(74, 148, 0),
                    Color.FromRgb(88, 177, 0),
                    Color.FromRgb(102, 205, 0),
                    Color.FromRgb(116, 233, 0),
                    Color.FromRgb(131, 254, 7),
                    Color.FromRgb(145, 255, 35),
                    Color.FromRgb(159, 255, 63),
                    Color.FromRgb(173, 255, 92),
                    Color.FromRgb(187, 255, 120),
                    Color.FromRgb(201, 254, 148),
                    Color.FromRgb(216, 255, 177),
                    Color.FromRgb(230, 254, 205),
                    Color.FromRgb(244, 255, 233),
                    Color.FromRgb(7, 63, 0),
                    Color.FromRgb(11, 92, 0),
                    Color.FromRgb(15, 120, 0),
                    Color.FromRgb(18, 148, 0),
                    Color.FromRgb(22, 177, 0),
                    Color.FromRgb(25, 205, 0),
                    Color.FromRgb(29, 233, 0),
                    Color.FromRgb(38, 254, 7),
                    Color.FromRgb(62, 255, 35),
                    Color.FromRgb(87, 255, 63),
                    Color.FromRgb(112, 255, 92),
                    Color.FromRgb(137, 255, 120),
                    Color.FromRgb(162, 254, 148),
                    Color.FromRgb(186, 255, 177),
                    Color.FromRgb(211, 254, 205),
                    Color.FromRgb(236, 255, 233),
                    Color.FromRgb(0, 63, 15),
                    Color.FromRgb(0, 92, 23),
                    Color.FromRgb(0, 120, 30),
                    Color.FromRgb(0, 148, 37),
                    Color.FromRgb(0, 177, 44),
                    Color.FromRgb(0, 205, 51),
                    Color.FromRgb(0, 233, 58),
                    Color.FromRgb(7, 254, 69),
                    Color.FromRgb(35, 255, 90),
                    Color.FromRgb(63, 255, 111),
                    Color.FromRgb(92, 255, 132),
                    Color.FromRgb(120, 255, 154),
                    Color.FromRgb(148, 254, 175),
                    Color.FromRgb(177, 255, 196),
                    Color.FromRgb(205, 254, 217),
                    Color.FromRgb(233, 255, 239),
                    Color.FromRgb(0, 63, 39),
                    Color.FromRgb(0, 92, 57),
                    Color.FromRgb(0, 120, 75),
                    Color.FromRgb(0, 148, 92),
                    Color.FromRgb(0, 177, 110),
                    Color.FromRgb(0, 205, 128),
                    Color.FromRgb(0, 233, 146),
                    Color.FromRgb(7, 254, 162),
                    Color.FromRgb(35, 255, 172),
                    Color.FromRgb(63, 255, 183),
                    Color.FromRgb(92, 255, 193),
                    Color.FromRgb(120, 255, 204),
                    Color.FromRgb(148, 254, 215),
                    Color.FromRgb(177, 255, 225),
                    Color.FromRgb(205, 254, 236),
                    Color.FromRgb(233, 255, 247),
                    Color.FromRgb(0, 63, 63),
                    Color.FromRgb(0, 92, 92),
                    Color.FromRgb(0, 120, 120),
                    Color.FromRgb(0, 148, 148),
                    Color.FromRgb(0, 177, 177),
                    Color.FromRgb(0, 205, 205),
                    Color.FromRgb(0, 233, 233),
                    Color.FromRgb(7, 254, 254),
                    Color.FromRgb(35, 255, 255),
                    Color.FromRgb(63, 255, 255),
                    Color.FromRgb(92, 255, 255),
                    Color.FromRgb(120, 255, 255),
                    Color.FromRgb(148, 254, 254),
                    Color.FromRgb(177, 255, 255),
                    Color.FromRgb(205, 254, 254),
                    Color.FromRgb(233, 255, 255),
                    Color.FromRgb(0, 39, 63),
                    Color.FromRgb(0, 57, 92),
                    Color.FromRgb(0, 75, 120),
                    Color.FromRgb(0, 92, 148),
                    Color.FromRgb(0, 110, 177),
                    Color.FromRgb(0, 128, 205),
                    Color.FromRgb(0, 146, 233),
                    Color.FromRgb(7, 162, 254),
                    Color.FromRgb(35, 172, 255),
                    Color.FromRgb(63, 183, 255),
                    Color.FromRgb(92, 193, 255),
                    Color.FromRgb(120, 204, 255),
                    Color.FromRgb(148, 215, 254),
                    Color.FromRgb(177, 225, 255),
                    Color.FromRgb(205, 236, 254),
                    Color.FromRgb(233, 247, 255),
                    Color.FromRgb(0, 15, 63),
                    Color.FromRgb(0, 23, 92),
                    Color.FromRgb(0, 30, 120),
                    Color.FromRgb(0, 37, 148),
                    Color.FromRgb(0, 44, 177),
                    Color.FromRgb(0, 51, 205),
                    Color.FromRgb(0, 58, 233),
                    Color.FromRgb(7, 69, 254),
                    Color.FromRgb(35, 90, 255),
                    Color.FromRgb(63, 111, 255),
                    Color.FromRgb(92, 132, 255),
                    Color.FromRgb(120, 154, 255),
                    Color.FromRgb(148, 175, 254),
                    Color.FromRgb(177, 196, 255),
                    Color.FromRgb(205, 217, 254),
                    Color.FromRgb(233, 239, 255),
                    Color.FromRgb(7, 0, 63),
                    Color.FromRgb(11, 0, 92),
                    Color.FromRgb(15, 0, 120),
                    Color.FromRgb(18, 0, 148),
                    Color.FromRgb(22, 0, 177),
                    Color.FromRgb(25, 0, 205),
                    Color.FromRgb(29, 0, 233),
                    Color.FromRgb(38, 7, 254),
                    Color.FromRgb(62, 35, 255),
                    Color.FromRgb(87, 63, 255),
                    Color.FromRgb(112, 92, 255),
                    Color.FromRgb(137, 120, 255),
                    Color.FromRgb(162, 148, 254),
                    Color.FromRgb(186, 177, 255),
                    Color.FromRgb(211, 205, 254),
                    Color.FromRgb(236, 233, 255),
                    Color.FromRgb(31, 0, 63),
                    Color.FromRgb(46, 0, 92),
                    Color.FromRgb(60, 0, 120),
                    Color.FromRgb(74, 0, 148),
                    Color.FromRgb(88, 0, 177),
                    Color.FromRgb(102, 0, 205),
                    Color.FromRgb(116, 0, 233),
                    Color.FromRgb(131, 7, 254),
                    Color.FromRgb(145, 35, 255),
                    Color.FromRgb(159, 63, 255),
                    Color.FromRgb(173, 92, 255),
                    Color.FromRgb(187, 120, 255),
                    Color.FromRgb(201, 148, 254),
                    Color.FromRgb(216, 177, 255),
                    Color.FromRgb(230, 205, 254),
                    Color.FromRgb(244, 233, 255),
                    Color.FromRgb(55, 0, 63),
                    Color.FromRgb(80, 0, 92),
                    Color.FromRgb(105, 0, 120),
                    Color.FromRgb(130, 0, 148),
                    Color.FromRgb(154, 0, 177),
                    Color.FromRgb(179, 0, 205),
                    Color.FromRgb(204, 0, 233),
                    Color.FromRgb(224, 7, 254),
                    Color.FromRgb(227, 35, 255),
                    Color.FromRgb(231, 63, 255),
                    Color.FromRgb(234, 92, 255),
                    Color.FromRgb(238, 120, 255),
                    Color.FromRgb(241, 148, 254),
                    Color.FromRgb(245, 177, 255),
                    Color.FromRgb(248, 205, 254),
                    Color.FromRgb(252, 233, 255)
                });
            }
        }

        public static ColorCollection WebSafe
        {
            get
            {
                return new ColorCollection(new[]
                {
                    Color.FromRgb(255, 0, 255),
                    Color.FromRgb(255, 51, 255),
                    Color.FromRgb(204, 0, 204),
                    Color.FromRgb(255, 102, 255),
                    Color.FromRgb(204, 51, 204),
                    Color.FromRgb(153, 0, 153),
                    Color.FromRgb(255, 153, 255),
                    Color.FromRgb(204, 102, 204),
                    Color.FromRgb(153, 51, 153),
                    Color.FromRgb(102, 0, 102),
                    Color.FromRgb(255, 204, 255),
                    Color.FromRgb(204, 153, 204),
                    Color.FromRgb(153, 102, 153),
                    Color.FromRgb(102, 51, 102),
                    Color.FromRgb(51, 0, 51),
                    Color.FromRgb(204, 0, 255),
                    Color.FromRgb(204, 51, 255),
                    Color.FromRgb(153, 0, 204),
                    Color.FromRgb(204, 102, 255),
                    Color.FromRgb(153, 51, 204),
                    Color.FromRgb(102, 0, 153),
                    Color.FromRgb(204, 153, 255),
                    Color.FromRgb(153, 102, 204),
                    Color.FromRgb(102, 51, 153),
                    Color.FromRgb(51, 0, 102),
                    Color.FromRgb(153, 0, 255),
                    Color.FromRgb(153, 51, 255),
                    Color.FromRgb(102, 0, 204),
                    Color.FromRgb(153, 102, 255),
                    Color.FromRgb(102, 51, 204),
                    Color.FromRgb(51, 0, 153),
                    Color.FromRgb(102, 0, 255),
                    Color.FromRgb(102, 51, 255),
                    Color.FromRgb(51, 0, 204),
                    Color.FromRgb(51, 0, 255),
                    Color.FromRgb(0, 0, 255),
                    Color.FromRgb(51, 51, 255),
                    Color.FromRgb(0, 0, 204),
                    Color.FromRgb(102, 102, 255),
                    Color.FromRgb(51, 51, 204),
                    Color.FromRgb(0, 0, 153),
                    Color.FromRgb(153, 153, 255),
                    Color.FromRgb(102, 102, 204),
                    Color.FromRgb(51, 51, 153),
                    Color.FromRgb(0, 0, 102),
                    Color.FromRgb(204, 204, 255),
                    Color.FromRgb(153, 153, 204),
                    Color.FromRgb(102, 102, 153),
                    Color.FromRgb(51, 51, 102),
                    Color.FromRgb(0, 0, 51),
                    Color.FromRgb(0, 51, 255),
                    Color.FromRgb(51, 102, 255),
                    Color.FromRgb(0, 51, 204),
                    Color.FromRgb(0, 102, 255),
                    Color.FromRgb(102, 153, 255),
                    Color.FromRgb(51, 102, 204),
                    Color.FromRgb(0, 51, 153),
                    Color.FromRgb(51, 153, 255),
                    Color.FromRgb(0, 102, 204),
                    Color.FromRgb(0, 153, 255),
                    Color.FromRgb(153, 204, 255),
                    Color.FromRgb(102, 153, 204),
                    Color.FromRgb(51, 102, 153),
                    Color.FromRgb(0, 51, 102),
                    Color.FromRgb(102, 204, 255),
                    Color.FromRgb(51, 153, 204),
                    Color.FromRgb(0, 102, 153),
                    Color.FromRgb(51, 204, 255),
                    Color.FromRgb(0, 153, 204),
                    Color.FromRgb(0, 204, 255),
                    Color.FromRgb(0, 255, 255),
                    Color.FromRgb(51, 255, 255),
                    Color.FromRgb(0, 204, 204),
                    Color.FromRgb(102, 255, 255),
                    Color.FromRgb(51, 204, 204),
                    Color.FromRgb(0, 153, 153),
                    Color.FromRgb(153, 255, 255),
                    Color.FromRgb(102, 204, 204),
                    Color.FromRgb(51, 153, 153),
                    Color.FromRgb(0, 102, 102),
                    Color.FromRgb(204, 255, 255),
                    Color.FromRgb(153, 204, 204),
                    Color.FromRgb(102, 153, 153),
                    Color.FromRgb(51, 102, 102),
                    Color.FromRgb(0, 51, 51),
                    Color.FromRgb(0, 255, 204),
                    Color.FromRgb(51, 255, 204),
                    Color.FromRgb(0, 204, 153),
                    Color.FromRgb(102, 255, 204),
                    Color.FromRgb(51, 204, 153),
                    Color.FromRgb(0, 153, 102),
                    Color.FromRgb(153, 255, 204),
                    Color.FromRgb(102, 204, 153),
                    Color.FromRgb(51, 153, 102),
                    Color.FromRgb(0, 102, 51),
                    Color.FromRgb(0, 255, 153),
                    Color.FromRgb(51, 255, 153),
                    Color.FromRgb(0, 204, 102),
                    Color.FromRgb(102, 255, 153),
                    Color.FromRgb(51, 204, 102),
                    Color.FromRgb(0, 153, 51),
                    Color.FromRgb(0, 255, 102),
                    Color.FromRgb(51, 255, 102),
                    Color.FromRgb(0, 204, 51),
                    Color.FromRgb(0, 255, 51),
                    Color.FromRgb(0, 255, 0),
                    Color.FromRgb(51, 255, 51),
                    Color.FromRgb(0, 204, 0),
                    Color.FromRgb(102, 255, 102),
                    Color.FromRgb(51, 204, 51),
                    Color.FromRgb(0, 153, 0),
                    Color.FromRgb(153, 255, 153),
                    Color.FromRgb(102, 204, 102),
                    Color.FromRgb(51, 153, 51),
                    Color.FromRgb(0, 102, 0),
                    Color.FromRgb(204, 255, 204),
                    Color.FromRgb(153, 204, 153),
                    Color.FromRgb(102, 153, 102),
                    Color.FromRgb(51, 102, 51),
                    Color.FromRgb(0, 51, 0),
                    Color.FromRgb(51, 255, 0),
                    Color.FromRgb(102, 255, 51),
                    Color.FromRgb(51, 204, 0),
                    Color.FromRgb(102, 255, 0),
                    Color.FromRgb(153, 255, 102),
                    Color.FromRgb(102, 204, 51),
                    Color.FromRgb(51, 153, 0),
                    Color.FromRgb(153, 255, 51),
                    Color.FromRgb(102, 204, 0),
                    Color.FromRgb(153, 255, 0),
                    Color.FromRgb(204, 255, 153),
                    Color.FromRgb(153, 204, 102),
                    Color.FromRgb(102, 153, 51),
                    Color.FromRgb(51, 102, 0),
                    Color.FromRgb(204, 255, 102),
                    Color.FromRgb(153, 204, 51),
                    Color.FromRgb(102, 153, 0),
                    Color.FromRgb(204, 255, 51),
                    Color.FromRgb(153, 204, 0),
                    Color.FromRgb(204, 255, 0),
                    Color.FromRgb(255, 255, 0),
                    Color.FromRgb(255, 255, 51),
                    Color.FromRgb(204, 204, 0),
                    Color.FromRgb(255, 255, 102),
                    Color.FromRgb(204, 204, 51),
                    Color.FromRgb(153, 153, 0),
                    Color.FromRgb(255, 255, 153),
                    Color.FromRgb(204, 204, 102),
                    Color.FromRgb(153, 153, 51),
                    Color.FromRgb(102, 102, 0),
                    Color.FromRgb(255, 255, 204),
                    Color.FromRgb(204, 204, 153),
                    Color.FromRgb(153, 153, 102),
                    Color.FromRgb(102, 102, 51),
                    Color.FromRgb(51, 51, 0),
                    Color.FromRgb(255, 204, 0),
                    Color.FromRgb(255, 204, 51),
                    Color.FromRgb(204, 153, 0),
                    Color.FromRgb(255, 204, 102),
                    Color.FromRgb(204, 153, 51),
                    Color.FromRgb(153, 102, 0),
                    Color.FromRgb(255, 204, 153),
                    Color.FromRgb(204, 153, 102),
                    Color.FromRgb(153, 102, 51),
                    Color.FromRgb(102, 51, 0),
                    Color.FromRgb(255, 153, 0),
                    Color.FromRgb(255, 153, 51),
                    Color.FromRgb(204, 102, 0),
                    Color.FromRgb(255, 153, 102),
                    Color.FromRgb(204, 102, 51),
                    Color.FromRgb(153, 51, 0),
                    Color.FromRgb(255, 102, 0),
                    Color.FromRgb(255, 102, 51),
                    Color.FromRgb(204, 51, 0),
                    Color.FromRgb(255, 51, 0),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(255, 51, 51),
                    Color.FromRgb(204, 0, 0),
                    Color.FromRgb(255, 102, 102),
                    Color.FromRgb(204, 51, 51),
                    Color.FromRgb(153, 0, 0),
                    Color.FromRgb(255, 153, 153),
                    Color.FromRgb(204, 102, 102),
                    Color.FromRgb(153, 51, 51),
                    Color.FromRgb(102, 0, 0),
                    Color.FromRgb(255, 204, 204),
                    Color.FromRgb(204, 153, 153),
                    Color.FromRgb(153, 102, 102),
                    Color.FromRgb(102, 51, 51),
                    Color.FromRgb(51, 0, 0),
                    Color.FromRgb(255, 0, 51),
                    Color.FromRgb(255, 51, 102),
                    Color.FromRgb(204, 0, 51),
                    Color.FromRgb(255, 0, 102),
                    Color.FromRgb(255, 102, 153),
                    Color.FromRgb(204, 51, 102),
                    Color.FromRgb(153, 0, 51),
                    Color.FromRgb(255, 51, 153),
                    Color.FromRgb(204, 0, 102),
                    Color.FromRgb(255, 0, 153),
                    Color.FromRgb(255, 153, 204),
                    Color.FromRgb(204, 102, 153),
                    Color.FromRgb(153, 51, 102),
                    Color.FromRgb(102, 0, 51),
                    Color.FromRgb(255, 102, 204),
                    Color.FromRgb(204, 51, 153),
                    Color.FromRgb(153, 0, 102),
                    Color.FromRgb(255, 51, 204),
                    Color.FromRgb(204, 0, 153),
                    Color.FromRgb(255, 0, 204),
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(204, 204, 204),
                    Color.FromRgb(153, 153, 153),
                    Color.FromRgb(102, 102, 102),
                    Color.FromRgb(51, 51, 51),
                    Color.FromRgb(0, 0, 0)
                });
            }
        }

        #endregion

        #region Static Methods

        public static ColorCollection GetPalette(ColorPalette palette)
        {
            ColorCollection result;

            switch (palette)
            {
                case ColorPalette.Named:
                    result = NamedColors;
                    break;
                case ColorPalette.Office2010:
                    result = Office2010Standard;
                    break;
                case ColorPalette.Paint:
                    result = PaintPalette;
                    break;
                case ColorPalette.Standard:
                    result = QbColors;
                    break;
                case ColorPalette.None:
                    result = new ColorCollection();
                    break;
                case ColorPalette.WebSafe:
                    result = WebSafe;
                    break;
                case ColorPalette.Standard256:
                    result = StandardPalette;
                    break;
                default:
                    throw new ArgumentException("Invalid palette", nameof(palette));
            }

            return result;
        }

        public static ColorCollection ScaledPalette(IEnumerable<Color> topRow)
        {
            var results = new ColorCollection();

            topRow = topRow.ToArray();
            results.AddRange(topRow);

            for (int i = 5; i >= 0; i--)
            {
                foreach (var color in topRow)
                {
                    var hsl = new HslColor(color.ToColor()) {L = (5 + i + 16 * i) / 100D};

                    results.Add(hsl.ToRgbColorNew());
                }
            }

            return results;
        }

        #endregion
    }
}