using AutoMapper;
using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using Serilog;

namespace MyClassroom.Application.Commands
{
    public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, BaseResponse<ClassroomDto>>
    {
        private readonly ILogger _logger;
        private readonly IClassroomRepository _ClassroomRepository;
        private readonly IMapper _mapper;

        public CreateClassroomCommandHandler(ILogger logger, IClassroomRepository ClassroomRepository, IMapper mapper)
        {
            _ClassroomRepository = ClassroomRepository ?? throw new ArgumentNullException(nameof(ClassroomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<ClassroomDto>> Handle(CreateClassroomCommand command, CancellationToken cancellationToken)
        {

            var createClassroomRequest = command.CreateClassroomRequest;
            var userContext = command.UserContext;

            _logger.Information("----- Creating Class room - {@Classroom}", createClassroomRequest);

            Classroom Classroom = new(createClassroomRequest.Title, createClassroomRequest.Description, userContext.UserId);

            var ClassroomResult = _ClassroomRepository.CreateAsync(Classroom);
            var response = _mapper.Map<ClassroomDto>(ClassroomResult);

            _logger.Information("----- Created Class room - {@Classroom}", Classroom);

            return new BaseResponse<ClassroomDto>(response);
        }
    }
}
