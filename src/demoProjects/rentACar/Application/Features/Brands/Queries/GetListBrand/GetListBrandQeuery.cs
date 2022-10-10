using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public  class GetListBrandQeuery: IRequest<BrandListModel>
    {
      
 

        public PageRequest PageRequest { get; set; }
        public class GetListBrandQeuerHandler : IRequestHandler<GetListBrandQeuery, BrandListModel>

        {
           private readonly IBrandRepository _brandRepository;
           private readonly IMapper _mapper;

            public GetListBrandQeuerHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<BrandListModel> Handle(GetListBrandQeuery request, CancellationToken cancellationToken)
            {
             IPaginate<Brand> brands =await  _brandRepository.GetListAsync(index: request.PageRequest.Page,size:request.PageRequest.PageSize);

                BrandListModel mappedBrandListModel = _mapper.Map<BrandListModel>(brands);

                return mappedBrandListModel;
            }
        }
    }
}
