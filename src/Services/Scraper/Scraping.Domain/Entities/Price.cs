﻿namespace Scraping.Domain.Entities;

#nullable disable

public class Price
{
    public string Currency { get; set; }
    public double NumericFloat { get; set; }
    public int NumericInteger { get; set; }
}
