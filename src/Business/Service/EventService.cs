using Business.IService;
using Contracts.Dtos;
using Contracts.SearchArgs;
using Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Service
{

    public class EventService : IEventService
    {
        private List<Event> eventList;
        IElasticClient elasticClient;
        public EventService(IElasticClient _elasticClient)
        {
            elasticClient = _elasticClient;
            eventList = new List<Event>();
        }

        public List<Event> GetAllEvents()
        {
            return eventList;
        }
        public List<Event> GetAllEventsByElasticSearch()
        {
            var entities = elasticClient.Search<Event>(p => p.From(0).Size(30)
           .Query(q => q
           .MatchAll()
           ));
            return entities.Documents.ToList();
        }
        public List<Event> SearchEvents(EventSearchArgs searchArgs)
        {
           

            var events = elasticClient.Search<Event>(a => a.From(0).Size(30).
            Query(q => q
                .Match(m => m
                    .Field(f => f.Title)
                    .Query(searchArgs.Title).MinimumShouldMatch(new MinimumShouldMatch("50")))
               
            ));

     
            return events.Documents.ToList();
        }
        public Event GetEvent(string id)
        {
            return eventList.SingleOrDefault(e => e.Id == id);
        }
        public void PostEvent(EventCreateOrUpdateDto eventCreateDto)
        {
            var eventt = new Event(eventCreateDto.Id,eventCreateDto.LocationId,eventCreateDto.Title,eventCreateDto.Complated);
            eventList.Add(eventt);
            try
            {
               var response = elasticClient.IndexDocument<Event>(eventt);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
  
        }
        public void PutEvent(EventCreateOrUpdateDto eventCreateDto)
        {
            var eventt = eventList.SingleOrDefault(e=>e.Id == eventCreateDto.Id);
            eventt.ChangeLocationId(eventCreateDto.LocationId);
            eventt.ChangeTitle(eventCreateDto.Title);
            eventt.ChangeComplated(eventCreateDto.Complated);
        }
    }
}
