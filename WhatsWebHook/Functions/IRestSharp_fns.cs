using RestSharp;
namespace WhatsWebHook.Functions
{
    public interface IRestSharp_fns
    {
        Task<RestResponse> post_async();
    }
}
