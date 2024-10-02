using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IGenericDal<T, E> where T : class where E : class
    {
        Task<BaseRep<string>> Create(T model);
        Task<BaseRep<string>> Update(int ID, T model);
        Task<BaseRep<string>> Delete(int ID);
        Task<BaseRep<List<T>>> GetAll();
        Task<BaseRep<T>> GetByID(int ID);
    }
}
