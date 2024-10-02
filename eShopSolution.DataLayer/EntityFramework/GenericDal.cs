using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class GenericDal<T, E> : IGenericDal<T, E> where T : class where E : class
    {
        public readonly ApplicationContext _context;
        public readonly IMapper _mapper;
        public GenericDal(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseRep<List<T>>> GetAll()
        {
            var list = await _context.Set<E>().ToListAsync();
            var result = _mapper.Map<List<T>>(list);
            BaseRep<List<T>> baseRep = new BaseRep<List<T>>();
            baseRep.code = 200;
            baseRep.Value = result;
            return baseRep;
        }
        public async Task<BaseRep<T>> GetByID(int ID)
        {
            var model = await _context.Set<E>().FindAsync(ID);
            var result = _mapper.Map<T>(model);
            BaseRep<T> baseRep = new BaseRep<T>();
            if (result != null)
            {
                baseRep.code = 200;
            }
            else
            {
                baseRep.code = 404;
            }
            baseRep.Value = result;
            return baseRep;
        }
        public async Task<BaseRep<string>> Create(T model)
        {
            BaseRep<string> baseRep = new BaseRep<string>();
            try
            {
                var resutl = await _context.Set<E>().AddAsync(_mapper.Map<E>(model));
                await _context.SaveChangesAsync();
                baseRep.code = 200;
                baseRep.Value = "Insert Success";
            }
            catch (Exception ex)
            {
                baseRep.code = 500;
                baseRep.Value = "Insert Fail: " + ex.Message;
            }
            return baseRep;
        }

        public async Task<BaseRep<string>> Update(int ID, T model)
        {
            try
            {
                var Entity = await _context.Set<E>().FindAsync(ID);
                if (Entity == null)
                {
                    return new BaseRep<string>() { code = 404, Value = "Not Found" };
                }
                _mapper.Map(model, Entity);
                await _context.SaveChangesAsync();
                return new BaseRep<string>() { code = 200, Value = "Update Success" };
            }
            catch (Exception ex)
            {
                return new BaseRep<string>() { code = 500, Value = "Update Fail: " + ex.Message };
            }
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            try
            {
                var model = await _context.Set<E>().FindAsync(ID);
                if (model != null)
                {
                    _context.Set<E>().Remove(model);
                    _context.SaveChanges();
                    return new BaseRep<string>() { code = 200, Value = "Delete Success" };
                }
                else
                {
                    return new BaseRep<string>() { code = 404, Value = "Not Found" };
                }
            }
            catch (Exception ex)
            {
                return new BaseRep<string>() { code = 200, Value = "Delete Fail: " + ex.Message };
            }
        }
    }
}
