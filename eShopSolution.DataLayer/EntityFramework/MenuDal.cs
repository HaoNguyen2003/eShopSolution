using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class MenuDal : GenericDal<MenuModel, AspNetMenu>, IMenuDal
    {
        public MenuDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
