using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Event : BaseEntity
    {
        public string LocationId { get; private set; }
        public string Title { get; private set; }
        public bool Complated { get; private set; }

        public DateTime CreatedDate { get; set; }

        public Event()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Event(string locationId, string title, bool complated) : this()
        {
            LocationId = locationId;
            Title = title;
            Complated = complated;
            CreatedDate = DateTime.Now;
        }
        public Event(string id, string locationId, string title, bool complated) : this(locationId, title, complated)
        {
            Id = id;
        }
        public void ChangeLocationId(string locationId)
        {
            LocationId = locationId;
        }
        public void ChangeTitle(string title)
        {
            Title = title;
        }
        public void ChangeComplated(bool complated)
        {
            Complated = complated;
        }
    }
}
