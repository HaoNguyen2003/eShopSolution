using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EmailService.Model;

namespace eShopSolution.EmailService.Service
{
    public interface ISenderEmail
    {
        public Response<string> SendEmail(Message message);
    }
}
