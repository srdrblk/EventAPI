using Business.IService;
using Contracts.Dtos;
using Contracts.SearchArgs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        [HttpGet]
        public IEnumerable<Event> Events()
        {

            return eventService.GetAllEvents();
        }
        [HttpPost("/Search")]
        public List<Event> Search([FromForm]EventSearchArgs eventSearch)
        {
          //  var eventSearch = new EventSearchArgs() { IsComplated = isCompleted, Title = title };
            return eventService.SearchEvents(eventSearch);
        }
        [HttpGet("/GetByElastic")]
        public List<Event> GetByElastic()
        {
            //  var eventSearch = new EventSearchArgs() { IsComplated = isCompleted, Title = title };
            return eventService.GetAllEventsByElasticSearch();
        }
        [HttpGet("{id}")]
        public Event Event(string id)
        {

            return eventService.GetEvent(id);
        }
        [HttpPost]
        public void Event(EventCreateOrUpdateDto createEvent)
        {
            eventService.PostEvent(createEvent);
        }


        [HttpPut("{id}")]
        public void Event(string id, EventCreateOrUpdateDto createEvent)
        {
            if (!String.IsNullOrEmpty(id))
            {
                createEvent.Id = id;
                eventService.PutEvent(createEvent);
            }
        }
    }
}
