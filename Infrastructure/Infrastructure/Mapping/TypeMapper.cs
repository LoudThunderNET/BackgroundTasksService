using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BackgroundTasksService.AppServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTaskService.Infrastructure.Mapping
{
    public class TypeMapper : ITypeMapper
    {
        private readonly IMapper _mapper;

        public TypeMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDest Map<TSource, TDest>(TSource source) => _mapper.Map<TDest>(source);
        public void Map<TSource, TDest>(TSource source, TDest dest) => _mapper.Map(source, dest);
    }
}
