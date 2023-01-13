using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Models.Service.Messages.Response;
using InGameServices.Models;
using InGameServices.Models.ServiceRating.Messages.Request;

namespace InGameServices.Application.Services
{
    public class ServiceRatingService : IServiceRatingService
    {

        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly IServiceRatingValidator _serviceRatingValidator;

        public ServiceRatingService(IServiceRatingRepository serviceRatingRepository, IServiceRatingValidator serviceRatingValidator)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _serviceRatingValidator = serviceRatingValidator;
        }

        public async Task<CreateServiceRatingResponse> Create(CreateServiceRatingRequest request)
        {
            await _serviceRatingValidator.ValidateIfServiceExists(request.ServiceId);
            await _serviceRatingValidator.ValidateIfUserExists(request.UserId);
            _serviceRatingValidator.ValidateRatingRange(request.Rating);
            await _serviceRatingValidator.ValidateIfServiceRatingExists(request.ServiceId, request.UserId);

            var entity = new ServiceRating(request.UserId, request.ServiceId, request.Rating, request.Comment);

            await _serviceRatingRepository.Create(entity);

            return new CreateServiceRatingResponse
            {
                ServiceRating = (ServiceRatingDto)entity
            };
        }

        public async Task<DeleteServiceRatingResponse> Delete(DeleteServiceRatingRequest request)
        {
            var entity = await _serviceRatingRepository.GetServiceRatingByUserIdAndServiceId(request.UserId, request.ServiceId);
            _serviceRatingValidator.ValidateIfServiceRatingIsNull(entity);
            await _serviceRatingRepository.Delete(entity);
            return new DeleteServiceRatingResponse();
        }

        public async Task<UpdateServiceRatingResponse> Update(UpdateServiceRatingRequest request)
        {
            await _serviceRatingValidator.ValidateIfServiceExists(request.ServiceId);
            await _serviceRatingValidator.ValidateIfUserExists(request.UserId);
            _serviceRatingValidator.ValidateRatingRange(request.Rating);

            var entity = await _serviceRatingRepository.GetServiceRatingByUserIdAndServiceId(request.UserId, request.ServiceId);
            _serviceRatingValidator.ValidateIfServiceRatingIsNull(entity);

            entity.Comment = request.Comment;
            entity.Rating = request.Rating;

            await _serviceRatingRepository.Update(entity);

            return new UpdateServiceRatingResponse
            {
                ServiceRating = (ServiceRatingDto)entity
            };
        }
    }
}