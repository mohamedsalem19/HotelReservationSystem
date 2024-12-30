﻿using HotelReservationSystem.Data.Repositories;
using HotelReservationSystem.Features.RoomManagement.RoomTypes.Queries;
using HotelReservationSystem.Models.Enums;
using HotelReservationSystem.Models.RoomManagement;
using HotelReservationSystem.ViewModels.Responses;
using MediatR;
using System.Security.Claims;

namespace HotelReservationSystem.Features.RoomManagement.RoomTypes.Commands
{
    public record AddRoomTypeCommand(string name, double price) : IRequest<ResponseViewModel<bool>>;

    public class AddRoomTypeCommandHandler : IRequestHandler<AddRoomTypeCommand, ResponseViewModel<bool>>
    {
        private readonly IRepository<RoomType> _repository;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddRoomTypeCommandHandler(IRepository<RoomType> repository,
            IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ResponseViewModel<bool>> Handle(AddRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);

            if (!response.IsSuccess)
                return response;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdClaim, out int userId);
            _repository.Add(new RoomType
            {
                Name = request.name,
                Price = request.price,
                Description = "iyeoeeyr",
                CreatedBy = userId
            });

            _repository.SaveChanges();
            return response;
        }

        private async Task<ResponseViewModel<bool>> ValidateRequest(AddRoomTypeCommand request)
        {
            if(string.IsNullOrEmpty(request.name))
            {
                return new FailureResponseViewModel<bool>(ErrorCode.FieldIsEmpty, "Name is required");
            }

            if (request.price <= 0)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.InvalidInput, "Price must be greater than Zero");
            }

            var roomtypeExists = await _mediator.Send(new IsRoomTypeExistsQuery(request.name));
             
            if(roomtypeExists)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.ItemAlreadyExists);
            }

            return new SuccessResponseViewModel<bool>(true);
        }
    }
}
