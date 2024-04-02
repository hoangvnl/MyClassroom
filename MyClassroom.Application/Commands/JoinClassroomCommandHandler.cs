using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.Application.Commands
{
    public class JoinClassroomCommandHandler : IRequestHandler<JoinClassroomCommand, BaseResponse<UserJoinClassroomResponse>>
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public JoinClassroomCommandHandler(IClassroomRepository ClassroomRepository, UserManager<ApplicationUser> userManager, IUserClassroomRepository userClassroomRepository, IMapper mapper)
        {
            _classroomRepository = ClassroomRepository ?? throw new ArgumentNullException(nameof(ClassroomRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userClassroomRepository = userClassroomRepository ?? throw new ArgumentNullException(nameof(userClassroomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<UserJoinClassroomResponse>> Handle(JoinClassroomCommand request, CancellationToken cancellationToken)
        {
            var returnValue = await Validate(request);
            if (returnValue != null)
            {
                return returnValue;
            }

            var JoinClassroomRequest = request.JoinClassroomRequest;

            var userClassroomJoinType = JoinClassroomRequest.UserClassroomJoinType switch
            {
                Contracts.UserJoinClassroomTypes.Student => Domain.AggregatesModel.UserClassroomAggregate.UserClassroomJoinType.Student,
                Contracts.UserJoinClassroomTypes.Teacher => Domain.AggregatesModel.UserClassroomAggregate.UserClassroomJoinType.Teacher
            };

            var userClassroom = _userClassroomRepository.CreateAsync(new UserClassroom(JoinClassroomRequest.ClassroomId, JoinClassroomRequest.UserId, userClassroomJoinType));
            returnValue = new BaseResponse<UserJoinClassroomResponse>(_mapper.Map<UserJoinClassroomResponse>(userClassroom));


            return returnValue;
        }

        private async Task<BaseResponse<UserJoinClassroomResponse>> Validate(JoinClassroomCommand request)
        {
            var JoinClassroomRequest = request.JoinClassroomRequest;

            if (await _classroomRepository.GetByIdAsync(JoinClassroomRequest.ClassroomId) == null)
            {
                return new BaseResponse<UserJoinClassroomResponse>(APIProblemFactory.ClassroomNotFound());
            }

            if (await _userManager.FindByIdAsync(JoinClassroomRequest.UserId.ToString()) == null)
            {
                return new BaseResponse<UserJoinClassroomResponse>(APIProblemFactory.UserNotFound());
            }

            if ((await _userClassroomRepository.GetAllUserIdsByClassroomIdAsync(JoinClassroomRequest.ClassroomId)).Contains(JoinClassroomRequest.UserId))
            {
                return new BaseResponse<UserJoinClassroomResponse>(APIProblemFactory.UserAlreadyJoinedClassroom());
            }

            return null;
        }
    }
}
