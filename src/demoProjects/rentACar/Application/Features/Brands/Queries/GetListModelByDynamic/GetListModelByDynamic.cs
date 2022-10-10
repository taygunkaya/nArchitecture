﻿using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<ModelListModel>
    {
        public Dynamic  Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListModelByDynamicHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModelByDynamicHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,include:
                                                m => m.Include(c => c.Brand),
                                                index: request.PageRequest.Page,
                                                size: request.PageRequest.PageSize
                                                );

                ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
