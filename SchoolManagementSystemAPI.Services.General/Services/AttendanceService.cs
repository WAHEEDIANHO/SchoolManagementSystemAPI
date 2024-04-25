using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services
{
    public sealed class AttendanceService: IAttendanceService
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceHeaderRepository _headerRepo;
        private readonly IAttendanceDetailRepository _detailRepo;

        public AttendanceService(IMapper mapper, IAttendanceHeaderRepository headerRepo, IAttendanceDetailRepository detailRepo) 
        {
            _mapper = mapper;
            _headerRepo = headerRepo;
            _detailRepo = detailRepo;

        }

        public async Task<bool> CreateGradeAttendanceSheet(AttendanceHeaderReqDTO attendanceSheet)
        {
           AttendanceHeader attendanceHeader = _mapper.Map<AttendanceHeader>(attendanceSheet);
            await _headerRepo.Add(attendanceHeader);
            return true;
        }

        public async Task<IEnumerable<AttendanceHeaderDTO>> GetAttendanceSheet()
        {
            var attendances = await _headerRepo.GetAll();
            return _mapper.Map<IEnumerable<AttendanceHeaderDTO>>(attendances);
        }
        public async Task<IEnumerable<AttendanceDetailDTO>> GetStudentAttendant(string attendanceSheetId, string userId)
        {
           /*if (!Guid.TryParse(attendanceSheetId, out var parseGuid))
                return Enumerable.Empty<AttendanceDetailDTO>();*/

            //var sheet = await _headerRepo.GetByKey(parseGuid);
            //if (sheet == null) throw new Exception("attendant sheet not find!!!");

            var res =_detailRepo.GetStudentAttendant(attendanceSheetId, userId);
            return _mapper.Map<IEnumerable<AttendanceDetailDTO>>(res);
        }




        public async Task<bool> MarkAttendant(AttendanceDetailReqDTO attendance)
        {
            var isValidStudent = Guid.TryParse(attendance.UserId, out var parseGuid);
            var isValidSheet = await _headerRepo.GetByKey(attendance.AttendanceHeaderId);
            if (isValidSheet != null || isValidStudent)
            {
                AttendanceDetail attendanceDetail = _mapper.Map<AttendanceDetail>(attendance);
                await _detailRepo.Add(attendanceDetail);
                return true;
            }
            else throw new Exception("Invalid AttendanceSheet");
        }

        public Task<bool> UnmarkAttendant(string attendanceID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAttendant(string attendanceId, string timeOut)
        {
            throw new NotImplementedException();
        }
    }
}
