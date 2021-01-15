
using Contracts.Dtos;
using Contracts.SearchArgs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.IService
{
    public interface IEventService
    {
        List<Event> GetAllEvents();
        Event GetEvent(string id);
        void PostEvent(EventCreateOrUpdateDto eventCreateDto);
        void PutEvent(EventCreateOrUpdateDto eventCreateDto);

        List<Event> SearchEvents(EventSearchArgs searchArgs);
        List<Event> GetAllEventsByElasticSearch();
    }
}
