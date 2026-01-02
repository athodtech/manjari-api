using System.Data;

namespace AthodBeTrackApi.Repositories
{
    public interface ITagRepository
    {
        DataTable GetQuestionTagForMapping(int tagId);
        DataTable GetQuestionTagMapping(int tagId);
        bool UpdateQuestionTagMapping(int tagId, int userId, DataTable QuestionTagMapping);
    }
}