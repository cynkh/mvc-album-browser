using System;

namespace mvc_album_browser
{
    public class Mapper<TSrc, TDst> : IMapper<TSrc, TDst> where TSrc : class
                                                          where TDst : class
    {
        private Func<TSrc, TDst> _map;
        public Mapper()
        {
            SetMap(src => default(TDst));
        }
        public Mapper(Func<TSrc, TDst> map)
        {
            SetMap(map);
        }
        public Type SourceType
        {
            get
            {
                return typeof(TSrc);
            }
        }
        public Type DestinationType
        {
            get
            {
                return typeof(TDst);
            }
        }
        public TDst Map(TSrc src)
        {
            return _map(src);
        }
        public void SetMap(Func<TSrc, TDst> map)
        {
            _map = map;
        }
    }
}