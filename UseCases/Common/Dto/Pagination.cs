using System.Collections.Generic;

namespace UseCases.Common.Dto
{
    public class Pagination<T>
    {
        public IEnumerable<T> Data { get; }

        public int Count { get; }

        public Pagination(IEnumerable<T> data, int count)
        {
            Data = data;
            Count = count;
        }
    }
}
