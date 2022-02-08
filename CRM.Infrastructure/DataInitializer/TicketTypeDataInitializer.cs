using CRM.Infrastructure.Persistance.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Infrastructure.DataInitializer
{
    public class TicketTypeDataInitializer : IDataInitializer
    {
        private readonly IUnitOfWork _uow;
        public TicketTypeDataInitializer(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        
        public void InitializeData()
        {
            if (!_uow.TicketTypes.TableNoTracking.Any(a => a.Title == "Invoice"))
            {
                _uow.TicketTypes.Add(new Domain.Models.Ticket.TicketType
                {
                    Title = "Invoice",
                    Name = "فاکتور",
                    ModelId = "1"
                });
            }
            if (!_uow.TicketTypes.TableNoTracking.Any(a => a.Title == "Device"))
            {
                _uow.TicketTypes.Add(new Domain.Models.Ticket.TicketType
                {
                    Title = "Device",
                    Name = "دستگاه",
                    ModelId = "2"
                });
            }
            if (!_uow.TicketTypes.TableNoTracking.Any(a => a.Title == "DeviceType"))
            {
                _uow.TicketTypes.Add(new Domain.Models.Ticket.TicketType
                {
                    Title = "DeviceType",
                    Name = "نوع دستگاه",
                    ModelId = "3"
                });
            }
            if (!_uow.TicketTypes.TableNoTracking.Any(a => a.Title == "Inquiry"))
            {
                _uow.TicketTypes.Add(new Domain.Models.Ticket.TicketType
                {
                    Title = "Inquiry",
                    Name = "استعلام",
                    ModelId = "4"
                });
            }
            if (!_uow.TicketTypes.TableNoTracking.Any(a => a.Title == "InquiryDate"))
            {
                _uow.TicketTypes.Add(new Domain.Models.Ticket.TicketType
                {
                    Title = "InquiryDate",
                    Name = "تاریخ و ساعت استعلام",
                    ModelId = "5"
                });
            }
        }
    }
}
