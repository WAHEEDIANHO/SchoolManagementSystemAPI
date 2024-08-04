using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface ITopicService
{
    Task<bool> AddTopic(TopicReqDTO topic);
    Task<TopicDTO> GetTopicById(string id);
    Task<IEnumerable<TopicDTO>> GetGradeSubjectTopics(int GradeNumber, string SubjectTitle, int? TermTermNumber = null);
    Task<bool> UpdateTopic(TopicDTO topic);
    Task<bool> DeleteTopic(string id);
    Task<bool> AddLesson(LessonReqDTO lesson);
    Task<bool> DeleteLesson(string id);
    Task<bool> UpdateLesson(LessonDTO lesson);
    Task<IEnumerable<LessonDTO>> GetTopicLesson(string topicId);
    Task<LessonDTO> GetLessonById(string lessonId);
}