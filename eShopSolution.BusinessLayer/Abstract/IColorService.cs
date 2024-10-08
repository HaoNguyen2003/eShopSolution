﻿using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IColorService : IGenericService<ColorModel, Colors>
    {
        public Task<int> GetIntColorByName(string name);
    }
}
