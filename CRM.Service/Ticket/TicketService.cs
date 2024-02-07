using CRM.Common.Api;
using CRM.Common.Utilities;
using CRM.Entities.DataModels.Basic;
using CRM.Repository.Core;
using CRM.Service.People;
using CRM.ViewModels.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Service.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPeopleService _peopleService;
        public TicketService(IUnitOfWork uow, IPeopleService peopleService)
        {
            this._uow = uow;
            _peopleService = peopleService;
        }
        public async Task<ResultContent<DataSourceResult>> GetTicketsAsync(DataSourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.TableNoTracking
                        .Where(w => w.IsDeleted == false)
                        .Include(i => i.Customer)
                        .ThenInclude(i => i.Person)
                        .ThenInclude(i => i.User)
                        .ToDataSourceResultAsync(request, cancellationToken);

            return new ResultContent<DataSourceResult>(true, result);
        }

        public async Task<ResultContent<Entities.DataModels.Basic.Ticket>> GetTicketAsync(long id, CancellationToken cancellationToken)
        {
           
            var result = await _uow.Tickets.TableNoTracking
                        .Include(i => i.Device)
                        .ThenInclude(i => i.DeviceBrand).Include(i => i.Device).ThenInclude(i => i.DeviceType)
                        .Include(i => i.Fellows)
                        .Include(i => i.Customer)
                        .ThenInclude(i => i.Person)
                        .ThenInclude(i => i.User)
                        .Include(i => i.Repairer)
                        .ThenInclude(i => i.Person)
                        .ThenInclude(i => i.User)
                        .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
            
            return new ResultContent<Entities.DataModels.Basic.Ticket>(true, result);

        }

        public async Task<ResultContent<TicketPrerequisiteViewModel>> PrerequisiteAsync(CancellationToken cancellationToken)
        {
            var result = new TicketPrerequisiteViewModel();
            var types = await _uow.DeviceTypes.TableNoTracking.ToListAsync(cancellationToken);
            result.DeviceTypeList = types.Select(s => new DeviceTypeViewModel { id = s.Id, label = s.Title }).ToList();

            var brands = await _uow.DeviceBrands.TableNoTracking.ToListAsync(cancellationToken);
            result.DeviceBrandList = brands.Select(s => new DeviceBrandViewModel { id = s.Id, label = s.Title }).ToList();

            var last = await _uow.Tickets.TableNoTracking.OrderByDescending(o => o.Id).FirstOrDefaultAsync(cancellationToken);
            if (last == null)
                result.LastTicketNumber = 1;
            else
                result.LastTicketNumber = last.Number++;
            return new ResultContent<TicketPrerequisiteViewModel>(true, result);
        }
        public async Task<ResultContent<TicketPrerequisiteViewModel>> CreateAsync(TicketAddEditViewModel ticketAddEditViewModel, CancellationToken cancellationToken)
        {
            var peopleCreateResponse = await _peopleService.CreateAsync(new PersonUser_AddEdit_ViewModel
            {
                Person = new PersonViewModel
                {
                    ePersonType = Common.Enums.ePersonType.Customer,
                    Name = ticketAddEditViewModel.CustomerName,
                },
                User = new UserViewModel
                {
                    Email = ticketAddEditViewModel.CustomerEmail,
                    PhoneNumber = ticketAddEditViewModel.CustomerPhone,
                }
            }, cancellationToken);
            if (!peopleCreateResponse.IsSuccess)
                return new ResultContent<TicketPrerequisiteViewModel>(false, null, "Customer creation failed");

            var peopleId = (long)peopleCreateResponse.Data;

            var device = new Device
            {
                DeviceTypeId = ticketAddEditViewModel.DeviceTypeId,
                DeviceBrandId = ticketAddEditViewModel.DeviceBrandId,
                Model = ticketAddEditViewModel.DeviceModel,
                Description = ticketAddEditViewModel.DeviceDescrption,
                Accessories = ticketAddEditViewModel.DeviceAccessories,
                Warranty = ticketAddEditViewModel.DeviceWaranty,
            };

            var ticket = new Entities.DataModels.Basic.Ticket
            {
                Date = DateTime.Parse(ticketAddEditViewModel.TicketDate),
                Number = ticketAddEditViewModel.TicketNumber,
                InquiryPrice = ticketAddEditViewModel.InquiryPrice,
                Device = device,
                CustomerId = peopleId
            };

            var fellow = new TicketFellow
            {
                Ticket = ticket,
                Status = Common.Enums.eTicketStatus.Waiting,
                FellowDate = DateTime.Now,
            };

            await _uow.Fellows.AddAsync(fellow, cancellationToken);
            await _uow.Devices.AddAsync(device, cancellationToken);
            await _uow.Tickets.AddAsync(ticket, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);

            return new ResultContent<TicketPrerequisiteViewModel>(true, null);


        }


    }
}
