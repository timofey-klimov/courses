namespace Wep.App.Dto.Request.Students
{
    public class GetStudentsRequest
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 5;
    }
}
