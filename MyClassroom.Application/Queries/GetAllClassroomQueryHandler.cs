using AutoMapper;
using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using Serilog;
using ClassroomDto = MyClassroom.Contracts.ClassroomDto;

namespace MyClassroom.Application.Queries
{
    public class GetAllClassroomQueryHandler : IRequestHandler<GetAllClassroomQuery, BaseResponse<List<ClassroomDto>>>
    {
        private readonly IClassroomRepository _ClassroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAllClassroomQueryHandler(IClassroomRepository ClassroomRepository, IUserClassroomRepository userClassroomRepository, IMapper mapper, ILogger logger)
        {
            _ClassroomRepository = ClassroomRepository ?? throw new ArgumentNullException(nameof(ClassroomRepository));
            _userClassroomRepository = userClassroomRepository ?? throw new ArgumentNullException(nameof(userClassroomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<List<ClassroomDto>>> Handle(GetAllClassroomQuery request, CancellationToken cancellationToken)
        {
            var ClassroomIds = await _userClassroomRepository.GetAllClassroomIdsByUserIdAsync(request.UserContext.UserId);
            var allClassrooms = (await _ClassroomRepository.GetListAsync(null)).Where(c => ClassroomIds.Contains(c.Id));

            return new BaseResponse<List<ClassroomDto>>(_mapper.Map<List<ClassroomDto>>(allClassrooms));
        }
    }
}
