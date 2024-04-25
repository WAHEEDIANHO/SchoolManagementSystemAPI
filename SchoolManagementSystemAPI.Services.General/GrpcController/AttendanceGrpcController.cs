using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcAttendance;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class AttendanceGrpcController: GrpcAttendanceBase
    {
        private readonly IAttendanceService _service;
        private readonly IMapper _mapper;

        public AttendanceGrpcController(IAttendanceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override Task<CreateResp> CreateSheet(AttendanceHeaderReq request, ServerCallContext context)
        {
            CreateResp resp = new();
            AttendanceHeaderReqDTO attendanceHeaderReq = _mapper.Map<AttendanceHeaderReqDTO>(request);
            resp.Res = _service.CreateGradeAttendanceSheet(attendanceHeaderReq).GetAwaiter().GetResult();
            return Task.FromResult(resp);
        }

        public override Task<ListAttendanceHeader> GetAllAttendantSheet(GetAllAttendantSheetReq request, ServerCallContext context)
        {
            ListAttendanceHeader resp = new();
            var sheet = _service.GetAttendanceSheet().GetAwaiter().GetResult();
            foreach(var attendanceSheet in sheet) { resp.Attendance.Add(_mapper.Map<AttendanceHeaderGrpc>(attendanceSheet)); }
            return Task.FromResult(resp);
        }

        public override Task<ListAttendanceDetail> GetStudentAttendant(GetStudentAttendantReq request, ServerCallContext context)
        {
            ListAttendanceDetail resp = new();
            //var attendanceSheet = _mapper.Map<AttendanceHeaderReqDTO>(request.AttendanceHeaderReq);
            //string userId = request.UserId.Id;
            var studentAttendant = _service.GetStudentAttendant(request.AttendanceSheetId, request.UserId).GetAwaiter().GetResult();
            foreach(var stuAttendant in studentAttendant) { resp.Attendance.Add(_mapper.Map<AttendanceDetailGrpc>(stuAttendant)); }
            return Task.FromResult(resp);   
        }

        public override Task<CreateResp> MarkAttendant(AttendanceDetailReq request, ServerCallContext context)
            {
            CreateResp resp = new();
            resp.Res = _service.MarkAttendant(_mapper.Map<AttendanceDetailReqDTO>(request)).GetAwaiter().GetResult();
            return Task.FromResult(resp);
        }
    }
}
