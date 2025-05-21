using Grocery.Errors;
using Microsoft.AspNetCore.Mvc;
using MisrInsuranceOrderManagment.Errors;
using MisrInsuranceOrderManagment.Repository.GenericRepository;

namespace MisrInsuranceOrderManagment
{
    public static class ApplicationServicesExtensions 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
             


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
