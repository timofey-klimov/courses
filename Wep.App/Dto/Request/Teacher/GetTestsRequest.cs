﻿namespace Wep.App.Dto.Request.Teacher
{
    public class GetTestsRequest
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;
    }
}
