using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class TopicService: ITopicService
{
    private readonly ITopicRepository _topicRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IMapper _mapper;

    public TopicService(
        ITopicRepository topicRepository, 
        IMapper mapper,
        ILessonRepository lessonRepository
        )
    {
        _topicRepository = topicRepository;
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }
    public async Task<bool> AddTopic(TopicReqDTO topic)
    {
        await _topicRepository.Add(_mapper.Map<Topic>(topic));
        return true;
    }

    public async Task<TopicDTO> GetTopicById(string id)
    {
        return  _mapper.Map<TopicDTO>(await _topicRepository.GetByKey(id));
    }

    public async Task<IEnumerable<TopicDTO>> GetGradeSubjectTopics(int GradeNumber, string SubjectTitle)
    {
        return _mapper.Map<IEnumerable<TopicDTO>>(await _topicRepository.GetGradeSubjectTopics(GradeNumber, SubjectTitle));
    }

    public async Task<bool> UpdateTopic(TopicDTO topic)
    {
        var exist = await _topicRepository.GetByKey(topic.TopicId);
        if (exist != null)
        {
            _topicRepository.Update(_mapper.Map<Topic>(topic));
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteTopic(string id)
    {
        var topic = await _topicRepository.GetByKey(id);
        if (topic != null)
        {
            _topicRepository.Delete(topic);
            return true;
        }
        return false;  
    }

    public async Task<bool> AddLesson(LessonReqDTO lesson)
    {
        await _lessonRepository.Add(_mapper.Map<Lesson>(lesson));
        return true;
    }

    public async Task<bool> DeleteLesson(string id)
    {
        Lesson delLesson = await _lessonRepository.GetByKey(id);
        if (delLesson != null)
        {
            _lessonRepository.Delete(delLesson);
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateLesson(LessonDTO upLesson)
    {
        var lesson = await _lessonRepository.GetByKey(upLesson.LessonId);
        if (lesson != null)
        {
            _lessonRepository.Update(_mapper.Map<Lesson>(upLesson));
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<LessonDTO>> GetTopicLesson(string topicId)
    {
        var res = await _topicRepository.GetByKey(topicId);
        return _mapper.Map<IEnumerable<LessonDTO>>(res.Lessons);
    }

    public async Task<LessonDTO> GetLessonById(string lessonId)
    {
        return _mapper.Map<LessonDTO>(await _lessonRepository.GetByKey(lessonId));
    }
}