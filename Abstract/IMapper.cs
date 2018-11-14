using System;

namespace mvc_album_browser
{
    public interface IMapper<TSrc, TDst> where TSrc : class
                                        where TDst : class
    {
        TDst Map(TSrc src);
        void SetMap(Func<TSrc, TDst> map);
    }
}