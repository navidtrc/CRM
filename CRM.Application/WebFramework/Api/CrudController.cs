using AutoMapper;
using AutoMapper.QueryableExtensions;
//using CRM.Application.SecurityApplication.Queries.FindAll;
using CRM.Domain.Models.Core;
using CRM.Infrastructure.Persistance.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Application.WebFramework.Api
{
    [ApiVersion("1")]
    public class CrudController<TViewModel, TSelectViewModel, TEntity, TKey> : BaseController
        where TViewModel : class, new()
        where TSelectViewModel : class, new()
        where TEntity : class, IEntity<TKey>, new()
    {
        public CrudController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public virtual async Task<ActionResult<List<TSelectViewModel>>> Get([DataSourceRequest] DataSourceRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(new BaseFindAllQuery<TEntity, TSelectViewModel> { Request = request }, cancellationToken);
        //    if (result.Errors == null)
        //        return Ok(result);
        //    return BadRequest(result.Errors);
        //}

        //    [HttpGet("{id}")]
        //    public virtual async Task<ApiResult<TSelectViewModel>> Get(TKey id, CancellationToken cancellationToken)
        //    {
        //        var dto = await Repository.TableNoTracking.ProjectTo<TSelectViewModel>(_mapper.ConfigurationProvider)
        //            .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

        //        if (dto == null)
        //            return NotFound();

        //        return dto;
        //    }

        //    [HttpPost]
        //    public virtual async Task<ApiResult<TSelectViewModel>> Create(TViewModel dto, CancellationToken cancellationToken)
        //    {
        //        var model = dto.ToEntity(_mapper);

        //        await Repository.AddAsync(model, cancellationToken);

        //        var resultDto = await Repository.TableNoTracking.ProjectTo<TSelectViewModel>(_mapper.ConfigurationProvider)
        //            .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

        //        return resultDto;
        //    }

        //    [HttpPut]
        //    public virtual async Task<ApiResult<TSelectViewModel>> Update(TKey id, TViewModel dto, CancellationToken cancellationToken)
        //    {
        //        var model = await Repository.GetByIdAsync(cancellationToken, id);

        //        model = dto.ToEntity(_mapper, model);

        //        await Repository.UpdateAsync(model, cancellationToken);

        //        var resultDto = await Repository.TableNoTracking.ProjectTo<TSelectViewModel>(_mapper.ConfigurationProvider)
        //            .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);

        //        return resultDto;
        //    }

        //    [HttpDelete("{id}")]
        //    public virtual async Task<ApiResult> Delete(TKey id, CancellationToken cancellationToken)
        //    {
        //        var model = await Repository.GetByIdAsync(cancellationToken, id);

        //        await Repository.DeleteAsync(model, cancellationToken);

        //        return Ok();
        //    }
        //}

        //public class CrudController<TViewModel, TSelectViewModel, TEntity> : CrudController<TViewModel, TSelectViewModel, TEntity, int>
        //    where TViewModel : BaseViewModel<TViewModel, TEntity, int>, new()
        //    where TSelectViewModel : BaseViewModel<TSelectViewModel, TEntity, int>, new()
        //    where TEntity : class, IEntity<int>, new()
        //{
        //    public CrudController(IRepository<TEntity> repository, IMapper mapper)
        //        : base(repository, mapper)
        //    {
        //    }
        //}

    }
}
