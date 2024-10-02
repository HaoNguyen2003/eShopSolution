using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IGenericService<T, E> where T : class where E : class
    {
        public Task<BaseRep<string>> Create(T model);
        public Task<BaseRep<string>> Update(int ID, T model);
        public Task<BaseRep<string>> Delete(int ID);
        public Task<BaseRep<List<T>>> GetAll();
        public Task<BaseRep<T>> GetByID(int ID);
    }
}
