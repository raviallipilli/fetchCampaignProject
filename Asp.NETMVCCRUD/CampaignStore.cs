using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo
{
    public sealed class CampaignStore
    {
        private static CampaignStore _instance = null;

        public static CampaignStore Instance
        {
            get
            {
                    if (_instance == null)
                    {
                        _instance = new CampaignStore();
                    }
                    return _instance;
            }
        }


        private static List<Campaign> _campaigns = new List<Campaign>();

        public List<Campaign> GetAll()
        {
            return _campaigns;
        }

        public Campaign Get(int id)
        {
            return _campaigns.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Campaign campaign)
        {
            _campaigns.Add(campaign);
        }

        public void Edit(Campaign campaign)
        {
            Delete(campaign);
            Add(campaign);
        }

        public void Delete(Campaign campaign)
        {
            if (_campaigns.FirstOrDefault(c => c.Id == campaign.Id) != null)
            {
                _campaigns.Remove(campaign);
            }
        }
    }
}