﻿using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using ShopifySharp.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp.Lists;

namespace ShopifySharp
{
    /// <summary>
    /// A service for getting Shopify Events
    /// Reference: https://help.shopify.com/api/reference/event
    /// </summary>
    public class EventService : ShopifyService
    {
        /// <summary>
        /// Creates a new instance of <see cref="EventService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public EventService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }

        /// <summary>
        /// Gets a count of all site events.
        /// </summary>
        /// <param name="filter">Options for filtering the result.</param>
        public virtual async Task<int> CountAsync(EventCountFilter filter = null)
        {
            return await ExecuteGetAsync<int>("events/count.json", "count", filter);
        }

        /// <summary>
        /// Retrieves the <see cref="Event"/> with the given id.
        /// </summary>
        /// <param name="eventId">The id of the event to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Event"/>.</returns>
        public virtual async Task<Event> GetAsync(long eventId, string fields = null)
        {
            return await ExecuteGetAsync<Event>($"events/{eventId}.json", "event", fields);
        }

        /// <summary>
        /// Returns a list of events for the given subject and subject type. A full list of supported subject types can be found at https://help.shopify.com/api/reference/event
        /// </summary>
        /// <param name="subjectId">Restricts results to just one subject item, e.g. all changes on a product.</param>
        /// <param name="subjectType">The subject's type, e.g. 'Order' or 'Product'. Known subject types are 'Articles', 'Blogs', 'Custom_Collections', 'Comments', 'Orders', 'Pages', 'Products' and 'Smart_Collections'.  A current list of subject types can be found at https://help.shopify.com/api/reference/event </param>
        public virtual async Task<ListResult<Event>> ListAsync(long subjectId, string subjectType, ListFilter<Event> filter = null)
        {
            // Ensure the subject type is plural
            if (!subjectType.Substring(subjectType.Length - 1).Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                subjectType = subjectType + "s";
            }
            
            return await ExecuteGetListAsync($"{subjectType?.ToLower()}/{subjectId}/events.json", "events", filter);
        }

        /// <summary>
        /// Returns a list of events.
        /// </summary>
        public virtual async Task<ListResult<Event>> ListAsync(ListFilter<Event> filter)
        {
            return await ExecuteGetListAsync($"events.json", "events", filter);
        }

        /// <summary>
        /// Returns a list of events for the given subject and subject type. A full list of supported subject types can be found at https://help.shopify.com/api/reference/event
        /// </summary>
        /// <param name="subjectId">Restricts results to just one subject item, e.g. all changes on a product.</param>
        /// <param name="subjectType">The subject's type, e.g. 'Order' or 'Product'. Known subject types are 'Articles', 'Blogs', 'Custom_Collections', 'Comments', 'Orders', 'Pages', 'Products' and 'Smart_Collections'.  A current list of subject types can be found at https://help.shopify.com/api/reference/event </param>
        public virtual async Task<ListResult<Event>> ListAsync(long subjectId, string subjectType, EventListFilter filter)
        {
            return await ListAsync(subjectId, subjectType, (ListFilter<Event>) filter);
        }

        /// <summary>
        /// Returns a list of events.
        /// </summary>
        public virtual async Task<ListResult<Event>> ListAsync(EventListFilter filter = null)
        {
            return await ListAsync(filter?.AsListFilter());
        }
    }
}
