﻿namespace App.Service.Products;

public record UpdateProductRequest(string Name, decimal Price, int UnitStock);
